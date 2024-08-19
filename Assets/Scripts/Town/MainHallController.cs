using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainHallController : MonoBehaviour
{
    [HideInInspector] public string structureName { get; private set; }
    [HideInInspector] public int structureLevel;
    [HideInInspector] public float structureHealth;
    [HideInInspector] public float currentResources;
    [HideInInspector] public float maximumResources;

    [Header("Structure Settings")]
    [SerializeField] private GameObject structureLocation;
    [SerializeField] private GameObject structureLevel1;
    [SerializeField] private GameObject structureLevel2;
    [SerializeField] private GameObject structureLevel3;

    [Header("Town Settings")]
    [SerializeField] private GameObject townStructureLocation;
    [SerializeField] private GameObject townStructureLevel1;
    [SerializeField] private GameObject townStructureLevel2;
    [SerializeField] private GameObject townStructureLevel3;

    [Header("UI Settings")]
    [SerializeField] private GameObject UI;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;

    [Header("Town Controller")]
    [SerializeField] private TownController townController;

    private GameObject currentStructure;
    private GameObject currentTownStructure;
    private float deteriorationRate = 2;

    private void Start()
    {
        structureLevel = 1;
        InitializeStructure();
        UI.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (structureHealth > 0)
        {
            structureHealth -= deteriorationRate * Time.deltaTime;
        }
        else { structureHealth = 0; }

        description.text = "Structure Level :" + structureLevel + "\nStructure's Health : " + Mathf.Ceil(structureHealth);
    }

    private void InitializeStructure()
    {
        currentStructure = Instantiate(structureLevel1, structureLocation.transform);
        currentTownStructure = Instantiate(townStructureLevel1, townStructureLocation.transform);
        structureHealth = structureLevel * 5000;
        currentResources = 0;
        maximumResources = 1000;
    }

    // Interaction HUB
    public void ViewStructureScreen()
    {
        title.text = "Main Hall";
        UI.gameObject.SetActive(true);
    }

    public void ViewResources()
    {
        
    }

    //Screen button
    public void LevelUp()
    {
        //Missing condition to updagrade
        if (structureLevel < 3)
        {
            structureLevel++;
            Debug.Log(structureLevel);
            structureHealth = structureLevel * 5000;
            maximumResources = structureLevel + 500;
            UpdateStructure();
        }
    }

    public void FixStructure()
    {
        //Missing condition to updagrade
        structureHealth = structureLevel * 5000;
    }

    public void CancelUI()
    {
        UI.gameObject.SetActive(false);
    }

    private void UpdateStructure()
    {
        if (currentStructure != null)
        {
            Destroy(currentStructure);
            Destroy(currentTownStructure);
        }

        switch (structureLevel)
        {
            case 2:
                currentStructure = Instantiate(structureLevel2, structureLocation.transform);
                currentTownStructure = Instantiate(townStructureLevel2, townStructureLocation.transform);
                break;
            case 3:
                currentStructure = Instantiate(structureLevel3, structureLocation.transform);
                currentTownStructure = Instantiate(townStructureLevel3, townStructureLocation.transform);
                break;
            default:
                Debug.LogWarning("Invalid structure level.");
                break;
        }
    }
}
