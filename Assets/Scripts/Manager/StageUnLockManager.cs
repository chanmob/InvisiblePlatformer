using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUnLockManager : MonoBehaviour
{
    private readonly string sceneName = "Level";

    public int maxLevel { private set; get; }
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

            timeText.text = SaveAndLoad.instance.LoadFloatData(loadName).ToString("00 : 00 : 000");

            count++;
        }

        curLevel = SaveAndLoad.instance.LoadIntData("CurLevel");
        curLevel++;

        //For Test
        curLevel = 50;


        for(int i = 0; i < curLevel; i++)
        {
            sceneButtons[i].interactable = true;
            sceneButtons[i].GetComponent<CanvasGroup>().alpha = 1f;
        }
    }
}
