using UnityEngine;
using System.Collections;

public class title : MonoBehaviour {
    public GameObject[] mod;
    public Transform point;
    public int rate;
    public GameObject eff;

    private GameObject used;
    private int i1;
    private int i2;
    private int temp;
    // Use this for initialization
    void Start () {
        temp = rate;
        i1 = getNum();
        //Debug.Log(i);
        GameObject _used = (GameObject)Instantiate(mod[i1], point.position, mod[i1].transform.rotation);
        used = _used;
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(temp +"  time  "+Timer4.gettime());
        if (temp == Timer4.gettime())
        {
            temp += rate;
            do
            {
                i2 = getNum();
                Debug.Log(i1+"i2"+i2);
            }
            while (i1 == i2);
            i1 = i2;
            //Debug.Log(i1);
            Destroy(used);

            GameObject _used = (GameObject)Instantiate(mod[i1], point.position, mod[i1].transform.rotation);
            used = _used;

            GameObject effect = (GameObject)Instantiate(eff, point.position, Quaternion.identity);
            Destroy(effect, 2f);
        }
    }
    private int getNum()
    {
        return Random.Range(0, mod.Length - 1);
    }
}
