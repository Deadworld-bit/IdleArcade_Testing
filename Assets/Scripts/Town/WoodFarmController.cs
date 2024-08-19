using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WoodFarmController : MonoBehaviour
{
    [HideInInspector] public string structureName { get; private set; }
    [HideInInspector] public float income { get; set; }
    [HideInInspector] public int structureLevel;
    [HideInInspector] public float structureHealth;
    [HideInInspector] public float currentResources;
    [HideInInspector] public float maximumResources;

    [Header("Structure Settings")]
    [SerializeField] private GameObject structureLocation;
    [SerializeField] private GameObject structureLevel1;
    [SerializeField] private GameObject structureLevel2;
    [SerializeField] private GameObject structureLevel3;

    [Header("UI Settings")]
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject message;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;

    [Header("Town Controller")]
    [SerializeField] private TownController townController;

    private GameObject currentStructure;
    private float deteriorationRate = 2;
    private Coroutine resourceCoroutine;

    private void Start()
    {
        structureLevel = 1;
        InitializeStructure();
        if (structureHealth > 0)
        {
            resourceCoroutine = StartCoroutine(IncrementResourcesOverTime());
        }
        UI.gameObject.SetActive(false);
        message.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (structureHealth > 0)
        {
            structureHealth -= deteriorationRate * Time.deltaTime;
        }
        else { structureHealth = 0; }

        description.text = "Structure Level :" + structureLevel + "\nIncome : " + income + " Woods \n Storage: " + currentResources + " / " + maximumResources + " Woods \n Structure's Health : " + Mathf.Ceil(structureHealth);
    }

    private void InitializeStructure()
    {
        currentStructure = Instantiate(structureLevel1, structureLocation.transform);
        structureHealth = structureLevel * 3000;
        income = 100;
        currentResources = 0;
        maximumResources = 1000;
    }

    // Interaction HUB
    public void ViewStructureScreen()
    {
        title.text = "Wood Farm";
        UI.gameObject.SetActive(true);
    }

    public void GetResources()
    {
        float availableSpace = townController.townWoodMaximum - townController.townWood;
        if (currentResources <= availableSpace)
        {
            townController.townWood += currentResources;
            currentResources = 0;
        }
        else
        {
            townController.townWood += availableSpace;
            currentResources -= availableSpace;
        }
    }

    //Screen button
    public void LevelUp()
    {
        if (townController.townGold >= (structureLevel * 1000) && townController.townStone >= (structureLevel * 1000) && structureLevel < 3)
        {

            townController.townGold -= structureLevel * 1000;
            townController.townStone -= structureLevel * 1000;
            structureLevel++;
            income += structureLevel * 100;
            structureHealth = structureLevel * 3000;
            maximumResources += structureLevel * 500;
            UpdateStructure();
        }
        else
        {
            message.gameObject.SetActive(true);
        }
    }

    public void FixStructure()
    {
        if (townController.townGold >= (structureLevel * 100) && townController.townWood >= (structureLevel * 100) && townController.townStone >= (structureLevel * 100))
        {
            message.gameObject.SetActive(false);
            structureHealth = structureLevel * 3000;
            townController.townGold -= structureLevel * 100;
            townController.townWood -= structureLevel * 100;
            townController.townStone -= structureLevel * 100;
        }
        else
        {
            message.gameObject.SetActive(true);
        }
    }

    public void CancelUI()
    {
        UI.gameObject.SetActive(false);
        message.gameObject.SetActive(false);
    }

    private void UpdateStructure()
    {
        if (currentStructure != null)
        {
            Destroy(currentStructure);
        }

        switch (structureLevel)
        {
            case 2:
                currentStructure = Instantiate(structureLevel2, structureLocation.transform);
                break;
            case 3:
                currentStructure = Instantiate(structureLevel3, structureLocation.transform);
                break;
            default:
                Debug.LogWarning("Invalid structure level.");
                break;
        }
    }

    private IEnumerator IncrementResourcesOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f); // Wait for 4 seconds

            if (currentResources < maximumResources)
            {
                currentResources += income;
                // Clamp to maximum resources to prevent overflow
                currentResources = Mathf.Min(currentResources, maximumResources);
            }
        }
    }
}
