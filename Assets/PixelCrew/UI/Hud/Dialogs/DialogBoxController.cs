using System.Collections;
using PixelCrew.Model.Data;
using PixelCrew.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PixelCrew.UI.Hud.Dialogs
{
    public class DialogBoxController : MonoBehaviour
    {
        [Header("Left")] [SerializeField] private GameObject _container;
        [SerializeField] private Text _text;
        [SerializeField] private Image _avatar;

        [Header("Right")] [SerializeField] private GameObject _containerR;
        [SerializeField] private Text _textR;
        [SerializeField] private Image _avatarR;

        [Space] [SerializeField] private float _textSpeed = 0.09f;

        [Header("Sounds")] [SerializeField] private AudioClip _typing;
        [SerializeField] private Animator _animator;
        [SerializeField] private AudioClip _open;
        [SerializeField] private AudioClip _close;

        private static readonly int IsOpen = Animator.StringToHash("IsOpen");

        private DialogData _data;
        private int _currentSentence;
        private AudioSource _sfxSource;
        private Coroutine _typingRoutine;
        private UnityEvent _onComplete;

        private void Start()
        {
            _sfxSource = AudioUtils.FindSfxSource();
        }

        public void ShowDialog(DialogData data, UnityEvent onComplete)
        {
            _onComplete = onComplete;
            _data = data;
            _currentSentence = 0;
            _text.text = string.Empty;

            if (_data.Sentences[_currentSentence].Side == Side.Left)
            {
                _container.SetActive(true);
                _avatar.sprite =_data.Sentences[_currentSentence].Avatar;
            }
            else
            {
                _containerR.SetActive(true);
                _avatarR.sprite =_data.Sentences[_currentSentence].Avatar;
            }
            _sfxSource.PlayOneShot(_open);
            _animator.SetBool(IsOpen, true);
        }

        private IEnumerator TypeDialogText(Text textContainer, GameObject container)
        {
            textContainer.text = string.Empty;
            container.SetActive(true);
            var sentence = _data.Sentences[_currentSentence].Sentence;

            foreach (var letter in sentence)
            {
                textContainer.text += letter;
                _sfxSource.PlayOneShot(_typing);
                yield return new WaitForSeconds(_textSpeed);
            }

            _typingRoutine = null;
        }

        public void OnSkip()
        {
            if (_typingRoutine == null) return;

            _text.text = _data.Sentences[_currentSentence].Sentence;
            _textR.text = _data.Sentences[_currentSentence].Sentence;
            StopTypeAnimation();
        }

        public void OnContinue()
        {
            if (_typingRoutine != null)
            {
                OnSkip();
            }
            else
            {
                StopTypeAnimation();
                _currentSentence++;

                var isDialogCompleted = _currentSentence >= _data.Sentences.Length;
                if (isDialogCompleted)
                {
                    HideDialogBox();
                    _onComplete?.Invoke();
                }
                else
                    OnStartDialogAnimation();
            }
        }

        private void HideDialogBox()
        {
            _animator.SetBool(IsOpen, false);
            _sfxSource.PlayOneShot(_close);
        }

        private void StopTypeAnimation()
        {
            if (_typingRoutine != null)
                StopCoroutine(_typingRoutine);
            _typingRoutine = null;
        }

        private void OnStartDialogAnimation()
        {
            if (_data.Sentences[_currentSentence].Side == Side.Left)
            {
                _avatar.sprite = _data.Sentences[_currentSentence].Avatar;
                _typingRoutine = StartCoroutine(TypeDialogText(_text, _container));
                _containerR.SetActive(false);
            }
            else
            {
                _avatarR.sprite = _data.Sentences[_currentSentence].Avatar;
                _typingRoutine = StartCoroutine(TypeDialogText(_textR, _containerR));
                _container.SetActive(false);
            }
        }

        private void OnCloseDialogAnimation()
        {
            _container.SetActive(false);
            _containerR.SetActive(false);
        }
    }
}