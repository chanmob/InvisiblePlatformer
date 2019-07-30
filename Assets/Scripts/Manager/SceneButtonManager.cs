using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SceneButtonManager : Singleton<SceneButtonManager>
{
    private readonly string sceneName = "Level";

    public Button[] sceneButtonList;

    void Start()
    {
        sceneButtonList = GetComponentsInChildren<Button>();

        int count = 1;

        for (int i = 0; i < sceneButtonList.Length; i++)
        {
            string listenerSceneName = sceneName + " " + count;
            sceneButtonList[i].onClick.AddListener(() => SceneLoad.instance.LoadScene(listenerSceneName));

            var stageText = sceneButtonList[i].transform.FindInChildren("Text").GetComponent<Text>();
            stageText.text = "스테이지 " + (i + 1);

            var timeText = sceneButtonList[i].transform.FindInChildren("Time").GetComponent<Text>();
            string loadName = sceneName + " " + count + "Clear";

            timeText.text = SaveAndLoad.instance.LoadFloatData(loadName).ToString("0.00");

            count++;
        }
    }
}
