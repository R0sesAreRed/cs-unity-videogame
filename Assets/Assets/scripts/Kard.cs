using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class Kard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] GameObject CardDisplay;
    private GameObject copiedCard;
    private GameObject PickedUpCard;
    private RectTransform PickedUpRect;
    [SerializeField] GameObject HandField;
    [SerializeField] GameObject CastSelfField;
    [SerializeField] GameObject CastEnemyField;
    [SerializeField] GameObject CastEnemyField21;
    [SerializeField] GameObject CastEnemyField22;
    [SerializeField] GameObject CastBossField;
    private RectTransform HandFieldRect;
    void Awake() //wykonuje siê przy powstaniu obiektu, czyli gdy gracz dobiera karte
    {
        HandFieldRect = HandField.GetComponent<RectTransform>();
    }
    void Update() //wykonuje siê co update, nie zaœmiecaæ tej fuknji bo bedzie lagowaæ
    {
        if(PickedUpCard != null)
        {
            Vector2 mp = Input.mousePosition;
            PickedUpRect.transform.position = mp;          
        }
    }
    public void OnPointerDown(PointerEventData eventData) //lewy: podnosi karte, prawy: przykl¹da siê karcie
    {
        if (eventData.button == PointerEventData.InputButton.Left) 
        {
            PickedUpCard = Instantiate(gameObject, new Vector2(0, 0), Quaternion.identity, gameObject.transform.parent.transform);
            PickedUpRect = PickedUpCard.GetComponent<RectTransform>();
            PickedUpRect.pivot = new Vector2(0.5f, 0.5f);
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
            if (CardDisplay.activeSelf)
            {
                CardDisplay.SetActive(false);
            }
        }
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            copiedCard = Instantiate(gameObject, CardDisplay.transform.position, CardDisplay.transform.rotation, gameObject.transform.parent.transform);;
            copiedCard.transform.localScale = CardDisplay.transform.localScale*1.3f;
            copiedCard.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            foreach (Transform child in transform)
            {
                    child.gameObject.SetActive(false);
            }
        }
    }
    public void OnPointerUp(PointerEventData eventData) //lewy: zagrywa karte jeœli wyci¹gnie siê j¹ z rêki, prawy: chowa przygl¹danie siê karcie
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if(!CardNotPlayed())
            {
                GetComponentInParent<KardKounter>().playCard(gameObject);
            }
            else
            {
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(true);
                }
            }
            Destroy(PickedUpCard);
        }
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
            Destroy(copiedCard);
        }

    }
    private bool CardNotPlayed()
    {
        if (HandFieldRect != null)
        {
            Vector2 mousePosition = Input.mousePosition;
            return RectTransformUtility.RectangleContainsScreenPoint(HandFieldRect, mousePosition, null);
        }
        return false;
    }
    public GameObject CastTarget() 
    {
        Vector2 mousePosition = Input.mousePosition;
        if (RectTransformUtility.RectangleContainsScreenPoint(CastSelfField.GetComponent<RectTransform>(), mousePosition, null))
        {
            return CastSelfField;
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(CastEnemyField.GetComponent<RectTransform>(), mousePosition, null))
        {
            return CastEnemyField;
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(CastEnemyField21.GetComponent<RectTransform>(), mousePosition, null))
        {
            return CastEnemyField21;
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(CastEnemyField22.GetComponent<RectTransform>(), mousePosition, null))
        {
            return CastEnemyField22;
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(CastBossField.GetComponent<RectTransform>(), mousePosition, null))
        {
            return CastBossField;
        }
        return null;
    }

}
