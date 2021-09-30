using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private string sesname;
    private void Start()
    {
        getses();
    }
    private void Update()
    {
        //print(getses());
    }
    public void turnOption(string ses)
    {
        sesname = ses;
        setses(ses);
        Application.LoadLevel("optionses");
    }
    public void ToGame()
    {

        Application.LoadLevel(getses());
    }


    public void ToSes(string ScneneName)
    {
        Application.LoadLevel(ScneneName);
    }

    public void setses(string ses)
    {
        PlayerPrefs.SetString("sesname", ses);
    }
    public string getses()
    {
        return PlayerPrefs.GetString("sesname", "mainses");
    }
}
