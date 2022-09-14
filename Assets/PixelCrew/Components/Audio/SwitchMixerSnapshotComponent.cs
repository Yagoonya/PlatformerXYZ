using UnityEngine;
using UnityEngine.Audio;

namespace PixelCrew.Components.Audio
{
    public class SwitchMixerSnapshotComponent : MonoBehaviour
    {
        [SerializeField] private AudioMixerSnapshot[] _changed;

        public void Change()
        {
            foreach (var snapshot in _changed)
            {
                snapshot.TransitionTo(1f);   
            }
        }
        
    }
}