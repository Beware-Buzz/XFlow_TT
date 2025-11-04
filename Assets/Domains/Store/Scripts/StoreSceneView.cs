using Core;
using UnityEngine;

namespace Store
{
    public class StoreSceneView : MonoBehaviour, IView
    {
        [SerializeField]
        private Transform _bundlesContainer;
        [SerializeField]
        private Transform _cheatsContainer;
        public Transform BundlesContainer => _bundlesContainer;
        public Transform CheatsContainer => _cheatsContainer;
    }
}