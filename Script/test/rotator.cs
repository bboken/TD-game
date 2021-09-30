using UnityEngine;
using System.Collections;

public class rotator : MonoBehaviour {
  
    public float speed = 1;
    void Start()
    {

    }

    void Update()
    {

        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime * speed);
    }
    public void getspeed(float newspeed)
    {
        speed = newspeed;
    }
   
}
