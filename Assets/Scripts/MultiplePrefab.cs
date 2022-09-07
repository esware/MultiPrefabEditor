using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MultiplePrefab : EditorWindow
{
    private static string location = "";
    private static string localPath;
    private static string guide = "Select GameObjects";

    [MenuItem("Multiple Prefab/Multiple Prefab Creator Window")]
    static void Init()
    {
        //Get exist open Window or if none , make a new one:
        MultiplePrefab window = (MultiplePrefab)EditorWindow.GetWindow(typeof(MultiplePrefab), true, "Multiple Prefab Window");
        window.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Location to save",EditorStyles.boldLabel);
        location = EditorGUILayout.TextField("Folder Name:",location);
        if (Selection.activeGameObject != null)
        {
            GUI.backgroundColor = Color.green;
        }
        else
        {
            GUI.backgroundColor = Color.red;
        }

        if (GUI.Button(new Rect(50, 120, 200, 50), "Create Prefabs"))
        {
            if (Selection.activeGameObject != null)
            {
                CreatePrefab();
            }
            else
            {
                Debug.Log("No selection in hierarchy");
                guide = "No selection in hierarchy";
            }
        }
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Guide",EditorStyles.boldLabel);
        
        EditorGUILayout.LabelField(guide,GUILayout.Height(30));
    }

    [MenuItem("Multiple Prefab/Create Prefabs")]
    private static void CreatePrefab()
    {
        GameObject[] objectArray = Selection.gameObjects;

        foreach (var gameobject in objectArray)
        {
            localPath = "Assets/" + location + "/" + gameobject.name + ".prefab";

            if (localPath == AssetDatabase.GenerateUniqueAssetPath(localPath) || location == "")
            {
                localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

                PrefabUtility.SaveAsPrefabAssetAndConnect(gameobject, localPath, InteractionMode.UserAction);
                Debug.Log("Prefabs Created...  Location of prefabs :"+localPath);
                guide = "Prefabs Created... Location of prefabs :" + localPath;
                
            }
            else
            {
                Debug.Log("The folder`s name is wrong ... There is no such folder");
                guide = "The folder`s name is wrong ... There is no such folder";
                break;
            }
        }
    }
    
    [MenuItem("Multiple Prefab/Create Prefabs", true)]
    static bool ValidateCreatePrefab()
    {
        return Selection.activeGameObject != null && !EditorUtility.IsPersistent(Selection.activeGameObject);
    }
}
