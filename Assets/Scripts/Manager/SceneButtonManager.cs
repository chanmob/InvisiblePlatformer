using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SceneButtonManager : MonoBehaviour
{
    private readonly string sceneName = "Level";

    private List<Button> sceneButtonList = new List<Button>();

    void Start()
    {
        sceneButtonList = GetComponentsInChildren<Button>().ToList();

        int count = 1;

        foreach(var b in sceneButtonList)
        {
            string listenerSceneName = sceneName + " " + count;

            b.onClick.AddListener(() => SceneLoad.instance.LoadScene(listenerSceneName));
            count++;
        }
    }
}
