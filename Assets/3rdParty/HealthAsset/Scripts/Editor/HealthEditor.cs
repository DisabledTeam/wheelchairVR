using UnityEditor;

namespace HealthBar.Editor
{
    [CustomEditor(typeof(Health))]
    public class HealthEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var health = target as Health;
            health.needHealthBar = EditorGUILayout.Toggle("Need Health Bar", health.needHealthBar);
            if (health.needHealthBar)
                health.healthBar =
                    EditorGUILayout.ObjectField("Health bar", health.healthBar, typeof(HealthBar), true) as HealthBar;

            health.needDamagePopUp = EditorGUILayout.Toggle("Need Damage PopUp", health.needDamagePopUp);
            if (health.needDamagePopUp)
                health.damagePopUpper =
                    EditorGUILayout.ObjectField("Damage Pop Upper", health.damagePopUpper, typeof(DamagePopUpper),
                            true) as
                        DamagePopUpper;


            base.OnInspectorGUI();
        }
    }
}