using UnityEngine;

class M_GameManager : MonoBehaviour
{
    public GameObject board = null;
    public GameObject tileInstance = null;
    private BoardManager boardManager;
    private AtomManager atomManager;
    void Start()
    {
        boardManager = new BoardManager(board, tileInstance);
        atomManager = new AtomManager();

        boardManager.buildBoard(8);
    }
}
