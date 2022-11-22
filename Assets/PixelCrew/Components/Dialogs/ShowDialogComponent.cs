using System;
using PixelCrew.Model.Data;
using PixelCrew.Model.Definitions;
using PixelCrew.UI.Hud.Dialogs;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.Dialogs
{
    public class ShowDialogComponent : MonoBehaviour
    {
        [SerializeField] protected Mode _mode;
        [SerializeField] private DialogData _bound;
        [SerializeField] private DialogDefinition _external;
        [SerializeField] private UnityEvent _onComplete;

        private DialogBoxController _dialogBox;
        
        public void Show()
        {
            if (_dialogBox == null)
                _dialogBox = FindObjectOfType<DialogBoxController>();
            
            _dialogBox.ShowDialog(Data, _onComplete);
        }

        public void Show(DialogDefinition def)
        {
            _external = def;
            Show();
        }

        public DialogData Data
        {
            get
            {
                switch (_mode)
                {
                    case Mode.Bound:
                        return _bound;
                    
                    case Mode.External:
                        return _external.Data;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        
        public enum Mode
        {
            Bound,
            External
        }
    }
}