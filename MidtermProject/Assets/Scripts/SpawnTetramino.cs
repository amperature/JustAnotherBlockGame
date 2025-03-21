using UnityEngine;
using System.Collections.Generic;
public class SpawnTetramino : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] NextTetramino nexttetramino;
    public TetrisBlock HoldPiece;
    TetrisBlock RenderNext;
    public TetrisBlock NewTetramino() {
            
            TetrisBlock piece = nexttetramino.Randomizer();
            GameObject go = Instantiate(piece.gameObject, transform.position, Quaternion.identity);
            
            GameBehavior.Instance.AddProgress();
        return go.GetComponent<TetrisBlock>();
    }
    

}
