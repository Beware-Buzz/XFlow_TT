using System;

namespace Store
{
    public class StoreModel
    {
        public event Action<BundleModel> BundleSceneOpenRequested;

        public void RaiseBundleSceneOpenRequested(BundleModel bundleModel)
        {
            BundleSceneOpenRequested?.Invoke(bundleModel);
        }
    }
}