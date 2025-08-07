using System;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AbstractGenerator), true)]
public class ProceduralGenerationEditor : Editor
{
    AbstractGenerator generator;

    private void Awake()
    {
        generator = (AbstractGenerator)target;
    }
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate"))
        {
            generator.Generate();
        }
    }
}
