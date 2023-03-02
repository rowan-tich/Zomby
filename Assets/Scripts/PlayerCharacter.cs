using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{   
    
    public float moveSpeed = 10f;
    public float moveSpeedMod = 1f;

    //"Sound" Variables
    public GameObject noticeBubble;
    public GameObject investigateBubble;
    bool investigateBubbleTimer = false;
    float investigateBubbleTimerMax = 0.5f;
    float investigateBubbleTimerCount = 0f;
    float shotInvestigateBubbleMod = 3f;
    float soundBubbleMod = 1.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        look();
        investigativeBubbleManager();
    }

    void movement(){
        if(Input.GetKey("w")){
            transform.position += new Vector3(0, moveSpeed) * Time.deltaTime * moveSpeedMod;
        }
        if(Input.GetKey("a")){
            transform.position += new Vector3(-moveSpeed, 0) * Time.deltaTime * moveSpeedMod;
        }
        if(Input.GetKey("s")){
            transform.position += new Vector3(0, -moveSpeed) * Time.deltaTime * moveSpeedMod;
        }
        if(Input.GetKey("d")){
            transform.position += new Vector3(moveSpeed, 0) * Time.deltaTime * moveSpeedMod;
        }
    }

    void look(){
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }

    public void shotFired(float investigateBubbleMod){
        investigateBubble.GetComponent<SoundBubble>().updateRadius(investigateBubbleMod);
        resetInvestigativeBubble();
    }
    void resetInvestigativeBubble(){
        investigateBubbleTimerCount = 0f;
        investigateBubbleTimer = true;
    }
    void investigativeBubbleManager(){
        if(investigateBubbleTimer && investigateBubbleTimerCount < investigateBubbleTimerMax){
            investigateBubbleTimerCount += Time.deltaTime;
        }
        if(investigateBubbleTimer && investigateBubbleTimerCount > investigateBubbleTimerMax){
            investigateBubble.GetComponent<SoundBubble>().updateRadius(1f);
            investigateBubbleTimer = false;
        }
    }
}
