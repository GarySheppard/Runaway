using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody bulletRigidBody;

    public float speed = 100f;
    public PlayerController playerController;

    private void Awake()
    {
        bulletRigidBody = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        bulletRigidBody.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (other.CompareTag("Zombie"))
        {
            // Zombie hit!
            Debug.Log("Zombie Hit!");
            ZombieStats zombieStats = other.GetComponent<ZombieStats>();
            if (zombieStats != null)
            {
                zombieStats.TakeDamage(35);
                Debug.Log("Hit");
            }
        }
        else if (other.CompareTag("Breakable"))
        {
            Debug.Log("Object Hit");
            GameObject obj = other.gameObject;
            StartCoroutine(playerController.Explosion(obj, 8f, 0f));
        }
        else if (other.CompareTag("Explodable"))
        {
            Debug.Log("Object Hit");
            GameObject obj = other.gameObject;
            StartCoroutine(playerController.CheckExplosiveCollider(other, other.transform.position, 0f));
            StartCoroutine(playerController.Explosion(obj, 10f, 0f));
        }
        //Debug.Log("Hit man");
    }
}
