using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public GameObject crosshair;
    float midXPos = 0f;
    float midYPos = 0f;
    public bool aiming = false;
    public float aimSize;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(aiming){
            Camera.main.orthographicSize = aimSize;
            newAiming();
        }else{
            Camera.main.orthographicSize = 25f;
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        }
        
    }

    void newAiming(){

        midXPos = player.transform.position.x + (crosshair.transform.position.x - player.transform.position.x) / 2;
        midYPos = player.transform.position.y + (crosshair.transform.position.y - player.transform.position.y) / 2;
        transform.position = new Vector3(midXPos, midYPos, transform.position.z);
    }
}
