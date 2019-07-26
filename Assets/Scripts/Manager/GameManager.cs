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

    private float time;

    private bool end = false;

    private void Awake()
    {
        mobileController = GameObject.FindGameObjectWithTag("Controller");
        var ui = GameObject.FindGameObjectWithTag("GameUI");

        timeText = ui.transform.FindInChildren("TimeText").GetComponent<Text>();
        pausePanel = ui.transform.FindInChildren("ResumePanel");
        resultPanel = ui.transform.FindInChildren("ResultPanel");
    }

    private void FixedUpdate()
    {
        if (end)
            return;

        var ts = TimeSpan.FromSeconds(Time.time);

        if (ts.Hours >= 1)
            timeText.text = string.Format("{0:0} : {1:00} : {2:00}", ts.Hours, ts.Minutes, ts.Seconds);
        else
            timeText.text = string.Format("{0:00} : {1:00} : {2:000}", ts.Minutes, ts.Seconds, ts.Milliseconds);
    }

    public void GameEnd()
    {
        StartCoroutine(EndGameCoroutine());
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        mobileController.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        mobileController.SetActive(true);
    }

    private IEnumerator EndGameCoroutine()
    {
        end = true;
        mobileController.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().isDead = true;

        var cc = GameObject.FindObjectOfType<CameraBackground>();

        yield return StartCoroutine(cc.CameraBackgroundToWhite());

        string saveName = SceneManager.GetActiveScene().name + "Clear";

        SaveAndLoad.instance.HasData(saveName, exist => 
        {
            if (exist)
            {
                float beforeData = SaveAndLoad.instance.LoadFloatData(saveName);

                if(time > beforeData)
                {
                    SaveAndLoad.instance.SaveFloatData(saveName, time);
                }
            }
            else
            {
                SaveAndLoad.instance.SaveFloatData(saveName, time);
            }
        });

        resultPanel.SetActive(true);
    }
}
