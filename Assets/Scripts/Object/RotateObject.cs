using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public GameObject target;

    public float speed;
    public float radius;
    private float angle;

    private void Start()
    {
        if(target == null)
        {
            Debug.LogError("회전 시킬 오브젝트 없음");
        }
    }

    private void FixedUpdate()
    {
        if(target == null)
        {
            return;
        }

        angle += speed * Time.deltaTime;

        var offset = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;
        target.transform.position = transform.position + offset;
    }
}
