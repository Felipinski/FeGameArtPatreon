using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ProjectCreatorWindow : EditorWindow
{
    string projectName = String.Empty;

    [MenuItem("FeGameArt/Tools/ProjectCreator")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ProjectCreatorWindow));
    }

    void OnGUI()
    {
        EditorGUILayout.BeginVertical("Helpbox");// ("Project name", "");

        EditorGUILayout.LabelField("New project");

        projectName = EditorGUILayout.TextField("Project name", projectName);

        if (GUILayout.Button("Create"))
        {
            ProjectCreator.CopyFolder(projectName);
        }

        EditorGUILayout.EndVertical();
    }
}
