using UnityEngine;
using System.Collections;

public class Gameover : MonoBehaviour
{
    public GameObject GameOverMeun;
    public GameObject GameOverMeun2;
    [Header("Manager")]
    public PauseMenu PauseM;
    public PlayerStats Player;
    private int endgame = 0;
    // Use this for initialization
    void Start()
    {
        PauseM.ResumeGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.getLife() > endgame)
            return;
        else
        {
            PauseM.PauseGame();
            if (spawnBoss.iswin)
                GameOverMeun2.SetActive(true);
            else
                GameOverMeun.SetActive(true);
        }
    }
}
