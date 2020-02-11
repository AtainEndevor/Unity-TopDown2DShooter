using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlashController : MonoBehaviour {

    //Simple controller (timer essentially) to perform the muzzle flash

    public float duration = 1.0f;

	// Use this for initialization
	void Start () {

	}

    void Awake()
    {
        Destroy(gameObject, duration);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
