using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static Collection;
using TMPro;

public class CardPicker : MonoBehaviour
{
    [SerializeField] GameObject card1;
    [SerializeField] GameObject card2;
    [SerializeField] int sceneId;
    private Animator anim;
    private int choice = 1;
    private void Start() //zgarnia animator z PickCardBG
    {
        anim = gameObject.GetComponent<Animator>();
    }
    public void SetCardSprites(GameObject obj, Collection.Card crd) //wo³aæ te funkcje ¿eby przypisaæ odpowiednie sprity do kard do wyboru
    {
        obj.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = crd.graphic;
        obj.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = crd.name;
        obj.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = crd.desc;
        obj.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = crd.mana.ToString();
        obj.transform.GetChild(5).GetComponent<CardIdHolderPasser>().holdCard(crd);
    }
    public void playanim() //odpala animacje, te funkje wo³aæ po zabiciu przeciwnika (+ dodaæ jaka blokade zeby reszta ekranu nie dzia³a³a)
    {
        int first = Random.Range(0, Collection.CardSet.Count);
        int second = Random.Range(0, Collection.CardSet.Count);
        while(second == first)
        {
            second = Random.Range(0, Collection.CardSet.Count);
        }
        SetCardSprites(card1, Collection.CardSet[first]);
        SetCardSprites(card2, Collection.CardSet[second]);
        anim.SetTrigger("trigger");
    }
    public void pick(int a) //przypisuje wybór
    {
        choice = a;
    }
    public void ConfirmChoice() //wraca do mapy i dodaje karte do talii
    {
        if(MapEncounters.encType[0] !=11)
        {
            if (choice != 0)
            {
                Deck.Add(choice == 1 ? card1.transform.GetChild(5).GetComponent<CardIdHolderPasser>().card : card2.transform.GetChild(5).GetComponent<CardIdHolderPasser>().card);
                SceneManager.LoadScene(sceneId);
            }
        }
        else
        {
            SceneManager.LoadScene(sceneId);
        }

    }
}
