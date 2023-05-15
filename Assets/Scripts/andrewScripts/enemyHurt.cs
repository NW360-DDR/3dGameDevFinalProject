using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHurt : MonoBehaviour
{
    // not me blantantly stealing nate's ladder code to re-use as a jank enemy script
    public float dist;
    public int acceptableDistance = 3;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Check distance between player and enemy
        dist = Vector3.Distance(transform.position, Player.transform.position);
        if (Mathf.Abs(dist) < acceptableDistance && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Fin~");
            Destroy(gameObject);
        }
    }
}
