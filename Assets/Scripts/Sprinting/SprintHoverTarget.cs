using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SprintHoverTarget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    //just for demonstration
    [SerializeField] private Color activeColor, nonActiveColor;

    public static event Action TargetEntered, TargetExited;

    // only for testing in editor
    public bool canCheck;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            canCheck = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            canCheck = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (canCheck)
        {
            StartSprinting();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (canCheck)
        {
            StopSprinting();
        }
    }

    public void StartSprinting()
    {
        ActivateSprintTarget(true);
        TargetEntered?.Invoke();
    }

    public void StopSprinting()
    {
        ActivateSprintTarget(false);
        TargetExited?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        print("recheckNextTime");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void ActivateSprintTarget(bool status)
    {
        switch (status)
        {
            case false:
                gameObject.GetComponent<Image>().color = nonActiveColor;
                break;
            case true:
                gameObject.GetComponent<Image>().color = activeColor;
                break;
        }
        
    }

    
}
