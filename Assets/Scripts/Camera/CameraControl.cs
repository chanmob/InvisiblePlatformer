using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private BoxCollider2D box2d;

    private GameObject player;
    public GameObject objectCam;

    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        box2d = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(box2d.bounds.max.y < player.transform.position.y)
        {
            this.transform.position = new Vector3(pos.x, pos.y + 10, pos.z);
            objectCam.transform.position = new Vector3(pos.x, pos.y + 10, pos.z);
            pos = transform.position;
        }
        else if (box2d.bounds.min.y > player.transform.position.y)
        {
            this.transform.position = new Vector3(pos.x, pos.y - 10, pos.z);
            objectCam.transform.position = new Vector3(pos.x, pos.y - 10, pos.z);
            pos = transform.position;
        }
    }
}
