using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class CustomEditorUtils
    {
        public static IEnumerable<T> TryFindAssets<T>(string filter) where T : UnityEngine.Object
        {
            var findAssets = AssetDatabase.FindAssets(filter);
            if (findAssets.Length == 0)
            {
                Debug.LogError("No " + filter + " in project");
            }

            foreach (var asset in findAssets)
            {
                string path = AssetDatabase.GUIDToAssetPath(asset);
                var assetAtPath = AssetDatabase.LoadAssetAtPath<T>(path);
                yield return assetAtPath;
            }
        }
        
        
        public static T TryFindAsset<T>(string label) where T : UnityEngine.Object
        {
            var findAssets = AssetDatabase.FindAssets(label);
            if (findAssets.Length == 0)
            {
                Debug.LogError("No " + label + " in project");
                return default(T);
            }

            string path = AssetDatabase.GUIDToAssetPath(findAssets[0]);
            return AssetDatabase.LoadAssetAtPath<T>(path);
        }
    }
}