using UnityEngine;
using System.Collections;

public class Timer4 : MonoBehaviour {
    //long
    static int timer_i;
    //static bool istime = false;
	// Use this for initialization
	void Start () {
        timer_i = 0;
        InvokeRepeating("timer4", 1f, 1f);
        
    }
    void timer4()
    {
        timer_i += 1;
       // Debug.Log(timer_i);
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public static int gettime()
    {
        return timer_i;
    }
    //long
}
