using UnityEngine;
using System.Collections.Generic;
public class NextTetramino : MonoBehaviour
{

    //[SerializeField] private GameObject[] NextTetrominoes;

    // GameObject NextPiece() {
    //     GameObject p = SpawnTetramino.bag[nextPiece];
    //     return p;
    // }

    void Start()
    {
        RenderNext();
    }

    // Update is called once per frame

    public void RenderNext() {
       //Instantiate(NextPiece(), transform.position, Quaternion.identity);
    }
}
