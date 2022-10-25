using System;
using PixelCrew.UI.Widget;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PixelCrew.UI.Hud.Dialogs
{
    public class OptionDialogController : MonoBehaviour
    {
        [SerializeField] private GameObject _container;
        [SerializeField] private Text _contentText;
        [SerializeField] private Transform _optionsContainer;
        [SerializeField] private OptionalItemWidget _prefab;

        private DataGroup<OptionData, OptionalItemWidget> _dataGroup;

        private void Start()
        {
            _dataGroup = new DataGroup<OptionData, OptionalItemWidget>(_prefab, _optionsContainer);
        }

        public void OnOptionsSelected(OptionData selectedOption)
        {
            selectedOption.OnSelect.Invoke();
            _container.SetActive(false);
        }

        public void Show(OptionDialogData data)
        {
            _container.SetActive(true);
            _contentText.text = data.DialogText;
            
            _dataGroup.SetData(data.Options);
        }
        
    }
    
    [Serializable]
    public class OptionDialogData
    {
        public string DialogText;
        public OptionData[] Options;
    }
    
    [Serializable]
    public class OptionData
    {
        public string Text;
        public UnityEvent OnSelect;
    }
}