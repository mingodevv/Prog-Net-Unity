using UnityEngine;

public class BulletSkill1 : MonoBehaviour
{
    public GameObject impactVFX;
    public int damage = 50;
    public float lifeTime = 5f;
    public float vfxDuration = 2f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        HeroCore hero = collision.gameObject.GetComponent<HeroCore>();
        if (hero != null)
        {
            hero.TakeDamage(damage);
        }

        if (impactVFX != null)
        {
            GameObject vfxInstance = Instantiate(impactVFX, transform.position, Quaternion.identity);
            Destroy(vfxInstance, vfxDuration);
        }
        
        Destroy(gameObject);
    }
}