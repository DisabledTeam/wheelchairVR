using UnityEditor;

namespace HealthBar.Editor
{
    [CustomEditor(typeof(FloatingDamage))]
    public class FloatingDamageEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var floating = target as FloatingDamage;
            floating.needRandomScale = EditorGUILayout.Toggle("Need Random Scale", floating.needRandomScale);
            if (floating.needRandomScale)
            {
                floating.maxSize =
                    EditorGUILayout.FloatField("Max size", floating.maxSize);
                floating.minSize =
                    EditorGUILayout.FloatField("Min size", floating.minSize);
            }

            base.OnInspectorGUI();
        }
    }
}