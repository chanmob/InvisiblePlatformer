using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SceneButtonManager : MonoBehaviour
{
    private readonly string sceneName = "Level";

    private Button[] sceneButtonList;

    void Start()
    {
        sceneButtonList = GetComponentsInChildren<Button>();

        int count = 1;

        for (int i = 0; i < sceneButtonList.Length; i++)
        {
            string listenerSceneName = sceneName + " " + count;
            sceneButtonList[i].onClick.AddListener(() => SceneLoad.instance.LoadScene(listenerSceneName));

            var txt = sceneButtonList[i].transform.FindInChildren("Time").GetComponent<Text>();
            string loadName = sceneName + " " + count + "Clear";

            txt.text = SaveAndLoad.instance.LoadFloatData(loadName).ToString("0.00");

            count++;
        }
    }
}
