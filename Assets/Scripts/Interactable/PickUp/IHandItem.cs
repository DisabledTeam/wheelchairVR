using UnityEngine;
using WheelInput;

namespace Interactable
{
    public interface IHandItem
    {
        void SetInHolder(HandItemHolder holder, HandInputProvider handInputProvider);
        void RemoveFromHolder();
        Transform GetTransform();

        Vector3 GetPositionInHand();
        Vector3 GetRotationInHand();
    }
}