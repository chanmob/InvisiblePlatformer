using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public BoxCollider2D mapBox2d;
    public BoxCollider2D cameraBox2d;

    private GameObject player;
    public GameObject objectCam;

    private Vector3 pos;

    private float cameraHalfSize;

    // Start is called before the first frame update
    void Start()
    {        
        cameraHalfSize = cameraBox2d.size.y * 0.5f;
        player = GameObject.FindGameObjectWithTag("Player");
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(mapBox2d.bounds.max.y < player.transform.position.y)
        {
            this.transform.position = new Vector3(pos.x, pos.y + cameraHalfSize, pos.z);
            objectCam.transform.position = new Vector3(pos.x, pos.y + cameraHalfSize, pos.z);
            pos = transform.position;
        }
        else if (mapBox2d.bounds.min.y > player.transform.position.y)
        {
            this.transform.position = new Vector3(pos.x, pos.y - cameraHalfSize, pos.z);
            objectCam.transform.position = new Vector3(pos.x, pos.y - cameraHalfSize, pos.z);
            pos = transform.position;
        }
    }
}
