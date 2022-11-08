using PixelCrew.Model;
using UnityEngine;

namespace PixelCrew.Components
{
    public class RestoreStateComponent : MonoBehaviour
    {
        private string _id;
        private GameSession _session;
        public string Id => _id;
        private void Start()
        {
            _id = gameObject.name;
            _session = FindObjectOfType<GameSession>();
            var isRemoved = _session.RestoreState(_id);
            if(isRemoved)
                Destroy(gameObject);
        }
    }
}