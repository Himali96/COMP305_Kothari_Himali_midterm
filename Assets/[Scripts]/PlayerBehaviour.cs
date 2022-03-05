using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Movement")]
    public float horizontalForce;
    public float verticalForce;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundRadius, run;
    public LayerMask groundLayerMask;
    public bool isGrounded, isCrouching;

    [Header("Animation Properties")]
    public Animator animator;

    [Header("Cameras")]
    public CinemachineVirtualCamera playerCamera;

    [Header("UI and Interactables")]
    public bool isInteracting, isTressureOpened;

    [Header("Star")]
    public GameObject star;
    public Transform starSpawnPoint, starParent;

    [Header("Tressure")]
    public GameObject tressure;
    public Sprite chest;

    StarSpawnHandler ssh;

    private Rigidbody2D rigidBody2D;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ssh = StarSpawnHandler._instatnce;
    }

    void Update()
    {
        Interact();
    }

    void FixedUpdate()
    {
        Move();

        if(isTressureOpened)
        {
            ssh.SpawnFromPool ("STAR", starSpawnPoint.position, Quaternion.identity);
        }
    }

    private void Interact()
    {
        isInteracting = Input.GetKey(KeyCode.E);
    }

    private void Move()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayerMask);

        run = Input.GetAxisRaw("Horizontal");
        
        isCrouching = Input.GetKey(KeyCode.S);

        if (!isCrouching) rigidBody2D.velocity = new Vector2(run * horizontalForce, rigidBody2D.velocity.y);

        if (isGrounded)
        {
            float jump = Input.GetAxisRaw("Jump");
            float crouch = Input.GetAxisRaw("Crouch");

            if (run != 0 && crouch == 0)
            {
                run = Flip(run);
                animator.SetInteger("AnimationState", 1);
            }
            else if (run == 0 && jump == 0)
            {
                animator.SetInteger("AnimationState", 0);
            }

            if (jump > 0 && crouch == 0)
            {
                rigidBody2D.velocity = new Vector2(run * rigidBody2D.velocity.x, verticalForce);
                animator.SetInteger("AnimationState", 2);
            }
            else if (run == 0 && crouch < 0)
            {
                animator.SetInteger("AnimationState", 3);
            }
        }
    }

    private float Flip(float x)
    {
        x = (x > 0) ? 1 : -1;

        transform.localScale = new Vector3(x, 1.0f);
        return x;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (isInteracting && other.gameObject.name == "chest")
        {
            isTressureOpened = true;
            tressure.GetComponent<SpriteRenderer>().sprite = chest;
        }
    }

}
