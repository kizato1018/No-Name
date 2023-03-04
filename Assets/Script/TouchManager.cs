using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class TouchManager : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    // Start is called before the first frame update
    public Joystick joystick;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPointerDown(PointerEventData ped)
    {
        joystick.OnPointerDown(ped);
    }

    public void OnDrag(PointerEventData ped)
    {
        joystick.OnDrag(ped);
    }
    public void OnPointerUp(PointerEventData ped)
    {
        joystick.OnPointerUp(ped);
    }
}
