using UnityEngine;

namespace DefaultNamespace
{
    public class HandFloorCalibration : HandItemTool
    {
        [Header("Monitoring")]
        [SerializeField] private PlayerPositionSetter positionSetter;
        
        protected override void ItemTakenInHand()
        {
            positionSetter = FindObjectOfType<PlayerPositionSetter>();
            handInputProvider.firstButtonChanged.AddListener(OnFirstChanged);
        }

        protected override void ItemDroppedFromHand()
        {
            handInputProvider.firstButtonChanged.RemoveListener(OnFirstChanged);
        }

        private void OnFirstChanged(bool arg0)
        {
            var controller = transform.position;
            var floor = Vector3.zero;
            var delta = controller.y - floor.y;
            positionSetter.MoveLocalTransform(Vector3.down * delta);
        }

    }
}