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
        [Header("Settings")]
        [SerializeField] private PlayerHandAxis playerHandAxis;

        [Header("Links")]
        [SerializeField] private HandToolsConfig handToolsConfig;
        [SerializeField] private HandsItemControllerManagerSystem inventory;

        [Header("Monitoring")]
        [SerializeField] private int currentHandItemId;
        [SerializeField] private bool canSwap = true;
        [SerializeField] private float swapCooldown = 0.1f;

        private HandInputProvider HandInputProvider => inventory.GetInputProvider(playerHandAxis);

        public void SpawnNewItem(HandItem prefab)
        {
            var handItem = Instantiate(prefab);
            PutNewItem(handItem, playerHandAxis);
        }

        [InspectorButton("TestSpawnNextTool")]
        public void TestSpawnNextTool()
        {
            SpawnNextTool();
        }

        private void OnEnable()
        {
            HandInputProvider.grabButtonChanged.AddListener(OnGrabButtonChanged);
        }

        private void OnDisable()
        {
            HandInputProvider.grabButtonChanged.RemoveListener(OnGrabButtonChanged);
        }

        private void OnGrabButtonChanged(bool arg0)
        {
            if (!canSwap) return;

            if (arg0)
            {
                if (CheckHandToolsButton(HandInputProvider))
                    SpawnNextTool();
            }
        }

        private void PutNewItem(IHandItem handItem, PlayerHandAxis handAxis)
        {
            inventory.EquipHandItem(handAxis, handItem);
        }


        private bool CheckHandToolsButton(HandInputProvider handInputProvider)
        {
            // return handInputProvider.joyStick.y < -0.5 && handInputProvider.secondButton;
            return true;
        }

        private void SpawnNextTool()
        {
            currentHandItemId++;
            currentHandItemId %= handToolsConfig.toolsPrefabs.Length;
            SpawnNewItem(handToolsConfig.toolsPrefabs[currentHandItemId]);
            StartCoroutine(WaitSwap(swapCooldown));
        }

        private IEnumerator WaitSwap(float time)
        {
            DisableInventoryDrop();
            canSwap = false;
            yield return new WaitForSeconds(time);
            canSwap = true;
            EnableInventoryDrop();
        }

        private void DisableInventoryDrop()
        {
            inventory.DisableInventoryDrop();
        }

        private void EnableInventoryDrop()
        {
            inventory.EnableInventoryDrop();
        }
    }
}