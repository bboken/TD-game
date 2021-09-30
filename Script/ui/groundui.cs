using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class groundui : MonoBehaviour {
    
    public BuildManager GM;
    public GameObject[] ui;
    public PlayerStats Player;
    [SerializeField]
    private monsterUI missionUI;
    private int page = 0;
    private string Ttype;
    
    [Header ("tower")]
    public TurretInfo[] bows;
    public TurretInfo[] knife;
    public Text[] nobulid;
    public Text[] canUpgrade;
    public Text[] chooseupgrade;
    public Text[] isUpgrade;
    public Text full;

    private ground target;

    public void SetTarget(ground _target)
    {
        
        for (int i = 0; i < ui.Length; i++)
            ui[i].SetActive(false);
        target = _target;

        
        if (_target.TowerLevel == 0)
        {
            nobulid[0].text = "$" + bows[0].cost;
            nobulid[1].text = "$" + knife[0].cost;
            Debug.Log("0");
            ui[0].SetActive(true);
            missionUI.hideUI();
            page = 0;
        }
        else if (_target.TowerLevel == 2 && _target.isUpgraded)
        {
           
            switch (_target.Goundtype)
            {
                case "bow":
                    chooseupgrade[0].text = "$" + bows[2].cost;
                    chooseupgrade[1].text = "$" + bows[4].cost;
                    break;
                case "knife":
                    chooseupgrade[0].text = "$" + knife[2].cost;
                    chooseupgrade[1].text = "$" + knife[4].cost;
                    break;
            }
            chooseupgrade[2].text = "$" + _target.gettwValue() / 2;
            Debug.Log("4");
            ui[4].SetActive(true);
            missionUI.hideUI();
            page = 4;
        }
        else if (_target.TowerLevel == 4 && _target.isUpgraded)
        {
            full.text = "$" + _target.gettwValue() / 2;
            Debug.Log("3");
            ui[3].SetActive(true);
            missionUI.hideUI();
            page = 3;
        }

        else if (_target.isUpgraded)
        {
            switch (_target.GetGoundtype())
            {
                case "bow":
                    if(target.TowerLevel==1)
                        isUpgrade[0].text = "$" + bows[1].cost;
                    else
                        isUpgrade[0].text = "$" + bows[5].cost;
                    break;
                case "knife":
                    if (target.TowerLevel == 1)
                        isUpgrade[0].text = "$" + knife[1].cost;
                    else
                        isUpgrade[0].text = "$" + knife[5].cost;
                    break;
            }
            isUpgrade[1].text = "$" + _target.gettwValue() / 2;
            Debug.Log("2");
            ui[2].SetActive(true);
            missionUI.hideUI();
            page = 2;
        }
        else if (!_target.isUpgraded)
        {
            canUpgrade[0].text = "$" + target.GetTurrtToBuild().upgradeCost;
            canUpgrade[1].text ="$"+ target.gettwValue()/2;
            Debug.Log("1");
            ui[1].SetActive(true);
            missionUI.hideUI();
            page = 1;
        }
    }
    public void close()
    {
        GM.DeselectNode();
    }

    public void Hide()
    {
        ui[page].SetActive(false);
        missionUI.showUI();
    }

    public void Build1()
    {
        target.BuildTurret(bows[0], "bow");
        BuildManager.instance.DeselectNode();
       
    }

    public void Build2()
    {
        target.BuildTurret(knife[0], "knife");
        BuildManager.instance.DeselectNode();
       
    }

    public void upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();

    }

    public void upgrade2()
    {
        //Debug.Log(target.Goundtype + "LV" + target.TowerLevel);
        switch (target.Goundtype)
        {
            case "bow":
               
                switch (target.TowerLevel)
                {
                    case 1:
                        target.UpgradeTurret(bows[1], "bow");
                        BuildManager.instance.DeselectNode();
                        break;
                    case 2:
                        target.UpgradeTurret(bows[2], "bow");
                        BuildManager.instance.DeselectNode();
                        break;
                    case 3:
                        target.UpgradeTurret(bows[3], "bow");
                        BuildManager.instance.DeselectNode();
                        break;

                }
                break;
            case "knife":
                switch (target.TowerLevel)
                {
                    case 1:
                        target.UpgradeTurret(knife[1], "knife");
                        BuildManager.instance.DeselectNode();
                        break;
                    case 2:
                        target.UpgradeTurret(knife[2], "knife");
                        BuildManager.instance.DeselectNode();
                        break;
                    case 3:
                        target.UpgradeTurret(knife[3], "knife");
                        BuildManager.instance.DeselectNode();
                        break;

                }
                break;
            case "bomb":
                switch (target.TowerLevel)
                {
                   
                    case 3:
                        target.UpgradeTurret(bows[5], "bomb");
                        BuildManager.instance.DeselectNode();
                        break;

                }
                break;
            case "giant":
                switch (target.TowerLevel)
                {

                    case 3:
                        target.UpgradeTurret(knife[5], "giant");
                        BuildManager.instance.DeselectNode();
                        break;

                }
                break;
        }
    }

    public void upgrade3()
    {
        if (target.TowerLevel == 2)
        {
            switch (target.Goundtype)
            {
                case "bow":
                    target.UpgradeTurret(bows[4], "bomb");
                    BuildManager.instance.DeselectNode();
                    break;
                case "knife":
                    target.UpgradeTurret(knife[4], "giant");
                    BuildManager.instance.DeselectNode();
                    break;
            }
        }
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
  
}
