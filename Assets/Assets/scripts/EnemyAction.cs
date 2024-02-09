using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EnemyAction : MonoBehaviour
{
    [SerializeField] Sprite[] icons; //0 - attack, 1 - armor, 2 - heal, 3 - unknown
    public GameObject hpplayer;
    public GameObject actionicon;
    public UnityEngine.UI.Slider hp;
    public int action = 3, value;
    public void declareAction(int i)
    {
        actionicon.SetActive(true);
        switch (MapEncounters.encType[i])
        {
            case 1:
                {
                    switch (Random.Range(1, 3))
                    {
                        case 1:
                        {
                            actionicon.transform.GetComponent<UnityEngine.UI.Image>().sprite = unktype(0);
                            value = Random.Range(3, 7);
                            actionicon.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = unkval(value);
                        }break;                                                       
                        case 2:
                        {
                            if(hp.value < hp.maxValue)
                            {
                                actionicon.transform.GetComponent<UnityEngine.UI.Image>().sprite = unktype(2);
                                value = Random.Range(5, 11);
                                actionicon.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = unkval(value);
                                }
                            else
                            {
                                actionicon.transform.GetComponent<UnityEngine.UI.Image>().sprite = unktype(0);
                                value = Random.Range(3, 7);
                                actionicon.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = unkval(value);
                                }                                
                        }break;                           
                    }
                }
                break;
            case 2:
                {
                    switch (Random.Range(1, 3))
                    {
                        case 1:
                        {
                            actionicon.transform.GetComponent<UnityEngine.UI.Image>().sprite = unktype(0);
                            value = Random.Range(7, 13);
                            actionicon.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = unkval(value);
                        }break;                           
                        case 2:
                        {
                            actionicon.transform.GetComponent<UnityEngine.UI.Image>().sprite = unktype(1);
                            value = Random.Range(5, 10);
                            actionicon.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = unkval(value);                               
                        }break;                            
                    }
                }
                break;
            case 3:
            {
                switch (Random.Range(1, 4))
                {
                    case 1:
                    {
                        actionicon.transform.GetComponent<UnityEngine.UI.Image>().sprite = unktype(0);
                        value = Random.Range(3, 10);
                        actionicon.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = unkval(value);
                    }
                    break;
                    case 2:
                    {
                        actionicon.transform.GetComponent<UnityEngine.UI.Image>().sprite = unktype(1);
                        value = Random.Range(5, 10);
                        actionicon.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = unkval(value);
                    }
                    break;
                    case 3:
                    {
                        if (hp.value < hp.maxValue)
                        {
                            actionicon.transform.GetComponent<UnityEngine.UI.Image>().sprite = unktype(2);
                            value = Random.Range(1, 13);
                            actionicon.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = unkval(value);
                        }
                        else
                        {
                            actionicon.transform.GetComponent<UnityEngine.UI.Image>().sprite = unktype(1);
                            value = Random.Range(5, 10);
                            actionicon.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = unkval(value);
                        }
                    }
                    break;
                }
                }break;
            case 4:
            {
                actionicon.transform.GetComponent<UnityEngine.UI.Image>().sprite = unktype(0);
                value = Random.Range(2, 10);
                actionicon.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = unkval(value);
            }
            break;
            case 5:
            {
                if(hp.value < hp.maxValue)
                {
                    actionicon.transform.GetComponent<UnityEngine.UI.Image>().sprite = unktype(2);
                    value = Random.Range(5, 15);
                    actionicon.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = unkval(value);
                }
                else
                {
                    actionicon.transform.GetComponent<UnityEngine.UI.Image>().sprite = unktype(0);
                    value = Random.Range(3, 10);
                    actionicon.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = unkval(value);
                }
            }
            break;
            case 6:
            {
                switch (Random.Range(1, 4))
                {
                case 1:
                {
                    actionicon.transform.GetComponent<UnityEngine.UI.Image>().sprite = unktype(0);
                    value = Random.Range(5, 16);
                    actionicon.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = unkval(value);
                }
                break;
                case 2:
                {
                    actionicon.transform.GetComponent<UnityEngine.UI.Image>().sprite = unktype(1);
                    value = Random.Range(10, 15);
                    actionicon.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = unkval(value);
                }
                break;
                case 3:
                {
                    if (hp.value < hp.maxValue)
                    {
                        actionicon.transform.GetComponent<UnityEngine.UI.Image>().sprite = unktype(2);
                        value = Random.Range(5, 11);
                        actionicon.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = unkval(value);
                    }
                    else
                    {
                        actionicon.transform.GetComponent<UnityEngine.UI.Image>().sprite = unktype(1);
                        value = Random.Range(5, 16);
                        actionicon.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = unkval(value);
                    }
                }
                break;
                }
            }
            break;
        }
    }
    public void commitAction()
    {
        actionicon.SetActive(false);
        if(action == 0)
        {
            if(CardEffects.reflectamount > 0)
            {
                hp.value -= value;
                CardEffects.reflectamount--;
            }
            if(CardEffects.dodgeamount == 0)
            {
                int damage = value - hpplayer.transform.GetComponent<hpPool>().armor < 0 ? 0 : value - hpplayer.transform.GetComponent<hpPool>().armor;
                hpplayer.transform.GetComponent<hpPool>().armor = hpplayer.transform.GetComponent<hpPool>().armor - value < 0 ? 0 : hpplayer.transform.GetComponent<hpPool>().armor - value;
                hpplayer.transform.GetComponent<UnityEngine.UI.Slider>().value -= damage;
                hpplayer.transform.GetComponent<hpPool>().UpdateHpValue();
            }
            else
            {
                CardEffects.dodgeamount--;
            }
            
        }
        else if(action == 2)
        {
            hp.value = value + hp.value <= hp.maxValue ? value + hp.value : hp.maxValue;
        }
    }
    private Sprite unktype(int i)
    {
        action = i;
        if(Random.Range(1, 5) == 4)
        {
            return icons[3];
        }
        else
        {
            return icons[i];
        }
    }
    private string unkval(int v)
    {
        if (Random.Range(1, 5) == 4)
        {
            return "?";
        }
        else
        {
            return value.ToString();
        }
    }

}
