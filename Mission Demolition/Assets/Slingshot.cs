﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour {
    // class level variable
    [Header("Set in Inspector")]
    public GameObject prefabProjectile;
    public float velocityMult = 8f;

    [Header("Set dynamically")]
    public GameObject launchPoint;

    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;
    private Rigidbody projectileRigidbody;

    void Awake()
    {
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
    } // end of Awake method

    void Update()
    {
        // if slingshot is not in aimingMode, don't run the code
        if (!aimingMode) return;        //bad code, bad

        // get the current mouse position in 2D screen coordinates
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        // find the delta from the launchPos to the mousePos3D
        Vector3 mouseDelta = mousePos3D - launchPos;        // vector subtraction
        // Limit mouseDelta to the radius of the slingshot SphereCollider
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }   // end of if

        // move the projectile to this new position
        Vector3 projPos = launchPos + mouseDelta;       // vector addition
        projectile.transform.position = projPos;        // move our projectile to the new posititon

        if (Input.GetMouseButtonUp(0))
        {
            // the mouse has been released
            aimingMode = false;
            projectileRigidbody.isKinematic = false;
            projectileRigidbody.velocity = -mouseDelta * velocityMult;
            FollowCam.POI = projectile;     // set POI for our camera
            projectile = null;
        } // end of if
    }
    void OnMouseDown()
    {
        // the player has pressed the mouse button while over the slingshot
        aimingMode = true;
        // instantiating a projectile
        projectile = Instantiate(prefabProjectile) as GameObject;
        // start it at the launchPoint
        projectile.transform.position = launchPos;
        // set it to isKinematic for now
        //projectile.GetComponent<Rigidbody>().isKinematic = true;
        projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.isKinematic = true;
    }
	// Use this for initialization
	void OnMouseEnter () {
        //print("Slingshot:OneMouseEnter()");
	    launchPoint.SetActive(true);
	}
	
	// Update is called once per frame
	void OnMouseExit ()
	{
	    //print("Slingshot:OnMouseExit()");
	    launchPoint.SetActive(false);
	}
}
