using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HomingMissile : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 25f;
    public float rotateSpeed = 120f;  
    public int damage = 50;
    public float detectionRadius = 10f;
    public float lifeTime = 5f;
    public string targetTag = "Player";

    private Transform owner;   
    private Transform target;  
    private Rigidbody rb;

    public void SetOwner(Transform shooter)
    {
        owner = shooter;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        
        rb.linearVelocity = transform.forward * speed;
        
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        if (target == null)
            FindTarget();
        
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);

            rb.linearVelocity = transform.forward * speed;
        }
    }

    private void FindTarget()
    {
        GameObject[] heroes = GameObject.FindGameObjectsWithTag(targetTag);

        float closestDist = Mathf.Infinity;
        Transform nearest = null;

        foreach (var heroObj in heroes)
        {
            Transform hero = heroObj.transform;
            
            if (hero == owner)
                continue;

            float dist = Vector3.Distance(transform.position, hero.position);
            if (dist < detectionRadius && dist < closestDist)
            {
                closestDist = dist;
                nearest = hero;
            }
        }

        if (nearest != null)
            target = nearest;
    }

    private void OnCollisionEnter(Collision collision)
    {
        HeroCore hero = collision.gameObject.GetComponent<HeroCore>();
        if (hero != null && hero.transform != owner)
        {
            hero.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }
        
        Destroy(gameObject);
    }
}
