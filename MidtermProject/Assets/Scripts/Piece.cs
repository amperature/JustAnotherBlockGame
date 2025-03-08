using UnityEngine;

public class Piece : MonoBehaviour
{
    public BoardBehavior board { get; private set; }
    public TetrominoData data { get; private set; }
    public Vector3Int[] cells { get; private set; }
    public Vector3Int position { get; private set; } //tilemaps use vector3int and not vector 2

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Initialize(BoardBehavior board, Vector3Int position, TetrominoData data) {
        this.board = board;
        this.data = data;
        this.position = position;

        if (this.cells == null) {
            this.cells = new Vector3Int[data.cells.Length];
        }

        for (int i = 0; i < data.cells.Length; i++) {
            this.cells[i] = (Vector3Int)data.cells[i];
        }
    }


    // Update is called once per frame
    private void Update()
    {
        this.board.Clear(this);

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            Move(Vector2Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            Move(Vector2Int.right);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            Move(Vector2Int.down);         
        }
        
        if (Input.GetKeyDown(KeyCode.Space)) {
            while (Move(Vector2Int.down)) {
                continue;
            }
        }

        this.board.Set(this);
     
    }

    private bool Move(Vector2Int translation) {

        Vector3Int newPosition = this.position;
        newPosition.x += translation.x;
        newPosition.y += translation.y;

        bool valid = this.board.IsValidPosition(this, newPosition);

        if (valid) {
            this.position = newPosition;
        }

        return valid;
    }
}
