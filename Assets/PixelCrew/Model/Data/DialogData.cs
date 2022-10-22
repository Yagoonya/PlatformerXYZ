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


        public void XYZ()
        {
        }
    }


    [Serializable]
    public class SentencesData
    {
        [SerializeField] private string _sentence;
        [SerializeField] private Sprite _avatar;
        [SerializeField] private bool _isLeftWindow;

        public string Sentence => _sentence;
        public Sprite Avatar => _avatar;
        public bool IsLeftWindow => _isLeftWindow;
    }
}