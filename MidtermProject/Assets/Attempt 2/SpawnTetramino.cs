using UnityEngine;

public class SpawnTetramino : MonoBehaviour
{

    [SerializeField] private GameObject[] Tetrominoes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        NewTetramino();
    }

    // Update is called once per frame

    public void NewTetramino() {
        Instantiate(Tetrominoes[Random.Range(0, 1)], transform.position, Quaternion.identity);
    }
}
