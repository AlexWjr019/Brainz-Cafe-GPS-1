using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomerSatisfactionTimer : MonoBehaviour
{
    CustomerAI ai;
    CustomerSpawner cs;

    //ALL ZOMBIE SHARED CODE
    public Image timer_radial_image;
    public float time_remaining;
    public float max_time = 5.0f;
    private bool isSitting = false; // Flag to track if the customer is sitting
    private bool isLeaving = false;

    public bool isAttacking = false; // Flag to track if the customer is currently chasing the player

    public string playerTag = "Chairs"; // Tag of the player object
    public float moveSpeed = 5f; // Speed at which the customer moves towards the player

    //DAMAGE CODE ONLY NORMAL ZOMBIE AND BRUTE ZOMBIE USED, OTHERS NO NEED
    public float dmgRate = 1f; // Rate at which damage is applied to the table
    private float dmgTimer = 0f; // Timer to track the time since the last damage
    public float dmgAmt = 0f;

    //private Transform player; // Reference to the player's transform
    //private float destroyTimer = 0f;
    [SerializeField] private float leaveDelay = 3f;
    public CustomerInteraction CustomerInteraction;

    //DAMAGE CODE ONLY NORMAL ZOMBIE AND BRUTE ZOMBIE USED, OTHERS NO NEED
    private Coroutine dmgCoroutine; // Reference to the damage coroutine
    //private bool shouldDamageTable = true; // Flag to track if the customer should damage the table

    public int moneyDrop;
    public float deductionInterval = 5f;
    public int deductionAmount = 5;
    private bool shouldDeductPoints = true;

    //Declare which zombie
    public bool normalZombie = false;
    public bool acidZombie = false;
    public bool bruteZombie = false;
    public bool ClownZombie = false;

    //Clown Zombie Behaviour 
    public JumpScare jumpscareImage;
    public bool jumpscare = false;

    //Acid Zombie Behaviour
    public TileSpawner tileSpawner;
    public float constSpitTime;
    public float spitSpawnDelay;

    //satisfaction bar fill amount
    private Coroutine fillCoroutine; // Reference to the fill coroutine
    private float fillSpeed = 7.0f; // Speed at which the fill amount increases (you can adjust this value)

    //Normal Zombie Attack Animation
    Animator NormalAttackAnimator;
    string NormalcurrentState;
    const string NORMALZOMBIE_ATTACKING = "NZombieAttackingAnimation";
    //const string NORMALZOMBIE_STANDING = "NZombieStandingAnimation";

    //Brute Zombie Attack Animation
    Animator BruteAttackAnimator;
    string BrutecurrentState;
    const string BRUTEZOMBIE_ATTACKING = "BruteZombieAttackingAnimation";

    //Acid Zombie Attack Animation
    Animator AcidAttackAnimator;
    string AcidcurrentState;
    const string ACIDZOMBIE_ATTACKING = "AcidZombieAttackingAnimation";

    private bool isRepeatingAttackAnimation = false;

    void Start()
    {
        ai = GetComponent<CustomerAI>();
        cs = FindObjectOfType<CustomerSpawner>();

        time_remaining = max_time;
        timer_radial_image.gameObject.SetActive(true); // Deactivate the image at the start
        timer_radial_image.fillAmount = 0.0f; // Set the initial fill amount to 0

        NormalAttackAnimator = gameObject.GetComponent<Animator>();
        BruteAttackAnimator = gameObject.GetComponent<Animator>();
        AcidAttackAnimator = gameObject.GetComponent<Animator>();

        //Clown Zombie Code
        GameObject jumpScareObject = GameObject.FindGameObjectWithTag("JumpScare");
        if (jumpScareObject != null)
        {
            jumpscareImage = jumpScareObject.GetComponent<JumpScare>();
        }
        else
        {
            Debug.LogWarning("TileSpawner object not found with tag: TileSpawner");
        }

        //Acid Zombie Code
        GameObject tileSpawnerObject = GameObject.FindGameObjectWithTag("TileSpawner");
        if (tileSpawnerObject != null)
        {
            tileSpawner = tileSpawnerObject.GetComponent<TileSpawner>();
        }
        else
        {
            Debug.LogWarning("TileSpawner object not found with tag: TileSpawner");
        }

        CustomerInteraction = GetComponent<CustomerInteraction>();
    }

    void Update()
    {
        if (normalZombie)
        {
            if (isSitting)
            {
                if (time_remaining > 0)
                {
                    if (CustomerInteraction.foodName == null)
                    {
                        StartCoroutine(LeaveAfterDelay(leaveDelay));
                    }
                    else
                    {
                        time_remaining -= Time.deltaTime;
                        //timer_radial_image.fillAmount = time_remaining / max_time;
                        // Stop the previous fill coroutine (if running) to avoid conflicts
                        if (fillCoroutine != null)
                        {
                            StopCoroutine(fillCoroutine);
                        }
                        // Start a new fill coroutine
                        fillCoroutine = StartCoroutine(FillTimerRadialImage());
                    }
                }
                else
                {
                    if (!isAttacking)
                    {
                        StartAtk(dmgRate, spitSpawnDelay, constSpitTime);
                    }
                    else if (isAttacking)
                    {
                        if (CustomerInteraction.foodName == null)
                        {
                            StartCoroutine(LeaveAfterDelay(leaveDelay));
                        }
                    }
                }
            }
        }

        if (bruteZombie)
        {
            if (isSitting)
            {
                if (time_remaining > 0)
                {
                    if (CustomerInteraction.foodName == null && CustomerInteraction.foodName2 == null)
                    {
                        StartCoroutine(LeaveAfterDelay(leaveDelay));
                    }
                    else
                    {
                        time_remaining -= Time.deltaTime;
                        //timer_radial_image.fillAmount = time_remaining / max_time;
                        // Stop the previous fill coroutine (if running) to avoid conflicts
                        if (fillCoroutine != null)
                        {
                            StopCoroutine(fillCoroutine);
                        }
                        // Start a new fill coroutine
                        fillCoroutine = StartCoroutine(FillTimerRadialImage());
                    }
                }
                else
                {
                    if (!isAttacking)
                    {
                        StartAtk(dmgRate, spitSpawnDelay, constSpitTime);
                    }
                    else if (isAttacking)
                    {
                        if (CustomerInteraction.foodName == null && CustomerInteraction.foodName2 == null)
                        {
                            StartCoroutine(LeaveAfterDelay(leaveDelay));
                        }
                    }
                }
            }
        }

        if (acidZombie)
        {
            if (isSitting)
            {
                if (time_remaining > 0)
                {
                    if (CustomerInteraction.foodName == null && CustomerInteraction.foodName2 == null)
                    {
                        StartCoroutine(LeaveAfterDelay(leaveDelay));
                    }
                    else
                    {
                        float speedMultiplier = 1.0f; // Speed multiplier for decreasing satisfaction bar

                        if (time_remaining / max_time < 0.5f)
                        {
                            speedMultiplier = 1.3f; // Increase the speed by 30% when satisfaction is below 50%
                        }

                        time_remaining -= Time.deltaTime * speedMultiplier;
                        //timer_radial_image.fillAmount = time_remaining / max_time;
                        // Stop the previous fill coroutine (if running) to avoid conflicts
                        if (fillCoroutine != null)
                        {
                            StopCoroutine(fillCoroutine);
                        }
                        // Start a new fill coroutine
                        fillCoroutine = StartCoroutine(FillTimerRadialImage());
                    }
                }
                else
                {
                    if (!isAttacking)
                    {
                        StartAtk(dmgRate, spitSpawnDelay, constSpitTime);
                    }
                }
            }
            if (isAttacking)
            {
                if (CustomerInteraction.foodName == null && CustomerInteraction.foodName2 == null)
                {
                    StartCoroutine(LeaveAfterDelay(leaveDelay));
                }
            }
        }

        if (ClownZombie)
        {
            if (isSitting)
            {
                if (time_remaining > 0)
                {
                    if (CustomerInteraction.foodName == null && CustomerInteraction.foodName2 == null)
                    {
                        StartCoroutine(LeaveAfterDelay(leaveDelay));
                    }
                    else
                    {
                        time_remaining -= Time.deltaTime;
                        //timer_radial_image.fillAmount = time_remaining / max_time;
                        // Stop the previous fill coroutine (if running) to avoid conflicts
                        if (fillCoroutine != null)
                        {
                            StopCoroutine(fillCoroutine);
                        }
                        // Start a new fill coroutine
                        fillCoroutine = StartCoroutine(FillTimerRadialImage());
                    }
                }
                else
                {
                    if (jumpscare)
                    {
                        StartJumpScare();
                        StartCoroutine(DeductPointsOverTime(deductionInterval, deductionAmount));
                    }

                    if (!isAttacking)
                    {
                        //StartAttack();
                        //DestroyCustomer();
                        //jumpscareImage.SetActive(true);
                        //jumpscare player with a fade in and fade out screen and steal point from player
                        if (CustomerInteraction.foodName == null && CustomerInteraction.foodName2 == null)
                        {
                            StartCoroutine(LeaveAfterDelay(leaveDelay));
                        }
                    }
                    if (isAttacking)
                    {
                        if (CustomerInteraction.foodName == null && CustomerInteraction.foodName2 == null)
                        {
                            StartCoroutine(LeaveAfterDelay(leaveDelay));
                        }
                    }
                }
            }
        }
    }

    private IEnumerator FillTimerRadialImage()
    {
        float elapsedTime = 0.0f;
        float startFillAmount = timer_radial_image.fillAmount;

        while (elapsedTime < fillSpeed)
        {
            elapsedTime += Time.deltaTime;
            float newFillAmount = Mathf.Lerp(startFillAmount, 1.0f, elapsedTime / fillSpeed);
            timer_radial_image.fillAmount = newFillAmount;
            yield return null;
        }

        // Ensure the fill amount is exactly 1 when the coroutine ends
        if (time_remaining <= 0)
        {
            // If time_remaining is 0 or negative, set the fill amount to 1 directly
            timer_radial_image.fillAmount = 1.0f;
        }

        fillCoroutine = null; // Reset the coroutine reference
    }

    private IEnumerator AcidZombieAttackRoutine(float spawnDelay, float spitTime)
    {
        while (true)
        {
            changeAnimationState(ACIDZOMBIE_ATTACKING);
            AudioManager.Instance.Play("AcidSpit");
            yield return new WaitForSeconds(spawnDelay);
            tileSpawner.SpawnTile();
            yield return new WaitForSeconds(spitTime);
        }
    }

    private IEnumerator ContinuousDamageTable(float dmgRate)
    {
        while (true)
        {
            if (normalZombie)
            {
                changeAnimationState(NORMALZOMBIE_ATTACKING);
                DamageTable();
            }
            else if (bruteZombie)
            {
                changeAnimationState(BRUTEZOMBIE_ATTACKING);
                DamageTable();
            }

            yield return new WaitForSeconds(dmgRate);
        }
    }

    //Clown Zombie Behaviour
    private void SpawnImageForPlayer()
    {
        jumpscareImage.SpawnImage();
        jumpscare = false;
    }

    //Clown Zombie Behaviour
    public void StartJumpScare()
    {
        if (jumpscare) // Check if the jumpscare is true
        {
            SpawnImageForPlayer();
            jumpscare = false; // Set jumpscare to false after spawning the image
        }
    }

    //Clown Zombie Behaviour
    IEnumerator DeductPointsOverTime(float interval, int amount)
    {
        while(true)
        {
            if (CurrencyManager.Instance.currency >= amount)
            {
                shouldDeductPoints = true;
                // Deduct points and update player's points
                if (shouldDeductPoints)
                {
                    CurrencyManager.Instance.SpendMoney(amount);
                }
                
            }
            
            // If moneyDrop becomes 0, stop deducting points
            else if (CurrencyManager.Instance.currency <= 0)
            {
                shouldDeductPoints = false;
            }

            // Wait for the specified interval before deducting points again
            yield return new WaitForSeconds(interval);
        }
    }

    private IEnumerator StartTimerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (isSitting)
        {
            timer_radial_image.gameObject.SetActive(true); // Activate the image after the delay
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chairs"))
        {
            isSitting = true;
            StartCoroutine(StartTimerAfterDelay(2f)); // Start the timer after a 2-second delay

            if (ClownZombie)
            {
                jumpscare = true;
            }
        }

        if (collision.gameObject.CompareTag("Exit"))
        {
            if (isLeaving)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chairs"))
        {
            // Customer is leaving the chair
            isSitting = false;
            timer_radial_image.gameObject.SetActive(false); // Deactivate the image when the customer is leaving the chair
        }
    }

    public void StartAtk(float dmgRate, float spawnDelay, float spitTime)
    {
        isAttacking = true;

        if (acidZombie)
        {
            dmgCoroutine = StartCoroutine(AcidZombieAttackRoutine(spawnDelay, spitTime));
        }
        else if (normalZombie || bruteZombie)
        {
            dmgCoroutine = StartCoroutine(ContinuousDamageTable(dmgRate));
        }
    }

    private void StopAtk()
    {
        isAttacking = false;

        CurrencyManager.Instance.AddMoney(moneyDrop);

        if (dmgCoroutine != null)
        {
            StopCoroutine(dmgCoroutine);
        }
    }

    private void DamageTable()
    {
        Counter[] tables = FindObjectsOfType<Counter>();

        for (int i = 0; i < tables.Length; i++)
        {
            // Check if the table has a HealthBar component
            //Counter healthBar = table.GetComponent<Counter>();
            if (tables[i] != null)
            {
                // Apply damage to the table's health bar
                tables[i].TakeDamage(dmgAmt);
                AudioManager.Instance.Play("CustomerAttack");
            }
        }
    }

    private IEnumerator LeaveAfterDelay(float delay)
    {        
        StopAtk();

        yield return new WaitForSeconds(delay);

        ai.reachedEndOfPath = false;
        ai.target = cs.spawnPosition;
        isLeaving = true;
    }

    void changeAnimationState(string newState)
    {
        if (normalZombie)
        {
            // Stop animation from interrupting itself
            if (NormalcurrentState == newState) return;

            //Play new animation
            NormalAttackAnimator.Play(newState);

            //Update current state
            NormalcurrentState = newState;
        }

        if (bruteZombie)
        {
            // Stop animation from interrupting itself
            if (BrutecurrentState == newState) return;

            //Play new animation
            BruteAttackAnimator.Play(newState);

            //Update current state
            BrutecurrentState = newState;
        }

        if(acidZombie)
        {
            // Stop animation from interrupting itself
            if (AcidcurrentState == newState) return;

            //Play new animation
            AcidAttackAnimator.Play(newState);

            //Update current state
            AcidcurrentState = newState;
        }
    }
}