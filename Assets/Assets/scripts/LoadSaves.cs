using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSaves : MonoBehaviour
{
    GameObject Save1, Save2, Save3, Save4, Save5, Save6;
    [SerializeField] private List<Sprite> chips;
    [SerializeField] private int sceneId; // 0 - WelcomeScreen, 1 - Fight Encounter, 2 - Rest Encounter, 3 - Endscreen, 4 - Map, 5 - Pick a card 


    void assignSprite(Image chip)  
    {
        chip.sprite = chips[Random.Range(0, 9)];
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(sceneId); //zmienia scene, numer do przypisania w obiekcie LoadMenu

    }
    public void LoadFromDatabase()//tutaj mo¿na te¿ dodaæ wczytywanie z bazy danych zapisanych saveów...
    {

    }
    void Start()
    {
        List<GameObject> saves = new List<GameObject>() { Save1, Save2, Save3, Save4, Save5, Save6 };

        for (int i = 0; i < saves.Count; i++)
        {
            saves[i] = this.gameObject.transform.GetChild(i).gameObject;
            assignSprite(saves[i].GetComponent<Image>());
        }
    }
}
