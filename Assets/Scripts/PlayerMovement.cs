using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    /*
     * This controls the entire player. This script is attached to the player's root which. The player
     * has two children, top and bottom to control the player's torso and feet. Interactions that happen
     * at this level will happen to the player a whole.
     */
    public float Speed = 1;
    public float FastSpeed = 3;
    public KeyCode EnableFastSpeedWithKey = KeyCode.LeftShift;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var currentSpeed = Speed;
        if (Input.GetKey(EnableFastSpeedWithKey) && Input.GetAxis("Vertical") > 0)
        {
            currentSpeed = FastSpeed;
        }

        if (Input.GetAxis("Vertical") > 0 || (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") != 0))
            transform.Translate(transform.Find("Player Bottom").transform.up * currentSpeed * Time.deltaTime);
        if (Input.GetAxis("Vertical") < 0)
            transform.Translate(-transform.Find("Player Bottom").transform.up * currentSpeed * Time.deltaTime);
    }
}