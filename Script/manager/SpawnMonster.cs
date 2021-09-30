using UnityEngine;
using System.Collections;

public class SpawnMonster : MonoBehaviour {
    
    public GameObject item;
     Transform spawnPoint;
    
    public PlayerStats player;
    private void Start()
    {
        spawnPoint = startPoint.StartPoint;
        Spawn(item,1);
    }
    public void Spawn(GameObject enemy,int val)
    {
        if (val <= 0)
            val = 1;
        for(int i =0;i<val;i++)
            Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }

}
