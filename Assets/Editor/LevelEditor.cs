using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelEditor : EditorWindow
{
    [MenuItem("Tools/LevelEditor")]
    static void Init()
    {
        GetWindow<LevelEditor>();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("打开"))
        {
            OpenLevel();
        }

        if (GUILayout.Button("新建"))
        {
            CreateLevel();
        }

        if (GUILayout.Button("保存"))
        {
            SaveLevel();
        }
    }

    private void OpenLevel()
    {
        string path = EditorUtility.OpenFilePanel("Select a file", "", "json");
        Debug.Log(path);
        Handles.color = Color.red; // Change the color of the points if needed  
        Handles.DrawLine(new Vector3(0,0,0),new Vector3(1,0,0));
    }

    private void CreateLevel()
    {

    }

    private void SaveLevel()
    {
        string path = EditorUtility.SaveFilePanel("Save As", "", "Untitled", "json");
        Debug.Log(path);
    }
}
