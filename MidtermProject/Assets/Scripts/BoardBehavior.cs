using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardBehavior : MonoBehaviour
{
    public TetrominoData[] tetrominos; //
    public Tilemap tilemap {get; private set; }

    public Piece activePiece {get; private set; }

    public Vector3Int spawnPosition;

    public Vector2Int boardSize = new Vector2Int(10, 20);

    public RectInt bounds {
        get 
        {
            Vector2Int position = new Vector2Int(-this.boardSize.x / 2, -this.boardSize.y / 2);
            return new RectInt(position, this.boardSize);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.activePiece = GetComponentInChildren<Piece>();

        for (int i = 0; i < this.tetrominos.Length; i++) {
            this.tetrominos[i].Initialize();
        }
    }

    // Update is called once per frame
    void Start()
    {
        SpawnPiece();
    }

    public void SpawnPiece() { 
        int random = Random.Range(0, this.tetrominos.Length); //randomizer
        TetrominoData data = this.tetrominos[random];

        this.activePiece.Initialize(this, this.spawnPosition, data);

        Set(this.activePiece);
    }


    public void Set(Piece piece) {

        for (int i = 0; i < piece.cells.Length; i++) {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, piece.data.tile);
        }
    }

    public void Clear(Piece piece) {

        for (int i = 0; i < piece.cells.Length; i++) {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, null);
        }
    }

    public bool IsValidPosition(Piece piece, Vector3Int position) {

        RectInt bounds = this.bounds;

        for (int i = 0; i < piece.cells.Length; i++) {

            Vector3Int tilePosition = piece.cells[i] + position;

            if (!bounds.Contains((Vector2Int)tilePosition)) {
                return false;
            }
            
            if (this.tilemap.HasTile(tilePosition)) {
                return false;
            }
        }
        return true;
    }  
}
