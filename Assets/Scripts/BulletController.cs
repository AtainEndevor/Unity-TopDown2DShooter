using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    /*
     * This controls the bullet movement instantiated by the player top controller.
     * This will eventually hold more information like damage and other possible
     * effects that will transfer to the target once it hits something.
     */

    public GameObject MuzzleFlashPrefab;
    public float speed = 5f;
    public float range = 0.1f;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        //This shows the muzzle flash at the bullets source for a brief time before the bullet takes off
        Instantiate(MuzzleFlashPrefab, transform.position, transform.rotation);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
	}

    void Awake()
    {
        Destroy(gameObject, range);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
