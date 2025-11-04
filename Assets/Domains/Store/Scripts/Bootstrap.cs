using Store;
using UnityEngine;

namespace Core
{
    public class Bootstrap: MonoBehaviour
    {
        public void Awake()
        {
            var playerData = new PlayerData();
            var sceneModel = new SceneModel();
            new SceneController(sceneModel).Start();
            new StoreFeatureController(playerData, sceneModel).Start();
        }
    }
}