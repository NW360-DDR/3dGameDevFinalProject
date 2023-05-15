using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    private Owens_StateMachine brain;
    private GameObject player;

    public float distanceToChase;
    public float distanceToAttack = 2;
    public float cooldownTimer = 1;
    public float Dist;

    private bool withinAttackRange;
    private bool playerSpotted;

    private NavMeshAgent nav;
    private SphereCollider hurtbox;
    public Vector3 knockback;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        brain = GetComponent<Owens_StateMachine>();
        hurtbox = GetComponent<SphereCollider>();
        hurtbox.enabled = false;
        playerSpotted= false;
        brain.PushState(empty, emptyStart, emptyExit);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Dist = Vector3.Distance(transform.position, player.transform.position);
        playerSpotted =  Dist < distanceToChase;
        withinAttackRange = Dist < distanceToAttack;
    }
    
    //~~~~~~~~~~~~~~~STATES~~~~~~~~~~~~~~~~~~~~~

    void emptyStart()
    {
        nav.ResetPath();
    }
    void empty()
    {
        if (playerSpotted)
        {
            brain.PushState(Violence, ViolenceEnter, ViolenceExit);
        }
    }
    void emptyExit()
    {

    }

    void Violence() // This does not work for when we go up ladders
    {
        nav.SetDestination(player.transform.position);
        if (withinAttackRange)
        {
            brain.PushState(Attack, AttackEnter, AttackExit);
        }
    }
    void ViolenceEnter(){}
    void ViolenceExit(){}

    void AttackEnter()
    {
        hurtbox.enabled = true;
    }
    void Attack()
    {
        nav.enabled = false;
        hurtbox.enabled = false;
        /*Rigidbody playerRB = player.GetComponent<Rigidbody>();
        knockback = transform.position - player.transform.position;
        knockback = knockback.normalized;
        knockback.y += 5;
        playerRB.velocity = (knockback * player.GetComponent<palyerController_new>().knockbackMult);
        cooldownTimer -= Time.deltaTime;
        */
        if (cooldownTimer <= 0)
        {
            brain.PushState(Violence, ViolenceEnter, ViolenceExit);
            cooldownTimer = 1;
            
        }
    }
    void AttackExit() 
    { 
        nav.enabled = true;
        hurtbox.enabled = false;
    }


}
