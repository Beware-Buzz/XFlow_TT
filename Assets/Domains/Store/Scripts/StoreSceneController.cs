using Core;
using Core.Rewards;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Store
{
    public class StoreSceneController : IController
    {
        private const string BUNDLES_MANIFEST_PATH = "StoreBundlesManifest";
        private const string SCENE_NAME = "StoreScene";
        private readonly PlayerData _playerData;
        private readonly StoreModel _storeModel;
        private readonly StoreBundlesModel _bundlesModel = new ();

        public StoreSceneController(
            PlayerData playerData,
            StoreModel storeModel)
        {
            _playerData = playerData;
            _storeModel = storeModel;
        }

        public void Start()
        {
            var rootGameObjects = SceneManager.GetSceneByName(SCENE_NAME).GetRootGameObjects();
            var storeSceneView = rootGameObjects[0].GetComponent<StoreSceneView>();
            new StoreCheatController(
                _playerData,
                storeSceneView.CheatsContainer).Start();
            var container = storeSceneView.BundlesContainer;
            foreach (var bundleName in GetBundleNames())
            {
                var model = GetBundleModel(bundleName);
                new StoreBundleController(
                    _playerData,
                    container,
                    model,
                    _storeModel).Start();
            }
        }

        private string[] GetBundleNames()
        {
            return Resources.Load<StoreBundlesManifest>(BUNDLES_MANIFEST_PATH).BundleNames;
        }

        private BundleModel GetBundleModel(string bundleName)
        {
            if (_bundlesModel.BundleModels.TryGetValue(bundleName, out BundleModel bundleModel))
            {
                return bundleModel;
            }

            var settings = LoadBundleSettings(bundleName);

            bundleModel = new BundleModel()
            {
                TitleText = settings.Text,
                Cost = ToRewardActions(settings.Cost),
                Prize = ToRewardActions(settings.Prize)
            };
            _bundlesModel.BundleModels.Add(bundleName, bundleModel);
            return bundleModel;
        }

        private BundleSettings LoadBundleSettings(string bundleName)
        {
            return Resources.Load<BundleSettings>(bundleName);
        }

        private IRewardAction[] ToRewardActions(RewardActionHolder[] rewardActionHolders)
        {
            var length = rewardActionHolders.Length;
            var result = new IRewardAction[length];
            for (var i = 0; i < length; i++)
            {
                result[i] = rewardActionHolders[i].RewardAction;
            }
            return result;
        }
    }
}