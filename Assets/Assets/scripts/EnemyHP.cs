using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class EnemyHP : MonoBehaviour
{

    [SerializeField] Slider hp;
    [SerializeField] Slider hp21;
    [SerializeField] Slider hp22;
    [SerializeField] Slider hpboss;
    [SerializeField] GameObject cardpicker;

    private void Start()
    {
        if(!CreateEncounter.isCombat)
        {
            cardpicker.GetComponent<CardPicker>().playanim();
        }
    }
    public void endCombat()
    {
        if(hp.value + hp21.value + hp22.value + hpboss.value == 0)
        {
            cardpicker.GetComponent<CardPicker>().playanim();
            KardKounter.hand.Clear();
        }
    }  
    public void DefeatBoss()
    {
        if (hp.value + hp21.value + hp22.value + hpboss.value == 0)
        {
            SceneManager.LoadScene(2);
            EndScreenScript.endtext = "You won! Well Done";
        }
    }

    public void killenemy(GameObject a)
    {
        if(a.transform.GetChild(0).GetChild(0).GetComponent<Slider>().value == 0)
        {
            a.SetActive(false);
            a.GetComponent<RectTransform>().anchoredPosition = new Vector3(1000, 0, 0);
        }
    }
}
