using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    private Animator playerAnimator;
    private CharacterController playerCharController;
    private bool isOnLadder = false;
    private bool isFalling = false;
    private Collider Collider;
    private int lowOrHigh = -1;
    private bool isDocument = false;
    private bool isGenerator = false;
    private float maxDistance = 20f;
    private bool isOnCaltrops = false;
    private Transform caltropsTransform;
    private List<GameObject> equippedItems = new List<GameObject>();
    private bool isTakingInput = true;
    private float turnVelocity;
    private bool isCursorVisible = true;

    public Transform mainCamera;
    public float mouseSensitivity = 0.5f;
    public GameObject[] supportItemBank;
    public Transform supportItemLocation;
    public LayerMask groundLayer;
    public float fallSpeed = 5f;
    public GameObject caltropsPrefab;
    public GameObject explosivePrefab;

    public ExplosionEvent exploder;
    public bool isAiming = false;
    public bool isAttacking = false;
    public int currentWeapon = 0;

    public Item pistol;
    public Item grenade;
    public Item caltrops;

    void Start()
    {
        //Cursor.visible = false;
        playerAnimator = GetComponent<Animator>();
        playerCharController = GetComponent<CharacterController>();
        ThirdPersonShooter thirdPersonShooter = GetComponent<ThirdPersonShooter>();

        GameObject goItem;
        foreach (GameObject item in supportItemBank)
        {
            goItem = Instantiate(item, supportItemLocation, true);
            if (goItem.CompareTag("BaseballBat"))
            {
                goItem.transform.localPosition = new Vector3(-0.1f, 0f, -0.11f);
                goItem.transform.localRotation = Quaternion.Euler(94f, -17f, -62f);
            }
            else if (goItem.CompareTag("Pistol"))
            {
                goItem.transform.localPosition = new Vector3(0.13f, -0.07f, -0.02f);
                goItem.transform.localRotation = Quaternion.Euler(19f, 96f, 89f);
            }
            else if (goItem.CompareTag("Caltrops"))
            {
                goItem.transform.localPosition = new Vector3(0f, -0.01f, 0f);
                goItem.transform.localRotation = Quaternion.Euler(0f, 0f, -23f);
            }
            else if (goItem.CompareTag("Explosive"))
            {
                goItem.transform.localPosition = new Vector3(-0.06f, -0.04f, -0.04f);
                goItem.transform.localRotation = Quaternion.Euler(85f, 44f, -5f);
            }

            equippedItems.Add(goItem);
        }

        pistol.value = 6;
        caltrops.value = 2;
        grenade.value = 1;
    }

    void Update()
    {
        //Debug.DrawRay(transform.position, transform.forward * maxDistance, Color.red, 1f);
        if(isCursorVisible && Input.GetKeyDown(KeyCode.X))
        {
            Cursor.visible = false;
            isCursorVisible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else if(!isCursorVisible && Input.GetKeyDown(KeyCode.X))
        {
            Cursor.visible = true;
            isCursorVisible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        HandleMovement();
        CheckGround();
        ApplyFall();
        ChangeWeapon();
        if (isTakingInput)
        {
            Combat();
            //Debug.Log("Ran");
        }
        //CheckInteraction();
    }

    void FixedUpdate()
    {
        //MatchTarget();
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 input = new Vector2(horizontalInput, verticalInput).normalized;

        if (isOnCaltrops && caltropsTransform != null)
        {
            input *= 0.5f;
            if (Vector3.Distance(transform.position, caltropsTransform.position) > 2f)
            {
                isOnCaltrops = false;
                caltropsTransform = null;
            }
        }

        if ((input.y >= 0.1f)
            && (playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Movement")))
        //((input.x >= 0.1f) && ((Input.GetAxis("Mouse X") <= -0.05f) || (Input.GetAxis("Mouse X") >= 0.05f)))
        {
            //transform.localRotation = Quaternion.Euler(-mouseInput.y, mouseInput.x, 0f);
            float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, 0.1f);
            transform.localRotation = Quaternion.Euler(0f, angle, 0f);
            //Vector3 dirX = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized;
            //playerCharController.Move(dirX * 5f * Time.deltaTime);
        }

        playerAnimator.SetFloat("velx", input.x, 0.2f, Time.deltaTime);
        playerAnimator.SetFloat("vely", input.y, 0.3f, Time.deltaTime);
        //Debug.Log("velx: " + input.x + ", vely: " + input.y);

        bool isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        playerAnimator.SetBool("isSprinting", isSprinting);
        if (isSprinting && input.y >= 1f)
        {
            playerAnimator.SetFloat("vely", input.y + 1f, 0.3f, Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.C))
        {
            playerAnimator.SetBool("isCrouching", true);
        }
        else
        {
            playerAnimator.SetBool("isCrouching", false);
        }
    }

    void Combat()
    {
        if (currentWeapon == 0)
        {
            playerAnimator.SetBool("isAiming", false);
        }

        if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Interaction"))
        {
            if (currentWeapon >= 1 && Input.GetMouseButtonDown(1))
            {
                if (isAiming == false)
                {
                    playerAnimator.SetBool("isAiming", true);
                    isAiming = true;
                }
                else
                {
                    playerAnimator.SetBool("isAiming", false);
                    isAiming = false;
                }

            }

            if ((!playerAnimator.GetBool("isInteracting")) && (Input.GetMouseButtonDown(0)))
            {
                if (currentWeapon == 0)
                {
                    StartCoroutine(AttackCoroutine());
                    isAttacking = false;
                    Melee();
                    isTakingInput = false;
                    StartCoroutine(BlockInputForSeconds(3f));
                }
                else if (currentWeapon == 1)
                {
                    if (pistol.value > 0)
                    {
                        StartCoroutine(AttackCoroutine());
                        //Pistol();
                        isTakingInput = false;
                        StartCoroutine(BlockInputForSeconds(1.3f));
                        pistol.value--;
                    }
                }
                else if (currentWeapon == 2)
                {
                    if (caltrops.value > 0)
                    {
                        StartCoroutine(AttackCoroutine());
                        isAttacking = false;
                        Caltrops();
                        isTakingInput = false;
                        StartCoroutine(BlockInputForSeconds(2f));
                        caltrops.value--;
                    }
                }
                else if (currentWeapon == 3)
                {
                    if (grenade.value > 0)
                    {
                        StartCoroutine(AttackCoroutine());
                        isAttacking = false;
                        Explosive();
                        isTakingInput = false;
                        StartCoroutine(BlockInputForSeconds(2f));
                        grenade.value--;
                    }
                }
                //else if (currentWeapon == 4)
                //{
                //    //Noise maker
                //    StartCoroutine(AttackCoroutine());
                //}
                //isTakingInput = false;
                //StartCoroutine(BlockInputForSeconds(2f));
            }
        }
    }

    IEnumerator BlockInputForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        isTakingInput = true;
    }

    IEnumerator AttackCoroutine()
    {
        playerAnimator.SetBool("isAttacking", true);
        isAttacking = true;
        //playerAnimator.SetBool("isAiming", true);
        AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = stateInfo.length / 2;
        yield return new WaitForSeconds(animationDuration);
        playerAnimator.SetBool("isAttacking", false);
        //playerAnimator.SetBool("isAiming", false);
    }

    void Melee()
    {
        float hitboxSize = 0.7f;
        Vector3 hitboxPosition = transform.position + transform.forward * 0.5f;

        Collider[] hitColliders = Physics.OverlapBox(hitboxPosition, new Vector3(hitboxSize * 4f, hitboxSize * 5f, hitboxSize * 12f));

        foreach (var hitCollider in hitColliders)
        {
            //Debug.Log(hitCollider.name);
            if (hitCollider.CompareTag("Zombie"))
            {
                // Zombie hit!
                Debug.Log("Zombie Hit!");
                ZombieStats zombieStats = hitCollider.GetComponent<ZombieStats>();
                if (zombieStats != null)
                {
                    zombieStats.TakeDamage(20);
                }
            }
            else if (hitCollider.CompareTag("Breakable"))
            {
                GameObject obj = hitCollider.gameObject;
                StartCoroutine(Explosion(obj, 5f, 0.7f));
            }
            else if (hitCollider.CompareTag("Explodable"))
            {
                GameObject obj = hitCollider.gameObject;
                StartCoroutine(CheckExplosiveCollider(hitCollider, hitCollider.transform.position, 0.7f));
                StartCoroutine(Explosion(obj, 8f, 0.7f));
            }
           
        }
    }

    void Pistol()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            Debug.Log("Ray Hit Point: " + hit.point);
            if (hit.collider.CompareTag("Zombie"))
            {
                // Zombie hit!
                Debug.Log("Zombie Hit!");
                ZombieStats zombieStats = hit.collider.GetComponent<ZombieStats>();
                if (zombieStats != null)
                {
                    zombieStats.TakeDamage(50);
                }
            }
            else if (hit.collider.CompareTag("Breakable"))
            {
                Debug.Log("Object Hit");
                GameObject obj = hit.collider.gameObject;
                StartCoroutine(Explosion(obj, 8f, 0f));
            }
            else if (hit.collider.CompareTag("Explodable"))
            {
                Debug.Log("Object Hit");
                GameObject obj = hit.collider.gameObject;
                StartCoroutine(CheckExplosiveCollider(hit.collider, hit.collider.transform.position, 0f));
                StartCoroutine(Explosion(obj, 10f, 0f));
            }
        }
    }

    void Caltrops()
    {
        
        Vector3 spawnPosition = transform.position + transform.forward * 25f;
        GameObject caltropsObject = Instantiate(caltropsPrefab, spawnPosition, Quaternion.identity);
        float lifespan = 10f;
        Destroy(caltropsObject, lifespan);
    }

    void Explosive()
    {
        
        Vector3 spawnPosition = transform.position + transform.forward * 20f;
        GameObject explosiveObject = Instantiate(explosivePrefab, spawnPosition, Quaternion.identity);
        Collider explosiveCollider = explosiveObject.GetComponent<Collider>();
        Vector3 initialPosition = explosiveObject.transform.position;
        float lifespan = 5f;
        Destroy(explosiveObject, lifespan);

        StartCoroutine(CheckExplosiveCollider(explosiveCollider, initialPosition, 4.9f));
    }

    public IEnumerator CheckExplosiveCollider(Collider explosiveCollider, Vector3 initialPosition, float waitTime)
    {
        bool colliderValid = explosiveCollider != null;
        
        if (colliderValid)
        {
            // Define the maximum distance for damage application
            float maxDamageDistance = 15f;

            // Get the position of the explosive object
            Vector3 explosivePosition = explosiveCollider.bounds.center;

            /* A waitTime of 0f will essentially mark things we want to happen instantaneuosly, like a bullet breaking things
             * If colliders move to fast, there's a chance "yield return new Wait...()" won't execute properly, so this accounts for that 
             */
            if (waitTime > 0f)
            {
                yield return new WaitForSeconds(waitTime);
            }

            exploder.Explode(explosivePosition);

            // Check the distance to players and zombies
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>(); // Find all objects in the scene

            foreach (var obj in allObjects)
            {
                if (obj.CompareTag("Player") || obj.CompareTag("Zombie") || obj.CompareTag("Breakable") || obj.CompareTag("Explodable"))
                {
                    Transform objTransform = obj.transform;
                    float distance = Vector3.Distance(explosivePosition, objTransform.position);

                    if (obj.CompareTag("Player") && distance < maxDamageDistance)
                    {
                        PlayerStats playerStats = obj.GetComponent<PlayerStats>();
                        if (playerStats != null)
                        {
                            playerStats.TakeDamage(25);
                        }
                    }
                    else if (obj.CompareTag("Zombie") && distance < maxDamageDistance)
                    {
                        ZombieStats zombieStats = obj.GetComponent<ZombieStats>();
                        if (zombieStats != null)
                        {
                            zombieStats.TakeDamage(50);
                        }
                    }
                    //Check if the object is breakable
                    else if (obj.CompareTag("Breakable") && distance < maxDamageDistance)
                    {
                        StartCoroutine(Explosion(obj, 20f, 0.1f));
                    }
                    else if (obj.CompareTag("Explodable") && distance < maxDamageDistance)
                    {
                        StartCoroutine(CheckExplosiveCollider(obj.GetComponent<Collider>(), obj.transform.position, 0.1f));
                        StartCoroutine(Explosion(obj, 20f, 0.1f));
                    }
                }
            }
        }
    }

    public IEnumerator Explosion(GameObject obj, float explosionForce, float waitTime)
    {
        PropBreak propBreak = obj.GetComponent<PropBreak>();
        if (propBreak != null)
        {
            /* A waitTime of 0f will essentially mark things we want to happen instantaneuosly, like a bullet breaking things
             * If colliders move to fast, there's a chance "yield return new Wait...()" won't execute properly, so this accounts for that 
             */
            if (waitTime > 0f)
            {
                yield return new WaitForSeconds(waitTime);
            }
            
            GameObject brokenProp = propBreak.Break();
            PropExplosion propExplosion = brokenProp.GetComponent<PropExplosion>();
            if (propExplosion != null)
            {
                propExplosion.BreakDown(explosionForce);
            }
        }
        yield return null;
    }

    void ChangeWeapon()
    {

        for (int i = 1; i < 5; i++)
        {
            if (Input.GetKeyDown(i.ToString()))
            {
                currentWeapon = i - 1;
                playerAnimator.SetInteger("sItemEquip", currentWeapon);
                if (currentWeapon == 0)
                {
                    isAiming = false;
                }
                equippedItems[currentWeapon].SetActive(true);
            }
            else if (i - 1 != currentWeapon)
            {
                equippedItems[i - 1].SetActive(false);
            }
        }
        /*
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentWeapon--;
            if (currentWeapon < 0)
                currentWeapon = 3;
            playerAnimator.SetInteger("sItemEquip", currentWeapon);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            currentWeapon++;
            if (currentWeapon > 4)
                currentWeapon = 0;
            playerAnimator.SetInteger("sItemEquip", currentWeapon);
        }
        */
        }

        void CheckGround()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 0.1f, groundLayer))
        {
            isFalling = false;
        }
        else
        {
            isFalling = true;
        }
    }

    void ApplyFall()
    {
        if (playerAnimator.GetInteger("interactionState") == -1 && isFalling)
        {
            GetComponent<CharacterController>().Move(Vector3.down * fallSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            isOnLadder = true;
            Collider = other;
        }
        else if (other.CompareTag("LowItem"))
        {
            Collider = other;
            lowOrHigh = 0;
        }
        else if (other.CompareTag("HighItem"))
        {
            Collider = other;
            lowOrHigh = 1;
        }
        else if (other.CompareTag("Document"))
        {
            isDocument = true;
            Collider = other;
        }
        else if (other.CompareTag("Generator"))
        {
            isGenerator = true;
            Collider = other;
        }
        else if (other.CompareTag("Scaffolding"))
        {
            GameObject collidedObject = other.gameObject;
            if (!collidedObject.GetComponent<Rigidbody>()) // Check if the GameObject already has a Rigidbody component
            {
                collidedObject.AddComponent<Rigidbody>(); // Add Rigidbody component to the GameObject
            }
            other.isTrigger = false;
            AudioManager.instance.PlaySound("scaffolding_collapse");
            Collider = other;
        }
        if (other.CompareTag("Caltrops"))
        {
            isOnCaltrops = true;
            caltropsTransform = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            isOnLadder = false;
            Collider = null;
        }
        else if (other.CompareTag("LowItem"))
        {
            lowOrHigh = -1;
            Collider = null;
        }
        else if (other.CompareTag("HighItem"))
        {
            lowOrHigh = -1;
            Collider = null;
        }
        else if (other.CompareTag("Document"))
        {
            isDocument = false;
            Collider = null;
        }
        else if (other.CompareTag("Generator"))
        {
            isGenerator = false;
            Collider = null;
        }
        else if (other.CompareTag("Scaffolding"))
        {
            
            Collider = null;
        }
        if (other.CompareTag("Caltrops"))
        {
            isOnCaltrops = false;
            caltropsTransform = null;
        }
    }

    void CheckInteraction()
    {
        if (isOnLadder && Input.GetKeyDown(KeyCode.Space))
        {
            if (IsPlayerOnObject() && IsFacingLadder())
            {
                StartCoroutine(InteractionCoroutine(0));
            }
            else
            {
                Debug.Log("Move closer to the ladder and face towards it.");
            }
        }
        else if (lowOrHigh == 0 && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(InteractionCoroutine(1));
        }
        else if (lowOrHigh == 1 && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(InteractionCoroutine(2));
        }
        else if (isDocument && Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(InteractionCoroutine(3));
        }
        else if (isGenerator && Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(InteractionCoroutine(4));
        }
    }

    public IEnumerator InteractionCoroutine(int state)
    {
        playerAnimator.SetBool("isInteracting", true);
        playerAnimator.SetInteger("interactionState", state);
        /*
         * Supposed to wait for animation to finish, but not working as intended
        AnimatorStateInfo stateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
        float animationDuration = stateInfo.length;
        Debug.Log(animationDuration);
        yield return new WaitForSeconds(animationDuration);
        */

        //This (or some simillarly small time) works to allow the anim time to update
        yield return new WaitForSecondsRealtime(0.1f);
        playerAnimator.SetBool("isInteracting", false);
        playerAnimator.SetInteger("interactionState", -1);
    }

    public void MatchTarget(GameObject target)
    {
        AnimatorStateInfo animState = playerAnimator.GetCurrentAnimatorStateInfo(0);

        if ((animState.IsName("Match_To_Interaction"))
            && (!playerAnimator.IsInTransition(0))
            && (!playerAnimator.isMatchingTarget))
        {
            float matchTargetAnimTime = animState.normalizedTime;

            Transform targetTrans = target.transform;
            playerAnimator.MatchTarget(targetTrans.position,
                targetTrans.rotation,
                AvatarTarget.Root,
                new MatchTargetWeightMask(new Vector3(1f, 0f, 1f), 1f),
                matchTargetAnimTime,
                1f);
        }
    }

    bool IsPlayerOnObject()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.5f);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("LadderObject"))
            {
                return true;
            }
        }

        return false;
    }

    bool IsFacingLadder()
    {
        Vector3 ladderDirection = (Collider.transform.position - transform.position).normalized;
        float dotProduct = Vector3.Dot(transform.forward, ladderDirection);
        Debug.Log(Vector3.Angle(transform.forward, ladderDirection));
        return dotProduct > 0f && (Vector3.Angle(transform.forward, ladderDirection) <= 60 && Vector3.Angle(transform.forward, ladderDirection) > 45);
    }




}
