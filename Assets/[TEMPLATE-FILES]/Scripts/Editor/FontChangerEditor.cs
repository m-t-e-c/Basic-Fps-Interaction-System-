using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FontChanger))]
public class FontChangerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        FontChanger changer = (FontChanger)target;
        EditorGUILayout.Space(10);
        if (GUILayout.Button("Find All Text Objects"))
        {
            changer.FindAllTextObjects();
            Debug.Log("All Text Objects Finded");
        }

        EditorGUILayout.Space(10);

        if (GUILayout.Button("Change Fonts"))
        {
            changer.ChangeFont();
            Debug.Log("Fonts Changed!" );
        }
    }
}
