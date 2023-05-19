using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

 [CustomEditor(typeof(UIManager))]
public class UIManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("TestEndScreen"))
        {
            UIManager ui = (UIManager)target;

            ui.UpdateScore(100f, 1f);
            ui.ActivateVictoryPanel();
        }
    }
}
