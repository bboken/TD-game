using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ground : MonoBehaviour {
    
    public Material[] hoverColor;
    public Vector3 positionOffset;
    public PlayerStats player;
    public BuildManager buildManager;
    

    [HideInInspector]
    public string Goundtype = null;
    [HideInInspector]
    public int TowerLevel = 0;
    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretInfo turretToBuild;
    [HideInInspector]
    public bool isUpgraded = false;

    UnityEngine.AI.NavMeshObstacle navOj;

    private Turret _turret;
    private float hp = 0;
    private float temphp;
    private Renderer rend;
    private int type;

    private int twValue=0;


    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        navOj = GetComponent<UnityEngine.AI.NavMeshObstacle>();
        temphp = hp;
        type = 0;

    }
	
	// Update is called once per frame
	void Update () {
        //if (turret != null)Debug.Log(twValue);
      
        if(turret != null)
        {
            hp = turret.GetComponent<Turret>().getHp();
            //Debug.Log(hp);
            if (hp <= 0 && temphp != hp)
            {
                breakTurret();
            }
            temphp = hp;
        }
        if (type != spawnBoss.getBossVal())
        {
            type = spawnBoss.getBossVal();
            this.rend.material = hoverColor[type];
        }

    }

    public TurretInfo GetTurrtToBuild()
    {
        return turretToBuild;
    }

    public string GetGoundtype()
    {
        return Goundtype;
    }

   
    
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
    
    void OnMouseDown()
    {
      
        buildManager.SelectNode(this);
         
    }
 
   public void BuildTurret(TurretInfo blueprint,string type)
    {
        if (player.getMoney() >= blueprint.cost)
        {
            twValue += blueprint.cost;
            player.buyOj(blueprint.cost);
            UpgradeHp();
            TowerLevel = 1;
            //Debug.Log("BuildTurret");
            if (turret != null)
                return;
            Goundtype = type;
            navOj.enabled = true;
            GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), blueprint.prefab.transform.rotation);
            turret = _turret;

            turretToBuild = blueprint;
            buildManager.playBuild();
            GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
            Destroy(effect, 1f);
        }
        else
            return;
        // Debug.Log("Turret build!");
    }

    public void UpgradeTurret()
    {
        if (player.getMoney() >= turretToBuild.upgradeCost)
        {
            twValue += turretToBuild.upgradeCost;
            player.buyOj(turretToBuild.upgradeCost);
            UpgradeHp();
            //TowerLevel += 1;
            Destroy(turret);

            GameObject _turret = (GameObject)Instantiate(turretToBuild.upgradedPrefab, GetBuildPosition(), turretToBuild.upgradedPrefab.transform.rotation);
            turret = _turret;

            buildManager.playBuild();
            GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
            Destroy(effect, 1f);

            isUpgraded = true;
        }
        else
            return;
        // Debug.Log("Turret Upgraded!");
    }

    public void UpgradeTurret(TurretInfo blueprint, string type)
    {
        if (player.getMoney() >= blueprint.cost)
        {
            twValue += blueprint.cost;
            player.buyOj(blueprint.cost);
            UpgradeHp();
            //Debug.Log("UpgradeTurret");
            TowerLevel += 1;
            Goundtype = type;
            Destroy(turret);

            GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), blueprint.prefab.transform.rotation);
            turret = _turret;

            turretToBuild = blueprint;
            buildManager.playBuild();
            GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
            Destroy(effect, 1f);

            isUpgraded = false;
        }
        else
            return;
        //Debug.Log("Turret rebuild!");
    }

    public void SellTurret()
    {
        //PlayerStats.Money += turretToBuild.GetSellAmount();
        TowerLevel = 0;
        navOj.enabled = false;
        buildManager.playSell();
        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 2f);
        //Spawn a cool effect
        player.buyOj(-twValue/2);
        Destroy(turret);
        turretToBuild = null;
        twValue = 0;


    }

    public void breakTurret()
    {
       
        TowerLevel = 0;
        navOj.enabled = false;
        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 2f);
        //Spawn a cool effect

        Destroy(turret);
        turretToBuild = null;
        twValue = 0;

    }

    void UpgradeHp()
    {
        hp = 0;
        temphp = hp;
    }

    public  int gettwValue()
    {
        return twValue;
    }
    
}
