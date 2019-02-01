using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour {
    // class level variables (inspector variables)
    // prefab for instantiating apples
    public GameObject aaplePrefab;
    // speed at which the AppleTree moves
    public float speed = 1f;
    // distance where the AppleTree turns
    public float leftAndRightEdge = 20f;
    // Chance that the AppleTree will change directions
    public float chanceToChangeDirections = 0.1f;
    // Rate at which Apples will be instantiated
    public float secondsBetweenAppleDrops = 1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
        // basic movement across the screen
	    Vector3 pos = transform.position;   // save the current position
	    pos.x += speed * Time.deltaTime;
	    transform.position = pos;

        // changing direction
	    if (pos.x < -leftAndRightEdge)
	    {
	        speed = Mathf.Abs(speed);   //move right

	    }
	    else if (pos.x > leftAndRightEdge)
	    {
	        speed = -Mathf.Abs(speed); // move left
	    }

    }

    void FixedUpdate()
    {
       

        if (Random.value < chanceToChangeDirections)
        {
            speed *= -1;
        }
    }
}
