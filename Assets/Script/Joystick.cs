using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    // Init
    private Image jsContainer;
    private Image joystick;
    private Color jsContainer_color;
    private Color joystick_color;
    public Vector2 InputDirection = Vector2.zero;
    private IEnumerator fadeout;

    void Start()
    {
        // Get the Component we attach this script (JoystickContainer)
        jsContainer = GetComponent<Image>();
        jsContainer_color = jsContainer.color;

        // Get the only one child component (Joystick)
        joystick = transform.GetChild(0).GetComponent<Image>();
        joystick_color = joystick.color;


    }

    public void OnDrag(PointerEventData ped)
    {
        Vector2 position = Vector2.zero;

        // Get InputDirection
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            jsContainer.rectTransform,
            ped.position,
            ped.pressEventCamera,
            out position
        );

        float x = (position.x / jsContainer.rectTransform.sizeDelta.x);
        float y = (position.y / jsContainer.rectTransform.sizeDelta.y);

        InputDirection = new Vector2(x, y);
        InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;

        // Define the area in which joystick can move around
        joystick.rectTransform.anchoredPosition = new Vector2(
            InputDirection.x * jsContainer.rectTransform.sizeDelta.x / 3,
            InputDirection.y * jsContainer.rectTransform.sizeDelta.y / 3
        );
    }

    public void OnPointerDown(PointerEventData ped)
    {
        gameObject.transform.position = ped.position;
        if(fadeout != null)
            StopCoroutine(fadeout);
        jsContainer.color = jsContainer_color;
        joystick.color = joystick_color;
        OnDrag(ped);
    }

    public void OnPointerUp(PointerEventData ped)
    {
        // If mouse release, the InputDirection variable have to return to Vector3(0.0, 0.0, 0.0);
        InputDirection = Vector3.zero;
        joystick.rectTransform.anchoredPosition = Vector3.zero;
        fadeout = Fadeout(1.0f);
        StartCoroutine(fadeout);
    }

    IEnumerator Fadeout(float duration)
    {
        yield return new WaitForSeconds(2.0f);
        Color jsContainer_fade_speed = Color.black * jsContainer_color.a / duration;
        Color joystick_fade_speed  = Color.black * joystick_color.a / duration;
        while(jsContainer.color.a > 0 || joystick.color.a > 0)
        {
            jsContainer.color -= jsContainer_fade_speed * Time.deltaTime;
            joystick.color -= joystick_fade_speed * Time.deltaTime;
            if(jsContainer.color.a < 0)
                jsContainer.color = Color.clear;
            if(joystick.color.a < 0)
                joystick.color = Color.clear;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}