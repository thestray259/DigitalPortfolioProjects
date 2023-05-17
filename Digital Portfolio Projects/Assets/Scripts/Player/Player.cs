using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform view;
    [SerializeField] Transform lookAt; 
    [SerializeField] float speed = 5;
    [SerializeField] float jumpForce = 5;
    [SerializeField] float turnRate = 100;
    [SerializeField] float attackDistance = 2;
    [SerializeField] float damage = 10; 
    [SerializeField] ForceMode forceMode;
    [SerializeField] string tagName = "Enemy";
    
    Rigidbody rb;
    bool isGrounded = false;
    bool isAttacking = false;
    readonly float distToGround = 0.5f;
    public bool canFollow = true;
    public bool isCalled = false;
    static readonly int _enemyLayerMask = 1 << 6;
    Vector3 attackPosition;
    Quaternion attackRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        view = (view == null) ? Camera.main.transform : view;
    }

    void Update()
    {
        {
            // xz movement
            Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            direction = Vector3.ClampMagnitude(direction, 1);
        
            // convert direction from world space to view space
            Quaternion viewSpace = Quaternion.AngleAxis(view.rotation.eulerAngles.y, Vector3.up);
            direction = viewSpace * direction;

            // face direction
            if (direction.magnitude > 0) transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), turnRate * Time.deltaTime);
            // if punching, don't move
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Punch") && !animator.IsInTransition(0)) transform.SetPositionAndRotation(attackPosition, attackRotation);
            else
            {
                if ((direction * speed).magnitude > 0 && (Input.GetMouseButton(1) || Input.GetKey(KeyCode.LeftShift)))
                {
                    animator.SetBool("isRunning", true);
                    direction = (speed * 2f) * Time.deltaTime * direction.normalized;
                    transform.position += direction;
                }
                else
                {
                    animator.SetBool("isRunning", false);
                    direction = speed * Time.deltaTime * direction.normalized;
                    transform.position += direction;
                }
            }
            animator.SetFloat("speed", (direction * speed).magnitude);
        } // move character (xyz)

        // if attack anim is done playing, then set isAttacking to false
        //if ()
                

        GroundCheck();
        OnJump();
        OnAttack();

        if (Input.GetKeyDown(KeyCode.C))
        {
            isCalled = true;
        } // call companion to player
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (canFollow == true) canFollow = false;
            else canFollow = true; 
        } // companion follow

        // set animation stuff
        animator.SetBool("isGrounded", isGrounded);        
        if (GetComponent<Health>().isDead == true) animator.SetTrigger("dead"); 
    }

    private void GroundCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f))
        {
            Debug.Log("Grounded");
            isGrounded = true; 
        }
        else
        {
            Debug.Log("Not Grounded");
            isGrounded = false; 
        }
    }

    private void OnJump() 
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jumping");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("jump"); 
        } 
    }

    private void OnAttack()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackDistance, _enemyLayerMask);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // primary / regular attack
            // play attack animation 
            // can't interupt animation with other attacks, but can with sprint/dodge
            animator.SetTrigger("punch");
            isAttacking = true;
            attackPosition = transform.position;
            attackRotation = transform.rotation;

            if (colliders.Length > 0)
            {
                //foreach (Collider collider in colliders)
                {
                    //if (colliders[0].gameObject == gameObject) continue;

                    if (colliders[0].CompareTag(tagName))
                    {
                        if (colliders[0].gameObject.TryGetComponent(out GenEnemyBT genEnemyBT))
                        {
                            genEnemyBT.gameObject.GetComponent<Health>().Damage(damage);
                        }
                    }
                }
            }
        } // left click
    }
}
