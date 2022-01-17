using UnityEngine;

namespace Interactable
{
    public interface IHandItem
    {
        void SetInHolder(HandItemHolder holder);
        void RemoveFromHolder();
        Transform GetTransform();
    }
}