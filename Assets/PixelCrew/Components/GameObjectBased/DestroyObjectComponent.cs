using PixelCrew.Model;
using UnityEngine;

namespace PixelCrew.Components.GameObjectBased
{
    [RequireComponent(typeof(RestoreStateComponent))]
    public class DestroyObjectComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _objectToDestroy;
        private RestoreStateComponent _state;
        
        private void Start()
        {
            _state = GetComponent<RestoreStateComponent>();
        }

        public void DestroyObject()
        {
            Destroy(_objectToDestroy);
            if (_state != null)
                GameSession.Instance.StoreState(_state.Id);
        }
    }
}