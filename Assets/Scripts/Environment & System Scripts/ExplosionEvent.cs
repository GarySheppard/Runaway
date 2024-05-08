using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEvent: MonoBehaviour
{
    public GameObject explosionEffect;
    
    public void Explode(Vector3 location)
    {
        GameObject particles = Instantiate(explosionEffect, location, Quaternion.identity);
        AudioManager.instance.PlaySound("explosion_" + Random.Range(1, 3));
        Destroy(particles, 1.5f);
    }
}
