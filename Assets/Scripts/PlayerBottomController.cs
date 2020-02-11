using UnityEngine;
using System.Collections;
public class PlayerBottomController : MonoBehaviour
{

    /*
     * Controls the player's leg animations. Do not put interaction in this script that
     * affect the player. This is strictly just for show.
     */

    private float rotateOffset = 0f;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float inputF = Input.GetAxis("Vertical");
        float inputR = Input.GetAxis("Horizontal");
        bool run = Input.GetButton("Run");

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Standing Still
        if (inputR == 0 && inputF == 0)
        {
            anim.SetInteger("state", 0);
            rotateOffset = 0;
        }

        //Walking Forward
        if (inputR == 0 && inputF > 0)
        {
            rotateOffset = 0;
            if (run)
            {
                anim.SetInteger("state", 4);
            }
            else
            {
                anim.SetInteger("state", 1);
            }
        }

        //Walking Angle Left
        if (inputR > 0 && inputF > 0)
        {
            rotateOffset = -45;
            if (run)
            {
                anim.SetInteger("state", 4);
            }
            else
            {
                anim.SetInteger("state", 1);
            }
        }

        //Walking Angle Right
        if (inputR < 0 && inputF > 0)
        {
            rotateOffset = 45;
            if (run)
            {
                anim.SetInteger("state", 4);
            }
            else
            {
                anim.SetInteger("state", 1);
            }
        }

        //Walking Left
        if (inputR > 0 && inputF == 0)
        {
            anim.SetInteger("state", 2);
            rotateOffset = -90;
        }

        //Walking Right
        if (inputR < 0 && inputF == 0)
        {
            anim.SetInteger("state", 3);
            rotateOffset = 90;
        }

        //Walking Backwards
        if (inputR == 0 && inputF < 0)
        {
            rotateOffset = 0;
            anim.SetInteger("state", 1);
        }

        //Walking Backwards Angle Left
        if (inputR > 0 && inputF < 0)
        {
            rotateOffset = 45;
            anim.SetInteger("state", 1);
        }

        //Walking Backwards Angle Right
        if (inputR < 0 && inputF < 0)
        {
            rotateOffset = -45;
            anim.SetInteger("state", 1);
        }

        transform.rotation = Quaternion.AngleAxis(rotateOffset, Vector3.forward) * Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
    }
}