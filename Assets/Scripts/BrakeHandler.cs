using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BrakeHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    bool brakePressed;
    // Start is called before the first frame update
    void Start()
    {
        brakePressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        brakePressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        brakePressed = false;
    }

    public bool BrakePressed()
    {
        return brakePressed;
    }
}
