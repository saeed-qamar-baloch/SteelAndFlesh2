using System.Collections;
using UnityEngine;

public class SingleSwordAttack : MonoBehaviour
{
    
    public int singleSwordValue;
    public Animator Anim;


    public Transform attackArea;
    public float giveDamage = 10f;
    public float attackRadius;
    public LayerMask KnightLayer;
    public PlayerScript playerScript;

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Anim.SetBool("SingleHandAttackActive", true);
        
        }

        SingleSwordModes();


    }



    void SingleSwordModes()
    {
        if (Input.GetMouseButtonDown(0))
        {

            singleSwordValue = Random.Range(1, 7);
            if (singleSwordValue == 1)

            {
              
                Attack();
                // animition 

                StartCoroutine(SingleAttack1());
            }


            if (singleSwordValue == 2)

            {

                
                Attack();
                //attack 
                // animition 

                StartCoroutine(SingleAttack2());
            }




            if (singleSwordValue == 3)

            {
               
                Attack();
                // animition 

                StartCoroutine(SingleAttack3());
            }

            if (singleSwordValue == 4)

            {
                
                Attack();
                // animition 

                StartCoroutine(SingleAttack4());
            }

            if (singleSwordValue == 4)

            {

                Attack();
                // animition 

                StartCoroutine(SingleAttack5());
            }
        }


    }

    void Attack()
    {
        Collider[] hitKnight = Physics.OverlapSphere(attackArea.position, attackRadius, KnightLayer);

        foreach (Collider knight in hitKnight)
        {
            KnightAI knightAI = knight.GetComponent<KnightAI>();

            if (knightAI != null)
            {
                knightAI.TakeDamage(giveDamage);
            }

        }

    }

    private void OnDrawGizmosSelected()
    {
        if (attackArea == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackArea.position, attackRadius);
    }


    IEnumerator SingleAttack1()
    {
        Anim.SetBool("SingleAttack1", true);
        yield return new WaitForSeconds(0.2f);
        Anim.SetBool("SingleAttack1", false);
    }

    IEnumerator SingleAttack2()
    {
        Anim.SetBool("SingleAttack2", true);
        yield return new WaitForSeconds(0.2f);
        Anim.SetBool("SingleAttack2", false);
    }


    IEnumerator SingleAttack3()
    {
        Anim.SetBool("SingleAttack3", true);
        yield return new WaitForSeconds(0.2f);
        Anim.SetBool("SingleAttack3", false);
    }

    IEnumerator SingleAttack4()
    {
        Anim.SetBool("SingleAttack4", true);
        yield return new WaitForSeconds(0.2f);
        Anim.SetBool("SingleAttack4", false);
    }


    IEnumerator SingleAttack5()
    {
        Anim.SetBool("SingleAttack5", true);
        yield return new WaitForSeconds(0.2f);
        Anim.SetBool("SingleAttack5", false);
    }
}
