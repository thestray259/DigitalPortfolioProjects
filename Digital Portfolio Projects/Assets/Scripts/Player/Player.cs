using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] Animator animator;
    [SerializeField] Transform view;
    [SerializeField] Transform lookAt; 
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float turnRate;
    [SerializeField] float attackDistance;
    [SerializeField] float damage; 
    [SerializeField] ForceMode forceMode;
    [SerializeField] string tagName;
    
    Rigidbody rb;
    Vector3 force = Vector3.zero; 
    [SerializeField] Vector3 velocity = Vector3.zero;
    bool isGrounded = false; 
    float airTime = 0;
    float distToGround = 0.5f;
    public bool canFollow = true;

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
        Vector3 forward = view.forward;
        forward.y = 0; 

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
                transform.position += speed * Time.deltaTime * -view.right;
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                // move backwards 
                transform.position += speed * Time.deltaTime * -forward;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                // move right 
                transform.position += speed * Time.deltaTime * view.right;
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
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackDistance);

        if (Input.GetKeyDown(KeyCode.Mouse0)) // left click
        {
            // primary / regular attack 
            Debug.Log("Player Primary Attack");
            // play attack animation 
            // can't interupt animation with other attacks, but can with sprint/dodge 
            animator.SetTrigger("punch"); 

            foreach (Collider collider in colliders)
            {
                if (collider.gameObject == gameObject) continue;

                if (tagName == "" || collider.CompareTag(tagName))
                {
                    if (collider.gameObject.TryGetComponent<GenEnemyBT>(out GenEnemyBT genEnemyBT))
                    {
                        genEnemyBT.gameObject.GetComponent<Health>().health -= damage; 
                    }
                }
            }
        }
    }
}
