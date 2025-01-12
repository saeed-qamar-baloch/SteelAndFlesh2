using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightAI : MonoBehaviour
{
 
    [Header("Character Info")]
    public float movingSpeed=5f;
    public float runningSpeed;
    public float currentMovingSpeed;
    public float turningSpeed = 300f;
    public float stopSpeed = 1f;
    public float maxHealth = 120f;
    public float currentHealth;

    [Header("Destination Var")]
    public Vector3 destination;
    public bool destinationReached;

    [Header("Knight AI")]
    public GameObject playerBody;
    public LayerMask playerLayer;
    public float visionRadius;
    public float attackRadius;
    public bool playerInVisionRadius;
    public bool playerInAttackRadius;

    [Header("Knight Attack Var")]
    public int singleMeleeVal;
    public Transform attackArea;
    public float giveDamage;
    public float attackingRadius;
    bool previouslyAttack;
    public float timeBtwAttack;
    public Animator anim;

    private void Start()
    {
        currentMovingSpeed = movingSpeed;
        currentHealth = maxHealth;
        playerBody = GameObject.Find("Main Player");
    }


    private void Update()
    {
        playerInVisionRadius = Physics.CheckSphere(transform.position, visionRadius, playerLayer);
        playerInAttackRadius = Physics.CheckSphere(transform.position, attackRadius, playerLayer);

        if (playerInVisionRadius && !playerInAttackRadius)
        {
            anim.SetBool("Idle", false);
            Walk();
        }

        if (playerInVisionRadius && playerInAttackRadius)
        {
            anim.SetBool("Idle", false);
            ChasePlayer();
        }

        if (playerInVisionRadius && playerInAttackRadius)
        {
            anim.SetBool("Idle", true);
            SingleMeleeModes();
        }
    }


    public void Walk()
    {
        currentMovingSpeed = movingSpeed;

        if (transform.position != destination)
        {
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0;
            float destinationDistance = destinationDirection.magnitude;

            if (destinationDistance >= stopSpeed)
            {
                // Turning
                destinationReached = false;
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turningSpeed * Time.deltaTime);

                // Moving AI
                transform.Translate(Vector3.forward * currentMovingSpeed * Time.deltaTime);

                anim.SetBool("Walk", true);
                anim.SetBool("Attack", false);
                anim.SetBool("Run", false);
            }
            else
            {
                destinationReached = true;
            }
        }
    }

    void ChasePlayer()
    {
        currentMovingSpeed = runningSpeed;
        transform.position += transform.forward * currentMovingSpeed * Time.deltaTime;
        transform.LookAt(playerBody.transform);

        anim.SetBool("Walk", false);
        anim.SetBool("Attack", false);
        anim.SetBool("Run", true);
    }

    void SingleMeleeModes()
    {
        if (!previouslyAttack)
        {
            singleMeleeVal = Random.Range(1, 5);

            if (singleMeleeVal == 1)
            {
                // Attack
           
                Attack();
                // Animation
                StartCoroutine(Attack1());
            }

            if (singleMeleeVal == 2)
            {
                // Attack
        
                Attack();
                StartCoroutine(Attack2());
            }

            if (singleMeleeVal == 3)
            {
                // Attack
             
                Attack();
                StartCoroutine(Attack3());
            }

            if (singleMeleeVal == 4)
            {
                // Attack
           
                Attack();
                StartCoroutine(Attack4());
            }



        }
    }


    void Attack()
    {
        Collider[] hitPlayer = Physics.OverlapSphere(attackArea.position, attackingRadius, playerLayer);

        foreach (Collider player in hitPlayer)
        {
            PlayerScript playerScript = player.GetComponent<PlayerScript>();
            if (playerScript != null)
            {
                playerScript.playerDamage(giveDamage);
            }
        }

        previouslyAttack = true;
        Invoke(nameof(ActivateAttack), timeBtwAttack);
    }


    private void OnDrawGizmosSelected()
    {
        if (attackArea == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackArea.position, attackingRadius);
    }

    private void ActivateAttack()
    {
        previouslyAttack = false;
    }

    IEnumerator Attack1()
    {
        anim.SetBool("Attack1", true);
        movingSpeed = 0f;
        runningSpeed = 0f;
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("Attack1", false);
        movingSpeed = 1f;
        runningSpeed = 3f;
    }

    IEnumerator Attack2()
    {
        anim.SetBool("Attack2", true);
        movingSpeed = 0f;
        runningSpeed = 0f;
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("Attack2", false);
        movingSpeed = 1f;
        runningSpeed = 3f;
    }

    IEnumerator Attack3()
    {
        anim.SetBool("Attack3", true);
        movingSpeed = 0f;
        runningSpeed = 0f;
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("Attack3", false);
        movingSpeed = 1f;
        runningSpeed = 3f;
    }

    IEnumerator Attack4()
    {
        anim.SetBool("Attack4", true);
        movingSpeed = 0f;
        runningSpeed = 0f;
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("Attack4", false);
        movingSpeed = 1f;
        runningSpeed = 3f;
    }

    public void LocateDestination(Vector3 destination)
    {
        this.destination = destination;
        destinationReached = false;
    }
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        anim.SetTrigger("GetHit");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        anim.SetBool("IsDead", true);
        this.enabled = false;
        GetComponent<Collider>().enabled = false;
    }


}
