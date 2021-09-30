using UnityEngine;
using System.Collections;

public class BuildManager : MonoBehaviour {
    
    public static BuildManager instance;
    
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }
    public GameObject buildEffect;
    public AudioClip acBuild;
    public GameObject sellEffect;
    public AudioClip acSell;
    public GameObject clickEffect;

    private static TurretInfo turretToBuild;
    private ground selectedground;
 
    public groundui groundUI;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    

    public void SelectNode(ground ground)
    {
        if (selectedground == ground)
        {
            DeselectNode();
            return;
        }
        clickEffect.transform.position = ground.transform.position;
        clickEffect.SetActive(true);

        selectedground = ground;
        
        groundUI.SetTarget(selectedground);
    }

    public void DeselectNode()
    {
        clickEffect.SetActive(false);
        selectedground = null;
        groundUI.Hide();
    }

    public void SelectTurretToBuild(TurretInfo turret)
    {
        turretToBuild = turret;
        DeselectNode();

    }

    public static TurretInfo GetTurrtToBuild()
    {
        return turretToBuild;
    }

    public void playBuild()
    {
        musicManager.playclip(acBuild);
    }

    public void playSell()
    {
        musicManager.playclip(acSell);
    }
    
}
