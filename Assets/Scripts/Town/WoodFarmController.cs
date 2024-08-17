using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WoodFarmController : MonoBehaviour
{
    [HideInInspector] public string structureName { get; private set; }
    [HideInInspector] public float income { get; set; }
    [HideInInspector] public int structureLevel; // Start with level 1
    [HideInInspector] public float structureHealth;
    [HideInInspector] public float deteriorationRate = 2;

    [Header("Structure Settings")]
    [SerializeField] private GameObject structureLocation;
    [SerializeField] private GameObject structureLevel1;
    [SerializeField] private GameObject structureLevel2;
    [SerializeField] private GameObject structureLevel3;

    [Header("UI Settings")]
    [SerializeField] private GameObject UI;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;

    private GameObject currentStructure;

    private void Start()
    {
        currentStructure = Instantiate(structureLevel1, structureLocation.transform);
        structureLevel = 1;
        structureHealth = structureLevel * 3000;
        income = 100 * structureLevel;
        Debug.Log(structureLevel);
        UI.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (structureHealth > 0)
        {
            structureHealth -= deteriorationRate * Time.deltaTime;
        }
        else { structureHealth = 0; }
        title.text = "Wood Farm";
        description.text = "Structure Level :" + structureLevel + "\nIncome : " + income + " Woods \n Structure's Health : " + Mathf.Ceil(structureHealth);
    }

    public void ViewStructureScreen()
    {
        UI.gameObject.SetActive(true);
    }

    public void LevelUp()
    {
        //Missing condition to updagrade
        if (structureLevel < 3)
        {
            structureLevel++;
            Debug.Log(structureLevel);
            structureHealth = structureLevel * 3000;
            UpdateStructure();
        }
    }

    public void FixStructure()
    {
        //Missing condition to updagrade
        structureHealth = structureLevel * 3000;
    }

    private void UpdateStructure()
    {
        Destroy(currentStructure);
        if (structureLevel == 2)
        {
            currentStructure = Instantiate(structureLevel2, structureLocation.transform);
        }
        else if (structureLevel == 3)
        {
            currentStructure = Instantiate(structureLevel3, structureLocation.transform);
        }
    }

    public void CancelUI()
    {
        UI.gameObject.SetActive(false);
    }
}
