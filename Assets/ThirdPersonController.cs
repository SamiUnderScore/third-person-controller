using UnityEngine;
using UnityEngine.EventSystems;

public class ThirdPersonController : MonoBehaviour
{
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float moveSpeed, rotateSpeed;
    [SerializeField] private Transform cameraMan;

    private Vector3 _moveDirection;

    void Update()
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
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(joystick.Direction.normalized.x, joystick.Direction.normalized.y) * Mathf.Rad2Deg, transform.eulerAngles.z);
        transform.rotation = Quaternion.LookRotation(_moveDirection, Vector3.up);
    }

    void MovePlayer()
    {
        controller.Move(moveSpeed * _moveDirection * Time.deltaTime);
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
