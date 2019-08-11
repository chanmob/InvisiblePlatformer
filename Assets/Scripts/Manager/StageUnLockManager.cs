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

        if (curLevel > maxLevel)
        {
            curLevel = maxLevel;
        }
        
        for(int i = 0; i < curLevel + 1; i++)
        {
            sceneButtons[i].interactable = true;
            sceneButtons[i].GetComponent<CanvasGroup>().alpha = 1f;
        }

        if(curLevel < 50)
        {
            var blink = sceneButtons[curLevel].GetComponentsInChildren<Transform>(true);
            StartCoroutine(StartTextBlink(blink.FindInObjects("Text").GetComponent<Text>()));
            StartCoroutine(StartTextBlink(blink.FindInObjects("Time").GetComponent<Text>()));
            StartCoroutine(StartImageBlink(blink.FindInObjects("Image").GetComponent<Image>()));
        }
    }

    private IEnumerator StartImageBlink(Image _image)
    {
        float[] alphas = new float[]
        {
            0.5f,
            1f
        };

        float alpha;
        int idx = 0;

        while (true)
        {
            alpha = alphas[idx];

            _image.CrossFadeAlpha(alpha, 0.5f, false);

            yield return new WaitForSeconds(0.5f);

            idx++;

            if (idx >= alphas.Length)
                idx = 0;
        }
    }

    private IEnumerator StartTextBlink(Text _text)
    {
        float[] alphas = new float[]
        {
            0.5f,
            1f
        };

        float alpha;
        int idx = 0;

        while (true)
        {
            alpha = alphas[idx];

            _text.CrossFadeAlpha(alpha, 0.5f, false);

            yield return new WaitForSeconds(0.5f);

            idx++;

            if (idx >= alphas.Length)
                idx = 0;
        }
    }
}
