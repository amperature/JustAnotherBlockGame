using UnityEngine;
using System.Collections.Generic;
public class NextTetramino : MonoBehaviour
{
    [SerializeField] private TetrisBlock[] Tetrominoes;
    List<TetrisBlock> bag = new List<TetrisBlock>();

    private TetrisBlock nextPiece; // Prefab reference
    private GameObject nextPieceInstance; //The Clone Itself

    public TetrisBlock Randomizer() {
        FillBag();
        TetrisBlock p = bag[0];
        bag.RemoveAt(0);
        return p;
    }

    void FillBag() {
        if (bag.Count == 0)
        {
            bag.AddRange(Tetrominoes);
            bag.Shuffle();
        }
        
    }
    void Update()
    {
        RenderNext();
    }

    public void RenderNext() {
   
        if (bag.Count == 0) {
            FillBag();
        }
     if (nextPiece != bag[0]) {
        if (nextPieceInstance != null) {
            Destroy(nextPieceInstance);
        }

        nextPieceInstance = Instantiate(bag[0].gameObject, transform.position, Quaternion.identity);

        nextPiece = bag[0];
     }
    }
}
