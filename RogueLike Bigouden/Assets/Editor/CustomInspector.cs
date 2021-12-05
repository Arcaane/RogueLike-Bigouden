using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WaveSkip))]
public class CustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        WaveSkip _waveSkip = (WaveSkip)target;
        if (GUILayout.Button("Skip Wave"))
        {
            _waveSkip.SkipWave();
        }
    }
}
