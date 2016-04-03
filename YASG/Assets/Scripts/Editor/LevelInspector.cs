using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;

namespace Assets.Scripts.Editor
{
    [CustomEditor(typeof(GameFieldManager))]
    public class LevelInspector : UnityEditor.Editor
    {
       /* public override void OnInspectorGUI()
        {
            GameFieldManager gameFieldManager = (GameFieldManager)target;
            EditorGUILayout.LabelField("Size x: ", gameFieldManager.SizeX.ToString());
            EditorGUILayout.LabelField("Size y: ", gameFieldManager.SizeY.ToString());
            EditorGUILayout.LabelField("Speed: ", gameFieldManager.Speed.ToString());
            EditorGUILayout.LabelField("Delta speed: ", gameFieldManager.DeltaSpeed.ToString());
        }*/
    }
}
