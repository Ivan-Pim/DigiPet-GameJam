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
    #endregion

    #region INPUT PARAMETERS
    public InputActionAsset m_ActionAsset;
    InputAction jumpAction;
    InputAction moveAction;

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
            }
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
        #endregion

        #region GRAVITY

        if (IsAirHanging()) { 
            SetGravityScale(Data.gravityScale * Data.jumpHangGravityMult); 
        }
        else if (isJumpFalling) SetGravityScale(Data.gravityScale * Data.fallGravityMult);
        else SetGravityScale(Data.gravityScale);

        rb.linearVelocityY = Mathf.Max(rb.linearVelocityY, -Data.maxFallSpeed);
        #endregion

        animator.SetBool("Jumping", IsJumping);
        animator.SetBool("Falling", isJumpFalling);
        animator.SetFloat("XSpeed", Mathf.Abs(moveInput.x));
    }
    private void FixedUpdate()
    {
        Run(1);
    }

    #region INPUT CALLBACKS
    public void OnJumpInput()
    {
        JumpInputBufferLeft = Data.jumpInputBufferTime;
    }
    
    public void OnJumpRelease()
    {
        if (IsJumping)
        {
            isJumpFalling = true;
        }
    }
    #endregion

    #region GENERAL METHODS
    public void SetGravityScale(float gravity)
    {
        rb.gravityScale = gravity;
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
    #endregion

    #region CHECK METHODS
    public void CheckFacingDirection(bool IsMovingRight)
    {
        if (IsMovingRight != IsFacingRight) Turn();
    }

    private bool CanJump()
    {
        return CoyoteBufferLeft > 0 && !IsJumping; 
    }

    private bool CanAirJump()
    {
        return MultiJumpsLeft > 0 && (IsJumping || isJumpFalling);
    }

    private bool IsAirHanging()
    {
        return (IsJumping || isJumpFalling) && Mathf.Abs(rb.linearVelocityY) < Data.jumpHangThreshold;
    }
    #endregion
}
