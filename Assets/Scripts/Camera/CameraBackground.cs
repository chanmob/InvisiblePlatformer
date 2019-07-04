using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBackground : MonoBehaviour
{
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        StartCoroutine(CameraBackgroundControl());
    }

    private IEnumerator CameraBackgroundControl()
    {
        Color32[] colors = new Color32[]
        {
            new Color32(20, 0, 0, 255),
            new Color32(20, 0, 20, 255),
            new Color32(0, 0, 20, 255),
            new Color32(0, 20, 20, 255),
            new Color32(0, 20, 0, 255),
            new Color32(20, 20, 0, 255),
            new Color32(0, 0, 0, 255)
        };

        Color32 color;
        int idx = 0;

        while (true)
        {
            color = colors[idx];
            cam.backgroundColor = Color.Lerp(cam.backgroundColor, color, 2.5f * Time.deltaTime);

            if (cam.backgroundColor == color)
            {
                idx++;

                if (idx >= colors.Length)
                    idx = 0;
            }

            yield return null;
        }
    }

    public IEnumerator CameraBackgroundToWhite()
    {
        while (true)
        {
            cam.backgroundColor = Color.Lerp(cam.backgroundColor, Color.white, Time.deltaTime);

            if(cam.backgroundColor == Color.white)
            {
                break;
            }

            yield return null;
        }
    }
}
