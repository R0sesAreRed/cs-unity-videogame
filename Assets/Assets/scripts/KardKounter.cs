using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using TMPro;
using static Collection;

public class KardKounter : MonoBehaviour
{
    [SerializeField] GameObject card;
    [SerializeField] GameObject manabar;
    public static List<GameObject> hand = new List<GameObject>();
    public static List<Collection.Card> handcards = new List<Collection.Card>();
    private List<Collection.Card> currentDeck = Collection.Deck;
    private List<Collection.Card> drawdeck = new List<Collection.Card>();
    public float angleBetweenCards = 5;
    public int manaDiscoutBy = 0;
    public int discountto = 100;
    private int reduceCostTo(int i, GameObject a)
    {
        if(discountto == 100)
        {
            return 0;
        }
        else
        {
            return a.transform.GetChild(5).GetComponent<CardIdHolderPasser>().card.mana - i;
        } 
    }

    public void updateHand() //rodziela po równo karty w rêce
    {
        float angle = -1*((hand.Count - 1) * -(angleBetweenCards/2));
        for(int i = 0; i < hand.Count; i++)
        {        
            hand[i].transform.Rotate(0, 0, -1* hand[i].transform.localRotation.eulerAngles.z + angle);
            angle -= angleBetweenCards;
        }
    }

    public void drawCard()
    {

        if (drawdeck.Count == 0)
        {
            drawdeck.AddRange(currentDeck);
        }        
        if(hand.Count < 10) //max ilosc kart na rêce
        {
            Collection.Card crd = drawdeck[Random.Range(0, drawdeck.Count)];
            drawdeck.Remove(crd);
            GameObject obj = Instantiate(card, transform.position, Quaternion.identity, gameObject.transform);
            obj.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = crd.graphic;
            obj.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = crd.name;
            obj.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = crd.desc;
            obj.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = crd.mana.ToString();
            obj.transform.GetChild(5).GetComponent<CardIdHolderPasser>().holdCard(crd);
            handcards.Add(obj.transform.GetChild(5).GetComponent<CardIdHolderPasser>().card);
            hand.Add(obj);
        }
        updateHand();

    }

    public void playCard(GameObject a) 
    {

        if(manabar.GetComponent<manabar>().manaleft >= a.transform.GetChild(5).GetComponent<CardIdHolderPasser>().card.mana - manaDiscoutBy - reduceCostTo(discountto, a))
        {            
            manabar.GetComponent<manabar>().manaleft -= (a.transform.GetChild(5).GetComponent<CardIdHolderPasser>().card.mana - manaDiscoutBy - reduceCostTo(discountto, a)) > 0 ?
                (a.transform.GetChild(5).GetComponent<CardIdHolderPasser>().card.mana - manaDiscoutBy - reduceCostTo(discountto, a)) : 0; //kosz many
            manaDiscoutBy = 0;
            discountto = 100; //reset przecen many
            CardEffects.CardEffect(a.transform.GetChild(5).GetComponent<CardIdHolderPasser>().card.id, a);//efekt karty
            manabar.GetComponent<manabar>().UpdateManaBar(); //update paska many
            handcards.Remove(a.transform.GetChild(5).GetComponent<CardIdHolderPasser>().card); //usuniecie akrty z reki
            hand.Remove(a);
            foreach(GameObject card in hand)
            {
                Debug.Log(card);
            }
            Destroy(a); //zniszczenie obiektu w rece
            updateHand(); //odswiezenie reki

        }
        else
        {
            foreach (Transform child in a.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}
