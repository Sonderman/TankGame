using UnityEngine;

public class Bomb : MonoBehaviour
{

    public GameObject explosionPrefab;
    public float radius = 7f;
    public float explosionForce = 500f;
    void Start()
    {
        Destroy(gameObject, 2f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        if (health)
        {
            health.TakeDamage(10);
        }
        CreateExplosionEffect();
    }

    private void CreateExplosionEffect()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Collider[] nearbyObjects= Physics.OverlapSphere(transform.position, radius);
        foreach (var item in nearbyObjects)
        {
            Rigidbody rb = item.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.AddExplosionForce(explosionForce,transform.position,radius);
            }
        }
    }
}
