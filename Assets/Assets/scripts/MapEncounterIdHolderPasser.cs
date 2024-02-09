using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Collection;

public class MapEncounterIdHolderPasser : MonoBehaviour
{
    public int encounterId = 0;

    public void holdId(int c)
    {
        encounterId = c;
    }
}
