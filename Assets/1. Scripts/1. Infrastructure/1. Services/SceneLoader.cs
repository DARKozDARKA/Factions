using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase.Infastructure
{
    public class SceneLoader
    {
        private ICoroutineRunner _coroutineRunner;

        private SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(string sceneName, Action action)
        {
            _coroutineRunner.StartCoroutine(LoadScene(sceneName, action));
        }

        private IEnumerator LoadScene(string sceneName, Action action)
        {
            if (SceneManager.GetActiveScene().name == sceneName)
            {
                action?.Invoke();
                yield break;
            }
            
            AsyncOperation newScene = SceneManager.LoadSceneAsync(sceneName);

            while (!newScene.isDone)
                yield return null;

            action?.Invoke();

        }
    }

}
