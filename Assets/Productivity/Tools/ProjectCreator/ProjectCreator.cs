using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ProjectCreator : MonoBehaviour
{
    private static string projectHubPath = "C:\\Users\\felip\\Documents\\Unity Projects\\FeGameArt\\FeGameArt-Patreon\\Assets\\Project\\01-Patreon";
    private static string sourcePath = "C:\\Users\\felip\\Documents\\Unity Projects\\FeGameArt\\FeGameArt-Patreon\\Assets\\Project\\01-Patreon\\00-Base";
    private static string targetPath = "C:\\Users\\felip\\Documents\\Unity Projects\\FeGameArt\\FeGameArt-Patreon\\Assets\\Project\\01-Patreon\\00-";

    public static void CopyFolder(string folderName)
    {
        int currentProjectAmount = new DirectoryInfo(projectHubPath).GetFiles().Length;

        string newTargetPath = targetPath + folderName;
        newTargetPath = newTargetPath.Replace("00", string.Format("0{0}", currentProjectAmount.ToString()));

        FileUtil.CopyFileOrDirectory(sourcePath, newTargetPath);

        DirectoryInfo newContent = new DirectoryInfo(newTargetPath);

        AssetDatabase.Refresh();

        var files = newContent.GetFiles();

        foreach (var content in files)
        {
            if(content.Name.Contains("Scene"))
            {
                FileInfo file = new FileInfo(content.FullName);

                //file.MoveTo(file.Directory.FullName + "\\" + newName);
            }
        }

        AssetDatabase.Refresh();
    }
}
