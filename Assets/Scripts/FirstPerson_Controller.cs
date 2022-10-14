using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPerson_Controller : MonoBehaviour
{
    // u are so good
    CharacterController controller;
    public Transform fpsCamera;

    public float sensetivity = 200f;
    public float speed = 15f;
    float xRotation = 0f;

    // Jump funciones
    private bool isGrounded;
    public Transform groundSensor;
    public LayerMask ground;
    public float sensorRadius = 0.1f;
    private Vector3 playerVelocity;
    public float gravity = -9.81f;
    public float jumpForce = 20f;
    public float jumpHeight = 1f;
   

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensetivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensetivity * Time.deltaTime;

        xRotation -= mouseY;
        /*con esto le sumamos a una variable independiente todo el rato el valor de mouseY, por lo que nunca llegará a ser 0 y la camara no se bugueará, 
        porque mousee y vuelve a ser 0 al dejar de mover el raton. Al hacer -= hacemos que se invierta el movimiento.

        */
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        fpsCamera.localRotation = Quaternion.Euler(xRotation, 0, 0);

        transform.Rotate(Vector3.up * mouseX);

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move.normalized * speed * Time.deltaTime);

        Jump();

    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(groundSensor.position, sensorRadius, ground);
        if(isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;

        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            //playerVelocity.y += jumpForce;
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
