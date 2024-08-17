using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class HubController : MonoBehaviour
{
    public static HubController instance;
    [SerializeField] TMP_Text interactionText;

    private void Awake()
    {
        instance = this;
    }

    public void EnableInteractionText(string text)
    {
        interactionText.text = text + " (E)";
        interactionText.gameObject.SetActive(true);
    }

    public void DisableInteractionText()
    {
        interactionText.gameObject.SetActive(false);
    }
}
