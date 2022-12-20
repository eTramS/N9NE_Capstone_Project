using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMOB : MonoBehaviour
{
    private string currentState = "IdleState";
    private Transform target;
    public float chaseRange = 3;
    public float attackRange = 2;
    public float speed = 2;


    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if (currentState == "IdleState")
        {
            if (distance < chaseRange )
            currentState = "ChaseState";
        } 
        else if (currentState == "ChaseState")
        {
            animator.SetTrigger("chase");
            animator.SetBool("isAttacking", false);

            if(distance < attackRange)
            currentState = "AttackState";


            if(target.position.x > transform.position.x){
                transform.Translate(transform.right * speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler (0, 180, 0);

            }
            else {
                transform.Translate(-transform.right * speed * Time.deltaTime);
                transform.rotation = Quaternion.identity;
            }
        }
        else if (currentState == "AttackState"){
            animator.SetBool("isAttacking", true);

            if(distance > attackRange)
            currentState = "ChaseState";
        }
       
          }
}
