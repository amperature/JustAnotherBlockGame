using UnityEngine;
using System.Collections;

public class PieceController : MonoBehaviour
{
    [SerializeField] private SpawnTetramino spawner;
    [SerializeField] private Transform HoldContainer;
    private float previousTime;
    public float fallTime = 0.1f;
    public float softDropTime = 0.01f;
    private TetrisBlock CurrentPiece;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private AudioClip pieceDrop;
    [SerializeField] private AudioClip lineClear;
    //[SerializeField] private AudioClip borderHit;

    //public IEnumerator PieceSlow;

    void ResetTime() {
        fallTime = 0.1f;
    }

    IEnumerator PieceSlow() {
            if (Input.GetKeyDown(KeyCode.A)) {

            fallTime = 0.001f;
            Debug.Log("yeah");
            }
        yield return new WaitForSeconds(0.2f);

        if (Input.GetKeyDown(KeyCode.A)) {
            fallTime = 0.1f;
        }
        //fallTime = 1f;

    }

    void Start()
    {
        CurrentPiece = spawner.NewTetramino();
        //slowPiece = StartCoroutine("PieceSlow");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameBehavior.Instance.State == Utilities.GameplayState.Play) {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                CurrentPiece.transform.position += new Vector3(-1, 0, 0);
                if(!CurrentPiece.ValidMove()) {
                    CurrentPiece.transform.position -= new Vector3(-1, 0, 0); //redo this code
                }
            }

            else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                CurrentPiece.transform.position += new Vector3(1, 0, 0);
                if(!CurrentPiece.ValidMove()) {
                    CurrentPiece.transform.position -= new Vector3(1, 0, 0);
                }
            }

            else if(Input.GetKeyDown(KeyCode.UpArrow)) {
                CurrentPiece.transform.RotateAround(CurrentPiece.transform.TransformPoint(CurrentPiece.rotationPoint), new Vector3(0, 0, -1), 90);
                if(!CurrentPiece.ValidMove()) {
                CurrentPiece.transform.RotateAround(CurrentPiece.transform.TransformPoint(CurrentPiece.rotationPoint), new Vector3(0, 0, -1), -90);
                }
            }

            if(Time.time - previousTime > (
                Input.GetKey(KeyCode.DownArrow) ? 
                softDropTime : //soft drop speed
                fallTime //normal gravity
                )) 
            {
                CurrentPiece.CheckForLines();
                CurrentPiece.transform.position += new Vector3(0, -1, 0);
                if(!CurrentPiece.ValidMove()) {
                    CurrentPiece.transform.position -= new Vector3(0, -1, 0);
                    CurrentPiece.AddToGrid();
                    CurrentPiece.enabled = false;
                    CurrentPiece = spawner.NewTetramino();
                    //FindAnyObjectByType<SpawnTetramino>().NewTetromino();
                }
                previousTime = Time.time;
            
            }
                StartCoroutine(PieceSlow());

            //if (Input.GetKeyDown(KeyCode.A)) {
            //}

            CheckForHold();
        }

    }
    void CheckForHold() {
        if (Input.GetKeyDown(KeyCode.C)) {
            if (spawner.HoldPiece == null) {
                spawner.HoldPiece = CurrentPiece;
                CurrentPiece = spawner.NewTetramino();
            }
            else {
                (CurrentPiece, spawner.HoldPiece) = (spawner.HoldPiece, CurrentPiece);
            }
            spawner.HoldPiece.transform.position = HoldContainer.position;
            spawner.HoldPiece.transform.rotation = Quaternion.identity;
            CurrentPiece.transform.position = spawner.transform.position;
            //spawner.HoldPiece.transform.RotateAround(spawner.HoldPiece.transform.TransformPoint(spawner.HoldPiece.rotationPoint), new Vector3(0, 0, 0), 0);
        }
    }
}
