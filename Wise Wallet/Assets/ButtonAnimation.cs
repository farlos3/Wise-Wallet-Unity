using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler

{
    [SerializeField] private Image _img;
    [SerializeField]private Sprite _default, _pressed;

    private Animator animator; // Reference to the Animator

    public void OnPointerDown(PointerEventData eventData)
    {
        _img.sprite = _pressed;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _img.sprite = _default;
    }

}
