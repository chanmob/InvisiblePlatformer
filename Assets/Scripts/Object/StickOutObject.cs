using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickOutObject : MonoBehaviour
{
    public float stickTime;
    public float delayTime;

    private bool sticked;

    public Vector2 spaceVec;
    private Vector2 pos;

    private void Start()
    {
        pos = transform.position;
        StartCoroutine(StickCoroutine());
    }

    private IEnumerator StickCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayTime);

            if (sticked)
            {
                Vector2 goalPos = new Vector2(pos.x + ((int)spaceVec.x).GetTileSize(), pos.y + ((int)spaceVec.y).GetTileSize());

                while (Vector2.Distance(transform.position, goalPos) >= 0.01f)
                {
                    transform.position = Vector2.Lerp(transform.position, goalPos, stickTime * Time.deltaTime);
                    yield return null;
                }

                transform.position = goalPos;
            }
            else
            {
                while (Vector2.Distance(transform.position, pos) >= 0.01f)
                {
                    transform.position = Vector2.Lerp(transform.position, pos, stickTime * Time.deltaTime);
                    yield return null;
                }

                transform.position = pos;
            }

            sticked = !sticked;
        }
    }
}
