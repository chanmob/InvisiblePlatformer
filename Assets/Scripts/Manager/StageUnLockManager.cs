using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StageUnLockManager : MonoBehaviour
{
    private readonly string sceneName = "Level";

    public int maxLevel { private set; get; } = 50;
    public int curLevel { private set; get; }

    public GameObject buttonParent;

    private Button[] sceneButtons;

    private void Awake()
    {
        sceneButtons = buttonParent.GetComponentsInChildren<Button>();

        int count = 1;

        for (int i = 0; i < sceneButtons.Length; i++)
        {
            string listenerSceneName = sceneName + " " + count;
            sceneButtons[i].onClick.AddListener(() => SceneLoad.instance.LoadScene(listenerSceneName));

            var stageText = sceneButtons[i].transform.FindInChildren("Text").GetComponent<Text>();
            stageText.text = "스테이지 " + (i + 1);

            var timeText = sceneButtons[i].transform.FindInChildren("Time").GetComponent<Text>();
            string loadName = sceneName + " " + count + "Clear";

            var clearTime = SaveAndLoad.instance.LoadFloatData(loadName);
            TimeSpan ts = TimeSpan.FromSeconds(clearTime);
            var tsHour = ts.TotalHours;
            timeText.text = string.Format("{0:00} : {1:00} : {2:000}", ts.Minutes + tsHour, ts.Seconds, ts.Milliseconds);

            count++;
        }

        curLevel = SaveAndLoad.instance.LoadIntData("CurLevel");
        Debug.Log(curLevel);

        if (curLevel > maxLevel)
        {
            curLevel = maxLevel;
        }
        
        for(int i = 0; i < curLevel + 1; i++)
        {
            sceneButtons[i].interactable = true;
            sceneButtons[i].GetComponent<CanvasGroup>().alpha = 1f;
        }
    }
}
