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
        GetWindow(typeof(ProjectCreatorWindow), true, "Project template window");
    }

    void OnGUI()
    {
        //Create label style
        GUIStyle labelStyle = new GUIStyle();

        labelStyle.fontSize = 17;
        labelStyle.normal.textColor = new Color(0.7f, 0.7f, 0.7f, 1.0f);
        labelStyle.alignment = TextAnchor.MiddleCenter;

        //Create title group
        EditorGUILayout.BeginVertical("Helpbox");
        EditorGUILayout.Space(2);
        EditorGUILayout.LabelField("Template", labelStyle);
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(2);

        //Create the name input field
        EditorGUILayout.BeginVertical("Helpbox");
        projectName = EditorGUILayout.TextField("Project name", projectName);

        EditorGUILayout.Space(2);

        //Draw the button and define a method that will be called when it's clicked
        if (GUILayout.Button("Create"))
        {
            ProjectCreator.CopyFolder(projectName);
        }

        EditorGUILayout.EndVertical();
    }
}
