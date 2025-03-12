using UnityEngine;

public class Piece : MonoBehaviour
{
    public BoardBehavior board { get; private set; }
    public TetrominoData data { get; private set; }
    public Vector3Int[] cells { get; private set; }
    public Vector3Int position { get; private set; } //tilemaps use vector3int and not vector 2
    public int rotationIndex { get; private set; }

    public float stepDelay = 10f;
    public float lockDelay = 10f;

    private float stepTime;
    private float lockTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Initialize(BoardBehavior board, Vector3Int position, TetrominoData data) {
        this.board = board;
        this.data = data;
        this.position = position;
        this.rotationIndex = 0;
        this.stepTime = Time.time + this.stepDelay;
        this.lockTime = 0f;


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

        this.lockTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Z)) {
            Rotate(-1);
        } 
        else if (Input.GetKeyDown(KeyCode.X)) {
            Rotate(1);
        }


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
            HardDrop();
        }

        if (Time.time >= this.stepTime) {
            Gravity();
        }

        this.board.Set(this);
    }

    private void Gravity() {
        this.stepTime = Time.deltaTime + this.stepDelay;

        Move(Vector2Int.down);

        if(this.lockTime >= this.lockDelay) {
            Lock();
        }
    }

    private void HardDrop() {
        while (Move(Vector2Int.down)) {
            continue;
        }
    }

    private void Lock() {
        this.board.Set(this);
        this.board.ClearLines();
        this.board.SpawnPiece();
    }

    private bool Move(Vector2Int translation) {

        Vector3Int newPosition = this.position;
        newPosition.x += translation.x;
        newPosition.y += translation.y;

        bool valid = this.board.IsValidPosition(this, newPosition);

        if (valid) {
            this.position = newPosition;
            this.lockTime = 0f;
        }

        return valid;
    }

    private void Rotate(int direction) {

        int originalRotation = this.rotationIndex;
        this.rotationIndex += Wrap(this.rotationIndex + direction, 0, 4);

        ApplyRotation(direction);

        if (!TestWallKicks(this.rotationIndex, direction)) {
            originalRotation = this.rotationIndex;
            ApplyRotation(direction); 
        }
    }

    private void ApplyRotation(int direction) {        
        for (int i = 0; i < this.cells.Length; i++) {
            Vector3 cell = this.cells[i];

            int x, y;

            switch (this.data.tetromino) {
                case Tetromino.I:
                case Tetromino.O:
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;
                    x = Mathf.CeilToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction)); // im replacing this with sprites oh my god
                    y = Mathf.CeilToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction)); // im replacing this with sprites oh my god
                    
                    break;

                default:
                    x = Mathf.RoundToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction)); // im replacing this with sprites oh my god
                    y = Mathf.RoundToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction)); // im replacing this with sprites oh my god
                    break;
            }

            this.cells[i] = new Vector3Int(x, y, 0);
        }
    }

    private bool TestWallKicks(int rotationIndex, int rotationDirection) {
        int wallKickIndex = GetWallKickIndex(rotationIndex, rotationDirection);
        
        for (int i = 0; i < this.data.wallKicks.GetLength(1); i++) {
            
            Vector2Int translation = this.data.wallKicks[wallKickIndex, i];

            if (Move(translation)) {
                return true;
            }
        }

        return false;

    }
    
    private int GetWallKickIndex(int rotationIndex, int rotationDirection) {
        int wallKickIndex = rotationIndex * 2;
        
        if (rotationDirection < 0) {
            wallKickIndex--;
        }
        return Wrap(wallKickIndex, 0, this.data.wallKicks.GetLength(0));

    }

    private int Wrap(int input, int min, int max) {
        if (input < min) {
            return max - (min - input) % (max - min);
        }
        else {
            return min + (input - min) % (max - min);
        }
    }
}
