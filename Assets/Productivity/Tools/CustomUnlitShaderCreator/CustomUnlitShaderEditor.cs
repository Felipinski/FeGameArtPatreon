using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomUnlitShaderEditor : Editor
{
    private static string sourcePath = "C:\\Users\\felip\\Documents\\Unity Projects\\FeGameArt\\FeGameArt-Patreon\\Assets\\Productivity\\Tools\\CustomUnlitShaderCreator\\CustomUnlitShader.shader";

    [MenuItem("Assets/Create/Shader/Custom Unlit Shader", priority = 1)]
    public static void CreateCustomUnlitShader()
    {
        //Define the folder path based on the current folder in the project inspector
        string folderPath = AssetDatabase.GetAssetPath(Selection.activeInstanceID);
        string filePath = folderPath + "\\CustomUnlitShader.shader";

        FileUtil.CopyFileOrDirectory(sourcePath, filePath);
        
        //Update the Project interface to show the new file
        AssetDatabase.Refresh();
    }
}
