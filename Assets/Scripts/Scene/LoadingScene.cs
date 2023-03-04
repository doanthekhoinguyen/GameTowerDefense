using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public Slider Progress;
    IEnumerator Start()
    {
        // fake time because scene load too fast
        float fakeProgressValue = 0;
        while (fakeProgressValue < 1)
        {
            yield return new WaitForSeconds(0.001f);
            fakeProgressValue += 0.001f;
            Progress.value = Mathf.Min(fakeProgressValue,1);
        }
        
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Home");
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            Progress.value = asyncOperation.progress;
            
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
