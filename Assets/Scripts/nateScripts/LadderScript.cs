using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScript : MonoBehaviour
{
    public LadderScript OtherSpot;
    Vector3 Telepront;
    GameObject Player;
    public int acceptableDistance = 3;
    public float dist;

    // Start is called before the first frame update
    void Start()
    {
        Telepront = OtherSpot.transform.position;
        // PUT HERE: Whatever we have the player named as/tagged as, populate this here.
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // this is squared, and already is long, not to mention roots are computationally exhaustive to be running every frame, so crappy distance formula!
        dist = Mathf.Pow((Player.transform.position.x - transform.position.x), 2) + Mathf.Pow((Player.transform.position.y - transform.position.y), 2);
        // If the player is close to this ladder thing, we can allow him to telepront.
        if (Mathf.Abs(dist) < Mathf.Pow(acceptableDistance, 2) && Input.GetKeyDown(KeyCode.E))
        {
            Vector3 newPos = new Vector3(Player.transform.position.x, Telepront.y, Player.transform.position.z);
            Player.transform.position = newPos;
        }
    }
}
