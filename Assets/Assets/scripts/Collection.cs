using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Collection : MonoBehaviour
{
    [SerializeField] Sprite errorcardsprite;
    [SerializeField] GameObject[] cardtemplates;
    public Sprite[] CardGraphic;
    public static int page = 0;
    public static int setlength = 0;
    public class Card
    {
        public Card(int id, string name, string desc, Sprite graphic, int cardset, int mana, bool unlocked)
        {
            this.id = id; //numer karty, po tym robiæ efekty i takie tam
            this.name = name; // nazwa karty
            this.desc = desc; // opis karty
            this.graphic = graphic; //grafika na karcie
            cardclass = cardset; //klasa postaci do ktorej przypisana jest karta: 0 - rogue, 1 - fighter, 2 - mage, 3 - warlock, 4 - archer, 5 - survivalist, 6 - meele(rogue + fighter), 7 - magic(mage + warlock), 8 - ranged(archer + survivalist)
            this.mana = mana; //koszt zagraina karty
            this.unlocked = unlocked; //czy karta zosta³a odblokowana
        }

        public int id;
        public string name;
        public string desc;
        public Sprite graphic;
        public int cardclass;
        public int mana;
        public bool unlocked;

        public override string ToString() => $"({id}, {name}, {desc}, {cardclass}, {mana}, {unlocked})";

    }

    public List<Card> Col = new List<Card>(); //lista wszystkich akrt w grze
    public static List<Card> CardSet = new List<Card>(); //lista kart dostenych dla wybranej klasy

    [SerializeField] public GameObject[] deckobject;
    public static List<Card> Deck = new List<Card>();

    [SerializeField] UnityEngine.UI.Button b;
    void Start()
    {
        Col.Add(new Card(0, "Swift strike", "Deals 8 damage", test1.CardGraphicStatic[0] != null ? test1.CardGraphicStatic[0] : errorcardsprite, 0, 1, true));
        Col.Add(new Card(1, "Evasive Maneuver", "Deals 7 damage and has 50% chance draw one card", test1.CardGraphicStatic[1] != null ? test1.CardGraphicStatic[1] : errorcardsprite, 0, 2, true));
        Col.Add(new Card(2, "Cloak and Dagger", " Deals 4 damage. Restores 2 health and provides 4 armor", test1.CardGraphicStatic[2] != null ? test1.CardGraphicStatic[2] : errorcardsprite, 0, 3, true));
        Col.Add(new Card(3, "Cheap Shot", " Deals 5 damage and has a 30% chance to disable enemie's attack", test1.CardGraphicStatic[3] != null ? test1.CardGraphicStatic[3] : errorcardsprite, 0, 4, true));
        Col.Add(new Card(4, "Efficient Slice", "Ignores enemy armor and deals 16 damage", test1.CardGraphicStatic[4] != null ? test1.CardGraphicStatic[4] : errorcardsprite, 0, 5, true));

        //fighter
        Col.Add(new Card(5, "Brutal strike", "Deals 13 damage, has 10% chance to stun the enemy", test1.CardGraphicStatic[5] != null ? test1.CardGraphicStatic[5] : errorcardsprite, 1, 1, true));
        Col.Add(new Card(6, "Defensive Stance", "Adds armor points equal to 20% of your current HP", test1.CardGraphicStatic[6] != null ? test1.CardGraphicStatic[6] : errorcardsprite  , 1, 2, true));
        Col.Add(new Card(7, "Vitality surge", " Restores 8 health, draws one card", test1.CardGraphicStatic[7] != null ? test1.CardGraphicStatic[7] : errorcardsprite, 1, 3, true));
        Col.Add(new Card(8, "Disarm", "Disables enemy attack for next round, has 20% chance to deal 7 damage", test1.CardGraphicStatic[8] != null ? test1.CardGraphicStatic[8] : errorcardsprite, 1, 4, true));
        Col.Add(new Card(9, "Efficient Block", " Adds 8 armor and reduces the mana cost of the next skill to 1", test1.CardGraphicStatic[9] != null ? test1.CardGraphicStatic[9] : errorcardsprite, 1, 5, true));

        //mage
        Col.Add(new Card(10, "Arcane Missile", "shoots 3 magical missles each dealing 3 damage, every one of them has 33% to crit", test1.CardGraphicStatic[10] != null ? test1.CardGraphicStatic[10] : errorcardsprite, 2, 1, true));
        Col.Add(new Card(11, "Mage Armor", "creates a magical barrier that adds 4 armor, draws 2 cards ", test1.CardGraphicStatic[11] != null ? test1.CardGraphicStatic[11] : errorcardsprite, 2, 2, true));
        Col.Add(new Card(12, "Rejuvenating Aura", "Creates healing aura, restoring 12 health", test1.CardGraphicStatic[12] != null ? test1.CardGraphicStatic[12] : errorcardsprite, 2, 3, true));
        Col.Add(new Card(13, "Mana tear", "Reduces mana cost next spell to 0, deals 8 damage", test1.CardGraphicStatic[13] != null ? test1.CardGraphicStatic[13] : errorcardsprite, 2, 4, true));
        Col.Add(new Card(14, "Elemental burst", "Deals 16 damage, but increases next spell mana cost by 1", test1.CardGraphicStatic[14] != null ? test1.CardGraphicStatic[14] : errorcardsprite, 2, 5, true));

        //warlock
        Col.Add(new Card(15, "Dreadfull drain", "Deals 8 damage and heals for 3 hp.", test1.CardGraphicStatic[15] != null ? test1.CardGraphicStatic[15] : errorcardsprite, 3, 1, true));
        Col.Add(new Card(16, "Dark pact", "Sacrifices 5 hp to deal 15 damage", test1.CardGraphicStatic[16] != null ? test1.CardGraphicStatic[16] : errorcardsprite, 3, 2, true));
        Col.Add(new Card(17, "Cursed chains", "Gives 5 armor, draws 2 cards", test1.CardGraphicStatic[17] != null ? test1.CardGraphicStatic[17] : errorcardsprite, 3, 3, true));
        Col.Add(new Card(18, "Mana siphon", "Deal 5 to everyone (even you), to gain 9 mana, draws 1 card", test1.CardGraphicStatic[18] != null ? test1.CardGraphicStatic[18] : errorcardsprite, 3, 4, true));
        Col.Add(new Card(19, "Infernal embrace", "Gain 5 armor and deals 9 damage", test1.CardGraphicStatic[19] != null ? test1.CardGraphicStatic[19] : errorcardsprite, 3, 5, true));

        //archer
        Col.Add(new Card(20, "Precision shot", "Deals 6 damage, Has 80% chance to deal critical damage", test1.CardGraphicStatic[20] != null ? test1.CardGraphicStatic[20] : errorcardsprite, 4, 1, true));
        Col.Add(new Card(21, "Dodge roll", "Deals 4 damage, allows to dodge the next attack", test1.CardGraphicStatic[21] != null ? test1.CardGraphicStatic[21] : errorcardsprite, 4, 2, true));
        Col.Add(new Card(22, "Rapid fire", "Shoots 4 arrows, every one of them has 20% to crit", test1.CardGraphicStatic[22] != null ? test1.CardGraphicStatic[22] : errorcardsprite, 4, 3, true));
        Col.Add(new Card(23, "Camouflage", "Your next attack strikes critically, draw a card", test1.CardGraphicStatic[23] != null ? test1.CardGraphicStatic[23] : errorcardsprite, 4, 4, true));
        Col.Add(new Card(24, "Piercing shot", "Fires an arrow that deals 8 damage, draws a card", test1.CardGraphicStatic[24] != null ? test1.CardGraphicStatic[24] : errorcardsprite, 4, 5, true));

        //survivalist
        Col.Add(new Card(25, "Adaptive tactics", "Randomly increases mana by 2 or heal 10 health, draws a card", test1.CardGraphicStatic[25] != null ? test1.CardGraphicStatic[25] : errorcardsprite, 5, 1, true));
        Col.Add(new Card(26, "Nature's Mend", "Deals 7 damage, restores 3 health", test1.CardGraphicStatic[26] != null ? test1.CardGraphicStatic[26] : errorcardsprite, 5, 2, true));
        Col.Add(new Card(27, "Evasive Retreat", "Dodges the next two attacks", test1.CardGraphicStatic[27] != null ? test1.CardGraphicStatic[27] : errorcardsprite, 5, 3, true));
        Col.Add(new Card(28, "Trap Setter", "The next attack against you is reflected, draws a card", test1.CardGraphicStatic[28] != null ? test1.CardGraphicStatic[28] : errorcardsprite, 5, 4, true));
        Col.Add(new Card(29, "Resourcefull Scavenge", "Increases mana by 2 and draws 2 cards", test1.CardGraphicStatic[29] != null ? test1.CardGraphicStatic[29] : errorcardsprite, 5, 5, true));

        //meele
        Col.Add(new Card(30, "Mighty Swing", "Deals 6 damage, has 70% chance to deal critical damage", test1.CardGraphicStatic[30] != null ? test1.CardGraphicStatic[30] : errorcardsprite, 6, 1, true));
        Col.Add(new Card(31, "Defensive Wall", "Increases armor by 5, draw 1 card", test1.CardGraphicStatic[31] != null ? test1.CardGraphicStatic[31] : errorcardsprite, 6, 2, true));
        Col.Add(new Card(32, "Quick Strike", "Executes 3 strikes, each dealing 4 damage, has 25% chance to crit", test1.CardGraphicStatic[32] != null ? test1.CardGraphicStatic[32] : errorcardsprite, 6, 3, true));
        Col.Add(new Card(33, "Counterattack", "If enemy attacks on the next turn, deal 8 damage to them, draws 1 card", test1.CardGraphicStatic[33] != null ? test1.CardGraphicStatic[33] : errorcardsprite, 6, 4, true));
        Col.Add(new Card(34, "Berserker Fury", "Permanently increases damage by 2, but deletes armor", test1.CardGraphicStatic[34] != null ? test1.CardGraphicStatic[34] : errorcardsprite, 6, 5, true));

        //magic
        Col.Add(new Card(35, "Arcane Bolt", "Deals 9 damage. Draws a card", test1.CardGraphicStatic[35] != null ? test1.CardGraphicStatic[35] : errorcardsprite, 7, 1, true));
        Col.Add(new Card(36, "Warding Shield", " Gives 5 armor, has a 15% chance to double. Draws a card", test1.CardGraphicStatic[36] != null ? test1.CardGraphicStatic[36] : errorcardsprite, 7, 2, true));
        Col.Add(new Card(37, "Elemental Infusion", "Increases the next attack damage, draw 2 cards", test1.CardGraphicStatic[37] != null ? test1.CardGraphicStatic[37] : errorcardsprite, 7, 3, true));
        Col.Add(new Card(38, "Mana Surge", "Deals 7 damage, gain 1 mana", test1.CardGraphicStatic[38] != null ? test1.CardGraphicStatic[38] : errorcardsprite, 7, 4, true));
        Col.Add(new Card(39, "Giant Fireball", "Deal 30 damage", test1.CardGraphicStatic[39] != null ? test1.CardGraphicStatic[39] : errorcardsprite, 7, 5, true));

        //ranged
        Col.Add(new Card(40, "Precise Shot", "Deals 8 damage, draw 1 card", test1.CardGraphicStatic[40] != null ? test1.CardGraphicStatic[40] : errorcardsprite, 8, 1, true));
        Col.Add(new Card(41, "Covering Fire", "Deals 4 damage to each enemy, draws two cards", test1.CardGraphicStatic[41] != null ? test1.CardGraphicStatic[41] : errorcardsprite, 8, 2, true));
        Col.Add(new Card(42, "Sniper Stance", "Increases damage for the next attack by 3, draws a card", test1.CardGraphicStatic[42] != null ? test1.CardGraphicStatic[42] : errorcardsprite, 8, 3, true));
        Col.Add(new Card(43, "Pin Down", "Deals 5 damage, has 40% chance do stun enemy", test1.CardGraphicStatic[43] != null ? test1.CardGraphicStatic[43] : errorcardsprite, 8, 4, true));
        Col.Add(new Card(44, "Runaan's Hurricane", "Deals 8 damage to all enemies.", test1.CardGraphicStatic[44] != null ? test1.CardGraphicStatic[44] : errorcardsprite, 8, 5, true));
        foreach (Card card in Col) //tworzy liste dostepnych kart ze wszyskich kart
        {
            if((card.cardclass == StartGame.selectedclass || card.cardclass == 6 + (int)(StartGame.selectedclass/2)) && card.unlocked == true)
            {
                CardSet.Add(card);
            }
        }
        setlength = CardSet.Count;
        CardSet = CardSet.OrderBy(a => a.mana.ToString()).ThenBy(a => a.name).ToList(); //sortuje po manie i nazwie
        refresh_dispalyed_cards();
    }
    public void setcardcontent(Card c, GameObject go) //ustawia graficzne elementy karty z listy
    {
        go.transform.GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = c.graphic;
        go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = c.name;
        go.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = c.desc;
        go.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = c.mana.ToString();
        go.transform.GetChild(5).GetComponent<CardIdHolderPasser>().holdCard(c);
    }

    public void shitlittlefucntion(int i)
    {
        if (i < 0)
        {
            if (Collection.page > 0)
            {
                Collection.page += i;
            }
        }
        else if (Collection.setlength - Collection.page * 8 > 8)
        {
            Collection.page += i;
        }
        refresh_dispalyed_cards();
    }
    private void refresh_dispalyed_cards() //aktywuje lub dezaktuwuje karty wedlug potrzeby i odpala setcardcontent()
    {
        for (int i = 0; i < 8; i++)
        {
            if (i + (page * 8) >= CardSet.Count)
            {
                cardtemplates[i].SetActive(false);
            }
            else
            {
                setcardcontent(CardSet[i + page * 8], cardtemplates[i]);
                cardtemplates[i].SetActive(true);
            }
        }
    }

    public void addCardToDeck(GameObject thisButton) //dodaje karte do decku po prawej, na przyciskach w kartach kolekcji
    {
        if (Deck.Count < 9 && (Deck.Count(x => x == thisButton.GetComponent<CardIdHolderPasser>().card)) < 3)
        {
            Deck.Add(thisButton.GetComponent<CardIdHolderPasser>().card);           
        }
        else if(Deck.Count < 10 && (Deck.Count(x => x == thisButton.GetComponent<CardIdHolderPasser>().card)) < 3)
        {
            Deck.Add(thisButton.GetComponent<CardIdHolderPasser>().card);
            b.interactable = true;
        }
        refresh_deck_cards();
    }
    private void setdeckcontent(Card c, GameObject go) //ustawia graficzne elementy karty z listy
    {
        go.transform.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = c.graphic;
        go.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = c.mana.ToString();
        go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = c.name;
        go.transform.GetChild(3).GetComponent<CardIdHolderPasser>().holdCard(c);
    }

    public void removeCardFromDeck(int i) //usuwa karte z decku, na przyciskach w kartach talii
    {
        Deck.RemoveAt(i);
        refresh_deck_cards();
        b.interactable = false;

    }
    private void refresh_deck_cards() //aktywuje lub dezaktuwuje karty wedlug potrzeby i odpala setdeckcontent()
    {
        Deck = Deck.OrderBy(a => a.mana.ToString()).ThenBy(a => a.name).ToList();//sortuje po manie i nazwie
        for (int i = 0; i < 10; i++)
        {
            if (i >= Deck.Count)
            {
                deckobject[i].SetActive(false);
            }
            else
            {
                setdeckcontent(Deck[i], deckobject[i]);
                deckobject[i].SetActive(true);
            }
        }
    }
}
