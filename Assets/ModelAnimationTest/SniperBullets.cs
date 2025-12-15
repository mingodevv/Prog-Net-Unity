using UnityEngine;

public class SniperBullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 100;
    public float lifeTime = 5f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        rb.linearVelocity = transform.forward * speed;
        
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
            return;
        
        HeroCore hero = collision.gameObject.GetComponent<HeroCore>();
        if (hero != null)
        {
            hero.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}