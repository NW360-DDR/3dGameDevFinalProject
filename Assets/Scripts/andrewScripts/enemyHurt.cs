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
        dist = Mathf.Pow((Player.transform.position.x - transform.position.x), 2) + Mathf.Pow((Player.transform.position.y - transform.position.y), 2);
        if (Mathf.Abs(dist) < Mathf.Pow(acceptableDistance, 2) && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Fin~");
            Destroy(gameObject);
        }
    }
}
