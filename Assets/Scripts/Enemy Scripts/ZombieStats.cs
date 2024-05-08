using UnityEngine;
using System.Collections;

public class ZombieStats : MonoBehaviour
{
    private Animator zombieAnimator;
    private ZombieController zombieControl;

    public int maxHealth = 100;
    public int currentHealth;
    public Item itemToBoost;

    private bool isAlive = true;
    private float destroyDelay = 3f;

    void Start()
    {
        zombieAnimator = GetComponent<Animator>();
        zombieControl = GetComponent<ZombieController>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if ((currentHealth <= 0)
            && (isAlive))
        {
            isAlive = false;
            StartCoroutine(DeathCoroutine());
            if (itemToBoost != null)
            {
                itemToBoost.value++;
            }
        }
        else if (currentHealth > 0)
        {
            StartCoroutine(zombieControl.GetHurt());
        }
    }

    /*
    private void Die()
    {
        Debug.Log("Zombie has died!");
        Destroy(zombieControl);
        zombieAnimator.SetBool("isInteracting", false);
        zombieAnimator.SetInteger("interactionState", -1);
        zombieAnimator.SetTrigger("die");
        Invoke("DestroyZombie", destroyDelay);
    }
    */

    IEnumerator DeathCoroutine()
    {
        Debug.Log("Zombie has died!");
        yield return new WaitForSecondsRealtime(0.5f);
        Destroy(zombieControl);
        zombieAnimator.SetBool("isInteracting", false);
        zombieAnimator.SetInteger("interactionState", -1);
        zombieAnimator.SetTrigger("die");
        Invoke("DestroyZombie", destroyDelay);
    }

    private void DestroyZombie()
    {
        Destroy(gameObject);
    }
}
