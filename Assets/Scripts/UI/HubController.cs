using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class HubController : MonoBehaviour
{
    public static HubController instance;
    [SerializeField] TMP_Text interactionText;
    [SerializeField] TMP_Text getResourceText;

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

    public void EnableGetResourceText(string text)
    {
        getResourceText.text = text + " (F)";
        getResourceText.gameObject.SetActive(true);
    }

    public void DisableGetResourceText()
    {
        getResourceText.gameObject.SetActive(false);
    }
}
