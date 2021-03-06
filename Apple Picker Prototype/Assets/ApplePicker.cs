﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplePicker : MonoBehaviour {
    [Header("Set in Inspector")]
    public GameObject basketPrefab;

    public int numBaskets = 3;
    public float basketBottomY = -14;
    public float basketSpacingY = 2f;
    public List<GameObject> basketList;
    // Use this for initialization
    void Start () {
        basketList = new List<GameObject>();

        for (int i = 0; i < numBaskets; i++)
        {
            GameObject tBasketGO = Instantiate<GameObject>(basketPrefab);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY + (basketSpacingY * i);
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void AppleDestroyed()
    {
        // destroy all of the falling apples
        // get the index of th elast basket in basketList
        int basketIndex = basketList.Count - 1;
        // get a reference to that Basket GameObject
        GameObject tBasketGO = basketList[basketIndex];
        // remeove the basket from the list and destroy its GameObject
        basketList.RemoveAt(basketIndex);
        Destroy(tBasketGO);

        if (basketList.Count == 0)
        {
            SceneManager.LoadScene("_Scene_0");
        }
    }
}
