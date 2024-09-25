using System;
using System.Collections;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Scenes
{
    public class ProgressTrackingNetworkSceneManager : NetworkSceneManagerDefault
    {
        public Action<float> OnSceneLoadProgress;

        protected override IEnumerator SwitchScene(SceneRef prevScene, SceneRef newScene, FinishedLoadingDelegate finished)
        {
            var asyncOperation = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Single);
            asyncOperation.allowSceneActivation = false;

            while (!asyncOperation.isDone)
            {
                OnSceneLoadProgress?.Invoke(asyncOperation.progress);

                if (asyncOperation.progress >= 0.9f)
                {
                    asyncOperation.allowSceneActivation = true;
                }

                yield return null;
            }

            yield return asyncOperation;

            var networkObjects = FindObjectsOfType<NetworkObject>();

            finished?.Invoke(networkObjects);
        }
    }
}