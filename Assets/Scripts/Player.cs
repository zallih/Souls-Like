using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Player")]
    public CharacterController player;
    public Animator anim;
    private Vector3 moveDirection;
    public float speed = 5f;

    [Header("Jump")]
    public float jumpForce;
    public float gravity = -19.62f;
    private Vector3 forceY;

    [Header("Camera")]
    public Transform camera;
    public float rotation;
    private float rotationSpeed;


    [Header("Detection")]
    public Transform detector;
    public LayerMask groundLayer;
    public bool isGrounded;
    public bool timeJump = false;


    [Header("Magic")]

    public GameObject[] magics;
    public Transform[] positionMagic;
    public bool isMagic = false;
    public int magicIndex;




    void Start() {

    }

    void FixedUpdate() {
        isGrounded = Physics.CheckSphere(detector.position, 0.3f, groundLayer);
        if (isGrounded && forceY.y < 0) {
            forceY.y = -2f; // Resetando a força Y quando está no chão
        }
    }

    void Update() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(horizontal, 0f, vertical);


        player.Move(forceY * Time.deltaTime);



        MovePlayer();
        Controller();
    }

    void MovePlayer() {

        if (moveDirection.magnitude >= 0.1f) {
            float vision = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angles = Mathf.SmoothDampAngle(transform.eulerAngles.y, vision, ref rotationSpeed, rotation);
            transform.rotation = Quaternion.Euler(0f, angles, 0f);
            Vector3 newDirection = Quaternion.Euler(0f, vision, 0f) * Vector3.forward;

            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float currentSpeed = isRunning ? speed * 2f : speed; // você pode ajustar o multiplicador

            player.Move(newDirection * currentSpeed * Time.deltaTime);

            anim.SetBool("isRunning", isRunning);
            anim.SetBool("isWalking", !isRunning);
        }
        else {
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
        }
    }


    public void TimeJump(bool isJump) {
        timeJump = isJump;
    }
    void Controller() {
        //Pulo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true && timeJump == false) {
            anim.SetBool("isJumping", true);
            forceY.y = MathF.Sqrt(jumpForce * -2 * gravity);

        }
        else {
            forceY.y += gravity * Time.deltaTime;
            anim.SetBool("isJumping", false);
            if (isGrounded == true && forceY.y < 0) {
                forceY.y = -1f;
            }
        }

        //Poderes

        if (Input.GetButtonDown("Fire1") && isMagic == false) {
            isMagic = true;
            anim.SetTrigger("Magic");
            // GameObject magic = Instantiate(magics[magicIndex], positionMagic.position, positionMagic.rotation);
            // magic.transform.parent = transform;
            anim.SetInteger("MagicIndex", magicIndex);
        }




    }

    public void Magic() {
        isMagic = true;
        GameObject magic = Instantiate(magics[magicIndex], positionMagic[magicIndex].position, positionMagic[magicIndex].rotation);
        Destroy(magic, 3f);

    }
    public void isMagicFalse() {
        isMagic = false;
    }
}


