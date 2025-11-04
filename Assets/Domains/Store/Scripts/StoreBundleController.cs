using System.Threading;
using System.Threading.Tasks;
using Core;
using UnityEngine;

namespace Store
{
    public class StoreBundleController : IController
    {
        private const string PREFAB_NAME = "StoreBundleView";
        private readonly BundleModel _bundleModel;
        private readonly PlayerData _playerData;
        private readonly Transform _viewContainer;
        private readonly StoreModel _storeModel;

        private StoreBundleView _view;
        private bool _isPurchaseInProgress;
        private CancellationTokenSource _purchaseCancellationTokenSource;

        public StoreBundleController(
            PlayerData playerData,
            Transform viewContainer,
            BundleModel bundleModel,
            StoreModel storeModel)
        {
            _playerData = playerData;
            _viewContainer = viewContainer;
            _bundleModel = bundleModel;
            _storeModel = storeModel;
        }

        public void Start()
        {
            _playerData.DataChanged += OnDataChanged;
            InitView();
        }

        private void Stop()
        {
            _playerData.DataChanged -= OnDataChanged;
            DisposePurchaseToken();
            if (!_view)
            {
                return;
            }

            _view.BuyButtonClicked -= OnBuyButtonClicked;
            _view.InfoButtonClicked -= OnInfoButtonClicked;
            _view.Dispose();
        }

        private void OnBuyButtonClicked()
        {
            DisposePurchaseToken();
            _purchaseCancellationTokenSource = new CancellationTokenSource();
            Purchase(_purchaseCancellationTokenSource.Token).FireAndForget();
        }

        private void OnDataChanged() => TryUpdateViewButtonState();

        private void OnInfoButtonClicked()
        {
            _storeModel.RaiseBundleSceneOpenRequested(_bundleModel);
        }

        private async Task Purchase(CancellationToken cancellationToken)
        {
            _isPurchaseInProgress = true;
            _view.SetInProgressState();
            await Task.Delay(3000, cancellationToken);
            //Я не хендлил кейс где токен кенселиться и надо возвращать вью в дефолт стейт - поскольку токен кенселиться только при стопе контроллера
            if (IsAvailable())
            {
                Buy();
                _view.SetActiveBuyButton(IsAvailable());
            }
            else
            {
                _view.SetActiveBuyButton(false);
            }

            _view.SetDefaultBuyButtonText();
            _isPurchaseInProgress = false;
        }

        private void TryUpdateViewButtonState()
        {
            if (!_view || _isPurchaseInProgress)
            {
                return;
            }

            _view.SetActiveBuyButton(IsAvailable());
        }

        private bool IsAvailable()
        {
            foreach (var rewardAction in _bundleModel.Cost)
            {
                if (!rewardAction.CanExecute(_playerData))
                {
                    return false;
                }
            }

            return true;
        }

        private void Buy()
        {
            foreach (var rewardAction in _bundleModel.Cost)
            {
                rewardAction.Execute(_playerData);
            }

            foreach (var rewardAction in _bundleModel.Prize)
            {
                rewardAction.Execute(_playerData);
            }
        }

        private void InitView()
        {
            var prefab = Resources.Load<StoreBundleView>(PREFAB_NAME);
            _view = Object.Instantiate(prefab, _viewContainer);
            _view.Init(_bundleModel.TitleText, IsAvailable());
            _view.SetDefaultBuyButtonText();
            _view.BuyButtonClicked += OnBuyButtonClicked;
            _view.InfoButtonClicked += OnInfoButtonClicked;
        }

        private void DisposePurchaseToken()
        {
            if (_purchaseCancellationTokenSource == null)
            {
                return;
            }

            _purchaseCancellationTokenSource.Cancel();
            _purchaseCancellationTokenSource.Dispose();
        }
    }
}