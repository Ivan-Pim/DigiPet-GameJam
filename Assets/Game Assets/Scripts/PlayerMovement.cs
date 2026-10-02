using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Scriptable Object thingamajig. In this case I think it's just a collection of values we're gonna use
    public PlayerData Data;

    #region COMPONENTS
    public Rigidbody2D rb { get; private set; }
    public Animator animator;
    #endregion

    #region STATE PARAMETERS
    public bool IsFacingRight { get; private set; }
    public bool IsJumping { get; private set; }
    private bool isJumpFalling;
    private bool isInGroundPound;
    #endregion

    #region INPUT PARAMETERS
    public InputActionAsset m_ActionAsset;
    InputAction jumpAction;
    InputAction moveAction;
    InputAction groundPoundAction;

    /// <summary>
    InputAction happyAction;
    InputAction angryAction;
    InputAction eatAction;
    /// //////////////////////////////////////////////

    public Vector2 moveInput { get; private set; }
    public float JumpInputBufferLeft {  get; private set; }
    public float CoyoteBufferLeft { get; private set; }
    private float MultiJumpsLeft;
    #endregion

    #region CHECK PARAMETERS
    [Header("Checks")]

    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private Vector2 _groundCheckSize = new Vector2(2f, 0.03f);
    #endregion

    #region LAYERS & TAGS
    [SerializeField] private LayerMask _groundLayer;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void OnEnable() {
        m_ActionAsset.FindActionMap("Player").Enable();
    }
    void Start() {
        jumpAction = InputSystem.actions.FindAction("Jump");
        moveAction = InputSystem.actions.FindAction("Move");
        groundPoundAction = InputSystem.actions.FindAction("Ground Pound");

        happyAction = InputSystem.actions.FindAction("HappyDemo");
        angryAction = InputSystem.actions.FindAction("AngryDemo");
        eatAction = InputSystem.actions.FindAction("EatDemo");
        ////////////////////////////////////////////////////////

        SetGravityScale(Data.gravityScale);
        IsFacingRight = false;
    }

    private void Update()
    {
        // this code should not be in the final release!!!!!!!
        ///////////////////////////////////////////////////////
        #region DEMO ACTIONS
        if (happyAction.WasPressedThisFrame()) animator.SetTrigger("GetHappy");
        if (angryAction.WasPressedThisFrame()) animator.SetTrigger("GetAngry");
        if (eatAction.WasPressedThisFrame()) animator.SetTrigger("StartEating");
        #endregion

        #region TIMERS
        CoyoteBufferLeft -= Time.deltaTime;
        JumpInputBufferLeft -= Time.deltaTime;
        #endregion

        #region INPUT HANDLING
        moveInput = moveAction.ReadValue<Vector2>();
        if (moveInput.x != 0) CheckFacingDirection(moveInput.x > 0);

        if (jumpAction.WasPressedThisFrame()) OnJumpInput();
        if (jumpAction.WasReleasedThisFrame()) OnJumpRelease();
        #endregion

        #region COLLISION CHECKS
        if (!IsJumping) {
            if (Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer)) {
                CoyoteBufferLeft = Data.coyoteTime;
                MultiJumpsLeft = Data.MultiJumps;
                isJumpFalling = false;
                isInGroundPound = false;
            } else isJumpFalling = true;
        }
        #endregion

        #region JUMP CHECKS
        if (IsJumping && rb.linearVelocityY < 0) {
            IsJumping = false;
            isJumpFalling = true;
        }

        if (JumpInputBufferLeft > 0) {
            if (CanJump()) {
                IsJumping = true;
                isJumpFalling = false;
                Jump(false);
            } else if (CanAirJump()) {
                IsJumping = true;
                isJumpFalling = false;
                MultiJumpsLeft--;
                Jump(true);
            }
            
        }

        if (groundPoundAction.WasPressedThisFrame() && CanGroundPound()) {
            Sleep(Data.impactDuration);

            isInGroundPound = true;
            GroundPound(Data.haltHorizontalMomentum);
        }
        #endregion

        #region GRAVITY

        if (IsAirHanging()) { 
            SetGravityScale(Data.gravityScale * Data.jumpHangGravityMult); 
        }
        else if (isJumpFalling) SetGravityScale(Data.gravityScale * Data.fallGravityMult);
        else SetGravityScale(Data.gravityScale);

        if (isInGroundPound) rb.linearVelocityY = Mathf.Max(rb.linearVelocityY, -Data.maxPoundFallSpeed);
        else rb.linearVelocityY = Mathf.Max(rb.linearVelocityY, -Data.maxFallSpeed);
        #endregion

        animator.SetBool("Jumping", IsJumping);
        animator.SetBool("Falling", isJumpFalling);
        animator.SetBool("InGroundPound", isInGroundPound);
        animator.SetFloat("XSpeed", Mathf.Abs(moveInput.x));
    }
    private void FixedUpdate()
    {
        if (!isInGroundPound || !Data.haltHorizontalMomentum) Run(1);
    }

    #region INPUT CALLBACKS
    public void OnJumpInput() {
        JumpInputBufferLeft = Data.jumpInputBufferTime;
    }
    
    public void OnJumpRelease() {
        if (IsJumping) {
            isJumpFalling = true;
        }
    }
    #endregion

    #region GENERAL METHODS
    public void SetGravityScale(float gravity)
    {
        rb.gravityScale = gravity;
    }

    public void Sleep(float duration) {
        //nameof() instead of string somehow
        StartCoroutine(nameof(PerformSleep), duration);
    }

    private IEnumerator PerformSleep(float duration) {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(duration); // Realtime since timescale is 0
        Time.timeScale = 1;
    }
    #endregion

    // Movement methods
    #region RUN METHODS
    private void Run(float speedLimiter)
    {
        float targetSpeed = moveInput.x * Data.runMaxSpeed;
        targetSpeed = Mathf.Lerp(rb.linearVelocity.x, targetSpeed, speedLimiter);


        float accel = Data.runAccelAmount;
        if (IsAirHanging()) accel *= Data.jumpHangAccelMult;

        float speedDif = targetSpeed - rb.linearVelocityX;
        // calculate the force to apply to the player (the further we are from the desired speed, the faster we change)
        float movement = speedDif * accel;

        rb.AddForceX(movement);
    }

    private void Turn()
    {
        // flip the sprite for turning, and store the current direction
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        IsFacingRight = !IsFacingRight;
    }
    #endregion

    #region JUMP METHODS
    private void Jump(bool isAirJump)
    {
        JumpInputBufferLeft = 0f;
        CoyoteBufferLeft = 0f;

        #region Perform Jump
        float force = Data.jumpForce;
        if (isAirJump) force *= Data.airJumpForceMult;

        if (rb.linearVelocityY < 0) force -= rb.linearVelocityY;
        rb.AddForceY(force, ForceMode2D.Impulse);
        #endregion
    }

    private void GroundPound(bool haltHorizontal)
    {
        Vector2 force = new Vector2(0, -Data.maxPoundFallSpeed);

        if (rb.linearVelocityY > 0) force.y -= rb.linearVelocityY;
        if (haltHorizontal) force.x -= rb.linearVelocityX;
        rb.AddForce(force, ForceMode2D.Impulse);

    }
    #endregion

    #region CHECK METHODS
    public void CheckFacingDirection(bool IsMovingRight)
    {
        if (IsMovingRight != IsFacingRight) Turn();
    }

    public bool IsAirborne()
    {
        return (IsJumping || isJumpFalling);
    }

    private bool CanJump()
    {
        return CoyoteBufferLeft > 0 && !IsJumping; 
    }

    private bool CanAirJump()
    {
        return MultiJumpsLeft > 0 && (IsAirborne() && ! isInGroundPound);
    }

    private bool CanGroundPound()
    {
        return IsAirborne();
    }

    private bool IsAirHanging()
    {
        return (IsAirborne() && !isInGroundPound) && Mathf.Abs(rb.linearVelocityY) < Data.jumpHangThreshold;
    }
    #endregion

    #region EDITOR METHODS
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_groundCheckPoint.position, _groundCheckSize);
    }
    #endregion
}
