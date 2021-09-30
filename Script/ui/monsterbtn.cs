using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class monsterbtn : MonoBehaviour {
    
    [Header("Button setting")]
    public Text TxtNum;
    public Text TxtIncome;
    public int startnum;
    public int Max = 40;
    public bool isrepeat;
    public int RespawnRate = 1;
    public SpawnMonster SM;
    public PlayerStats player;

    [Header("mission setting")]
    
    public int cost;
    public int income;
    public float RecoverRate = 1f;
    public MissionInfo[] monster;
 


    private int num;
    private int temp;
    


    // Use this for initialization
    void Start () {
        TxtIncome.text =""+income;
        num = startnum;
        TxtNum.text = num + "/" + Max;
        temp = RespawnRate;
    }
	// Update is called once per frame
	void Update () {
        // Debug.Log(temp +""+ Timer4.gettime() + "num"+ num);
        TxtNum.text = num + "/" + Max;
        //Debug.Log(num + "/" + max);
        if(isrepeat)
            if (temp == Timer4.gettime())
                increasenum();
       
    }

    void increasenum()
    {
        if (num < Max)
            num++;
        temp += RespawnRate;
    }
    public void Spawn()
    {
        //Debug.Log("click");
        if (!PlayerStats.isspawn)
        {
            if (player.getIncome() >= -income)
            {
                if (num > 0)
                {
                    PlayerStats.isspawn = true;
                    if (player.getMoney() >= cost)
                    {
                        player.buyOj(cost);
                        player.modifyIncome(income);
                        num--;
                        putMon();
                    }
                }
            }
        }
    }
    private void putMon()
    {
            StartCoroutine(Spawn(RecoverRate));
    }
    


   IEnumerator Spawn(float T)
    {
       
        for (int i = 0; i < monster.Length; i++)
            for (int j = 0; j < monster[i].spawnNum; j++)
            {
                
                SM.Spawn(monster[i].prefab, monster[i].SpawnRate);
                yield return new WaitForSeconds(T);
                if (i == monster.Length - 1 && j == monster[i].spawnNum - 1)
                    PlayerStats.isspawn = false;
            }
        
    }
    
}
