using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using NaughtyAttributes;
using TMPro;

public class DisableRaycastUI : MonoBehaviour
{
    [Button]
    private void DisableRaycastInImages()
    {
        Image[] images = GetComponentsInChildren<Image>();
        foreach(Image img in images)
        {
            img.raycastTarget = false;
        }
        Debug.Log("Raycasts disabled in all IMG elements");
    }

    [Button]
    private void EnableRaycastInImages()
    {
        Image[] images = GetComponentsInChildren<Image>();
        foreach (Image img in images)
        {
            img.raycastTarget = true;
        }
        Debug.Log("Raycasts enabled in all IMG elements");
    }

    [Button]
    private void DisableRaycastTMP_Text()
    {
         TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text text in texts)
        {
            text.raycastTarget = false;
        }
        Debug.Log("Raycasts disabled in all TMP_Text elements");
    }


    [Button]
    private void EnableRaycastTMP_Text()
    {
        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();
        foreach (TMP_Text text in texts)
        {
            text.raycastTarget = true;
        }
        Debug.Log("Raycasts enabled in all TMP_Text elements");
    }
}
