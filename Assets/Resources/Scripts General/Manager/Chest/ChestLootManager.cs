using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestLootManager : MonoBehaviour
{

    [Header("Class Arrays")]
    [SerializeField] ChestLootArray[] playerTypeLoots;

    [Header("Buttons")]
    [SerializeField] Button weaponButton;
    [SerializeField] Button abilityButton;
    [SerializeField] Button moneyButton;

    [Header("Other Configs")]
    [SerializeField] Transform parentObject;

    private GameObject weapon;

    private GameObject[] abilities = new GameObject[2];

    private int abilityNum;


    private void Start()
    {
        weapon = GameObject.FindGameObjectWithTag("Weapon");
    }

    private void OnEnable()
    {
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
        GameObject randomWeapon = GetRandomObjectNotInScene(loot.weapons);
        if (randomWeapon != null)
        {
            weaponButton.onClick.AddListener(() => ActivateLoot(randomWeapon, weapon));
        }

        GameObject randomAbility = GetRandomObjectNotInScene(loot.abilities);
        if (randomAbility != null)
        {
            abilityButton.onClick.AddListener(() => ActivateLoot(randomAbility, abilities[abilityNum]));

            abilityNum++;

            if (abilityNum > 1)
            {
                abilityNum = 0;
            }
        }

        moneyButton.onClick.AddListener(() => GetMoney());
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

    private void ActivateLoot(GameObject loot, GameObject type)
    {
        if (type != null)
        {
            Destroy(type);
        }

        if (loot != null)
        {
            type = Instantiate(loot, parentObject.position, parentObject.rotation, parentObject);
        }

        gameObject.SetActive(false);
    }

    private void GetMoney()
    {
        MoneyManager.instance.money += 30;

        gameObject.SetActive(false);
    }

}
