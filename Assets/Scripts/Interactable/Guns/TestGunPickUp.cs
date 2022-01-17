using UnityEngine;

namespace Interactable.Guns
{
    public class TestGunPickUp : MonoBehaviour
    {
        [SerializeField] private SimpleShoot simpleShoot;

        public void OnFireButton()
        {
            simpleShoot.TriggerShoot();
        }
    }
}