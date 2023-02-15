using System;
using UnityEngine;
using UnityEngine.Serialization;

public class TPS_Sprint_Gravity : MonoBehaviour
{
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float walkSpeed, sprintSpeed;
    [SerializeField] private Transform cameraMan;
    
    [SerializeField] private bool sprinting;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private float gravity = 9.81f; // The strength of the fake gravity force
    [SerializeField] private float groundCheckDistance = 0.1f; // The distance to check for ground using a SphereCast
    [SerializeField] private LayerMask groundMask; // The layer mask for ground objects
    private Vector3 _velocity;
        
    private float _playerSpeed;
    private Vector3 _moveDirection;

    private void OnEnable()
    {
        SprintChecker.SetSprintStatus += SetSprintStatus;
    }

    private void OnDisable()
    {
        SprintChecker.SetSprintStatus -= SetSprintStatus;
    }

    void FixedUpdate()
    {
        if(joystick.Direction.magnitude > 0.1f)
        {
            ProcessJoystickInput();
        }
        
        print($"grounded: {CheckGround()}");
        if (!CheckGround())
        {
            controller.Move(_velocity * Time.fixedDeltaTime);
        }
        
    }
    
    
    bool CheckGround()
    {
        bool isGrounded = Physics.CheckSphere(groundChecker.position, groundCheckDistance, groundMask);
        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
    
        _velocity.y += -gravity * Time.fixedDeltaTime;
        return isGrounded;
    }


    private void ProcessJoystickInput()
    {
        //Calculates the direction
        CalculateDireciton();

        //Moves the Character Controller
        MovePlayer();

        //Rotates the player according to the input rotation
        transform.rotation = Quaternion.LookRotation(_moveDirection, Vector3.up);
    }

    void MovePlayer()
    {
        if (sprinting)
        {
            if (_playerSpeed < sprintSpeed)
                _playerSpeed = Mathf.MoveTowards(_playerSpeed, sprintSpeed, sprintSpeed * Time.fixedDeltaTime);
        
            controller.Move(_playerSpeed * _moveDirection * Time.fixedDeltaTime);
        }
        else
        {
            if (_playerSpeed != walkSpeed)
                _playerSpeed = Mathf.MoveTowards(_playerSpeed, walkSpeed, walkSpeed * Time.fixedDeltaTime);

            controller.Move(_playerSpeed * _moveDirection * Time.fixedDeltaTime);
        }
    }

    void CalculateDireciton()
    {
        Vector3 cameraForward = cameraMan.forward;
        Vector3 cameraRight = cameraMan.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        //Calculates the direction
        _moveDirection = (cameraForward * joystick.Vertical + cameraRight * joystick.Horizontal);
    }

    void SetSprintStatus(bool canSprint)
    {
        sprinting = canSprint;
    }
}
