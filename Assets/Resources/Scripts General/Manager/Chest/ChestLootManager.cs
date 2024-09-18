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
    [SerializeField] ChestLootArray[] playerTypeLoots; // Arrays de loot para cada tipo de jugador.

    [Header("Buttons")]
    [Space]
    [SerializeField] Button weaponButton; // Botón para seleccionar un arma.
    [SerializeField] Button abilityButton; // Botón para seleccionar una habilidad.
    [SerializeField] Button moneyButton; // Botón para obtener dinero.

    [Header("Other Configs")]
    [Space]
    [SerializeField] GameObject changeAbility; // Panel para cambiar habilidades.

    private Transform parentObject; // El objeto padre al que se instanciarán las armas.

    private GameObject weapon; // La arma actualmente equipada.

    private int abilities = 0; // Contador de habilidades obtenidas.

    [SerializeField]
    Sprite abilityImage; // Imagen de la habilidad.

    [SerializeField] Image test; // Imagen de prueba para las habilidades.


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
        // Activa los botones y configura el loot del jugador.
        weaponButton.gameObject.SetActive(true);
        abilityButton.gameObject.SetActive(true);
        moneyButton.gameObject.SetActive(true);
        SetPlayerLoot();
    }

    // Configura el loot disponible según el tipo de jugador.
    private void SetPlayerLoot()
    {
        ChestLootArray loot = GetLootForPlayerType(PlayerStats.instance.playerType);

        if (loot != null)
        {
            AssignLootToButtons(loot);
        }
    }

    // Obtiene el loot correspondiente para el tipo de jugador.
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

    // Asigna el loot a los botones según el loot disponible.
    private void AssignLootToButtons(ChestLootArray loot)
    {
        // Limpia los listeners anteriores de los botones.
        weaponButton.onClick.RemoveAllListeners();
        abilityButton.onClick.RemoveAllListeners();
        moneyButton.onClick.RemoveAllListeners();

        // Asigna un arma aleatoria al botón de arma.
        GameObject randomWeapon = GetRandomObjectNotInScene(loot.weapons);
        if (randomWeapon != null)
        {
            string weaponName = StringUtils.CapitalizeFirstLetter(randomWeapon.name);

            weaponButton.GetComponentInChildren<TextMeshProUGUI>().text = weaponName;
            weaponButton.onClick.AddListener(() => StartCoroutine(ActivateLoot(randomWeapon, false)));
        }

        // Asigna una habilidad aleatoria al botón de habilidad.
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

        // Configura el botón de dinero.
        moneyButton.GetComponentInChildren<TextMeshProUGUI>().text = "30 gold";
        moneyButton.onClick.AddListener(() => GetMoney());
    }

    // Obtiene el componente Image que no es el objeto del botón en sí.
    private Image GetIcon(GameObject gameObject)
    {
        return gameObject.GetComponentsInChildren<Image>().FirstOrDefault(img => img.gameObject != gameObject);
    }

    // Obtiene un objeto aleatorio que no está actualmente en la escena.
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

    // Verifica si el objeto está en la escena.
    private bool IsObjectInScene(GameObject obj)
    {
        return GameObject.Find(obj.name) != null;
    }

    // Activa el loot seleccionado.
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

    // Añade dinero al jugador y desactiva el objeto del cofre.
    private void GetMoney()
    {
        MoneyManager.instance.money += 30;

        gameObject.SetActive(false);
    }

}
