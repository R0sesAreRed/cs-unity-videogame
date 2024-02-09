using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Collection;

public class test1 : MonoBehaviour
{
    public static Sprite[] CardGraphicStatic;
    public Sprite[] CardGraphic;
    private void Awake()
    {
        CardGraphicStatic = CardGraphic;
    }
}
