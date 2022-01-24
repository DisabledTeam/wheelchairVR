using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Valve.VR;

namespace DefaultNamespace
{
    public class HandPlayerLocalFly : HandItemTool
    {
        [Header("Settings")]
        [SerializeField] private float speed = 0.5f;

        [Header("Links")]
        [SerializeField] private TextMeshProUGUI localPositionText;

        [Header("Monitoring")]
        [SerializeField] private PlayerPositionSetter positionSetter;
        [SerializeField] private FlyMod flyMod;


        public enum FlyMod
        {
            XZ,
            Y
        }

        protected override void ItemTakenInHand()
        {
            positionSetter = FindObjectOfType<PlayerPositionSetter>();
            handInputProvider.firstButtonChanged.AddListener(OnFirstButton);
            Debug.Log("ItemTakenInHand AddListener",this);
        }

        protected override void ItemDroppedFromHand()
        {
            handInputProvider.firstButtonChanged.RemoveListener(OnFirstButton);
            Debug.Log("ItemDroppedFromHand RemoveListener",this);
        }

        private void OnFirstButton(bool arg0)
        {
            if (arg0)
            {
                Debug.Log("OnFirstButton HandPlayerLocalFly",this);
                var values = Enum.GetValues(typeof(FlyMod)).Cast<FlyMod>().ToList();
                var indexOf = values.IndexOf(flyMod);
                indexOf = (indexOf + 1) % values.Count;
                flyMod = values[indexOf];
            }
        }

        private void Update()
        {
            if (isActive)
            {
                var joyStick = handInputProvider.joyStick;
                // if (!handInputProvider.secondButton) joyStick = Vector2.zero;

                Vector3 moveVector;
                switch (flyMod)
                {
                    case FlyMod.XZ:
                        moveVector = new Vector3(joyStick.x, 0, joyStick.y).normalized;
                        localPositionText.text =
                            $"X:{positionSetter.LocalPosition.x} Z:{positionSetter.LocalPosition.z}";
                        break;
                    case FlyMod.Y:
                        moveVector = new Vector3(0, joyStick.y, 0).normalized;
                        localPositionText.text = $"Y:{positionSetter.LocalPosition.y}";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                positionSetter.MoveRelativeForward(moveVector * (speed * Time.deltaTime));
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (handInputProvider)
            {
                handInputProvider.firstButtonChanged.RemoveListener(OnFirstButton);
                Debug.Log("OnDisable RemoveListener",this);
            }
        }
    }
}