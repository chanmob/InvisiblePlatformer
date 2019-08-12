using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeStage.AntiCheat.ObscuredTypes;

public class MainUI : MonoBehaviour
{
    private bool soundOff;

    public Sprite[] soundSprites = new Sprite[2];

    public Image soundImage;

    private void Start()
    {
        if (ObscuredPrefs.HasKey("SOUND"))
        {
            var sound = ObscuredPrefs.GetInt("SOUND");

            if (sound == 1)
            {
                soundOff = true;
                AudioListener.volume = 0;
                soundImage.sprite = soundSprites[1];
            }
            else if (sound == 0)
            {
                soundOff = false;
                AudioListener.volume = 1;
                soundImage.sprite = soundSprites[0];
            }
        }
    }

    public void Review()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.ChanMob.CellCorporation");
    }

    public void SoundControl()
    {
        if (soundOff)
        {
            soundOff = false;
            AudioListener.volume = 1;
            soundImage.sprite = soundSprites[0];
            ObscuredPrefs.SetInt("SOUND", 0);
        }

        else
        {
            soundOff = true;
            AudioListener.volume = 0;
            soundImage.sprite = soundSprites[1];
            ObscuredPrefs.SetInt("SOUND", 1);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
