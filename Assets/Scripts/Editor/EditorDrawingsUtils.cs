using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class EditorDrawingsUtils
    {
        public static GUIStyle GetLargeLabelStyle(int fontSize, Color color, FontStyle fontStyle)
        {
            var style = new GUIStyle(EditorStyles.largeLabel);
            style.fontSize = fontSize;
            style.normal.textColor = color;
            style.fontStyle = fontStyle;
            return style;
        }


        public static bool ShowStyleButton(string label, Color color, float width)
        {
            Color savedColor = GUI.backgroundColor;
            GUI.backgroundColor = color;
            bool res = ShowStyleButton(label, width);
            GUI.backgroundColor = savedColor;
            return res;
        }

        public static bool ShowStyleButton(string label, float width)
        {
            bool res = GUILayout.Button(label, GUILayout.Width(width));
            return res;
        }

        public static bool ShowStyleButton(string label, Color color)
        {
            Color savedColor = GUI.backgroundColor;
            GUI.backgroundColor = color;
            bool res = GUILayout.Button(label);
            GUI.backgroundColor = savedColor;
            return res;
        }
    }
}