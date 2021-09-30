using UnityEngine;
using System.Collections;

public class TP : MonoBehaviour {
    public GameObject Gm;
    Transform spawnPoint;


    // Use this for initialization

    private void Start()
    {
        spawnPoint = startPoint.StartPoint;
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy"|| other.gameObject.tag == "debug")
        {
            Debug.Log("TP");
            other.transform.position = spawnPoint.position;
            PlayerStats.decreaseLife();
        }
    }
    */

}
