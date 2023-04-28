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
    Vector3 force = Vector3.zero; 
    [SerializeField] Vector3 velocity = Vector3.zero;
    bool isGrounded = false; 
    float airTime = 0;
    float distToGround = 0.5f;
    public bool canFollow = true;
    private static int _enemyLayerMask = 1 << 6;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        view = (view == null) ? Camera.main.transform : view;
    }

    void Update()
    {
        // xz movement
        Vector3 direction = Vector3.zero;
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");
        direction = Vector3.ClampMagnitude(direction, 1);
        
        // convert direction from world space to view space
        Quaternion viewSpace = Quaternion.AngleAxis(view.rotation.eulerAngles.y, Vector3.up);
        direction = viewSpace * direction;

        // move character (xyz)
        Move(view);
        OnJump();
        OnAttack(); 

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (canFollow == true) canFollow = false;
            else canFollow = true; 
        } // companion follow

        // face direction (needs fixing) - works when iskinematic = false 
        if (direction.magnitude > 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), turnRate * Time.deltaTime);
        }

        // set animation stuff
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("speed", (direction * speed).magnitude); 
        if (GetComponent<Health>().isDead == true) animator.SetTrigger("dead"); 
    }

    private void FixedUpdate()
    {
        rb.AddForce(force, forceMode);
        GroundCheck(); 
    }

    private void Move(Transform view)
    {
        Vector3 forward = view.transform.forward;
        Vector3 right = view.transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // check if punch anim is playing, if not then can move, if so then can't move 
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                // move forward
                transform.position += speed * Time.deltaTime * forward;
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                // move left 
                transform.position += speed * Time.deltaTime * -right;
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                // move backwards 
                transform.position += speed * Time.deltaTime * -forward;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                // move right 
                transform.position += speed * Time.deltaTime * right;
            }
        }

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

        if (Input.GetKeyDown(KeyCode.Mouse0)) // left click
        {
            // primary / regular attack 
            Debug.Log("Player Primary Attack");
            // play attack animation 
            // can't interupt animation with other attacks, but can with sprint/dodge 
            // need to freeze rotation during punch
            animator.SetTrigger("punch"); 

            if (colliders.Length > 0)
            {
                foreach (Collider collider in colliders)
                {
                    if (collider.gameObject == gameObject) continue;

                    if (collider.CompareTag(tagName))
                    {
                        if (collider.gameObject.TryGetComponent<GenEnemyBT>(out GenEnemyBT genEnemyBT))
                        {
                            genEnemyBT.gameObject.GetComponent<Health>().Damage(damage);
                        }
                    }
                }
            }
        }
    }
}
