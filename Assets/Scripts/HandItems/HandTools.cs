using System;
using System.Collections;
using InspectorButton;
using Interactable;
using UnityEngine;
using WheelInput;

namespace DefaultNamespace
{
    [InspectorButtonClass]
    public class HandTools : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField] private HandsItemControllerManagerSystem inventory;
        [SerializeField] private HandItem teleporterPrefab;
        [SerializeField] private HandItem[] testPrefabs;


        [Header("Monitoring")]
        [SerializeField] private int currentHandItemId;
        [SerializeField] private bool canSwap = true;


        private void Update()
        {
            if (canSwap)
            {
                if (CheckHandToolsButton(inventory.LeftHandInputProvider))
                {
                    currentHandItemId++;
                    currentHandItemId %= testPrefabs.Length;
                    SpawnNewItem(testPrefabs[currentHandItemId], PlayerHandAxis.LeftHand);
                    StartCoroutine(WaitSwap(0.1f));
                }

                if (CheckHandToolsButton(inventory.RightHandInputProvider))
                {
                    currentHandItemId++;
                    currentHandItemId %= testPrefabs.Length;
                    SpawnNewItem(testPrefabs[currentHandItemId], PlayerHandAxis.RightHand);
                    StartCoroutine(WaitSwap(0.1f));
                }
            }
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


        private IEnumerator WaitSwap(float time)
        {
            canSwap = false;
            yield return new WaitForSeconds(time);
            canSwap = true;
        }
    }
}