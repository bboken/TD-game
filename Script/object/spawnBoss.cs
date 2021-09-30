using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class spawnBoss : MonoBehaviour
{
    [Header("setting")]
    public GameObject Boss;
    public Text time;
    
    public int spawnRate;
    public GameObject winMeun;
    public  float increaseRate;
    public static float _increaseRate;
    public int Goal = 1;
    public AudioClip spawn;

    [Header("Manager")]
    public PauseMenu PM;
    public SpawnMonster SM;
    

    public static int BossDie;
    private static int _Time;
    private static Enemy Bossinfo;
    private int temp;
    public static bool iswin = false;
    private bool isplay = false;
    // Use this for initialization
    void Start()
    {
        
        _increaseRate = increaseRate;
        Bossinfo = Boss.GetComponent<Enemy>();
        _Time = 0;
        temp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!iswin && Goal == BossDie)
        {
            iswin = true;
            PM.PauseGame();
            winMeun.SetActive(true);
        }
        if( _Time< spawnRate) 
            time.text = "Time : " +( spawnRate - _Time);
        else
            time.text = "Boss is life";
        if (temp != Timer4.gettime())
        {
            StartCoroutine(Spawn());
            temp = Timer4.gettime();
        }
        if (_Time == spawnRate-3 && !isplay)
        {
            isplay = true;
            musicManager.playclip(spawn);
        }
        if (_Time==spawnRate)
        {
            isplay = false;
            SM.Spawn(Boss,1);
            _Time++;
        }

    }

    IEnumerator Spawn()
    {


        yield return new WaitForSeconds(1);
        _Time++;
        //Debug.Log("_Time" + _Time);
    }
    public static void resetTime()
    {
        _Time = 0;
    }
    public static void KillBoss()
    {
        BossDie++;
    }
    public static float getincreaseRate()
    {
        return _increaseRate;
    }
    public static int getBossDie()
    {
         return  BossDie;
    }
    public static int getBossVal()
    {
        if (BossDie % 30 < 10)
            return 0;
        else if (BossDie % 30 < 20)
            return 1;
        else if (BossDie % 30 >= 20)
            return 2;
        else
            return 0;
    }

}
