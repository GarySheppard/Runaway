using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    private Animator playerAnimator;
    private PlayerController playerController;
    private Interactor interactor;
    private bool isAlive = true;

    public int maxHealth = 100;
    public int currentHealth;
    public GameObject gameover;

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        interactor = GetComponent<Interactor>();
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
        }
        AudioManager.instance.PlaySound("player_hurt_"+Random.Range(1,3));
        Debug.Log(currentHealth);
    }



    public void Heal(int healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
    }

    IEnumerator DeathCoroutine()
    {
        Debug.Log("Player has died!");
        Destroy(playerController);
        Destroy(interactor);
        playerAnimator.SetBool("isSprinting", false);
        playerAnimator.SetBool("isCrouching", false);
        playerAnimator.SetBool("isAttacking", false);
        playerAnimator.SetBool("isAiming", false);
        playerAnimator.SetBool("matchToInteraction", false);
        playerAnimator.SetBool("isInteracting", false);
        playerAnimator.SetTrigger("die");

        /*
         * Supposed to dynamically wait for the animator to transition from the current state to the Dying state, but it's not working as intended,
         * so it simply waits for a few seconds to account for the time to transition
        //Waits until transition to Dying state to end
        yield return new WaitUntil(() => playerAnimator.GetAnimatorTransitionInfo(0).normalizedTime >= 1.0f);
        Debug.Log("Waited for transition");

        //Waits until Dying animation state is over
        yield return new WaitUntil(() => playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
        Debug.Log("Waited for animation");
        */

        yield return new WaitForSecondsRealtime(4.5f);

        EndGame();
    }

    private void EndGame()
    {
        Time.timeScale = 0f;
        gameover.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
