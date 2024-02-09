using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardIdHolderPasser : MonoBehaviour //przypisuje guzikowi kazdej karty w kolekcji konkretne informacje o kracie,                                                  
{                                               //zeby mozna je by³o ³atwo przekazaæ dalej
    public Collection.Card card = new Collection.Card(9999, "" , "" ,null, 9999, 9999, false);

    public void holdCard(Collection.Card c)
    {
        card = c;

    }
}
