using UnityEngine;
using System.Collections;

public class lighting : MonoBehaviour
{
    public Light oj;
    private int val;
    // Use this for initialization
    void Start()
    {

        oj.intensity = 1;
        val = spawnBoss.getBossVal();
    }

    // Update is called once per frame
    void Update()
    {
        if (val != spawnBoss.getBossVal())
        {
            val = spawnBoss.getBossVal();
            switch (val)
            {
                case 0:
                    oj.intensity = 1.3f;
                    break;
                case 1:
                    oj.intensity = 0.5f;
                    break;
                case 2:
                    oj.intensity = 1;
                    break;
            }
        }
        else
            return;

    }
}

