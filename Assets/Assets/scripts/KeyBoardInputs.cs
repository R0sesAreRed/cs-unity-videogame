using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardInputs : MonoBehaviour
{
    private Animator _animator;


    void Start() 
    {
        _animator = gameObject.GetComponent<Animator>();
    }


    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            _animator.SetTrigger("RTPS"); 
        }
    }
}
