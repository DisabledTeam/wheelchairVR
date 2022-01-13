using UnityEngine;

namespace DefaultNamespace
{
    public class WheelRoot : MonoBehaviour
    {
        public virtual bool CanDetach()
        {
            return true;
        }

        public virtual void OnDetach()
        {
        }

        public virtual void OnAttach()
        {
        }


        public void Initialize(WheelChair wheelChair)
        {
        }
    }
    
    
    public class Test1WheelRoot : WheelRoot
    {
    }

    public class Test2WheelRoot : WheelRoot
    {
    }

}