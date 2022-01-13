using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(WheelsTest))]
    [CanEditMultipleObjects]
    public class WheelsTestEditor : UnityEditor.Editor
    {
        private WheelsTest _wheelsTest;
    
        private void OnEnable()
        {
            _wheelsTest = target as WheelsTest;
        }
    
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Motor"))
            {
                _wheelsTest.Motor();
            }
    
            if (GUILayout.Button("Brake"))
            {
                _wheelsTest.Brake();
            }
        }
    }



}