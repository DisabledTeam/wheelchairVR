using HealthBar;
using UnityEngine;
using UnityEngine.Events;

namespace Fight
{
    public class PlayerTargetMarker : MonoBehaviour
    {
        public static UnityEvent PlayerTargetMarkerSpawned = new UnityEvent();
        public static PlayerTargetMarker GetInstance() => instance;

        private static PlayerTargetMarker instance;


        [Header("Links")]
        [SerializeField] private Transform followPoint;
        [SerializeField] private Health health;
        
        public Transform FollowPoint=>followPoint;
        public Health Health=>health;
        
        
        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            PlayerTargetMarkerSpawned.Invoke();
        }
    }
}