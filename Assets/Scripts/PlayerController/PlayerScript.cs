using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerScript : MonoBehaviour
{

    [Header("Player health and Energy")]
    private float playerhealth = 200f;
    public float presentHealth;
    [Header("Player Movement")]
    public float MovementSpeed = 50f;
    public float rotSpeed = 300f;
    public MainCameraScripts MCS;
    Quaternion requiredRotation;
    [Header("Player Animator")]
    public Animator animator;
    [Header("Player Collision & Gravity")]
    public CharacterController CC;
    public float SurfaceCheckRedius = 0.1f;
    public Vector3 SurfaceCheckOffSet;
    public LayerMask SurfaceLayer;
    bool onSurface;
   [SerializeField] float fallingSpeed;
   [SerializeField] Vector3 moveDir;

    private void Awake()
    {
        presentHealth = playerhealth;
    }

    private void Update()
    {    
        if (onSurface)
        {
            fallingSpeed = -0.5f;
        }
        else
        {
            fallingSpeed += Physics.gravity.y * Time.deltaTime;
        }
        var velocity = moveDir * MovementSpeed;
      PlayerMovement();
        SurfaceCheck();
        Debug.Log("Player on surface: "+ onSurface);
    }
    void PlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        float movementAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(Vertical));

        var MovementInput = (new Vector3(horizontal, 0, Vertical)).normalized;
        var MovementDirection = MCS.flatRotation * MovementInput;

     
        var movement = MovementDirection * MovementSpeed * Time.deltaTime;
        movement.y = fallingSpeed * Time.deltaTime; 

        CC.Move(movement); 

        if (movementAmount > 0)
        {
            requiredRotation = Quaternion.LookRotation(MovementDirection);
        }

        moveDir = MovementDirection;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, requiredRotation, rotSpeed * Time.deltaTime);

       animator.SetFloat("movementValue", movementAmount);
    }
    void SurfaceCheck()
    {
        onSurface = Physics.CheckSphere(transform.TransformPoint(SurfaceCheckOffSet), SurfaceCheckRedius, SurfaceLayer);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.TransformPoint(SurfaceCheckOffSet), SurfaceCheckRedius);
    }

    public void playerDamage(float takeDamage)
    {
        presentHealth -= takeDamage;

        if(presentHealth <=0)
        {
            PlayerDie();
        }
    }

    private void PlayerDie()
    {
        Cursor.lockState = CursorLockMode.None;
        Object.Destroy(gameObject, 1.0f);

    }

}
