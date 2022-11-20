using System;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.Model.Data
{
    [Serializable]
    public class DialogData
    {
        [SerializeField] private SentencesData[] _sentences;
        public SentencesData[] Sentences => _sentences;
        
    }


    [Serializable]
    public struct SentencesData
    {
        [SerializeField] private string _sentence;
        [SerializeField] private Sprite _avatar;
        [SerializeField] private Side _side;

        public string Sentence => _sentence;
        public Sprite Avatar => _avatar;
        public Side Side => _side;
    }

    public enum Side
    {
        Left,
        Right
    }
}