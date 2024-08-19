using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TownController : MonoBehaviour
{
    [HideInInspector] public float townGold;
    [HideInInspector] public float townWood;
    [HideInInspector] public float townIron;
    [HideInInspector] public float townStone;
    [HideInInspector] public float townGoldMaximum;
    [HideInInspector] public float townWoodMaximum;
    [HideInInspector] public float townIronMaximum;
    [HideInInspector] public float townStoneMaximum;

    [Header("UI Settings")]
    [SerializeField] private GameObject UI;
    [SerializeField] private TextMeshProUGUI gold;
    [SerializeField] private TextMeshProUGUI wood;
    [SerializeField] private TextMeshProUGUI iron;
    [SerializeField] private TextMeshProUGUI stone;

    private void Start()
    {
        InitializeTown();
    }

    private void Update()
    {
        UpdateTownResources();
    }

    private void InitializeTown()
    {
        townGold = 1000;
        townWood = 1000;
        townIron = 1000;
        townStone = 1000;
        townGoldMaximum = 5000;
        townWoodMaximum = 5000;
        townIronMaximum = 5000;
        townStoneMaximum = 5000;
    }

    private void UpdateTownResources()
    {
        gold.text = "" + townGold + " / " + townGoldMaximum;
        iron.text = "" + townIron + " / " + townIronMaximum;
        wood.text = "" + townWood + " / " + townWoodMaximum;
        stone.text = "" + townStone + " / " + townStoneMaximum;
    }
}
