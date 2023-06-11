using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieHandler : MonoBehaviour
{
    public float health;// The health of the zombie.
    public GameObject deadEffect;// The effect to play when the zombie dies, we will use the GoreBlast script.
    NavMeshAgent navMeshAgent; // The NavMeshAgent component, which is used to move the zombie and handles the AI in the game.
    public GameObject TheTarget; // The target to move towards, which is the player.
    Transform target; // The transform of the target.
    float attackCoolDownTimer; // The timer to keep track of when the zombie can attack again.
    public Animator animator; // The animator component, which is used to play animations.

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = TheTarget.transform;

         // Set the destination of the NavMeshAgent to the target position (the player).
        navMeshAgent.SetDestination(target.position); 
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
      // Calculate the distance between the zombie and the player
        float distance = Vector3.Distance(transform.position, target.position);

        if (distance < 25 || health < 100)
        {

            // If the distance between the zombie and the player is greater than 3.5,
            // then move towards the player.
            if (distance > 3.5f)
            {
               // Set the destination of the NavMeshAgent to the target position (the player).
                navMeshAgent.SetDestination(target.position);

                //optimization code for moving between attack and move animations.
                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                bool isAttacking = stateInfo.IsName("Attack");
                bool hasAttacked = stateInfo.normalizedTime >= 1f;
                if (stateInfo.IsName("Attack") && !hasAttacked)
                {
                    animator.SetBool("move", true);
                }
                else
                {
                    navMeshAgent.isStopped = false;
                    animator.SetBool("move", true);
                }
            }
            else
            {
               // If the distance between the zombie and the player is less than 3.5,
               // then stop moving and attack the player.
                navMeshAgent.isStopped = true;
                if (attackCoolDownTimer <= 0)
                {
                  // If the attack cooldown timer is less than or equal to zero,
                  // then attack the player and reset the attack cooldown timer.
                    target.GetComponent<PlayerHealth>().TakeDamage(10);
                    attackCoolDownTimer = 3f;
                    animator.SetBool("move", false);
                    animator.SetTrigger("Attack");
                }

                  // Rotate the zombie to face the player.
                Vector3 direction = (target.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(
                    new Vector3(direction.x, 0, direction.z)
                );
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    lookRotation,
                    Time.deltaTime * 10
                );
            }
        }
        else
        {
         // If the distance between the zombie and the player is greater than 25,
         // then stop moving and play the idle animation.
            navMeshAgent.isStopped = true;
            animator.SetBool("move", false);
        }

         // Decrease the attack cooldown timer by the time that has passed since the last frame.
        if (attackCoolDownTimer > 0)
        {
            attackCoolDownTimer -= Time.deltaTime;
        }
    }

   // This function handles the zombie taking damage.
    public void TakeDamage()
    {
        health -= 10;

         // If the health of the zombie is less than or equal to zero,
         // then destroy the zombie and play the dead effect.
        if (health <= 0)
        {

            // Play the dead effect and destroy it after 3 seconds.
            GameObject _effect =
                Instantiate(
                    deadEffect,
                    transform.position + new Vector3(0, 2, 0),
                    Quaternion.identity
                ) as GameObject;
            Destroy(_effect, 3f);
            MenuHandler menuHandler = FindObjectOfType<MenuHandler>();

            // if the menu handler is not null, then increase the score by 10.
            if (menuHandler)
            {
                menuHandler.score += 10;
            }

            // Destroy the zombie.
            Destroy(gameObject);
        }
    }
}
