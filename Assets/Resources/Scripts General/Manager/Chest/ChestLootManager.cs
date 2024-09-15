using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestLootManager : MonoBehaviour
{

    [Header("Class Arrays")]
    [Space]
    [SerializeField] ChestLootArray[] playerTypeLoots;

    [Header("Buttons")]
    [Space]
    [SerializeField] Button weaponButton;
    [SerializeField] Button abilityButton;
    [SerializeField] Button moneyButton;

    [Header("Other Configs")]
    [Space]
    [SerializeField] GameObject changeAbility;

    private Transform parentObject;

    private GameObject weapon;

    private int abilities = 0;

    [SerializeField]
    Sprite abilityImage;

    [SerializeField] Image test;


    private void Start()
    {
        if (weapon == null)
        {
            weapon = GameObject.FindGameObjectWithTag("Weapon");
        }
        
        if (parentObject == null)
        {
            parentObject = weapon.transform.parent;
        }
        
    }

    private void OnEnable()
    {
        weaponButton.gameObject.SetActive(true);

        abilityButton.gameObject.SetActive(true);

        moneyButton.gameObject.SetActive(true);

        SetPlayerLoot();
    }

    private void SetPlayerLoot()
    {
        ChestLootArray loot = GetLootForPlayerType(PlayerStats.instance.playerType);

        if (loot != null)
        {
            AssignLootToButtons(loot);
        }
    }

    private ChestLootArray GetLootForPlayerType(PlayerType playerType)
    {
        foreach (var lootArray in playerTypeLoots)
        {
            if (lootArray.type == playerType)
            {
                return lootArray;
            }
        }

        return null;
    }

    private void AssignLootToButtons(ChestLootArray loot)
    {
        weaponButton.onClick.RemoveAllListeners();
        abilityButton.onClick.RemoveAllListeners();
        moneyButton.onClick.RemoveAllListeners();

        GameObject randomWeapon = GetRandomObjectNotInScene(loot.weapons);
        if (randomWeapon != null)
        {
            string weaponName = StringUtils.CapitalizeFirstLetter(randomWeapon.name);

            weaponButton.GetComponentInChildren<TextMeshProUGUI>().text = weaponName;
            weaponButton.onClick.AddListener(() => StartCoroutine(ActivateLoot(randomWeapon, false)));
        }

        GameObject randomAbility = GetRandomObjectNotInScene(loot.abilities);
        if (randomAbility != null)
        {
            string abilityName = StringUtils.CapitalizeFirstLetter(randomAbility.name);

            abilityImage = randomAbility.GetComponent<IAbility>().img;

            abilityButton.GetComponentInChildren<TextMeshProUGUI>().text = abilityName;

            test = GetIcon(abilityButton.gameObject);

            test.sprite = abilityImage;

            abilityButton.onClick.AddListener(() => StartCoroutine(ActivateLoot(randomAbility, true)));

        }

        moneyButton.GetComponentInChildren<TextMeshProUGUI>().text = "30 gold";

        moneyButton.onClick.AddListener(() => GetMoney());
    }

    private Image GetIcon(GameObject gameObject)
    {
        return gameObject.GetComponentsInChildren<Image>().FirstOrDefault(img => img.gameObject != gameObject);
    }


    private GameObject GetRandomObjectNotInScene(GameObject[] objects)
    {
        List<GameObject> validObjects = new List<GameObject>();

        foreach (GameObject obj in objects)
        {
            if (!IsObjectInScene(obj))
            {
                validObjects.Add(obj);
            }
        }

        if (validObjects.Count > 0)
        {
            int randomIndex = Random.Range(0, validObjects.Count);
            return validObjects[randomIndex];
        }

        return null;
    }

    private bool IsObjectInScene(GameObject obj)
    {
        return GameObject.Find(obj.name) != null;
    }

    private IEnumerator ActivateLoot(GameObject loot, bool isAbility)
    {
        if (isAbility)
        {
            if (abilities == 2)
            {
                changeAbility.SetActive(true);

                weaponButton.gameObject.SetActive(false);

                abilityButton.gameObject.SetActive(false);

                moneyButton.gameObject.SetActive(false);

                yield return new WaitUntil(() => !changeAbility.activeSelf);
            }

            else
            {
                abilities++;
            }
        }

        else
        {
            Destroy(weapon);
        }

        yield return new WaitForEndOfFrame();

        if (loot != null)
        {

            GameObject newType = Instantiate(loot, parentObject.position, parentObject.rotation, parentObject);

            newType.name = loot.name;

            if (!isAbility)
            {
                weapon = newType;
            }
        }

        gameObject.SetActive(false);

    }

    private void GetMoney()
    {
        MoneyManager.instance.money += 30;

        gameObject.SetActive(false);
    }

}
