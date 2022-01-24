using System;
using UnityEngine;
using WheelInput;

namespace Interactable.Guns
{
    public class TestGunPickUp : MonoBehaviour
    {
        private enum TestGunPickUpShot
        {
            TriggerShoot,
            Shoot
        }

        [SerializeField] private HandItem handItem;
        [SerializeField] private SimpleShoot simpleShoot;
        [SerializeField] private TestGunPickUpShot testGunPickUpShot;


        private HandInputProvider _handInputProvider;

        public void OnFireButton()
        {
            switch (testGunPickUpShot)
            {
                case TestGunPickUpShot.TriggerShoot:
                    simpleShoot.TriggerShoot();
                    break;
                case TestGunPickUpShot.Shoot:
                    simpleShoot.Shoot();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnEnable()
        {
            handItem.takenInHand.AddListener(OnTaken);
            handItem.droppedFromHand.AddListener(OnDropped);
        }

        private void OnDisable()
        {
            handItem.takenInHand.RemoveListener(OnTaken);
            handItem.droppedFromHand.RemoveListener(OnDropped);
            if (_handInputProvider) _handInputProvider.firstButtonChanged.RemoveListener(OnFirstButtonChanged);
        }

        private void OnDropped(HandInputProvider arg0)
        {
            _handInputProvider = null;
            arg0.firstButtonChanged.RemoveListener(OnFirstButtonChanged);
        }

        private void OnTaken(HandInputProvider arg0)
        {
            _handInputProvider = arg0;
            arg0.firstButtonChanged.AddListener(OnFirstButtonChanged);
        }

        private void OnFirstButtonChanged(bool arg0)
        {
            OnFireButton();
        }
    }
}