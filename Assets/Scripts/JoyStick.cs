using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class JoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

    private Image bgJSImage;
    private Image JSImage;
    public Vector3 InputVectors { set; get; }

    void Start()
    {
        bgJSImage = GetComponent<Image>();
        JSImage = transform.GetChild(0).GetComponent<Image>();
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgJSImage.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / bgJSImage.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgJSImage.rectTransform.sizeDelta.y);

            //float x = (bgJSImage.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
            //float y = (bgJSImage.rectTransform.pivot.x == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;

            InputVectors = new Vector3(pos.x * 2 + 1, 0, pos.y * 2 - 1);
            InputVectors = (InputVectors.magnitude > 1.0f) ? InputVectors.normalized : InputVectors;

            JSImage.rectTransform.anchoredPosition = new Vector3(InputVectors.x *
                (bgJSImage.rectTransform.sizeDelta.x / 3),
                InputVectors.z * (bgJSImage.rectTransform.sizeDelta.y / 3));
        }
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        InputVectors = Vector3.zero;
        JSImage.rectTransform.anchoredPosition = Vector3.zero;
    }

    public float Horizontal()
    {
        if (InputVectors.x != 0)
        {
            return InputVectors.x;
        }
        else
        {
            return Input.GetAxis("Horizontal");
        }
    }
    public float Vertical()
    {
        if (InputVectors.z != 0)
        {
            return InputVectors.z;
        }
        else
        {
            return Input.GetAxis("Vertical");
        }
    }
}