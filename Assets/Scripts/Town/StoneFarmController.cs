using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoneFarmController : MonoBehaviour
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
        resourceCoroutine = StartCoroutine(IncrementResourcesOverTime());
        UI.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (structureHealth > 0)
        {
            structureHealth -= deteriorationRate * Time.deltaTime;
        }
        else { structureHealth = 0; }

        description.text = "Structure Level :" + structureLevel + "\nIncome : " + income + " Stones \n Storage: " + currentResources + " Stones \n Structure's Health : " + Mathf.Ceil(structureHealth);
    }

    private void InitializeStructure()
    {
        currentStructure = Instantiate(structureLevel1, structureLocation.transform);
        structureHealth = structureLevel * 4000;
        income = 100 * structureLevel;
        currentResources = 0;
        maximumResources = 1000;
    }

    // Interaction HUB
    public void ViewStructureScreen()
    {
        title.text = "Stone Farm";
        UI.gameObject.SetActive(true);
    }

    public void GetResources()
    {
        townController.townGold += currentResources;
    }

    //Screen button
    public void LevelUp()
    {
        //Missing condition to updagrade
        if (structureLevel < 3)
        {
            structureLevel++;
            Debug.Log(structureLevel);
            structureHealth = structureLevel * 4000;
            maximumResources = structureLevel + 500;
            UpdateStructure();
        }
    }

    public void FixStructure()
    {
        //Missing condition to updagrade
        structureHealth = structureLevel * 4000;
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
