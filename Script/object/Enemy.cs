using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    
    private Transform target;
    private Enemy targetEnemy;
    public static Transform pos;

    public GameObject deathEffect;
    [Header("Attributes")]
    public float startHp;
    private float Hp;
    //public int Ap;
    public float startSpeed = 0f;
    public int unit = 0;
    public bool isAttack = false;
    public  bool isTarget = false;
    public bool isBoss;
    public AudioClip hit;
    public AudioClip money;

    public float range = 15f;
    public float startfireRate = 1f;
    private float fireRate;
    private float fireCountdown = 0f;

    [Header("Enemy setting")]
    public int coin = 0;
    //public int cost;
    //public int income;

    [Header("Unity Stuff")]
    public Image healthBar;
    public string TowerTag = "Turret";
    public GameObject bulletPrefab;
    
    [HideInInspector]
    public float Speed;

    [SerializeField]
    Transform _destination;
    Transform _startpoint;
    UnityEngine.AI.NavMeshAgent _navMesgAgent;
    private bool isDead = false;
    private Vector3 Position;
    private Vector3 newPosition;
    private int temp = 0;
    private bool isplay;



    //private Transform target;

    // Use this for initialization
    void Start () {
        temp = Timer4.gettime();
        if (isBoss && spawnBoss.getBossDie()!=0)
        {
            Hp = startHp * Mathf.Pow(spawnBoss.getincreaseRate() , (1 + spawnBoss.getBossDie()) );
             Debug.Log("HP " + startHp + " increaseRate " + spawnBoss.getincreaseRate() + " increaseVal " + (1 + spawnBoss.getBossDie()));
        }

        _destination = endPoint.EndPoint;
        _startpoint = startPoint.StartPoint;
        if(!isBoss)
            Hp = startHp;
        Speed = startSpeed;
        fireRate = startfireRate;
        Position = newPosition;

        _navMesgAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        _navMesgAgent.speed = Speed;

        if (_navMesgAgent ==null)
        {
            Debug.LogError("the nav mesg is miss by " + gameObject.name);
        }
        else
        {
            SetDestination();
        }
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }
    void Update()
    {
        if (this.tag == "debug")
            pos = this.transform;
        if (isBoss)
            Debug.Log("HP " + startHp + " increaseRate " + spawnBoss.getincreaseRate() + " BossDie " + (1 + spawnBoss.getBossDie()));

        if (temp == Timer4.gettime())
            _navMesgAgent.speed = Speed;
        SetDestination();
        Position = newPosition;
        newPosition = this.transform.position;
        //Debug.Log("Speed" + Speed);

        //Debug.Log("target"+target);
        debug();
        attact();

    }

    private void SetDestination()
    {
        if(_destination != null)
        {
            Vector3 targetVector = _destination.transform.position;
            _navMesgAgent.SetDestination(targetVector);
        }
    }

    public bool getisTarget()
    {
        return isTarget;
    }


    public void TakeDamage(float amount)
    {
        Hp -= amount;
        healthBar.fillAmount = Hp / startHp;
        if(!isplay)
            StartCoroutine(play(hit));
        // Debug.Log(Hp +"  "+ startHp+"="+ Hp / startHp);
        // Debug.Log("HP"+healthBar.fillAmount);

        if (Hp <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Slow(float pct)
    {
        Speed = startSpeed * (1f - pct);
    }

    void Die()
    {
        isDead = true;

        PlayerStats.Money += coin;
        if (isBoss)
        {
            spawnBoss.resetTime();
            spawnBoss.KillBoss();
            
        }
        
        musicManager.playclip(money);
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EndPoint")
        {
            
            _navMesgAgent.enabled= false;
            //Debug.Log("TP");
            this.transform.position = _startpoint.position;
            if(this.tag=="Enemy")
            {
                if (!isBoss)
                    PlayerStats.decreaseLife(1);
                else
                    PlayerStats.decreaseLife(30);
            }
            //Debug.Log("TPed");
            _navMesgAgent.enabled = true;

        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(TowerTag);
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

        if (nearestEneny != null && shortestDistance <= range)
        {
            target = nearestEneny.transform;
            targetEnemy = nearestEneny.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {

            bullet.Seek(target);

        }
       

    }

    private void debug()
    {
        if (this.tag == "debug")
        {

            //Debug.Log("Distance"+Vector3.Distance(newPosition, Position));
            if (Vector3.Distance(newPosition, Position) < 0.001)
            {
                isAttack = true;
            }

        }
    }
    private void attact()
    {
        if (isAttack)
        {
            if (target != null)
                _navMesgAgent.speed = 0f;
            if (fireCountdown <= 0f)
            {

                Shoot();

                UpdateTarget();
                fireCountdown = 1f / fireRate;
            }
            temp = Timer4.gettime() + 1;
        }
        fireCountdown -= Time.deltaTime;
        if (this.tag == "debug")
            if (target == null)
                isAttack = false;
    }

    IEnumerator play(AudioClip clip)
    {
        isplay = true;
        musicManager.playclip(clip);

        yield return new WaitForSeconds(0.15f);
        isplay = false;
    }

 
}
