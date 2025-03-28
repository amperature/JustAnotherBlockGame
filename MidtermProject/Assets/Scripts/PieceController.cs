using UnityEngine;
using System.Collections;
using TMPro;

public class PieceController : MonoBehaviour
{
    [SerializeField] private SpawnTetramino spawner;
    [SerializeField] private Transform HoldContainer;
    private float previousTime;
    public float fallTime = 0.01f;
    public float softDropTime = 0.005f;
    private TetrisBlock CurrentPiece;
    //int progress = FindObjectOfType<GameBehavior>().Progress;

    //[SerializeField] private TextMeshProUGUI timeUI;

    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //public IEnumerator PieceSlow;

    void Start()
    {
        CurrentPiece = spawner.NewTetramino();
        //slowPiece = StartCoroutine("PieceSlow");
        //StartCoroutine("MyCoroutine");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameBehavior.Instance.State == Utilities.GameplayState.Play) {
            if (Input.GetKey(KeyCode.LeftArrow)) {
                timer += Time.deltaTime;
                if (timer > 0.1f) {
                    Movement(-1);
                    timer = 0.0f;
                }
            }

            else if (Input.GetKey(KeyCode.RightArrow)) { //use coroutine for movement left to right. set speed for frame
                timer += Time.deltaTime;
                if (timer > 0.1f) {
                    Movement(1);
                    timer = 0.0f;
                }
            }
                //t -= Time.deltaTime;
            if(Input.GetKeyDown(KeyCode.UpArrow)) {
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

            if (Input.GetKeyDown(KeyCode.Space)) {
                CurrentPiece.CheckForLines();
                CurrentPiece.transform.position += new Vector3(0, -15, 0);
                if(!CurrentPiece.ValidMove()) {
                    CurrentPiece.transform.position -= new Vector3(0, -15, 0);
                    CurrentPiece.AddToGrid();
                    CurrentPiece.enabled = false;
                    CurrentPiece = spawner.NewTetramino();
                }
            }

            CheckForHold();
        }

    }

    void Movement(int i) {
        CurrentPiece.transform.position += new Vector3(i, 0, 0);
        if(!CurrentPiece.ValidMove()) {
            CurrentPiece.transform.position -= new Vector3(i, 0, 0); //redo this code
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
