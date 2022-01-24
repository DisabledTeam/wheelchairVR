using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class HandFloorCalibration : HandItemTool
    {
        [Header("Settings")]
        [SerializeField] private float holdTime = 1f;

        [Header("Links")]
        [SerializeField] private PlayerHeight playerHeight;
        [SerializeField] private Slider slider;
        [Header("Monitoring")]
        [SerializeField] private PlayerPositionSetter positionSetter;

        private Coroutine _tpCoroutine;

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
            if (arg0)
            {
                _tpCoroutine = StartCoroutine(TeleportCoroutine());
            }
            else
            {
                if (_tpCoroutine != null)
                {
                    StopCoroutine(_tpCoroutine);
                    _tpCoroutine = null;
                    slider.value = 0;
                }
            }
        }

        private IEnumerator TeleportCoroutine()
        {
            var time = 0f;
            while (time < holdTime)
            {
                time += Time.deltaTime;
                slider.value = time / holdTime;
                yield return null;
            }

            ChangeHeight();
        }

        private void ChangeHeight()
        {
            var controller = transform.position;
            var floor = Vector3.zero;
            var delta = controller.y - floor.y;
            positionSetter.MoveLocalTransform(Vector3.down * delta);
            positionSetter.SetY(positionSetter.Position.y);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (handInputProvider) handInputProvider.firstButtonChanged.RemoveListener(OnFirstChanged);
        }
    }
}