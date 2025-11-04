using System;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Store
{
    public class StoreCheatView : MonoBehaviour, IView
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _textDescription;
        [SerializeField] private TMP_Text _textAmount;

        public event Action ButtonClicked;
        
        public void Init(string description, string amount)
        {
            _button.onClick.AddListener(OnButtonClicked);
            _textDescription.text = description;
            _textAmount.text = amount;
        }

        public void SetAmount(string amount)
        {
            _textAmount.text = amount;
        }

        public void Dispose()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }
        
        private void OnButtonClicked() => ButtonClicked?.Invoke();
    }
}