using Core;

namespace Store
{
    public class StoreFeatureController : IController
    {
        private readonly PlayerData _playerData;
        private readonly SceneModel _sceneModel;
        private StoreModel _storeModel;

        public StoreFeatureController(
            PlayerData playerData,
            SceneModel sceneModel)
        {
            _playerData = playerData;
            _sceneModel = sceneModel;
        }

        public void Start()
        {
            _storeModel = new StoreModel();
            _storeModel.BundleSceneOpenRequested += OnBundleSceneOpenRequested;
            new StoreSceneController(
                _playerData,
                _storeModel).Start();
        }

        private void OnBundleSceneOpenRequested(BundleModel bundleModel)
        {
            new StoreBundleSceneController(
                _playerData,
                _sceneModel,
                bundleModel).Start();
        }
    }
}