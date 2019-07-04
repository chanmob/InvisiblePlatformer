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

    public IEnumerator EndGameCoroutine()
    {
        end = true;

        var cc = GameObject.FindObjectOfType<CameraBackground>();

        yield return StartCoroutine(cc.CameraBackgroundToWhite());

        string saveName = SceneManager.GetActiveScene().name;

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
