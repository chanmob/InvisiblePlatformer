using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CodeStage.AntiCheat.ObscuredTypes;

public class GameManager : Singleton<GameManager>
{
    private Text timeText;

    public GameObject result;
    public GameObject mobileController;

    private float time;

    private bool end = false;

    private void Start()
    {
        timeText = GameObject.FindGameObjectWithTag("TimeText").GetComponent<Text>();
    }

    private void Update()
    {
        if (end)
            return;

        time += Time.deltaTime;

        timeText.text = time.ToString("0.00");
    }

    public void GameEnd()
    {
        StartCoroutine(EndGameCoroutine());
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

        result.SetActive(true);
    }
}
