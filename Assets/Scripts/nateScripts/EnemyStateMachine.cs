using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    private Owens_StateMachine brain;
    private GameObject player;

    public float distanceToChase;
    public float distanceToAttack;
    public float cooldownTimer = 1;

    private bool withinAttackRange;
    private bool playerSpotted;

    private NavMeshAgent nav;
    private SphereCollider hurtbox;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        brain = GetComponent<Owens_StateMachine>();
        hurtbox = GetComponent<SphereCollider>();
        hurtbox.enabled = false;
        playerSpotted= false;

    }

    // Update is called once per frame
    void Update()
    {
        
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

    void AttackEnter(){ }
    void Attack()
    {
        //anim.setBool("Attack", true);
        hurtbox.enabled = true;
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0)
        {
            brain.PushState(Violence, ViolenceEnter, ViolenceExit);
            cooldownTimer = 1;
        }
    }
    void AttackExit() { }


}
