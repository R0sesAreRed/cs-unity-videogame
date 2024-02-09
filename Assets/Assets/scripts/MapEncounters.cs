using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;

public class MapEncounters : MonoBehaviour
{
    [SerializeField] private GameObject bg;
    [SerializeField] private GameObject Enc_fight;
    [SerializeField] private GameObject Enc_rest;
    [SerializeField] private GameObject Enc_unk;
    [SerializeField] private GameObject Enc_boss;
    private int[] enctypes = new int[4] { 0, 1, 20, 21 };
    private int encno = 1;
    private GameObject currGO;
    public List<Vector2> Map = new List<Vector2>();
    public static List<int> distr = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //distribution, max 10 wierszy, tutaj zapisywana jest ilosc encounterów na wiersz
    public static int[][] enc = new int[62][]; //max 7 wierszy, max 6 encounterów na wiersz + wiersz 0 (start) + wiersz 8 (boss)
    public List<GameObject> gOEncounter = new List<GameObject>();
    public float thickness = 10f;
    private static int rows = 0;



    public static int[] encType = {0, 0};
    public static int playerPosition = 0;



    void Start()
    {
        RectTransform bgrect = bg.GetComponent<RectTransform>();

        if(enc[1] == null)
        {
            CreateGameFromValues();

            for (int i = 0; enc[i] != null; i++) //tak d³ugo a¿ elementy istniej¹
            {
                distr[enc[i][0]] += 1; //zlicza iloœæ elementów w kazdym wierszu
                rows = rows < enc[i][0] ? enc[i][0] : rows; //zlicza ilosc wygenerowanych wierszy
            }
            rows += 2;

            createPaths();
        }
        for (int i = 0; enc[i] != null; i++)
        {
            Spawn(enc[i][0], distr[enc[i][0]], enc[i][1], enc[i][2], rows, bgrect, i);
        }
        unlockButtons();
        gOEncounter[0].transform.GetChild(0).gameObject.SetActive(true);
    }

    public void CreateGameFromValues()
    {
        int rows = 4;
        enc[0] = new int[] { 0, 1, 1, 0, 0, 0, 0, 0, 0, 0};
        createRow(1, 1);
        createRow(encno, 2);
        createRow(encno, 3);
        for (; Random.Range(1, 101) <= 85 && rows < 7; rows++)
        {
            createRow(encno, rows);
        }
        enc[encno] = new int[] { rows, 1, 3, 0, 0, 0, 0, 0, 6, 0};
    }

    private int ce() //create encounter
    {
        int k = Random.Range(0, 3);
        return k == 2 ? k + Random.Range(0, 2) : k;
    }
    private int secondenc() //create 2nd encounter
    {
        if (Random.Range(1, 4) == 1)
            return Random.Range(1, 6);
        else
            return 0;
    }

    private void createRow(int j, int k)
    {
        int enct = enctypes[ce()];
        if (enct == 0 || enct == 20)
            enc[j ] = new int[] { k, 1, enct, 0, 0, 0, 0, 0, Random.Range(1, 6),  secondenc()};
        else
            enc[j] = new int[] { k, 1, enct, 0, 0, 0, 0, 0, Random.Range(11, 13), 0};
        enct = enctypes[ce()];

        if (enct == 0 || enct == 20)
            enc[j + 1] = new int[] { k, 2, enct, 0, 0, 0, 0, 0, Random.Range(1, 6), secondenc()};
        else
            enc[j + 1] = new int[] { k, 2, enct, 0, 0, 0, 0, 0, Random.Range(11, 13), 0};

        encno += 2;
        for (int i = 2; Random.Range(1, 3) == 1 && i < 4; i++)
        {
            enct = enctypes[ce()];
            if (enct == 0 || enct == 20)
                enc[j + i] = new int[] { k, i + 1, enct, 0, 0, 0, 0, 0, Random.Range(1, 6), secondenc()};
            else
                enc[j + i] = new int[] { k, i + 1, enct, 0, 0, 0, 0, 0, Random.Range(11, 13), 0};
            encno++;
        }
    }

    private void createPaths()
    {
        for(int i = 0; enc[i+1] != null; i++) //losowo tworzy œciezki od 0
        {
            for(int j = 0; j <= Random.Range(1, distr[enc[i][0]+1]); j++)
            {
                int k = Random.Range(distr.Take(enc[i][0]+1).Sum(), distr.Take(enc[i][0] + 2).Sum());
                enc[i][j + 3] = k;
            }
        }
        for(int i = distr.Sum()-2; i > 0; i--) //uzupe³nia braki w sciezkach, zapobiega slepym zaulkom
        {
            for(int j = distr.Take(enc[i][0]-1).Sum(); !enc[j].Skip(3).Take(6).Any(x => x == i); j++)
            {
                if(enc[j+1][0] == enc[i][0])
                {
                    int k = Random.Range(distr.Take(enc[i][0] - 1).Sum(), distr.Take(enc[i][0]).Sum());
                    for(int l = 5; l < 9; l++)
                    {
                        if (enc[k][l] == 0)
                        {
                            enc[k][l] = i; break;
                        }
                    }
                    break;
                }
            }
        }
    }

    public void Spawn(int a, int b, int c, int d, int e, RectTransform bgrect, int f) //automatycznie dodaje encounter na mape w zale¿noœci od tabelki enc[][]
    {
        Vector3 position = new Vector3((float)bgrect.rect.width /-2 , (float)bgrect.rect.height/ -2, 0);
        position += new Vector3(((float)bgrect.rect.width / (1 + b)) * (c),
            ((float)bgrect.rect.height / e) * (1 + a)
            , 0); //dzia³a, nie ruszaæ
        switch (d)
        {
            case 0:
                currGO = Enc_fight;
                break;
            case 1:
                currGO = Enc_rest;
                break;
            case 20:
                currGO = Enc_unk;
                break;
            case 21:
                currGO = Enc_unk;
                break;
            case 3:
                currGO = Enc_boss;
                break;
        }
        GameObject obj = Instantiate(currGO, bgrect, true);
        obj.transform.localPosition = position;
        obj.GetComponent<MapEncounterIdHolderPasser>().holdId(f);
        gOEncounter.Add(obj);
        Map.Add(obj.GetComponent<RectTransform>().position); //zamienia end[][] w liste stworzonych na jej postawie GameObjectow
    }

    public void mapMove(GameObject go)
    {
        gOEncounter[playerPosition].transform.GetChild(0).gameObject.SetActive(false); //znika znacznik
        playerPosition = go.GetComponent<MapEncounterIdHolderPasser>().encounterId;
        gOEncounter[playerPosition].transform.GetChild(0).gameObject.SetActive(true); //pojawia znacznik
        encType = new int[]{ enc[go.GetComponent<MapEncounterIdHolderPasser>().encounterId][8], enc[go.GetComponent<MapEncounterIdHolderPasser>().encounterId][9]}; //statyczna wartosc typu encounteru
        unlockButtons();
        SceneManager.LoadScene(1);
    }

    public void unlockButtons()
    {
        foreach (var obj in gOEncounter)
        {
            obj.GetComponent<Button>().interactable = false;
        }
        for(int j = 3; enc[playerPosition][j] != 0; j++)
        {
            gOEncounter[enc[playerPosition][j]].GetComponent<Button>().interactable = true;
        }
    }
}
