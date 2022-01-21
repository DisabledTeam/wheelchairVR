using System;
using InspectorButton;
using Interactable;
using UnityEngine;
using WheelInput;

namespace DefaultNamespace
{
    [InspectorButtonClass]
    public class HandTools : MonoBehaviour
    {
        [SerializeField] private HandsItemControllerManagerSystem inventory;
        [SerializeField] private HandItem teleporterPrefab;


        private void Update()
        {
        }

        private void DisableInventoryDrop()
        {
            inventory.DisableInventoryDrop();
        }

        private void EnableInventoryDrop()
        {
            inventory.EnableInventoryDrop();
        }

        [InspectorButton("TestTeleporter")]
        public void TestTeleporter()
        {
            SpawnNewItem(teleporterPrefab, PlayerHandAxis.RightHand);
        }

        private bool CheckHandToolsButton(HandInputProvider handInputProvider)
        {
            return handInputProvider.joyStick.y < -0.5 && handInputProvider.secondButton;
        }


        public void SpawnNewItem(HandItem prefab, PlayerHandAxis handAxis)
        {
            var handItem = Instantiate(prefab);
            PutNewItem(handItem, handAxis);
        }

        private void PutNewItem(IHandItem handItem, PlayerHandAxis handAxis)
        {
            inventory.EquipHandItem(handAxis, handItem);
        }
    }
}