using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DisplayInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject InfoCard;
    private GameObject CurrentInfoCard;
    private RectTransform InfoCardRect;

    void Start()
    {
        InfoCardRect = InfoCard.GetComponent<RectTransform>();
    }
    void Update() //karta info podaza za kursorem
    {
        if (CurrentInfoCard != null)
        {
            Vector2 mp = Input.mousePosition;
            mp += new Vector2(InfoCardRect.rect.width / 2 + 10, 0); //karta inforamacji musi byæ poza kursorem, inaczej miga
            CurrentInfoCard.transform.position = mp;
        }
    }
    public void OnPointerEnter(PointerEventData eventData) //wykonuje siê gdy nakieruje siê kursor na token savea
    {
        if (CurrentInfoCard == null)
        {
            Vector2 spawnPosition = Input.mousePosition;
            spawnPosition += new Vector2(InfoCardRect.rect.width/2 + 10, 0);
            // Create Image GameObject
            CurrentInfoCard = Instantiate(InfoCard, spawnPosition, Quaternion.identity, gameObject.transform);
            //Image imageComponent = CurrentInfoCard.GetComponent<Image>();
            TextMeshProUGUI t = CurrentInfoCard.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>(); 
            t.text = "BLANK"; //zast¹piæ wczytywanymi informacjami o zapisie gry
        }
    }

    public void OnPointerExit(PointerEventData eventData) //wykonuje siê gdy zdejmie siê kursor z tokenu savea
    {  
        if (CurrentInfoCard != null)
            Destroy(CurrentInfoCard);
    }
}
