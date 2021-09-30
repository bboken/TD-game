using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class musicManager : MonoBehaviour
{
    
    private float volume;
    static AudioSource AS;
    public AudioClip[] music;
    public Toggle[] tog;
    public Slider musicSlider;
    public GameObject btnOn;
    public GameObject btnOff;
    public bool isOption;
    private int clickNum = 0;
    private int musicmum;


    // Use this for initialization
    void Start()
    {

        AS = GetComponent<AudioSource>();
        AS.volume = getVolume();
        AS.clip = music[PlayerPrefs.GetInt("musicmumber", 0)];
        AS.Play();
        
        if (isOption)
        {
            if (PlayerPrefs.GetInt("musicmumber", 0) == 0)
                tog[0].isOn = true;
            else if (PlayerPrefs.GetInt("musicmumber", 0) == 1)
                tog[1].isOn = true;
            else if (PlayerPrefs.GetInt("musicmumber", 0) == 2)
                tog[2].isOn = true;


            //DontDestroyOnLoad(this.gameObject);

            if (getVolume() != 0)
            {
                turnOn();
            }
            else
            {
                turnOff();
            }
        }
    }


    // Update is called once per frame
    void Update()
    {

        if (isOption)
        {
            if (getVolume() != 0)
            {
                turnOn();
                PlayerPrefs.SetFloat("isON", 1);
            }
            else
            {
                turnOff();
            }
        }
    }

    public float getVolume()
    {
        return PlayerPrefs.GetFloat("volumeData", 0.5f);
    }

    public void setVolume(float newVolume)
    {

        if (volume != newVolume)
        {
            volume = newVolume;
            PlayerPrefs.SetFloat("volumeData", volume);
            PlayerPrefs.Save();

        }

    }

    //toggle
    public void onClick()
    {
        clickNum += 1;
        PlayerPrefs.SetFloat("isON", clickNum % 2);
        if (getisval())
        {
            turnOn();


        }
        else
        {
            turnOff();

        }
    }

    public void turnOff()
    {
        setVolume(AS.volume);
        btnOn.gameObject.SetActive(false);
        btnOff.gameObject.SetActive(true);
        AS.volume = 0;
        musicSlider.value = 0;
    }

    public void turnOn()
    {
        
        btnOn.gameObject.SetActive(true);
        btnOff.gameObject.SetActive(false);
        musicSlider.value = getVolume();
        AS.volume = getVolume();
    }

    public bool getisval()
    {
        if (PlayerPrefs.GetFloat("isON", 1) == 1)
            return true;
        else
            return false;

    }

    //music
    public void toMusic1()
    {
        AS.Stop();
        AS.clip = music[0];
        AS.Play();
        PlayerPrefs.SetInt("musicmumber", 0);


    }
    public void toMusic2()
    {
        AS.Stop();
        AS.clip = music[1];
        AS.Play();
        PlayerPrefs.SetInt("musicmumber", 1);
    }
    public void toMusic3()
    {
        AS.Stop();
        AS.clip = music[2];
        AS.Play();
        PlayerPrefs.SetInt("musicmumber", 2);
    }

    public static void playclip(AudioClip clip)
    {
        AS.PlayOneShot(clip);
    }
    

}
