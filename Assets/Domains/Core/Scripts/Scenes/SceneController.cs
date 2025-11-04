using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class SceneController : IController
    {
        private readonly SceneModel _sceneModel;
        private List<GameObject> _rootGameObjectsBuffer = new(1);

        public SceneController(SceneModel sceneModel)
        {
            _sceneModel = sceneModel;
        }

        public void Start()
        {
            _sceneModel.SceneOpenRequested += OnSceneOpenRequested;
        }

        private void Stop()
        {
            _sceneModel.SceneOpenRequested -= OnSceneOpenRequested;
        }

        private void OnSceneOpenRequested(
            TaskCompletionSource<GameObject> completionSource,
            string sceneName,
            CancellationToken cancellationToken)
        {
            TaskLoadSceneAndSetResultAsync(
                completionSource,
                sceneName,
                cancellationToken).FireAndForget();
        }

        private async Task TaskLoadSceneAndSetResultAsync(
            TaskCompletionSource<GameObject> completionSource,
            string sceneName,
            CancellationToken cancellationToken)
        {
            try
            {
                var gameObject = await LoadSceneAsync(sceneName, cancellationToken);
                completionSource.TrySetResult(gameObject);
                return;
            }
            catch (OperationCanceledException exception)
            {
                Debug.Log(exception);
            }
            catch (Exception exception)
            {
                Debug.LogError(exception);
            }

            completionSource.TrySetResult(null);
        }

        private async Task<GameObject> LoadSceneAsync(
            string sceneName,
            CancellationToken cancellationToken)
        {
            var op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            if (op == null)
            {
                throw new System.InvalidOperationException(
                    $"{nameof(SceneController)}.{nameof(LoadSceneAsync)}: Can't load '{sceneName}'");
            }

            await op;
            if (cancellationToken.IsCancellationRequested)
            {
                await SceneManager.UnloadSceneAsync(sceneName);
                return null;
            }

            var scene = SceneManager.GetSceneByName(sceneName);
            _rootGameObjectsBuffer.Clear();
            scene.GetRootGameObjects(_rootGameObjectsBuffer);

            if (_rootGameObjectsBuffer.Count != 1)
            {
                throw new System.InvalidOperationException(
                    $"{nameof(SceneController)}.{nameof(LoadSceneAsync)}: Scene '{sceneName}' must have exactly one root gameObject");
            }

            return _rootGameObjectsBuffer[0];
        }
    }
}