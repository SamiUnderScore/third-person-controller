using UnityEngine;
using UnityEngine.Serialization;

public class TPS_Gravity : MonoBehaviour
{
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform cameraMan;
    
    [SerializeField] private Transform groundChecker;
    [SerializeField] private float gravity = 9.81f; // The strength of the fake gravity force
    [SerializeField] private float groundCheckDistance = 0.1f; // The distance to check for ground using a SphereCast
    [SerializeField] private LayerMask groundMask; // The layer mask for ground objects
    private Vector3 _velocity;
    
    private Vector3 _moveDirection;

    void FixedUpdate()
    {
        if(joystick.Direction.magnitude > 0.1f)
        {
            ProcessJoystickInput();
        }
    }

    private void ProcessJoystickInput()
    {
        //Calculates the direction
        CalculateDireciton();

        //Moves the Character Controller
        MovePlayer();

        //Rotates the player according to the input rotation
        transform.rotation = Quaternion.LookRotation(_moveDirection, Vector3.up);
        
        print($"grounded: {CheckGround()}");
        if (!CheckGround())
        {
            controller.Move(_velocity * Time.fixedDeltaTime);
        }
    }

    void MovePlayer()
    {
        controller.Move(moveSpeed * _moveDirection * Time.fixedDeltaTime);
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

}
