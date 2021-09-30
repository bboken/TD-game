using UnityEngine;
using System.Collections;

public class bg : MonoBehaviour {

    public GameObject[] Terrain;
    public Material[] skybox;
    private int type;
    
	// Use this for initialization
	void Start () {
        type = 0;
        for(int i = 0; i < Terrain.Length; i++)
        {
            Terrain[i].SetActive(false);
        }
        Terrain[type].SetActive(true);
        RenderSettings.skybox = skybox[type];
    }
	
	// Update is called once per frame
	void Update () {
	if(type != spawnBoss.getBossVal())
        {
            type = spawnBoss.getBossVal();
            for (int i = 0; i < Terrain.Length; i++)
            {
                Terrain[i].SetActive(false);
            }
            Terrain[type].SetActive(true);
            RenderSettings.skybox = skybox[type];
        }
	}
}
