using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CardEffects : MonoBehaviour
{
    private static float cdb = 0; //constant damage bonus
    private static float nadb = 0; //next attack damage bonus
    private static float nadm = 1; //next attack damage multiplier
    private static GameObject hh; //handhandler
    private static GameObject ph; //playerhealth
    private static GameObject mb; //manabar
    private static GameObject pl;
    private static float enArm = 0;//enemyarmor
    public GameObject player;
    public GameObject playerhp;
    public GameObject manab;
    public static int dodgeamount = 0;
    public static int reflectamount = 0;

    private void Start()
    {
        hh = gameObject;
        ph = playerhp;
        mb = manab;
        pl = player;
    }

    private static void dealdmg(GameObject a, float dmg)
    {
        if (a != pl)
        {
            a.GetComponent<Kard>().CastTarget().transform.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Slider>().value -= (dmg*nadm + cdb + nadb - enArm) > 0 ? (dmg * nadm + cdb + nadb - enArm) : 0;
            enArm = enArm - (dmg * nadm + cdb + nadb) > 0 ? enArm - (8.0f + cdb + nadb) : 0;
            nadb = 0;
            nadm = 1;
        }
    }

    public static void CardEffect(int i, GameObject a)
    {
        if(a.GetComponent<Kard>().CastTarget() != null)
        {
            if (a.GetComponent<Kard>().CastTarget().transform.GetChild(0).GetComponents<EnemyAction>().Length != 0)
            {
                if (a.GetComponent<Kard>().CastTarget().transform.GetChild(0).GetComponent<EnemyAction>().action == 1)
                {
                    enArm = a.GetComponent<Kard>().CastTarget().transform.GetChild(0).GetComponent<EnemyAction>().value;
                }
            }

            switch (i)
            {
                case 0:
                    {
                        dealdmg(a, 8f);
                    }
                    break;
                case 1:
                    {
                        dealdmg(a, 7f);
                        if (Random.Range(0, 2) == 1)
                        {
                            hh.GetComponent<KardKounter>().drawCard(); //dobieranie kart    
                        }
                    }
                    break;
                case 2:
                    {
                        dealdmg(a, 4f);
                        ph.transform.GetChild(0).GetComponent<UnityEngine.UI.Slider>().value += 2.0f; //leczenie siebie
                        ph.transform.GetChild(0).GetComponent<hpPool>().armor += 4; //armor
                    }
                    break;
                case 3:
                    {
                        if (Random.Range(0, 10) < 3)
                        {
                            if (a.GetComponent<Kard>().CastTarget().transform.GetChild(0).GetComponent<EnemyAction>().action == 0) //wy³¹czanie ataku
                            {
                                a.GetComponent<Kard>().CastTarget().transform.GetChild(0).GetComponent<EnemyAction>().action = 3;
                            }
                        }
                        dealdmg(a, 5f);

                    }
                    break;
                case 4:
                    {
                        a.GetComponent<Kard>().CastTarget().transform.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Slider>().value -= (16.0f * nadm + cdb + nadb); //dmg ignorujacy armor
                        nadb = 0;
                        nadm = 1;
                    }
                    break;
                case 5:
                    {
                        if (Random.Range(0, 10) == 0)
                        {
                            a.GetComponent<Kard>().CastTarget().transform.GetChild(0).GetComponent<EnemyAction>().action = 3; //stun
                        }
                        dealdmg(a, 13f);

                    }
                    break;
                case 6:
                    {
                        ph.transform.GetChild(0).GetComponent<hpPool>().armor += (int)(ph.transform.GetChild(0).GetComponent<UnityEngine.UI.Slider>().value * 0.2f);
                    }
                    break;
                case 7:
                    {
                        a.GetComponent<Kard>().CastTarget().transform.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Slider>().value += 8f;//leczenie celu
                        hh.GetComponent<KardKounter>().drawCard();
                    }
                    break;
                case 8:
                    {
                        if (a.GetComponent<Kard>().CastTarget().transform.GetChild(0).GetComponent<EnemyAction>().action == 0)
                        {
                            a.GetComponent<Kard>().CastTarget().transform.GetChild(0).GetComponent<EnemyAction>().action = 3;
                        }
                        if (Random.Range(0, 5) == 0)
                        {
                            dealdmg(a, 7f);
                        }
                    }
                    break;
                case 9:
                    {
                        ph.transform.GetChild(0).GetComponent<hpPool>().armor += 8; //armor
                        hh.GetComponent<KardKounter>().discountto = 1; //redukcja kosztu do wartoœci
                    }
                    break;
                case 10:
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (Random.Range(0, 3) == 0) { dealdmg(a, 6f); }
                            else { dealdmg(a, 3f); } //crit
                        }
                    }
                    break;
                case 11:
                    {
                        ph.transform.GetChild(0).GetComponent<hpPool>().armor += 4; //armor
                        hh.GetComponent<KardKounter>().drawCard();
                        hh.GetComponent<KardKounter>().drawCard();
                    }
                    break;
                case 12:
                    {
                        a.GetComponent<Kard>().CastTarget().transform.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Slider>().value += 12f;//leczenie celu
                    }
                    break;
                case 13:
                    {
                        hh.GetComponent<KardKounter>().discountto = 0;
                        dealdmg(a, 8f);
                    }
                    break;
                case 14:
                    {
                        hh.GetComponent<KardKounter>().manaDiscoutBy = -1;
                        dealdmg(a, 16f);
                    }
                    break;
                case 15:
                    {
                        dealdmg(a, 8f);
                        ph.transform.GetChild(0).GetComponent<UnityEngine.UI.Slider>().value += 3.0f;
                    }
                    break;
                case 16:
                    {
                        ph.transform.GetChild(0).GetComponent<UnityEngine.UI.Slider>().value -= 5.0f;
                        dealdmg(a, 15f);
                    }
                    break;
                case 17:
                    {
                        ph.transform.GetChild(0).GetComponent<hpPool>().armor += 5; //armor
                        hh.GetComponent<KardKounter>().drawCard();
                        hh.GetComponent<KardKounter>().drawCard();
                    }
                    break;
                case 18:
                    {
                        ph.transform.GetChild(0).GetComponent<UnityEngine.UI.Slider>().value -= 5.0f; //dmg na siebie
                        foreach (GameObject enemy in EndTurn.enemies) //aoe taki sam dmg
                        {
                            dealdmg(enemy, 5f);
                        }
                        mb.GetComponent<manabar>().manaleft += 9; //mana
                        hh.GetComponent<KardKounter>().drawCard();
                    }
                    break;
                case 19:
                    {
                        ph.transform.GetChild(0).GetComponent<hpPool>().armor += 5; //armor
                        dealdmg(a, 9f);
                    }
                    break;
                case 20:
                    {
                        if (Random.Range(0, 5) == 0)
                        {
                            dealdmg(a, 6f);
                        }
                        else
                        {
                            dealdmg(a, 12f);
                        }
                    }
                    break;
                case 21:
                    {
                        dealdmg(a, 4f);
                        dodgeamount = Mathf.Max(1, dodgeamount);

                    }
                    break;
                case 22:
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (Random.Range(0, 5) == 0)
                            {
                                dealdmg(a, 4f);
                            }
                            else
                            {
                                dealdmg(a, 8f);
                            }
                        }

                    }
                    break;
                case 23:
                    {
                        hh.GetComponent<KardKounter>().drawCard();
                        nadm = 2;
                    }
                    break;
                case 24:
                    {
                        dealdmg(a, 8f);
                        hh.GetComponent<KardKounter>().drawCard();
                    }
                    break;
                case 25:
                    {
                        if (Random.Range(0, 2) == 0)
                        {
                            mb.GetComponent<manabar>().maxmana = mb.GetComponent<manabar>().maxmana + 2 > 10 ? 10 : mb.GetComponent<manabar>().maxmana + 2; //mana
                        }
                        else
                        {
                            ph.transform.GetChild(0).GetComponent<UnityEngine.UI.Slider>().value += 10.0f;
                        }
                        hh.GetComponent<KardKounter>().drawCard();
                    }
                    break;
                case 26:
                    {
                        dealdmg(a, 7f);
                        ph.transform.GetChild(0).GetComponent<UnityEngine.UI.Slider>().value += 3.0f;
                    }
                    break;
                case 27:
                    {
                        dodgeamount = Mathf.Max(2, dodgeamount);
                    }
                    break;
                case 28:
                    {
                        dodgeamount = Mathf.Max(1, dodgeamount);
                        reflectamount = Mathf.Max(1, reflectamount);
                        hh.GetComponent<KardKounter>().drawCard();
                    }
                    break;
                case 29:
                    {
                        mb.GetComponent<manabar>().manaleft += 2;
                        hh.GetComponent<KardKounter>().drawCard();
                        hh.GetComponent<KardKounter>().drawCard();
                    }
                    break;
                case 30:
                    {
                        if (Random.Range(0, 10) < 7)
                        {
                            dealdmg(a, 12f);
                        }
                        else
                        {
                            dealdmg(a, 6f);
                        }
                    }
                    break;
                case 31:
                    {
                        ph.transform.GetChild(0).GetComponent<hpPool>().armor += 5;
                        hh.GetComponent<KardKounter>().drawCard();
                    }
                    break;
                case 32:
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (Random.Range(0, 4) == 0)
                            {
                                dealdmg(a, 8f);
                            }
                            else
                            {
                                dealdmg(a, 4f);
                            }
                        }
                    }
                    break;
                case 33:
                    {
                        reflectamount = Mathf.Max(1, reflectamount);
                        hh.GetComponent<KardKounter>().drawCard();
                    }
                    break;
                case 34:
                    {
                        cdb += 2;
                        ph.transform.GetChild(0).GetComponent<hpPool>().armor = 0;
                    }
                    break;
                case 35:
                    {
                        dealdmg(a, 9f);
                        hh.GetComponent<KardKounter>().drawCard();
                    }
                    break;
                case 36:
                    {

                        if (Random.Range(0, 20) < 3)
                        {
                            ph.transform.GetChild(0).GetComponent<hpPool>().armor += 10;
                        }
                        else
                        {
                            ph.transform.GetChild(0).GetComponent<hpPool>().armor += 5;
                        }
                        hh.GetComponent<KardKounter>().drawCard();
                    }
                    break;
                case 37:
                    {
                        nadb += 3;
                        hh.GetComponent<KardKounter>().drawCard();
                        hh.GetComponent<KardKounter>().drawCard();
                    }
                    break;
                case 38:
                    {
                        dealdmg(a, 7f);
                        mb.GetComponent<manabar>().manaleft += 2;
                    }
                    break;
                case 39:
                    {
                        dealdmg(a, 30f);
                    }
                    break;
                case 40:
                    {
                        dealdmg(a, 8f);
                        hh.GetComponent<KardKounter>().drawCard();
                    }
                    break;
                case 41:
                    {
                        foreach (GameObject enemy in EndTurn.enemies) //aoe taki sam dmg
                        {
                            dealdmg(enemy, 4f);
                        }
                        hh.GetComponent<KardKounter>().drawCard();
                        hh.GetComponent<KardKounter>().drawCard();
                    }
                    break;
                case 42:
                    {
                        nadb += 3;
                        hh.GetComponent<KardKounter>().drawCard();
                    }
                    break;
                case 43:
                    {
                        if (Random.Range(0, 10) < 4)
                        {
                            a.GetComponent<Kard>().CastTarget().transform.GetChild(0).GetComponent<EnemyAction>().action = 3;
                        }
                        dealdmg(a, 5f);

                    }
                    break;
                case 44:
                    {
                        foreach (GameObject enemy in EndTurn.enemies) //aoe taki sam dmg
                        {
                            dealdmg(enemy, 8f);
                        }
                    }
                    break;
                default:
                    {
                        Debug.Log("Jest Ÿle");
                    }
                    break;

            }
            ph.transform.GetChild(0).GetComponent<hpPool>().UpdateHpValue();
        } 
    }
}
