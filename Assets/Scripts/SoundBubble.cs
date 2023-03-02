using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBubble : MonoBehaviour
{
    public float radius;
    public GameObject soundBubble;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CircleCollider2D>().radius = radius;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateRadius(float modifier){
        soundBubble.GetComponent<CircleCollider2D>().radius = radius * modifier;
    }
}
