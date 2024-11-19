
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public AudioSource collisionattack;
    public float damage = 1f;

    private void Start()
    {
        collisionattack = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            collisionattack.Play();

            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}


