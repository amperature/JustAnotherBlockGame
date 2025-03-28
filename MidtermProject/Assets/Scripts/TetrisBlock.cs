using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    public static int height = 20;
    public static int width = 10;
    
    private static Transform[,] grid = new Transform[10, 20];

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.hierarchyCount == 0) {
             Destroy(this.gameObject);
        }

    }
    public void AddToGrid() 
    {
        //prints every move to a grid
        foreach (Transform children in transform)  {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;
            //Debug.Log(roundedY);
            CheckEndGame();
            //audioSource.PlayOneShot(pieceDrop);

        }
    }

void CheckEndGame()
{
    // For every block in the column
    for(int j = 0; j < width; j++)
    {
        // Check to see if there are any blocks in the highest row
        if( grid[j, height-1] != null)
        {
            // If there are blocks at the top, the game is over
            GameBehavior.Instance.GameOver();
        }
    }
}
    public void CheckForLines() {
        // loops through to check vertically
        for(int i = height -1; i >= 0; i--) {
            if(HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    public bool HasLine(int i) {
        //is it full??
        for(int j = 0; j < width; j++) {
            if (grid[j, i] == null) {
                return false;
            }
        }
        return true;
    }

    public void DeleteLine(int i) {
        //basically just gets rid of whatever has been filled up vertically
        for(int j = 0; j < width; j++) 
        {
            Destroy(grid[j, i].gameObject);
            grid [j,i] = null;
            GameBehavior.Instance.ScorePoint();
            //audioSource.PlayOneShot(lineClear);
        }
    }

    public void RowDown(int i) {
        //moves rows down.
        for (int y = i; y < height; y++) {

            for (int j = 0; j < width; j++) {
                
                if ((grid[j,y]) != null) {

                    grid [j,y - 1] = grid[j, y];
                    grid [j,y] = null;
                    grid [j,y - 1].transform.position -= new Vector3(0, 1, 0);
                    
                }
            }
        }
    }

    public bool ValidMove() {
        foreach (Transform children in transform) {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            //Debug.Log(roundedY);

            //if (roundedY >= 18 || roundedY > height) {
            //}
            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
            {
                //Debug.Log("gameOver!");
                return false;
            }
           
           if (grid[roundedX, roundedY] != null) 
           {
                return false;
           }

        }
        return true;
    }

}
