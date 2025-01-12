
using System.Collections;
using UnityEngine;

public class Fist : MonoBehaviour
{

    public float timer = 0f;
    public int FistFightValue;
    public Animator Anim;


    public Transform attackArea;
    public float giveDamage = 10f;
    public float attackRadius;
    public LayerMask KnightLayer;

    [SerializeField] Transform LeftHandPunch;
    [SerializeField] Transform RightHandPunch;
    [SerializeField] Transform LeftLegKick;
    [SerializeField] Transform RightLegKick;
    private void Update()
    {

        if (Input.GetMouseButtonUp(0))
        {

            timer += Time.deltaTime;
        }

        else
        {
            Anim.SetBool("FistFightActive",true);
            timer = 0f;

        }


        if (timer > 5f)
        {
            Anim.SetBool("FistFightActive", false);
        }

        FistFightMode();
    }



    void FistFightMode()
    {
        if (Input.GetMouseButtonDown(0))
        {

            FistFightValue = Random.Range(1, 7);
            if(FistFightValue == 1)

            {
                //attack 
                attackArea = LeftHandPunch;
                attackRadius = 0.5f;
                Attack();
                // animition 

                StartCoroutine(SingleFist());
            }


            if (FistFightValue == 2)

            {

                attackArea = RightHandPunch;
                attackRadius = 0.6f;
                Attack();
                //attack 
                // animition 

                StartCoroutine(DoubleFist());
            }




            if (FistFightValue == 3)

            {
                //attack 
                attackArea = RightLegKick;
                attackRadius = 0.8f;
                Attack();
                // animition 

                StartCoroutine(SideKick());
            }

            if (FistFightValue == 4)

            {
                //attack 
                attackArea = RightLegKick;
                attackRadius = 0.8f;
                Attack();
                // animition 

                StartCoroutine(mmaKick());
            }
        }


    }

    void Attack()
    {
        Collider[] hitKnight = Physics.OverlapSphere(attackArea.position, attackRadius, KnightLayer);

        foreach (Collider knight in hitKnight)
        {
            KnightAI knightAI = knight.GetComponent<KnightAI>();

            if(knightAI != null)
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

        Gizmos.DrawWireSphere(attackArea.position,attackRadius);
    }


    IEnumerator SingleFist()
    {
        Anim.SetBool("SingleFist", true);
        yield return new WaitForSeconds(0.9f);
        Anim.SetBool("SingleFist", false);
    }

    IEnumerator DoubleFist()
    {
        Anim.SetBool("DoubleFist", true);
        yield return new WaitForSeconds(0.9f);
        Anim.SetBool("DoubleFist", false);
    }

  
    IEnumerator SideKick()
    {
        Anim.SetBool("SideKick", true);
        yield return new WaitForSeconds(0.9f);
        Anim.SetBool("SideKick", false);
    }

    IEnumerator mmaKick()
    {
        Anim.SetBool("MmaKick", true);
        yield return new WaitForSeconds(0.9f);
        Anim.SetBool("MmaKick", false);
    }



}

