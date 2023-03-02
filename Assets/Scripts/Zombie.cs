using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public GameObject player;
    public float hp = 100;
    float damage = 35;

    //movement variables
    int moveSpeed = 15;
    public bool followingPlayer = false;
    public float aimlessWalkTimeMax = 5f;
    float aimlessWalkTimeCount = 0f;
    bool beginAimlessWalkTime = true;
    public float walkTimeMax = 1f;
    float walktTimeCount = 0.0f;
    bool haveTurned = false;
    bool investigate = false;
    public float investigateTimerMax = 5f;
    public float investigateTimerCount = 0f;

    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0,0,Random.Range(0f,360f));
        aimlessWalkTimeMax += Random.Range(-2f,2f);
        walkTimeMax += Random.Range(-1f,1f);
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0){
            Object.Destroy(gameObject,0f);
        }
        if(followingPlayer){
            lookAtPlayer();
            transform.position += (Vector3)transform.up * Time.deltaTime * moveSpeed * 1.025f;
        }else if(investigate){
            transform.position += (Vector3)transform.up * Time.deltaTime * moveSpeed * 0.975f;
            if(investigateTimerCount < investigateTimerMax){
                investigateTimerCount += Time.deltaTime;
            }else{
                investigate = false;
                investigateTimerCount = 0f;
            }
        }else{
            aimlesslyWalk();
        }
    }

    void lookAtPlayer(){
        Vector3 diff = player.transform.position - transform.position;
        diff.Normalize();
 
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
    public void reduceHP(int reductor){
        hp -= reductor;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Notice"))
        {
            followingPlayer = true;
        }
        if(other.gameObject.CompareTag("Investigate")){
            lookAtPlayer();
            investigateTimerCount = 0f;
            investigate = true;
        }
    }
    void walkTowards(Vector3 location){

    }

    void aimlesslyWalk(){
        if(aimlessWalkTimeMax < 0.1 || walkTimeMax < 0.1){
            aimlessWalkTimeMax = 5f;
            walkTimeMax = 1f;
        }

        if(beginAimlessWalkTime && aimlessWalkTimeCount < aimlessWalkTimeMax){
            aimlessWalkTimeCount += Time.deltaTime;
        }

        if(beginAimlessWalkTime && aimlessWalkTimeCount > aimlessWalkTimeMax){
            walkTimeMax += Random.Range(-1f,1f);
            beginAimlessWalkTime = false;
            haveTurned = true;
        }

        if(walkTimeMax != walktTimeCount && !beginAimlessWalkTime){
            walktTimeCount += Time.deltaTime;
            transform.position += (Vector3)transform.up * Time.deltaTime * moveSpeed * 0.25f;
        }

        if(walkTimeMax < walktTimeCount){
            aimlessWalkTimeMax += Random.Range(-2f,2f);
            beginAimlessWalkTime = true;
            walktTimeCount = 0f;
            aimlessWalkTimeCount = 0f;
        }

        if(haveTurned){
            transform.Rotate(0,0,Random.Range(0f,360f));
            haveTurned = false;
        }
    }

    public void takeDamage(float damageTaken){
        hp -= damageTaken;
    }
}
