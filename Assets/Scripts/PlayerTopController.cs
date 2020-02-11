using UnityEngine;

public class PlayerTopController : MonoBehaviour {

    /*
     * Controller to animate more of the player's interaction animations. Like the feet, this script
     * should primarily focus on looks rather than functionality, though some interaction may be
     * required
     */

    public GameObject BulletPrefab;
    Animator anim;

    //Player State holders
    private bool shooting;
    private bool melee;
    private bool reloading;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        //Receive movement input. If moving, set the animator to run the appropriate animation
        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            anim.SetBool("Moving", true);
        else
            anim.SetBool("Moving", false);
        
        //Follow the mouse
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);
        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
    }

    void Update()
    {
        //Update the player's state if the animation is still happening. This prevents the player from shooting while he's still reloading.
        shooting = anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerHandgunShoot");
        melee = anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerHandgunMelee");
        reloading = anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerHandgunReload");

        /*
         * These are the controls for the player and where the top controller will affect the player as a whole, IE ammo, inventory, etc.
         * They should be passed to the player's main controller.
         */

        //ButtonDown = SemiAuto
        if (Input.GetButtonDown("Fire") && !shooting && !melee && !reloading)
        {
            //This is where the bullet is created. This will be modified when new weapons are added. IE Shotgun will instantiate several bullets in a spray
            anim.Play("PlayerHandgunShoot");
            Instantiate(BulletPrefab, transform.position + (transform.up * 0.87f) + (transform.right * 0.23f), transform.rotation);
        }
        if (Input.GetButtonDown("Melee") && !shooting && !melee && !reloading)
        {
            anim.Play("PlayerHandgunMelee");
        }
        if (Input.GetButtonDown("Reload") && !shooting && !melee && !reloading)
        {
            anim.Play("PlayerHandgunReload");
        }
    }

}
