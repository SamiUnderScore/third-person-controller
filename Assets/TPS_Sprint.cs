using UnityEngine;
using UnityEngine.Serialization;

public class TPS_Sprint : MonoBehaviour
{
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float walkSpeed, sprintSpeed;
    [SerializeField] private Transform cameraMan;
    
    private float _playerSpeed;
    [SerializeField] private bool sprinting;


    
    
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
            if (_playerSpeed < walkSpeed)
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
}
