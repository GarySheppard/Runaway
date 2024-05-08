using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.AI;

//[RequireComponent(typeof(Animator), typeof(SphereCollider))]
public class ZombieController : MonoBehaviour
{
    private Animator ZombieAnimator;
    private ZombieSound zombieSound;
    private bool isInitialAnimationComplete = false;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private NavMeshAgent navMeshAgent;

    public Transform player;
    public float moveSpeed = 1f;
    public float rotationSpeed = 5f;
    public float resetDistanceThreshold = 20f;
    public int damage = 10;

    bool isChasing = false;
    bool canAttack = true;
    bool isGrunting = false;
    float attackCooldown = 3.0f; // Set the desired cooldown time
    

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        ZombieAnimator.SetBool("isChasing", true);
    //        isChasing = true;
            
    //    }
    //}

    //void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        ZombieAnimator.SetBool("isChasing", false);
    //        isChasing = false;
    //    }
    //}

    void Awake()
    {
        ZombieAnimator = GetComponent<Animator>();

        if (ZombieAnimator == null)
            Debug.Log("Animator could not be found");
    }

    private void Start()
    {
        ZombieAnimator = GetComponent<Animator>();
        zombieSound = GetComponent<ZombieSound>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        closerOrNot();
        if (isChasing && HasPlayerWithinRange())
        {
            RotateTowardsPlayer();
            MoveTowardsPlayer();
            OnInitialAnimationComplete();
            if (!isGrunting)
            {
                
                isGrunting = true;
                StartCoroutine(zombieSound.ZombieGrunt());
            }
            
        }
        else
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer > resetDistanceThreshold)
            {
                float distanceToInitial = Vector3.Distance(new Vector3(transform.position.x, initialPosition.y, transform.position.z), initialPosition);
                if (distanceToInitial > 2)
                {
                    ResetToInitialPosition();
                }
                else
                {
                    ResetToInitial();
                }
            }
        }

        if ((IsPlayerTouchingZombie())
            && (ZombieAnimator.GetCurrentAnimatorStateInfo(0).IsTag("CanAttack")))
        {
            ZombieAnimator.SetInteger("interactionState", 1);
            ZombieAnimator.SetBool("isInteracting", true);

            // Check cooldown before attacking
            if (canAttack)
            {
                AttackPlayer();
                StartCoroutine(AttackCooldown());
            }
        }
        else
        {
            ZombieAnimator.SetInteger("interactionState", -1);
            ZombieAnimator.SetBool("isInteracting", false);
        }

        if (!isChasing)
        {
            isGrunting = false;
            if (isInitialAnimationComplete)
            {
                RemoveRotationAndTransitionToIdle();
                OnInitialAnimationNotComplete();
            }
        }
    }

    void ResetToInitialPosition()
    {
        Vector3 direction = (initialPosition - transform.position).normalized;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        Quaternion yOnlyRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        transform.rotation = yOnlyRotation;
        float angle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);

        if (angle > 0)
        {
            // Right
            ZombieAnimator.SetFloat("velx", 0.5f);
            ZombieAnimator.SetFloat("vely", 0.5f);
        }
        else if (angle < 0)
        {
            // Left
            ZombieAnimator.SetFloat("velx", -0.5f);
            ZombieAnimator.SetFloat("vely", 0.5f);
        }

        transform.position = Vector3.MoveTowards(transform.position, initialPosition, moveSpeed * Time.deltaTime);
    }

    void ResetToInitial()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        ZombieAnimator.SetFloat("velx", 0f);
        ZombieAnimator.SetFloat("vely", 0f);
    }

    void RotateTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion toRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        Quaternion yOnlyRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        transform.rotation = yOnlyRotation;
        float angle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);

        if (angle > 0)
        {
            //Right
            ZombieAnimator.SetFloat("velx", 0.5f);
            ZombieAnimator.SetFloat("vely", 0.5f);
        }
        else if (angle < 0)
        {
            //Left
            ZombieAnimator.SetFloat("velx", -0.5f);
            ZombieAnimator.SetFloat("vely", 0.5f);
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 targetPosition = new Vector3(player.position.x, 0, player.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        ZombieAnimator.SetFloat("velx", 0f);
        ZombieAnimator.SetFloat("vely", 0.8f);
    }

    bool HasPlayerWithinRange()
    {
        return Vector3.Distance(transform.position, player.position) < 10f;
    }

    void AttackPlayer()
    {
        float hitboxSize = 0.5f;
        Vector3 hitboxPosition = transform.position + transform.forward * 0.5f;

        Collider[] hitColliders = Physics.OverlapBox(hitboxPosition, new Vector3(hitboxSize * 4f, hitboxSize * 5f, hitboxSize * 8f));

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                // Player hit!
                Debug.Log("Player Hit!");
                PlayerStats playerStats = hitCollider.GetComponent<PlayerStats>();
                if (playerStats != null)
                {
                    playerStats.TakeDamage(damage);
                }
            }
        }
    }

    bool IsPlayerTouchingZombie()
    {
        float distance = Vector3.Distance(new Vector3(transform.position.x, player.position.y, transform.position.z), player.position);
        return distance < 2.5f;
    }

    void OnInitialAnimationComplete()
    {
        isInitialAnimationComplete = true;
    }

    void OnInitialAnimationNotComplete()
    {
        isInitialAnimationComplete = false;
    }

    void RemoveRotationAndTransitionToIdle()
    {
        ZombieAnimator.SetFloat("velx", 0f);
        ZombieAnimator.SetFloat("vely", 0f);
        StartCoroutine(TransitionToIdle());
    }

    IEnumerator TransitionToIdle()
    {
        yield return new WaitForSeconds(2.0f);
        ZombieAnimator.SetFloat("velx", 0f);
        ZombieAnimator.SetFloat("vely", 0f);
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public IEnumerator GetHurt()
    {
        canAttack = false;
        ZombieAnimator.SetInteger("interactionState", -1);
        ZombieAnimator.SetBool("isInteracting", false);
        ZombieAnimator.SetFloat("velx", 0);
        ZombieAnimator.SetFloat("vely", 0);
        ZombieAnimator.SetBool("isChasing", false);
        
        //Waits a little bit before starting the Zombie_Hurt animation to line up with most attack animations
        //yield return new WaitForSecondsRealtime(0.5f);

        ZombieAnimator.SetTrigger("hurt");

        //Waits a bit for the Zombie_Hurt animation to be transitioned to and play out
        yield return new WaitForSecondsRealtime(4.5f);

        canAttack = true;
    }

    void closerOrNot()
    {
        float distance = Vector3.Distance(new Vector3(transform.position.x, player.position.y, transform.position.z), player.position);
        //Debug.Log(distance);
        if (distance <= 10f)
        {
            ZombieAnimator.SetBool("isChasing", true);
            isChasing = true;
        }
        else
        {
            ZombieAnimator.SetBool("isChasing", false);
            isChasing = false;
        }
    }
}
