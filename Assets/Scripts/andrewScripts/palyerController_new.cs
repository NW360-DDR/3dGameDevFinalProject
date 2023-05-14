using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class palyerController_new : MonoBehaviour
{
    public Rigidbody playerRB;
    public Rigidbody rayCastTarget;
    Vector3 moveDir;

    private float tempPlayerx;
    private float tempPlayerz;
    public float smoothing;

    public float speed;
    public float speedMod;

    public float jumpForce;
    [SerializeField] bool isGrounded; //testing variable
    private float rayDist = 0.1f;
    public float gravity;
    private bool atJumpApex = false;

    AudioSource jumpsound; //Adding JumpSound
    private Animator animator; // Enables the animator controller to switch between idle and walking anim

    [SerializeField] new Vector3 forwardVector; // testing variable

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        jumpsound = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // this has to be in Update rather than FixedUpdate, otherwise jumps are inconsistent
        Jump();

        // Sets the walking bool to true or false if the player is moving. Enables animation switching
        if (moveDir != Vector3.zero)
        {
            animator.SetBool("isWakling", true);
        }
        else
        {
            animator.SetBool("isWakling", false);
        }

        // Sets the punching bool to true or false if the player is moving. Enables animation switching
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetBool("isPunching", true);
        }
       // else if (!(Input.GetKeyDown(KeyCode.Mouse0)))
       // {
       //     animator.SetBool("isPunching", false);
       // }
    }

    void FixedUpdate()
    {
        // Get Input Direction and Velocity
        float horizontal = Input.GetAxisRaw("Horizontal");
        float y = playerRB.velocity.y;

        // Sets move direction and applies force to player
        // playerRb.AddForce uses -moveDir because it "inverts" the movement. Without it, the controls are reversed.
        moveDir = transform.TransformVector(1, 0, 0) * horizontal;
        playerRB.AddForce(-moveDir.normalized * speed * speedMod * Time.deltaTime, ForceMode.Impulse);

        // Stops player and smoothes movement
        if (Input.GetAxisRaw("Horizontal") == 0f)
        {
            //grab temporary player x and z coordinates
            tempPlayerx = playerRB.velocity.x;
            tempPlayerz = playerRB.velocity.z;

            //get rid of all movement on the rigidbody
            playerRB.velocity = Vector3.zero;
            playerRB.angularVelocity = Vector3.zero;

            //add that speed back
            playerRB.velocity = new Vector3(tempPlayerx, playerRB.velocity.y, tempPlayerz);

            //make the player come to a slower stop depending on smoothing variable (set smoothing variable to a decimal)
            playerRB.velocity = playerRB.velocity * smoothing;
        }

        // Limits speed to speed variable
        if (playerRB.velocity.magnitude > speed)
        {
            playerRB.velocity = playerRB.velocity.normalized * speed;
        }

        //adds the player y coordinate back after doing all this math, this is important for jumping
        playerRB.velocity = new Vector3(playerRB.velocity.x, y, playerRB.velocity.z);
    }

    void Jump()
    {
        // Applies gravity effect to player
        playerRB.AddForce(Vector3.down * gravity, ForceMode.Impulse);

        // Ground Detection
        Debug.DrawRay(rayCastTarget.position, -transform.up * rayDist);
        isGrounded = Physics.Raycast(rayCastTarget.position, Vector3.down, rayDist);
        //print(isGrounded);

        // Makes the player Jump
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpsound.Play(); //Sound of player jumping
        }

        // Checks for apex of jump -- Doesn't do anything at the moment 4/10/23
        // Still doesn't do anything lol 5/14/23
        if (playerRB.velocity.y < 0 && atJumpApex == false)
        {
            atJumpApex = true;
            print("Aepx");
        }
        if (isGrounded)
        {
            atJumpApex = false;
        }
    }



} // end of program