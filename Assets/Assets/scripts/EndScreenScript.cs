using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenScript : MonoBehaviour
{
    public static string endtext;
    [SerializeField] GameObject endtxt;
    void Start()
    {
        endtxt.GetComponent<TextMeshProUGUI>().text = endtext;
    }
    public void End()
    {
        SceneManager.LoadScene(0);
    }
}
