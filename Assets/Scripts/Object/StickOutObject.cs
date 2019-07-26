using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickOutObject : MonoBehaviour
{
    public float stickTime;
    public float delayAppearTime;
    public float delayDisapperTime;
    public float startDelayTime;

    private bool sticked;

    public Vector2 spaceVec;
    private Vector2 pos;

    private IEnumerator Start()
    {
        pos = transform.position;

        if(startDelayTime > 0)
        {
            yield return new WaitForSeconds(startDelayTime);
        }

        StartCoroutine(StickCoroutine());
    }

    private IEnumerator StickCoroutine()
    {
        while (true)
        {
            if (sticked)
            {
                yield return new WaitForSeconds(delayDisapperTime);

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
                yield return new WaitForSeconds(delayAppearTime);

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
