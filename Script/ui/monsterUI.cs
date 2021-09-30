using UnityEngine;
using System.Collections;

public class monsterUI : MonoBehaviour {
    public GameObject[] pages;
    private int pagenum;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < pages.Length; i++)
        {
            //Debug.Log("pagenum" + i );
            pages[i].SetActive(false);
        }
        pagenum = 0;
        pages[pagenum].SetActive(true);
    }

    // Update is called once per frame
    void Update() {
    }

    public void down()
    {
        pages[pagenum].SetActive(false);
        if (pagenum > 0)
            pagenum --;
        else
            pagenum = 0;
        pages[pagenum].SetActive(true);
    }

    public void up()
    {
        pages[pagenum].SetActive(false);
        if (pagenum < pages.Length - 1)
            pagenum++;
        else
            pagenum = pages.Length - 1;
        pages[pagenum].SetActive(true);
    }

    public void hideUI()
    {
        pages[pagenum].SetActive(false);
    }
    public void showUI()
    {
        pages[pagenum].SetActive(true);
    }
}
