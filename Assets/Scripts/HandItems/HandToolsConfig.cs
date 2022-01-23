using Interactable;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "WheelchairVR/Hands/Create HandToolsConfig", fileName = "HandToolsConfig", order = 0)]
    public class HandToolsConfig : ScriptableObject
    {
        public HandItem[] toolsPrefabs;
    }
}