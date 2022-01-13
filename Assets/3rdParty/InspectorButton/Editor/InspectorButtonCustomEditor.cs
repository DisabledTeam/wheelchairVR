using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace InspectorButton.Editor
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    [CanEditMultipleObjects]
    public class InspectorButtonCustomEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Debug.Log("target " + target.name);
            System.Attribute[] attrs = System.Attribute.GetCustomAttributes(target.GetType());
            foreach (var attribute in attrs)
            {
                if (attribute.GetType() == typeof(InspectorButtonClassAttribute))
                {
                    DrawButtons();
                }
            }
        }


        private void DrawButtons()
        {
            foreach (var methodInfo in target.GetType().GetMethods())
            {
                var customAttributes = methodInfo.GetCustomAttributes(typeof(InspectorButtonAttribute));
                foreach (var customAttribute in customAttributes)
                {
                    DrawButton(target, customAttribute as InspectorButtonAttribute);
                }
            }
        }

        private void DrawButton(Object theObject, InspectorButtonAttribute attribute)
        {
            if (GUILayout.Button(attribute.ButtonName))
            {
                var method = theObject.GetType().GetMethod(attribute.FunctionName);

                if (method != null)
                {
                    //Invoke the method if != null Note: It works only of you dont need special parameters! (null)
                    method.Invoke(theObject, null);
                }
            }
        }
    }
}