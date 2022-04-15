using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FontChanger : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> allTextObjects = new List<TMP_Text>();

    [Header("Replace Font")]
    [SerializeField] private TMP_FontAsset font;

    [Header("Default Font")]
    [SerializeField] private TMP_FontAsset defaultFont;

    public void FindAllTextObjects()
    {
        allTextObjects.Clear();
        allTextObjects.AddRange(FindObjectsOfType<TMP_Text>());

        if (allTextObjects.Count == 0) return;
        defaultFont = allTextObjects[0].GetComponent<TMP_Text>().font;
    }

    public void ChangeFont()
    {
        foreach(TMP_Text text in allTextObjects)
        {
            text.GetComponent<TMP_Text>().font = font;
        }
    }
}