using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace XueXi
{
    public class SceneLoader : MonoBehaviour
    {
        public GameObject loadingScreen;
        public Slider progressBar;
        public string oldScenes;
        private Image image;
        public void Init()
        {
            image = transform.Find("Image").GetComponent<Image>();
            GameEvent.Instance.AddEventListener(GameEventType.LoadSceneStart, LoadScene);
        }

        private void LoadScene(object[] obj)
        {
            loadingScreen.SetActive(true);
            image.sprite = (Sprite)obj[2];
            StartCoroutine(LoadSceneAsync(obj[0].ToString(), (Vector3)obj[1]));
        }


        IEnumerator LoadSceneAsync(string sceneName,Vector3 pos)
        {
            if (oldScenes!="")
            {
                SceneManager.UnloadSceneAsync(oldScenes);
            }
            oldScenes = sceneName;
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

            while (progressBar.value<=0.9f)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                progressBar.value=Mathf.Lerp(progressBar.value, progress,0.025f);
                yield return null;
            }
            progressBar.value = 0;
            loadingScreen.SetActive(false);

            GameEvent.Instance.Dispatch(GameEventType.LoadSceneEixt, new object[] { pos });
        }

        private void OnDestroy()
        {
            GameEvent.Instance.RemoveEventListener(GameEventType.LoadSceneStart, LoadScene);
        }
    }
}
