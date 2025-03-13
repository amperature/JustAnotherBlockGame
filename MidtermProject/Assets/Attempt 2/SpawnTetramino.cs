using UnityEngine;
using System.Collections.Generic;
public class SpawnTetramino : MonoBehaviour
{

    [SerializeField] private GameObject[] Tetrominoes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //private List<GameObject> bag = new List<GameObject>();

    List<GameObject> bag = new List<GameObject>();
    //public List<GameObject> next = new List<GameObject>();
    int currentPiece = 1;
    public int nextPiece = 1;
    GameObject RenderNext;

    public GameObject Randomizer() {
        Debug.Log("current: " + currentPiece);
        Debug.Log("next: " + nextPiece);
        currentPiece = nextPiece;
        nextPiece = Random.Range(0, bag.Count-1);
        if (bag.Count == 0)
        {
            bag.AddRange(Tetrominoes);
        }
        RenderNext = bag[nextPiece];
        GameObject p = bag[currentPiece];
        bag.RemoveAt(nextPiece);
        return p;
    }

    void Start()
    {
        NewTetramino();
    }


    // Update is called once per frame

    public void NewTetramino() {
        Instantiate(Randomizer(), transform.position, Quaternion.identity);
    }
}
