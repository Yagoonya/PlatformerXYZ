using UnityEngine;

namespace PixelCrew.Components.ColliderBased
{
    public class PlatformAttachComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag(_tag))
            {
                col.transform.parent = transform;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(_tag))
            {
                other.transform.parent = null;
            }
        }
    }
}