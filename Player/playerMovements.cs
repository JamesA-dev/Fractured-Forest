using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovements : MonoBehaviour
{
    public bool CanMove = true;

    private bool isSprinting => canSprint && Input.GetKey(sprintKey);
    private bool shouldJump => Input.GetKeyDown(jumpKey) && characterController.isGrounded;

    private bool shouldCrunch => Input.GetKeyDown(CrouchKey) && !duringCrouchingAnimation && characterController.isGrounded;

    [Header("Movement Options")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canCrouch = true;
    [SerializeField] private bool canUseHeadBob = true;
    [SerializeField] private bool willSlideOnSlope = true;
    [SerializeField] private bool canZoom = true;
    [SerializeField] private bool useFootSteps = true;
    [SerializeField] private bool useStamina = true;


    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode CrouchKey = KeyCode.LeftControl;
    [SerializeField] private KeyCode zoomKey = KeyCode.Mouse1;

    [Header("Health")]
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float timeBeforeRegenStarts = 3;
    [SerializeField] private float healthInValueIncrement = 1;
    private float currentHealth;
    private Coroutine regeneratingHealth;
    public static Action<float> OnTakeDamage;
    public static Action<float> OnDamage;
    public static Action<float> OnHeal;

    [Header("Stanima")]
    [SerializeField] private float maxStamina = 100;
    [SerializeField] private float staminaMultiplier = 5;
    [SerializeField] private float timeBeforeStaminaRegenStarts = 5;
    [SerializeField] private float staminaValueIncrement = 2;
    [SerializeField] private float staminaTimeIncrement = 0.1f;
    private float currentStamina;
    private Coroutine regenatingStamina;
    public static Action<float> OnStaminaChange;


    [Header("Movement")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float SprintSpeed = 6.0f;
    [SerializeField] private float crouchSpeed = 1.5f;
    [SerializeField] private float slopeSpeed = 8;


    [Header("View")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 100)] private float uperlookLimit = 80.0f;
    [SerializeField, Range(1, 100)] private float lowerlookLimit = 80.0f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float gravity = 30.0f;

    [Header("Crouch")]
    [SerializeField] private float crouchHeight = 0.5f;
    [SerializeField] private float StandingHeight = 2.0f;
    [SerializeField] private float timeToCrouch = 0.25f;
    [SerializeField] private Vector3 crounchingCenter = new Vector3(0, 0.25f, 0);
    [SerializeField] private Vector3 standingCenter = new Vector3(0, 0, 0);
    [SerializeField] private GameObject crouchText;

    [Header("HeadBob")]
    [SerializeField] private float walkBobSpeed = 14f;
    [SerializeField] private float walkBobAmount = 0.05f;
    [SerializeField] private float sprintBobSpeed = 18f;
    [SerializeField] private float sprintBobAmount = 0.11f;
    [SerializeField] private float crouchBobSpeed = 8f;
    [SerializeField] private float crouchBobAmount = 0.25f;
    private float defaultYPos = 0;
    private float timer;

    [Header("Zoom")]
    [SerializeField] private float timeToZoom = 0.3f;
    [SerializeField] private float zoomFOV = 30f; //field of view
    private float defaultFOV;
    private Coroutine zoomRoutine;

    [Header("Sound")]
    [SerializeField] private float baseStepSpeed = 0.5f;
    [SerializeField] private float crouchStepMultipler = 1.5f;
    [SerializeField] private float sprintStepMultipler = 0.6f;
    [SerializeField] private AudioSource footsStepAudioSource = default;
    [SerializeField] private AudioClip[] woodClips = default;
    [SerializeField] private AudioClip[] metalClips = default;
    [SerializeField] private AudioClip[] grassClips = default;
    private float footsStepTimer = 0;
    private float GetCurrentOffset => isCrounching ? baseStepSpeed * crouchStepMultipler : isSprinting ? baseStepSpeed * sprintStepMultipler : baseStepSpeed;


    //Sliding Parameters
    private Vector3 hitPointNormal;

    private bool isSliding
    {
        get
        {
            if (characterController.isGrounded && Physics.Raycast(transform.position, Vector3.down, out RaycastHit slopetHit, 2f))
            {
                hitPointNormal = slopetHit.normal;
                return Vector3.Angle(hitPointNormal, Vector3.up) > characterController.slopeLimit;
            }
            else
            {
                return false;
            }


        }
    }

    private bool isCrounching;
    private bool duringCrouchingAnimation;




    private Camera playerCamera;
    private CharacterController characterController;

    private Vector3 moveDirection;
    private Vector2 currentInput;

    private float rotarionX = 0;

    private void OnEnable()
    {
        OnTakeDamage += ApplyDamage;
    }

    private void OnDisable()
    {
        OnTakeDamage -= ApplyDamage;
    }

    // Start is called before the first frame update
    void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        defaultYPos = playerCamera.transform.localPosition.y;
        defaultFOV = playerCamera.fieldOfView;
        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanMove)
        {
            HandleMovementInput();
            HandMouseLook();

            if (canJump)
                HandleJump();

            if (canCrouch)
                HandleCrouch();

            if (canUseHeadBob)
                HandleHeadBob();

            if (canZoom)
                HandleZoom();

            if (useFootSteps)
                Handle_FootSteps();

            if (useStamina)
                HandleStamina();

            ApplyFinalMovements();
        }
    }

    private void HandleMovementInput()
    {
        currentInput = new Vector2((isCrounching ? crouchSpeed : isSprinting ? SprintSpeed : walkSpeed) * Input.GetAxis("Vertical"), (isCrounching ? crouchSpeed : isSprinting ? SprintSpeed : walkSpeed) * Input.GetAxis("Horizontal"));

        float moveDirectionY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
        moveDirection.y = moveDirectionY;
    }

    private void HandleJump()
    {
        if (shouldJump)
            moveDirection.y = jumpForce;
    }

    private void HandleCrouch()
    {
        if (shouldCrunch)
            StartCoroutine(CrouchStand());
    }

    private void HandleHeadBob()
    {
        if (!characterController.isGrounded) return;

        if (Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f)
        {
            timer += Time.deltaTime * (isCrounching ? crouchBobSpeed : isSprinting ? sprintBobSpeed : walkBobSpeed);
            playerCamera.transform.localPosition = new Vector3(
                playerCamera.transform.localPosition.x,
                defaultYPos + Mathf.Sin(timer) * (isCrounching ? crouchBobAmount : isSprinting ? sprintBobAmount : walkBobAmount),
                playerCamera.transform.localPosition.z);
        }


    }

    private void HandleStamina()
    {
        if (isSprinting && currentInput != Vector2.zero)
        {
            if (regenatingStamina != null)
            {
                StopCoroutine(regenatingStamina);
                regenatingStamina = null;
            }


            currentStamina -= staminaMultiplier * Time.deltaTime;

            if (currentStamina < 0)
                currentStamina = 0;

            OnStaminaChange?.Invoke(currentStamina);

            if (currentStamina <= 0)
                canSprint = false;
        }
        if (!isSprinting && currentStamina < maxStamina && regenatingStamina == null)
        {
            regenatingStamina = StartCoroutine(RegenerateStamina());
        }
    }

    private void HandleZoom()
    {
        if (Input.GetKeyDown(zoomKey))
        {
            if (zoomRoutine != null)
            {
                StopCoroutine(zoomRoutine);
                zoomRoutine = null;
            }
            zoomRoutine = StartCoroutine(ToggleZoom(true));
        }
        if (Input.GetKeyUp(zoomKey))
        {
            if (zoomRoutine != null)
            {
                StopCoroutine(zoomRoutine);
                zoomRoutine = null;
            }
            zoomRoutine = StartCoroutine(ToggleZoom(false));
        }
    }

    private void HandMouseLook()
    {
        rotarionX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotarionX = Mathf.Clamp(rotarionX, -uperlookLimit, lowerlookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotarionX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
    }

    private void Handle_FootSteps()
    {
        if (!characterController.isGrounded) return;
        if (currentInput == Vector2.zero) return;

        footsStepTimer -= Time.deltaTime;

        if (footsStepTimer <= 0)
        {
            if (Physics.Raycast(playerCamera.transform.position, Vector3.down, out RaycastHit hit, 3))
            {
                switch (hit.collider.tag)
                {
                    case "FootSteps/Dirt":
                        footsStepAudioSource.PlayOneShot(woodClips[UnityEngine.Random.Range(0, woodClips.Length - 1)]);
                        break;
                    case "FootSteps/Grass":
                        footsStepAudioSource.PlayOneShot(grassClips[UnityEngine.Random.Range(0, grassClips.Length - 1)]);
                        break;
                    case "FootSteps/Metal":
                        footsStepAudioSource.PlayOneShot(metalClips[UnityEngine.Random.Range(0, metalClips.Length - 1)]);
                        break;
                }
            }
            footsStepTimer = GetCurrentOffset;
        }
    }

    private void ApplyDamage(float dmg)
    {
        currentHealth -= dmg;
        OnDamage?.Invoke(currentHealth);

        if (currentHealth <= 0)
            KillPlayer();
        else if (regeneratingHealth != null)
            StopCoroutine(regeneratingHealth);

        regeneratingHealth = StartCoroutine(RegenerateHealth());
    }

    private void KillPlayer()
    {
        currentHealth = 0;

        if (regeneratingHealth != null)
            StopCoroutine(regeneratingHealth);

        print("DEAD");
    }

    private void ApplyFinalMovements()
    {
        if (!characterController.isGrounded)

            moveDirection.y -= gravity * Time.deltaTime;

        if (willSlideOnSlope && isSliding)
            moveDirection += new Vector3(hitPointNormal.x - hitPointNormal.y, hitPointNormal.z) * slopeSpeed;

        characterController.Move(moveDirection * Time.deltaTime);



    }



    private IEnumerator CrouchStand()
    {
        if (isCrounching && Physics.Raycast(playerCamera.transform.position, Vector3.up, 1f))
            yield break;

        duringCrouchingAnimation = true;
        float timeElapsed = 0;
        float targetHeight = isCrounching ? StandingHeight : crouchHeight;
        float currentHeight = characterController.height;
        Vector3 targetCenter = isCrounching ? standingCenter : crounchingCenter;
        Vector3 currectCenter = characterController.center;

        while (timeElapsed < timeToCrouch)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
            characterController.center = Vector3.Lerp(currectCenter, targetCenter, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            crouchText.SetActive(false);
            yield return null;
        }

        characterController.height = targetHeight;
        characterController.center = targetCenter;

        isCrounching = !isCrounching;

        duringCrouchingAnimation = false;
    }

    private IEnumerator ToggleZoom(bool isEnter)
    {
        float targetFOV = isEnter ? zoomFOV : defaultFOV;
        float startingFOV = playerCamera.fieldOfView;
        float timeElapsed = 0;

        while (timeElapsed < timeToZoom)
        {
            playerCamera.fieldOfView = Mathf.Lerp(startingFOV, targetFOV, timeElapsed / timeToZoom);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        playerCamera.fieldOfView = targetFOV;
        zoomRoutine = null;

    }

    private IEnumerator RegenerateHealth()
    {
        yield return new WaitForSeconds(timeBeforeRegenStarts);
        WaitForSeconds timeToWait = new WaitForSeconds(healthInValueIncrement);

        while (currentHealth < maxHealth)
        {
            currentHealth += healthInValueIncrement;
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;

            OnHeal?.Invoke(currentHealth);
            yield return timeToWait;
        }
        regeneratingHealth = null;
    }

    private IEnumerator RegenerateStamina()
    {
        yield return new WaitForSeconds(timeBeforeStaminaRegenStarts);
        WaitForSeconds timeToWait = new WaitForSeconds(staminaTimeIncrement);

        while (currentHealth < maxStamina)
        {
            if (currentStamina > 0)
                canSprint = true;

            currentStamina += staminaValueIncrement;

            if (currentStamina > maxStamina)
                currentStamina = maxStamina;

            OnStaminaChange?.Invoke(currentStamina);

            yield return timeToWait;

        }
        regenatingStamina = null;

    }
}


