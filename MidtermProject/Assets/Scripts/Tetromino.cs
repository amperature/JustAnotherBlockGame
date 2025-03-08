using UnityEngine;
using UnityEngine.Tilemaps;

    public enum Tetromino {
        I,
        O,
        T,
        J,
        Z,
        S,
        L,
    }

   [System.Serializable] public class TetrominoData {
        public Tetromino tetromino;
        public Tile tile;
        public Vector2Int[] cells {
            get;
            private
            set;
        }

        public void Initialize() {
            this.cells = SRS.Cells[this.tetromino];
        }
    }
