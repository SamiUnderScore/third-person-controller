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
        gameObject.GetComponent<Image>().color = activeColor;
        TargetEntered?.Invoke();
    }

    public void StopSprinting()
    {
        gameObject.GetComponent<Image>().color = nonActiveColor;
        TargetExited?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        print("recheckNextTime");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
