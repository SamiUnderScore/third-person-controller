using System;
using UnityEngine;
using UnityEngine.UI;

public class SprintChecker : MonoBehaviour
{
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private SprintHoverTarget sprintHoverTarget;
    [SerializeField] private Button sprintButton;

    public static event Action<bool> SetSprintStatus;
    public static bool sprintStatus;
    private void OnEnable()
    {
        VariableJoystick.OnPointerDownCallback += JoystickPressedCallback;
        VariableJoystick.OnPointerUpCallback += JoystickLeftCallback;
        SprintHoverTarget.TargetEntered += SprintTargetEntered;
        SprintHoverTarget.TargetExited += SprintTargetExited;
    }

    private void OnDisable()
    {
        VariableJoystick.OnPointerDownCallback -= JoystickPressedCallback;
        VariableJoystick.OnPointerUpCallback -= JoystickLeftCallback;
        SprintHoverTarget.TargetEntered -= SprintTargetEntered;
        SprintHoverTarget.TargetExited -= SprintTargetExited;
    }

    void SprintTargetExited()
    {
        sprintStatus = false;
        SetSprintStatus?.Invoke(sprintStatus);
    }

    void SprintTargetEntered()
    {
        if (joystick.isPressing)
        {
            sprintStatus = true;
            SetSprintStatus?.Invoke(sprintStatus);
        }
    }

    void JoystickPressedCallback()
    {
        sprintHoverTarget.StopSprinting();
    }

    void JoystickLeftCallback()
    {
        
    }

    public static bool GetSprintStatus()
    {
        return sprintStatus;
    }

}
