using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class hpPool : MonoBehaviour
{

    [SerializeField] private GameObject HP;
    [SerializeField] private GameObject Arm;
    public int hpMax = 50;
    public static int hpCurr = 50;
    public int armor = 0;
    Image img;
    private void Start()
    {
        gameObject.transform.GetComponent<UnityEngine.UI.Slider>().maxValue = hpMax;
        gameObject.transform.GetComponent<UnityEngine.UI.Slider>().value = hpCurr;
        img = GetComponent<Image>();
        UpdateHpValue();
    }
    public void UpdateHpValue()
    {
        if (armor > 0)
        {
            Arm.SetActive(true);
            Arm.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = armor.ToString();
        }
        else
        {
            Arm.SetActive(false);
        }
        hpCurr = (int)gameObject.transform.GetComponent<UnityEngine.UI.Slider>().value;
        HP.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, ((-gameObject.GetComponent<RectTransform>().rect.height) + (gameObject.GetComponent<RectTransform>().rect.height*((float)hpCurr/(float)hpMax))), 0);

        if(hpCurr == 0)
        {
            SceneManager.LoadScene(2);
            EndScreenScript.endtext = "You lost! womp womp";
        }
    }
}
