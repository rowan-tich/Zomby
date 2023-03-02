using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    float velocityVal = 40f;
    float progress;
    float speed = 40f;
    public float timeToDestroy = 0.5f;


    Vector3 startPosition;
    Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        Object.Destroy(gameObject,1f);
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        progress += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(startPosition, targetPosition, progress);
    }

    public void setTargetPosition(Vector3 newTargetPosition){
        targetPosition = newTargetPosition;
    }
}
