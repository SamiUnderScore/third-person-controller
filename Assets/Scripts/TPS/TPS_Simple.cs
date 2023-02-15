using UnityEngine;
using UnityEngine.Serialization;

public class TPS_Simple : MonoBehaviour
{
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform cameraMan;
    
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
}
