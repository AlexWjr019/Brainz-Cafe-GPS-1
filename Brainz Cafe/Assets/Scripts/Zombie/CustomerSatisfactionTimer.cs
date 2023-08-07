using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomerSatisfactionTimer : MonoBehaviour
{
    //ALL ZOMBIE SHARED CODE
    public Image timer_radial_image;
    public float time_remaining;
    public float max_time = 5.0f;
    private bool isSitting = false; // Flag to track if the customer is sitting

    public bool isAttacking = false; // Flag to track if the customer is currently chasing the player

    public string playerTag = "Chairs"; // Tag of the player object
    public float moveSpeed = 5f; // Speed at which the customer moves towards the player

    //DAMAGE CODE ONLY NORMAL ZOMBIE AND BRUTE ZOMBIE USED, OTHERS NO NEED
    public float damageRate = 1f; // Rate at which damage is applied to the table
    private float damageTimer = 0f; // Timer to track the time since the last damage
    public float dmgAmt = 0f;

    private Transform player; // Reference to the player's transform
    private float destroyTimer = 0f;
    private float destroyDelay = 3f;
    public CustomerInteraction CustomerInteraction;

    //DAMAGE CODE ONLY NORMAL ZOMBIE AND BRUTE ZOMBIE USED, OTHERS NO NEED
    private Coroutine damageCoroutine; // Reference to the damage coroutine
    private bool shouldDamageTable = true; // Flag to track if the customer should damage the table

    public int moneyDrop;

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

    //satisfaction bar fill amount
    private Coroutine fillCoroutine; // Reference to the fill coroutine
    private float fillSpeed = 7.0f; // Speed at which the fill amount increases (you can adjust this value)

    //Normal Zombie Attack Animation
    Animator NormalAttackAnimator;
    string NormalcurrentState;
    const string NORMALZOMBIE_ATTACKING = "NZombieAttackingAnimation";
    const string NORMALZOMBIE_STANDING = "NZombieStandingAnimation";

    //Brute Zombie Attack Animation
    Animator BruteAttackAnimator;
    string BrutecurrentState;
    const string BRUTEZOMBIE_ATTACKING = "BruteZombieAttackingAnimation";

    //Acid Zombie Attack Animation
    Animator AcidAttackAnimator;
    string AcidcurrentState;
    const string ACIDZOMBIE_ATTACKING = "AcidZombieAttackingAnimation";
    

    void Start()
    {
        time_remaining = max_time;
        timer_radial_image.gameObject.SetActive(true); // Deactivate the image at the start
        timer_radial_image.fillAmount = 0.0f; // Set the initial fill amount to 0

        NormalAttackAnimator = gameObject.GetComponent<Animator>();
        BruteAttackAnimator = gameObject.GetComponent<Animator>();
        AcidAttackAnimator = gameObject.GetComponent<Animator>();

        // Find the player object with the specified tag
        GameObject playerObject = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Player object not found with tag: " + playerTag);
        }

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
                        // Do nothing, time_remaining will remain unchanged
                        if (destroyTimer < destroyDelay)
                        {
                            destroyTimer += Time.deltaTime;
                        }
                        else
                        {
                            CurrencyManager.Instance.AddMoney(moneyDrop);
                            DestroyCustomer();
                        }
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
                        // Customer satisfaction time has run out, start chasing the player
                        StartDamageTable();
                    }
                }
            }

            if (isAttacking)
            {
                DamageTable();
                shouldDamageTable = false;

                if (CustomerInteraction.foodName == null)
                {
                    shouldDamageTable = false; // Set the flag to stop damaging the table
                    StartCoroutine(LeaveAfterDelay(3f));
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
                        // Do nothing, time_remaining will remain unchanged
                        if (destroyTimer < destroyDelay)
                        {
                            destroyTimer += Time.deltaTime;
                        }
                        else
                        {
                            DestroyCustomer();
                        }
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
                        // Customer satisfaction time has run out, start chasing the player
                        StartDamageTable();
                    }
                }
            }

            if (isAttacking)
            {
                damageTimer += Time.deltaTime;
                if (damageTimer >= damageRate)
                {
                    DamageTable();
                    damageTimer = 0f;
                }
                if (CustomerInteraction.foodName == null && CustomerInteraction.foodName2 == null)
                {
                    shouldDamageTable = false; // Set the flag to stop damaging the table
                    StartCoroutine(LeaveAfterDelay(3f));
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
                        // Do nothing, time_remaining will remain unchanged
                        if (destroyTimer < destroyDelay)
                        {
                            destroyTimer += Time.deltaTime;
                        }
                        else
                        {
                            CurrencyManager.Instance.AddMoney(moneyDrop);
                            DestroyCustomer();
                        }
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
                    changeAnimationState(ACIDZOMBIE_ATTACKING);
                    //// spawn tile that make player slow
                    //SpawnTileForPlayer();
                    //DestroyCustomer();
                    // Start the attack routine for Acid Zombie
                    StartCoroutine(AcidZombieAttackRoutine(1.5f));
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
                        // Do nothing, time_remaining will remain unchanged
                        if (destroyTimer < destroyDelay)
                        {
                            destroyTimer += Time.deltaTime;
                        }
                        else
                        {
                            DestroyCustomer();
                        }
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
                        StartAttack();
                        
                    }


                    if (!isAttacking)
                    {
                        //StartAttack();
                        //DestroyCustomer();
                        //jumpscareImage.SetActive(true);
                        //jumpscare player with a fade in and fade out screen and steal point from player
                        if (CustomerInteraction.foodName == null && CustomerInteraction.foodName2 == null)
                        {
                            StartCoroutine(LeaveAfterDelay(3f));
                            CurrencyManager.Instance.AddMoney(moneyDrop);
                        }
                    }

                }
            }
            if (isAttacking)
            {
                if (CustomerInteraction.foodName == null && CustomerInteraction.foodName2 == null)
                {
                    StartCoroutine(LeaveAfterDelay(3f));
                    CurrencyManager.Instance.AddMoney(moneyDrop);
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


    //Acid Zombie Behaviour
    private void SpawnTileForPlayer()
    {
        // Check if the tileSpawner is assigned and the tilePrefab is set
        if (tileSpawner != null && tileSpawner.tilePrefab != null)
        {
            // Call the SpawnTile method on the tileSpawner
            tileSpawner.SpawnTile();
        }
    }

    //Clown Zombie Behaviour
    private void SpawnImageForPlayer()
    {
        jumpscareImage.SpawnImage();
        jumpscare = false;
    }

    //Clown Zombie Behaviour
    public void StartAttack()
    {
        if (jumpscare) // Check if the jumpscare is true
        {
            SpawnImageForPlayer();
            jumpscare = false; // Set jumpscare to false after spawning the image
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

    public void StartDamageTable()
    {
        isAttacking = true;
        damageCoroutine = StartCoroutine(ContinuousDamageTable());
        if (normalZombie)
        {
            changeAnimationState(NORMALZOMBIE_ATTACKING);
            StartCoroutine(ChangeAfterDamageAnimation(1.5f));
        }
        if (bruteZombie)
        {
            changeAnimationState(BRUTEZOMBIE_ATTACKING);
        }
    }

    private void StopDamageTable()
    {
        isAttacking = false;
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
        }


    }

    private void DestroyCustomer()
    {
        CurrencyManager.Instance.AddMoney(moneyDrop);
        Destroy(gameObject);
    }

    private void DamageTable()
    {
        if (normalZombie)
        {
            if (shouldDamageTable)
            {
                // Find all the table objects with the "Table" tag
                Counter[] tables = FindObjectsOfType<Counter>();

                for (int i = 0; i < tables.Length; i++)
                {
                    // Check if the table has a HealthBar component
                    //Counter healthBar = table.GetComponent<Counter>();
                    if (tables[i] != null)
                    {
                        // Apply damage to the table's health bar
                        tables[i].TakeDamage(dmgAmt);
                        AudioManager.instance.PlayZombieAttackBarrierAudio();
                    }
                }
            }
        }

        if (bruteZombie)
        {
            if (shouldDamageTable)
            {
                // Find all the table objects with the "Table" tag
                Counter[] tables = FindObjectsOfType<Counter>();

                for (int i = 0; i < tables.Length; i++)
                {
                    // Check if the table has a HealthBar component
                    //Counter healthBar = table.GetComponent<Counter>();
                    if (tables[i] != null)
                    {
                        // Apply damage to the table's health bar
                        tables[i].TakeDamage(dmgAmt);
                        AudioManager.instance.PlayZombieAttackBarrierAudio();
                    }
                }
            }
        }
    }

    private IEnumerator LeaveAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StopDamageTable();
        DestroyCustomer();
    }

    private IEnumerator ContinuousDamageTable()
    {
        while (true)
        {
            DamageTable();
            yield return new WaitForSeconds(damageRate);
        }
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

    private IEnumerator ChangeAfterDamageAnimation(float delay)
    {
        yield return new WaitForSeconds(delay);
        changeAnimationState(NORMALZOMBIE_STANDING);
    }

    private IEnumerator AcidZombieAttackRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnTileForPlayer();
        DestroyCustomer();       
    }

}