using System.Collections.Generic;
using UnityEngine;

namespace Interactable
{
    public class PickUpObject : MonoBehaviour
    {

        [SerializeField] private List<Component> disableComponents;
        
        public void PickUp()
        {
            // disableComponents.ForEach(c=>c.);
        }
        
        public void Drop()
        {
        }
        
    }
}