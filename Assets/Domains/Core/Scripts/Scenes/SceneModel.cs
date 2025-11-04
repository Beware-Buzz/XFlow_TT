using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Core
{
    public class SceneModel
    {
        public event Action<TaskCompletionSource<GameObject>, string, CancellationToken> SceneOpenRequested;

        public void RaiseSceneOpenRequested(
            TaskCompletionSource<GameObject> completionSource,
            string sceneName,
            CancellationToken cancellationToken)
        {
            SceneOpenRequested?.Invoke(
                completionSource,
                sceneName,
                cancellationToken);
        }
    }
}