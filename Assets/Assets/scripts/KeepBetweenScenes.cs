using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepBetweenScenes : MonoBehaviour
{
    private void Awake() 
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("MultiSceneSounds");
        if (musicObj.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }
}
