using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour
{
    [SerializeField] List<GameObject> saves = new List<GameObject>();
    public void NewGame()
    {
        foreach (GameObject game in saves)
        {
            game.SetActive(false);
        }
    }

    public void LoadGame() //nic
    {
        foreach(GameObject game in saves)
        {
            game.SetActive(true);
        }
    }

    public void Exit() //wy³¹cza program
    {
        Debug.Log("EXIT");
        Application.Quit();
    }

}
