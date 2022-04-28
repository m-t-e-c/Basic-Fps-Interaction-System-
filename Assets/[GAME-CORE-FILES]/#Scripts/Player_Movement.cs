using System;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class Player_Movement : Character
{
    Vector3 playerVelocity;

    [SerializeField]
    private float m_WalkSpeed = 2f;

    [SerializeField]
    private float m_SideSpeed = 2f;

    [SerializeField]
    private float m_BackSpeed = 2f;

    [SerializeField]
    private float m_RunSpeed = 2f;

    [SerializeField]
    private float m_JumpForce = 2f;

    [SerializeField]
    private bool m_Running;

    private Vector3 forwardSpeed;
    private Vector3 sideSpeed;

    private float directionY;

    private void Update()
    {
        Movement();
    }
    private void Movement()
    {
        m_Running = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_CharacterController.isGrounded)
                directionY = m_JumpForce / 100;
        }

        sideSpeed = transform.right * inputManager.m_Horizontal * m_SideSpeed;

        if (inputManager.m_Vertical > 0.0f)
        {
            forwardSpeed = m_Running ?
                transform.forward * inputManager.m_Vertical * m_RunSpeed
                : transform.forward * inputManager.m_Vertical * m_WalkSpeed;
        }
        else
            forwardSpeed = transform.forward * inputManager.m_Vertical * m_BackSpeed;

        playerVelocity = forwardSpeed + sideSpeed;
        playerVelocity.y = directionY;

        m_CharacterController.Move(playerVelocity * Time.deltaTime);
        directionY -= m_GravityAmount * Time.deltaTime;
    }
}
