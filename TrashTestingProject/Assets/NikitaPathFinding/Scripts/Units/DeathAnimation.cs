using UnityEngine;

namespace NikitaPathFinding.Scripts.Units
{
    public class DeathAnimation : MonoBehaviour
    {
        public void Death()
        {
            Destroy(this.gameObject.transform.parent.gameObject);
        }
    }
}
