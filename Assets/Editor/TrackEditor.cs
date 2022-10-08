using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class TrackEditor : EditorWindow
{

    [MenuItem("Window/Track Editor")]
    public static void ShowWindow()
    {
        GetWindow(typeof(TrackEditor));
    }

    private void OnGUI()
    {
        
    }
}
