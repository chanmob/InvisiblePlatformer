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
    private GameObject newRecord;

    private bool end = false;

    private float time;

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
        newRecord = result.FindInObjects("NewRecord");
    }

    private void Update()
    {
        if (end)
            return;

        time += Time.deltaTime;

        timespan = TimeSpan.FromSeconds(time);
        var th = timespan.TotalHours * 60;
        timeText.text = string.Format("{0:00} : {1:00} : {2:000}", timespan.Minutes + th, timespan.Seconds, timespan.Milliseconds);
    }

    public void GameEnd()
    {
        StartCoroutine(EndGameCoroutine());
    }

    public void PauseGame()
    {
        Time.timeScale = 0;

        var pauseUI = pausePanel.transform.GetComponentsInChildren<Transform>(true);
        var th = timespan.TotalHours * 60;
        pauseUI.FindInObjects("CurrentTime").GetComponent<Text>().text = string.Format("{0:00} : {1:00} : {2:000}", timespan.Minutes + th, timespan.Seconds, timespan.Milliseconds);

        var clearTime = SaveAndLoad.instance.LoadFloatData(SceneManager.GetActiveScene().name + "Clear");
        TimeSpan ts = TimeSpan.FromSeconds(clearTime);
        var tsHour = ts.TotalHours;
        timeText.text = string.Format("{0:00} : {1:00} : {2:000}", ts.Minutes + tsHour, ts.Seconds, ts.Milliseconds);
        pauseUI.FindInObjects("BestTime").GetComponent<Text>().text = string.Format("{0:00} : {1:00} : {2:000}", timespan.Minutes + th, timespan.Seconds, timespan.Milliseconds);

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

        var mm = GameObject.Find("DieMarkManager");
        if (mm != null)
        {
            mm.GetComponent<DieMarkManager>().ClearDieMark();
        }

        string[] sceneName = SceneManager.GetActiveScene().name.Split(' ');
        int level = int.Parse(sceneName[1]);
        Debug.Log(level);
        if (SaveAndLoad.instance.LoadIntData("CurLevel") < level)
        {
            Debug.Log("저장");
            SaveAndLoad.instance.SaveIntData("CurLevel", level);
        }

        string saveName = SceneManager.GetActiveScene().name + "Clear";
        Debug.Log(saveName);
        float time = (float)timespan.TotalSeconds;

        SaveAndLoad.instance.HasData(saveName, exist =>
        {
            if (exist)
            {
                float beforeData = SaveAndLoad.instance.LoadFloatData(saveName);

                if (time < beforeData)
                {
                    SaveAndLoad.instance.SaveFloatData(saveName, time);
                    newRecord.SetActive(true);
                }
                else
                {
                    time = beforeData;
                }
            }
            else
            {
                SaveAndLoad.instance.SaveFloatData(saveName, time);
                newRecord.SetActive(true);
            }
        });

        var h = timespan.TotalHours * 60;
        resultPanel.transform.FindInChildren("ClearTime").GetComponent<Text>().text = string.Format("클리어 시간 : " + "{0:00} : {1:00} : {2:000}", timespan.Minutes + h, timespan.Seconds, timespan.Milliseconds);

        TimeSpan ts = TimeSpan.FromSeconds(time);
        var tsHour = ts.TotalHours * 60;
        resultPanel.transform.FindInChildren("BestTime").GetComponent<Text>().text = string.Format("최고 기록 : " + "{0:00} : {1:00} : {2:000}", ts.Minutes + tsHour, ts.Seconds, ts.Milliseconds);

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
        var mm = GameObject.Find("DieMarkManager");
        if(mm != null)
        {
            mm.GetComponent<DieMarkManager>().ClearDieMark();
        }
        
        SceneLoad.instance.LoadScene("Main");
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
