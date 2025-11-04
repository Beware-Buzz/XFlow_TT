using System;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Store
{
    public class StoreBundleSceneView: MonoBehaviour, IView
    {
        private const string DEFAULT_BUY_BUTTON_TEXT = "Купить";
        private const string PROCESS_BUY_BUTTON_TEXT = "Обработка...";
        
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _buyButtonText;

        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _backButton;

        public event Action BuyButtonClicked;
        public event Action BackButtonClicked;

        public void Init(string tileText, bool isBuyButtonActive)
        {
            _buyButton.onClick.AddListener(OnBuyButtonClicked);
            _backButton.onClick.AddListener(OnBackButtonClicked);
            _titleText.text = tileText;
            SetActiveBuyButtonInternal(isBuyButtonActive);
        }

        public void Dispose()
        {
            gameObject.SetActive(false);
            _buyButton.onClick.RemoveListener(OnBuyButtonClicked);
            _backButton.onClick.RemoveListener(OnBackButtonClicked);
            Destroy(gameObject);
        }

        public void SetActiveBuyButton(bool isActive)
        {
            SetActiveBuyButtonInternal(isActive);
        }

        public void SetInProgressState()
        {
            _buyButtonText.SetText(PROCESS_BUY_BUTTON_TEXT);
            SetActiveBuyButtonInternal(false);
        }

        public void SetDefaultBuyButtonText()
        {
            _buyButtonText.SetText(DEFAULT_BUY_BUTTON_TEXT);
        }

        private void SetActiveBuyButtonInternal(bool isActive)
        {
            _buyButton.interactable = isActive;
        }

        private void OnBuyButtonClicked() => BuyButtonClicked?.Invoke();
        private void OnBackButtonClicked() => BackButtonClicked?.Invoke();
    }
}