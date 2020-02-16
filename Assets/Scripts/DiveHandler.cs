using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiveHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool divePressed;
    // Start is called before the first frame update
    void Start()
    {
        divePressed = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        divePressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        divePressed = false;
    }

    public bool DivePressed()
    {
        return divePressed;
    }


}
