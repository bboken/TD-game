using UnityEngine;
using System.Collections;

public class debugcar : MonoBehaviour {
    private Transform point;
    
    private Transform Decar;
	// Use this for initialization
	void Start () {
        
        Decar = this.transform;
    }
	
	// Update is called once per frame
	void Update () {
        point = Enemy.pos;
        if (point == null)
            return;
        Decar.position = point.position;
        Decar.rotation = point.rotation;
    }
}
