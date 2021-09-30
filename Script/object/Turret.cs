using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Turret : MonoBehaviour {

    private Transform target;
    private Enemy targetEnemy;
   

    [Header("Attributes")]
    public float startHp ;
    [HideInInspector]
    public float Hp;
    
    public float range = 15f;
    public float startFireRate = 1f;
    private float fireRate ;
    private float fireCountdown = 0f;
    public int AttactTarget = 0;
    public float Hprecover = 1f;
    public AudioClip attact;
    
    [Header("Use Knife")]

    public bool isknife = false;
    public float startAp;
    public GameObject effect;
    [HideInInspector]
    public float Ap;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public Image healthBar;
    public GameObject hpbar;

    public Transform partToRotate;
    public float turnSpeed = 0.1f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    private bool isplay;
    // Use this for initialization
    void Start () {
        
        Hp = startHp;
        fireRate = startFireRate;
        Ap = startAp;
        if(!isknife)
            InvokeRepeating("UpdateTarget", 0f, 0.5f);
      
    }
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEneny = null;

        foreach (GameObject enemy in enemies)
        {
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEneny = enemy;
                }
        }

        if(nearestEneny != null && shortestDistance <= range)
        {
            target = nearestEneny.transform;
            targetEnemy = nearestEneny.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Hp == startHp)
        {
            hpbar.SetActive(false);
        }
        if (Hp != startHp)
        {
            hpbar.SetActive(true);
            TakeDamage(-(startHp*0.01f*Hprecover) * Time.deltaTime);
        }
        if (!isknife && target == null)
        {
            return;
        }
       
        if (isknife)
        {
           Knife();
        }else
        {
            LockOnTarget();
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Knife()
    {
        //targetEnemy.TakeDamage(Ap * Time.deltaTime);
        if (!isplay)
        {
            StartCoroutine(play(attact));
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
       
        foreach (GameObject enemy in enemies)
        {
            //Debug.Log("att"+ enemy);
            float distanceToEnemy = Vector3.Distance(this.transform.position, enemy.transform.position);
          
            if (distanceToEnemy <= range)
            {
                //Debug.Log("att ing1" + enemy);
                GameObject effectIns = (GameObject)Instantiate(effect, enemy.transform.position, enemy.transform.rotation);
                Destroy(effectIns, 1.5f);
                enemy.GetComponent<Enemy>().TakeDamage(Ap*Time.deltaTime);

            }
           // Debug.Log("att end" + enemy);
        }
        
       
    }

    void Shoot()
    {
       GameObject bulletGO=(GameObject) Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        musicManager.playclip(attact);
        if (bullet != null)
            bullet.Seek(target);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void TakeDamage(float amount)
    {
        Hp -= amount;
        healthBar.fillAmount = Hp / startHp;
       // Debug.Log("hit");
    }
    public float getHp()
    {
        return Hp;

    }
    IEnumerator play(AudioClip clip)
    {
        isplay = true;
        musicManager.playclip(clip);
        
        yield return new WaitForSeconds(0.1f);
        isplay = false;
    }
}
