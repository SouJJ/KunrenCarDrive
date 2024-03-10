using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    private GameObject mainCamera;
    private GameObject subCamera;
    public bool mainCameraActive;

    public float currentSpeed;
    public float nomalSpeed;
    public float dashSpeed;
    public float gravity;
    public float jumpHeight;
    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    private void Start()
    {
        mainCamera = GameObject.Find("MainCamera");
        subCamera = GameObject.Find("SubCamera");
        mainCameraActive = true;
        subCamera.SetActive(false);

        nomalSpeed = 10.0f;
        dashSpeed = 4.0f;
        currentSpeed = nomalSpeed;
        gravity = -9.18f;
        jumpHeight = 3f;
        groundDistance = 0.4f;
    }

    void Update()
    {
        //Pキーでカメラ切り替え
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (mainCameraActive)
            {
                //サブカメラをアクティブに設定
                mainCamera.SetActive(false);
                subCamera.SetActive(true);
                mainCameraActive = false;
            }
            else
            {
                //メインカメラをアクティブに設定
                subCamera.SetActive(false);
                mainCamera.SetActive(true);
                mainCameraActive = true;
            }            
        }


        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //Debug.Log(isGrounded);  //ずっとfalse

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKey(KeyCode.LeftShift)) // && isGrounded
        {
            currentSpeed = nomalSpeed * dashSpeed;
        }
        else
        {
            currentSpeed = nomalSpeed;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * currentSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Space)) // && isGrounded
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}