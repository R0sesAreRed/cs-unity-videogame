using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class manabar : MonoBehaviour
{
    public int maxmana = 10;
    public int manaleft;
    public Image[] crystalsimg = new Image[10];
    public GameObject[] crystalsobj = new GameObject[10];
    [SerializeField] private Sprite full, empty;
    private void Awake() //je¿eli bêdzie potrzeba sprawdzania max iloœci kryszta³ów poza tworzeniem sceny (idk jakies mechaniki czy cos) to przeniesc to do UpdateManaBar()
    { 
        for(int i = 0; i < crystalsobj.Length; i++)
        {
            crystalsobj[i] = this.gameObject.transform.GetChild(i).gameObject;
            crystalsimg[i] = this.gameObject.transform.GetChild(i).GetComponent<Image>();
        }       
        maxmana = 1;
        manaleft = maxmana;
        GainOrRemoveMana();
    }

    public void GainOrRemoveMana()
    {
        for(int i = 0; i < maxmana; i++)
        {
            crystalsobj[i].SetActive(true);
        }
        for (int i = maxmana; i < crystalsobj.Length; i++)
        {
            crystalsobj[i].SetActive(false);
        }
    }

    public void UpdateManaBar() //zape³nia lub oproznia kryszta³y w zale¿noœci od intagera
    {
        for (int i = 0; i < (manaleft <= 10 ? manaleft : 10); i++)
        {
            crystalsimg[i].sprite = full;
        }
        for(int i = manaleft;i < 10; i++)
        {
            crystalsimg[i].sprite = empty;
        }
    }
}
