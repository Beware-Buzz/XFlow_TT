using UnityEngine;

namespace Store
{
    [CreateAssetMenu(menuName = "Scriptable Objects/StoreBundlesManifest")]
    public class StoreBundlesManifest : ScriptableObject
    {
        public string[] BundleNames;
    }
}