using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "WheelchairVR/Create PlayerHeight", fileName = "PlayerHeight", order = 0)]
    public class PlayerHeight : ScriptableObject
    {
        public float YStand;
        public float YInChair;
    }
}