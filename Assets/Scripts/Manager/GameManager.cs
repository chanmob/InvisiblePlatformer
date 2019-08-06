using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using CodeStage.AntiCheat.ObscuredTypes;

public class GameManager : Singleton<GameManager>
{
    private Text timeText;

    private GameObject mobileController;
    private GameObject pausePanel;
    private GameObject resultPanel;

    private bool end = false;

    private TimeSpan timespan;

    private void Awake()
    {
        mobileController = GameObject.FindGameObjectWithTag("Controller");
        var ui = GameObject.FindGameObjectWithTag("GameUI");

        var objectinUI = ui.GetComponentsInChildren<Transform>(true);

        timeText = objectinUI.FindInObjects("TimeText").GetComponent<Text>();
        pausePanel = objectinUI.FindInObjects("PausePanel");
        resultPanel = objectinUI.FindInObjects("ResultPanel");
        objectinUI.FindInObjects("PauseButton").GetComponent<Button>().onClick.AddListener(() => PauseGame());

        var pause = pausePanel.GetComponentsInChildren<Transform>(true);
        pause.FindInObjects("Restart").GetComponent<Button>().onClick.AddListener(() => {
            Time.timeScale = 1f;
            SceneLoad.instance.LoadedSceneLoad();
        });
        pause.FindInObjects("Main").GetComponent<Button>().onClick.AddListener(() => MainScene());
        pause.FindInObjects("Quit").GetComponent<Button>().onClick.AddListener(() => QuitGame());

        var result = resultPanel.GetComponentsInChildren<Transform>(true);
        result.FindInObjects("Next").GetComponent<Button>().onClick.AddListener(() => OpenNextScene());
        result.FindInObjects("Main").GetComponent<Button>().onClick.AddListener(() => MainScene());
        result.FindInObjects("Quit").GetComponent<Button>().onClick.AddListener(() => QuitGame());
    }

    private void FixedUpdate()
    {
        if (end)
            return;

        timespan = TimeSpan.FromSeconds(Time.time);
        var t = timespan.TotalHours * 60;
        timeText.text = string.Format("{0:00} : {1:00} : {2:000}", timespan.Minutes + t, timespan.Seconds, timespan.Milliseconds);
    }

    public void GameEnd()
    {
        StartCoroutine(EndGameCoroutine());
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        mobileController.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        mobileController.SetActive(true);
    }

    private IEnumerator EndGameCoroutine()
    {
        end = true;
        mobileController.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().isDead = true;

        var cc = GameObject.FindObjectOfType<CameraBackground>();

        StartCoroutine(cc.CameraBackgroundToWhite());

        string[] sceneName = SceneManager.GetActiveScene().name.Split(' ');
        int level = int.Parse(sceneName[1]);
        Debug.Log(level);
        if (SaveAndLoad.instance.LoadIntData("CurLevel") < level)
        {
            SaveAndLoad.instance.SaveIntData("CurLevel", level);
        }
        string saveName = SceneManager.GetActiveScene().name + "Clear";
        float time = (float)timespan.TotalSeconds;

        SaveAndLoad.instance.HasData(saveName, exist =>
        {
            if (exist)
            {
                float beforeData = SaveAndLoad.instance.LoadFloatData(saveName);

                if (time > beforeData)
                {
                    SaveAndLoad.instance.SaveFloatData(saveName, time);
                }
                else
                {
                    time = beforeData;
                }
            }
            else
            {
                SaveAndLoad.instance.SaveFloatData(saveName, time);
            }
        });

        var t = timespan.TotalHours * 60;
        resultPanel.transform.FindInChildren("ClearTime").GetComponent<Text>().text = string.Format("클리어 시간 : " + "{0:00} : {1:00} : {2:000}", timespan.Minutes + t, timespan.Seconds, timespan.Milliseconds);

        TimeSpan ts = TimeSpan.FromSeconds(time);
        resultPanel.transform.FindInChildren("BestTime").GetComponent<Text>().text = string.Format("최고 기록 : " + "{0:00} : {1:00} : {2:000}", ts.Minutes, ts.Seconds, ts.Milliseconds);

        yield return new WaitForSeconds(2f);

        resultPanel.SetActive(true);
    }

    private void OpenNextScene()
    {
        var idx = SceneLoad.instance.GetLoadedSceneIndex();
        idx++;
        SceneLoad.instance.LoadScene(idx);
    }

    private void MainScene()
    {
        var mm = GameObject.Find("DieMarkManager").GetComponent<DieMarkManager>();
        mm.ClearDieMark();

        SceneLoad.instance.LoadScene("Main");
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
