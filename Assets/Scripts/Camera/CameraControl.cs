using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public BoxCollider2D mapBox2d;
  
    private GameObject player;
    public GameObject objectCam;

    private Vector3 pos;

    private float cameraHalfSize;

    void Start()
    {
        mapBox2d = GetComponent<BoxCollider2D>();
        cameraHalfSize = mapBox2d.size.y;
        player = GameObject.FindGameObjectWithTag("Player");
        pos = transform.position;
    }

    void Update()
    {
        if(mapBox2d.bounds.max.y < player.transform.position.y)
        {
            this.transform.position = new Vector3(pos.x, pos.y + mapBox2d.size.y, pos.z);
            objectCam.transform.position = new Vector3(pos.x, pos.y + mapBox2d.size.y, pos.z);
            pos = transform.position;
        }
        else if (mapBox2d.bounds.min.y > player.transform.position.y)
        {
            this.transform.position = new Vector3(pos.x, pos.y - mapBox2d.size.y, pos.z);
            objectCam.transform.position = new Vector3(pos.x, pos.y - mapBox2d.size.y, pos.z);
            pos = transform.position;
        }

        if (mapBox2d.bounds.max.x < player.transform.position.x)
        {
            this.transform.position = new Vector3(pos.x + mapBox2d.size.x, pos.y, pos.z);
            objectCam.transform.position = new Vector3(pos.x + mapBox2d.size.x, pos.y, pos.z);
            pos = transform.position;
        }
        else if (mapBox2d.bounds.min.x > player.transform.position.x)
        {
            this.transform.position = new Vector3(pos.x - mapBox2d.size.x, pos.y, pos.z);
            objectCam.transform.position = new Vector3(pos.x - mapBox2d.size.x, pos.y, pos.z);
            pos = transform.position;
        }
    }
}
