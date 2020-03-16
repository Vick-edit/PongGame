using Extensions;
using UnityEngine;

namespace Components.Helpers
{
    /// <summary>
    ///     Компонент, уничтожающий объект, попавшие в его зону
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class DestroyCollidedMono : MonoBehaviour
    {
        [SerializeField] private LayerMask _collisionMask;


        private void OnTriggerEnter2D(Collider2D other)
        {
            if(_collisionMask.IsContainsLayer(other.gameObject.layer))
                Destroy(other.gameObject);
        }
    }
}