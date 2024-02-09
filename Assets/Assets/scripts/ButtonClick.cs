using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DynamicToStaticGraphic : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _img;
    [SerializeField] private Sprite _on, _off;
    [SerializeField] private AudioClip _click;
    [SerializeField] private AudioSource _source;

    public void OnPointerDown(PointerEventData eventData) //zmienia sprite przycisnietego przycisku i wydaje dzwiek
    {
        _img.sprite = _on;
        _source.PlayOneShot(_click);
    }

    public void OnPointerUp(PointerEventData eventData) //zmienia sprite z powrotem
    {
        _img.sprite = _off;
    }
}
