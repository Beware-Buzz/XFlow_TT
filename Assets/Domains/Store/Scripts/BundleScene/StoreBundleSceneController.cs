using System.Threading;
using System.Threading.Tasks;
using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Store
{
    public class StoreBundleSceneController : IController
    {
        private const string SCENE_NAME = "StoreBundleScene";
        private readonly SceneModel _sceneModel;
        private readonly PlayerData _playerData;
        private readonly BundleModel _bundleModel;
        private StoreBundleSceneView _view;
        private bool _isPurchaseInProgress;
        private CancellationTokenSource _purchaseCancellationTokenSource;
        private CancellationToken _sceneOpenToken;

        public StoreBundleSceneController(
            PlayerData playerData,
            SceneModel sceneModel,
            BundleModel bundleModel)
        {
            _playerData = playerData;
            _sceneModel = sceneModel;
            _bundleModel = bundleModel;
        }

        public void Start()
        {
            LoadScene(_sceneOpenToken).FireAndForget();
            _playerData.DataChanged += OnDataChanged;
        }

        private void Stop()
        {
            _playerData.DataChanged -= OnDataChanged;
            
            if (!_view)
            {
                return;
            }
            _view.BuyButtonClicked -= OnBuyButtonClicked;
            _view.Dispose();
        }

        private void OnDataChanged()=> TryUpdateViewButtonState();

        private void OnBackButtonClicked()
        {
            SceneManager.UnloadSceneAsync(SCENE_NAME);
            Stop();
        }

        private void OnBuyButtonClicked()
        {
            //DisposePurchaseToken();
            _purchaseCancellationTokenSource = new CancellationTokenSource();
            Purchase(_purchaseCancellationTokenSource.Token).FireAndForget();
        }

        private async Task LoadScene(CancellationToken cancellationToken)
        {
            var taskCompletionSource = new TaskCompletionSource<GameObject>();
            _sceneModel.RaiseSceneOpenRequested(
                taskCompletionSource,
                SCENE_NAME,
                cancellationToken);
            var gameObject = await taskCompletionSource.Task;
            if (cancellationToken.IsCancellationRequested || !gameObject)
            {
                Stop();
                return;
            }

            InitView(gameObject);
        }

        private void InitView(GameObject sceneRoot)
        {
            _view = sceneRoot.GetComponent<StoreBundleSceneView>();
            _view.Init(_bundleModel.TitleText, IsAvailable());
            _view.SetDefaultBuyButtonText();
            _view.BuyButtonClicked += OnBuyButtonClicked;
            _view.BackButtonClicked += OnBackButtonClicked;
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
    }
}