using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class EndTurn : MonoBehaviour
{

    [SerializeField] GameObject manabar;
    [SerializeField] GameObject handHandler;
    public static List<GameObject> enemies = new List<GameObject>();
    public void endTurn()
    {
        //koniec tury
        foreach (GameObject enemy in enemies)
        {
            if (enemy.transform.GetChild(0).GetComponent<Slider>().value > 0)
            {
                enemy.GetComponent<EnemyAction>().commitAction();
            }
        }


        //poczatek tury
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].transform.GetChild(0).GetComponent<Slider>().value > 0)
            {
                enemies[i].GetComponent<EnemyAction>().declareAction(i);
            }
        }
        


        //draw
        handHandler.GetComponent<KardKounter>().drawCard();
        //add and refill mana
        if (manabar.GetComponent<manabar>().maxmana <10)
        {
            manabar.GetComponent<manabar>().maxmana++;
            manabar.GetComponent<manabar>().GainOrRemoveMana();
        }
        manabar.GetComponent<manabar>().manaleft = manabar.GetComponent<manabar>().maxmana;
        manabar.GetComponent<manabar>().UpdateManaBar();
    }  
}
