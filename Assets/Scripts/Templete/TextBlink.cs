using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlink : MonoBehaviour
{
    public Text text;

    void Start()
    {
        StartCoroutine(StartBlink());
    }

    IEnumerator StartBlink()
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

            text.CrossFadeAlpha(alpha, 0.5f, false);

            yield return new WaitForSeconds(0.5f);

            idx++;

            if (idx >= alphas.Length)
                idx = 0;
        }
    }
}
