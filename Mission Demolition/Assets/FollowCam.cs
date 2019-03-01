﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public GameObject POI;       // the static point of interest  // static = singleton

    [Header("Set in Inspector")]
    public float easing = 0.05f;
    public Vector2 minXY = new Vector2(0,0);
    [Header("Set Dynamically")]
    public float camZ;                  // desired Z pos of the camera
	// Use this for initialization
	void Awake ()
	{
	    camZ = this.transform.position.z;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    if (POI == null) return;        //get out of this method, no POI

        // get the position of the poi
	    Vector3 destination = POI.transform.position;
        // limit the x & y to minimum values
	    destination.x = Mathf.Max(minXY.x, destination.x);
	    destination.y = Mathf.Max(minXY.y, destination.y);
        // interpolate from the current Camera position toward the destination
	    destination = Vector3.Lerp(transform.position, destination, easing);
        // force destination.z to be the camZ to keep the camera far enough away
	    destination.z = camZ;
	    transform.position = destination; // update camera position
	    Camera.main.orthographicSize = destination.y + 10;
	}
}
