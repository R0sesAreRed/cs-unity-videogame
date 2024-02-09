using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;


public class CreateEncounter : MonoBehaviour
{
    [SerializeField] private List<Sprite> PlayerClass;
    [SerializeField] private List<Sprite> EnemyType;
    [SerializeField] private List<Sprite> Background;
    [SerializeField] private List<Sprite> Boss;
    [SerializeField] private GameObject bg, pl, en, en21, en22, bo;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject manabar;
    [SerializeField] private KardKounter kk;
    [SerializeField] private GameObject cardpicker;
    [SerializeField] private hpPool hpp;
    public int bgNo, plNo = StartGame.selectedclass;
    public int[] enNo = MapEncounters.encType; //bg number (numer sprite t³a), player number (klasa postaci), encounter number (rodzaj encounteru)
    public static bool isCombat;
    public void Start()
    {
        if(EndTurn.enemies.Count > 0)
            EndTurn.enemies.Clear();
        plNo = StartGame.giveclass();
        Image background = bg.GetComponent<Image>();
        Image player = pl.GetComponent<Image>();
        Image boss = bo.transform.GetChild(0).GetComponent<Image>();
        RectTransform rt = canvas.GetComponent<RectTransform>();
        background.sprite = Background[Random.Range(0, Background.Count)];
        background.rectTransform.sizeDelta = new Vector2(background.sprite.texture.width, background.sprite.texture.height);
        if (background.sprite.texture.width < rt.rect.width && background.sprite.texture.height < rt.rect.height) //ten ca³y syf poni¿ej rozci¹ga odpowiednio t³o, tak ¿eby mo¿na by³o wrzucic jakikolwiek +/- prostok¹tny obraz na t³o i bedzie dzia³aæ
        {
            float ratio = Mathf.Max((rt.rect.width + 30) / background.sprite.texture.width, (rt.rect.height + 30) / background.sprite.texture.height);
            background.rectTransform.sizeDelta = new Vector2(bg.GetComponent<RectTransform>().rect.width * ratio, bg.GetComponent<RectTransform>().rect.height * ratio);
        }
        else if (background.sprite.texture.width < rt.rect.width)
        {
            float ratio = (rt.rect.width+30) / background.sprite.texture.width;
            background.rectTransform.sizeDelta = new Vector2(bg.GetComponent<RectTransform>().rect.width*ratio, bg.GetComponent<RectTransform>().rect.height*ratio);
        }
        else if(background.sprite.texture.height < rt.rect.height)
        {
            float ratio = (rt.rect.height+30) / background.sprite.texture.height;
            background.rectTransform.sizeDelta = new Vector2(bg.GetComponent<RectTransform>().rect.width * ratio, bg.GetComponent<RectTransform>().rect.height * ratio);
        }
        //a¿ do t¹d, poni¿ej tego ustawia sprite gracza i przeciwnika (chyba nie sprawdza³em czy boss dzia³a dobrze)
        player.sprite = PlayerClass[plNo];
        en.SetActive(false);
        en21.SetActive(false);
        en22.SetActive(false);
        bo.SetActive(false);
        en.transform.GetChild(0).GetChild(0).GetComponent<Slider>().value = 1;
        bo.transform.GetChild(0).GetChild(0).GetComponent<Slider>().value = 1;
        en21.transform.GetChild(0).GetChild(0).GetComponent<Slider>().value = 1;
        en22.transform.GetChild(0).GetChild(0).GetComponent<Slider>().value = 1;

        if (MapEncounters.encType[1] == 0)
        {
            en21.transform.GetChild(0).GetChild(0).GetComponent<Slider>().value = 0;
            en22.transform.GetChild(0).GetChild(0).GetComponent<Slider>().value = 0;
            bo.transform.GetChild(0).GetChild(0).GetComponent<Slider>().maxValue = 0;
            en21.GetComponent<RectTransform>().anchoredPosition = new Vector3(1000, 0, 0);
            en22.GetComponent<RectTransform>().anchoredPosition = new Vector3(1000, 0, 0);
            if (MapEncounters.encType[0] != 6)
                bo.GetComponent<RectTransform>().anchoredPosition = new Vector3(1000, 0, 0);
            else
                en.GetComponent<RectTransform>().anchoredPosition = new Vector3(1000, 0, 0);

            kk.drawCard();
            kk.drawCard();
            kk.drawCard();

            switch (MapEncounters.encType[0])
            {
                case 1:
                {
                        createCombat(0, en, 20);
                    }
                    break;
                case 2:
                {
                        createCombat(0, en, 8);
                    }
                break;
                case 3:
                {
                        createCombat(0, en, 13);
                    }
                break;
                case 4:
                {
                        createCombat(0, en, 17);
                    }
                break;
                case 5:
                {
                        createCombat(0, en, 22);

                    }
                break;
                case 6: //boss
                {
                    
                    bo.SetActive(true);
                    bo.transform.GetChild(0).GetChild(0).GetComponent<Slider>().maxValue = 100;
                    bo.transform.GetChild(0).GetChild(0).GetComponent<Slider>().value = 100;
                        en.transform.GetChild(0).GetChild(0).GetComponent<Slider>().value = 0;
                        EndTurn.enemies.Add(bo.transform.GetChild(0).gameObject);
                        boss.sprite = Boss[0];
                    isCombat = true;
                }
                break;
                case 11:
                {
                        Debug.Log("creating rest");
                        KardKounter.hand.Clear();
                        KardKounter.handcards.Clear();
                        en.transform.GetChild(0).GetChild(0).GetComponent<Slider>().value = 0;
                        bo.transform.GetChild(0).GetChild(0).GetComponent<Slider>().value = 0;
                        cardpicker.transform.GetChild(0).gameObject.SetActive(true);
                    cardpicker.transform.GetChild(1).gameObject.SetActive(true);
                    cardpicker.transform.GetChild(3).gameObject.SetActive(false);
                    isCombat = false;
                }
                    break;
                case 12:
                {
                        Debug.Log("creating rest");
                        KardKounter.hand.Clear();
                        KardKounter.handcards.Clear();
                        en.transform.GetChild(0).GetChild(0).GetComponent<Slider>().value = 0;
                        bo.transform.GetChild(0).GetChild(0).GetComponent<Slider>().value = 0;
                        cardpicker.transform.GetChild(0).gameObject.SetActive(false);
                    cardpicker.transform.GetChild(1).gameObject.SetActive(false);
                    cardpicker.transform.GetChild(3).gameObject.SetActive(true);
                    cardpicker.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "You found a safe place to rest. Heath fully restored";
                        hpPool.hpCurr = hpp.hpMax;
                    isCombat = false;
                }break;
                default:
                {
                    
                    isCombat = false;
                }
                break;
            }
        }
        else
        {
            
            en.transform.GetChild(0).GetChild(0).GetComponent<Slider>().value = 0;
            bo.transform.GetChild(0).GetChild(0).GetComponent<Slider>().value = 0;
            en.GetComponent<RectTransform>().anchoredPosition = new Vector3(1000, 0, 0);
            bo.GetComponent<RectTransform>().anchoredPosition = new Vector3(1000, 0, 0);

            kk.drawCard();
            kk.drawCard();
            kk.drawCard();
            kk.drawCard();

            switch (MapEncounters.encType[0])
            {
                case 1:
                    {
                        createCombat(0, en21, 20);
                    }
                    break;
                case 2:
                    {
                        createCombat(0, en21, 8);
                    }
                    break;
                case 3:
                    {
                        createCombat(0, en21, 13);
                    }
                    break;
                case 4:
                    {
                        createCombat(0, en21, 17);
                    }
                    break;
                case 5:
                    {
                        createCombat(0, en21, 22);
                    }
                    break;
            }
            switch (MapEncounters.encType[1])
            {
                case 1:
                    {
                        createCombat(0, en22, 20);
                    }
                    break;
                case 2:
                    {
                        createCombat(0, en22, 8);
                    }
                    break;
                case 3:
                    {
                        createCombat(0, en22, 13);
                    }
                    break;
                case 4:
                    {
                        createCombat(0, en22, 17);
                    }
                    break;
                case 5:
                    {
                        createCombat(0, en22, 22);
                    }
                    break;
            }
        }      
    }



    private void createCombat(int a, GameObject e, int hpi)
    {
        Debug.Log("creating encounter");
        cardpicker.transform.GetChild(0).gameObject.SetActive(true);
        cardpicker.transform.GetChild(1).gameObject.SetActive(true);
        EndTurn.enemies.Add(e.transform.GetChild(0).gameObject);
        e.SetActive(true);
        e.transform.GetChild(0).GetChild(0).GetComponent<Slider>().maxValue = hpi;
        e.transform.GetChild(0).GetChild(0).GetComponent<Slider>().value = hpi;
        e.transform.GetChild(0).GetComponent<Image>().sprite = EnemyType[MapEncounters.encType[a] - 1];
        isCombat = true;
        
    }
}