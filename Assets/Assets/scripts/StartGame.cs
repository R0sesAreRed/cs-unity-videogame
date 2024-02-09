using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class StartGame : MonoBehaviour
{
    [SerializeField] int sceneId;
    [SerializeField] int pasclas;
    public static int selectedclass = 0;

    public void startNewGame()
    {
        SceneManager.LoadScene(sceneId);
    }
    public void passclass()
    {
        selectedclass = pasclas;
    }

    public static int giveclass()
    {
        return selectedclass;
    }

}
