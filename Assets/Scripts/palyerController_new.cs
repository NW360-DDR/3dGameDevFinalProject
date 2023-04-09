using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class palyerController_new : MonoBehaviour
{
    public Rigidbody playerRB;
    Vector3 moveDir;

    private float tempPlayerx;
    private float tempPlayerz;

    public float smoothing;

    public float speed;
    public float speedMod;
    public float jumpForce;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        Ray groundDetection = new Ray(transform.position, -transform.up);
    }

    //very important that this is fixed update, normal update only runs once per frame which is not enough for good physics simulation
    void FixedUpdate()
    {
        // displays the raycast in editor window during runtime
        Debug.DrawRay(transform.position, -transform.up);

        //grab input
        float horizontal = Input.GetAxisRaw("Horizontal");


        //jumping
            if (groundDetection.isTouching)
            {

            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerRB.AddForce(new Vector3(0, jumpForce, 0));
            }

        //grab player y velocity
        float y = playerRB.velocity.y;

        //set the movement direction
        moveDir = transform.forward * horizontal;

        //does the actual movement
        playerRB.AddForce(moveDir.normalized * speed * speedMod * Time.deltaTime, ForceMode.Impulse);

        // stops the player and smoothes movement
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

        //limit player speed to speed variable
        if (playerRB.velocity.magnitude > speed)
        {
            playerRB.velocity = playerRB.velocity.normalized * speed;
        }

        //adds the player y coordinate back after doing all this math, this is important for jumping
        playerRB.velocity = new Vector3(playerRB.velocity.x, y, playerRB.velocity.z);






    }
}