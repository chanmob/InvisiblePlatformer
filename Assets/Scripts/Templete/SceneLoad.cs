using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : Singleton<SceneLoad>
{
    public void LoadedSceneLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

    public void LoadScene(int _sceneIdx)
    {
        SceneManager.LoadScene(_sceneIdx);
    }

    public string GetLoadedSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public int GetLoadedSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
