using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 5;
    public float speed = 5f;
    public float lifeTime = 5f;

    private Vector3 shootDirection;

    private void Start()
    {
        shootDirection = transform.forward;
        
        gameObject.layer = LayerMask.NameToLayer("Bullet");

        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.position += shootDirection * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
            return;

        HeroCore hero = collision.gameObject.GetComponent<HeroCore>();
        if (hero != null)
        {
            hero.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}