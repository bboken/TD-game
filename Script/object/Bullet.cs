using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
   
    private Transform target;

    [Header("Attributes")]
    public float speed = 70f;
    public int Ap;
    public float explosionRad = 0f;
    public GameObject imp;
    public bool isKnife;
    public bool isTurret;
    public bool isEnemy;
    public AudioClip bomb;
   

    public void Seek(Transform _target)
    {
        target = _target;

    }
	
	// Update is called once per frame
	void Update () {
	    if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFram = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFram)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFram, Space.World);
        

	}

    void HitTarget()
    {
        
            GameObject effectIns = (GameObject)Instantiate(imp, transform.position, transform.rotation);
            Destroy(effectIns, 1.5f);

            if (explosionRad > 0f)
            {
                Explode();
            }
            else
            {
                Damage(target,Ap);
            }
        
        Destroy(gameObject);

    }

    void Explode()
    {
        musicManager.playclip(bomb);
        Collider [] colliders = Physics.OverlapSphere(transform.position, explosionRad);
        foreach(Collider colliser in colliders)
        {
            if(colliser.tag == "Enemy")
            {
                if (!isKnife)
                {
                    float ExpAp = (explosionRad - Vector3.Distance(this.transform.position, colliser.transform.position)) / explosionRad;

                    // Debug.Log(ExpAp);
                    if (ExpAp >= 0.8f)
                        ExpAp = 1f;
                    if (ExpAp < 0.4f)
                        ExpAp = 0.4f;
                    Damage(colliser.transform, Ap * ExpAp);
                }else
                    Damage(colliser.transform, Ap);
            }
        }

    }

    void Damage (Transform oj,float ap)
    {
        if (isTurret)
        {
            Enemy e = oj.GetComponent<Enemy>();
            if (e != null)
            {
                e.TakeDamage(ap);
            }
        }
        if (isEnemy)
        {
            Turret T = oj.GetComponent<Turret>();

            if (T != null)
            {
                T.TakeDamage(ap);

            }
        }
    }

     void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRad);
    }

  
}
