using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float chaseRadius = 20f;
    [SerializeField] private float turnSpeed = 5f;
    private float distanceToEnemy =Mathf.Infinity;
    private NavMeshAgent navMeshAgent;
    private Animator anim;

    private bool isProvoked =false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToEnemy= Vector3.Distance(target.position, transform.position);
        if (distanceToEnemy>chaseRadius)
        {
            anim.SetTrigger("idle");
            isProvoked = false;
        }
        else if (isProvoked)
        {
            FaceTarget();
            EngagePlayer();
        }
        else if (distanceToEnemy <= chaseRadius)
        {
            isProvoked = true;
        }
        
    }

    private void EngagePlayer()
    {
        if (distanceToEnemy>navMeshAgent.stoppingDistance)
        {
            ChasePlayer();
        }
        else if (distanceToEnemy<=navMeshAgent.stoppingDistance)
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        anim.SetTrigger("attack");
        print("Atacking player"); 
    }

    void ChasePlayer()
    {
        anim.SetTrigger("move");
        navMeshAgent.SetDestination(target.position);
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    } 
}
