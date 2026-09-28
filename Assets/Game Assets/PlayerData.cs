using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Gravity")]
    [HideInInspector] public float gravityStrength; // Gravity needed for the desired jump height and time to peak (calculated)
    [HideInInspector] public float gravityScale; // The strength converted to a value of the unity gravity;

    public float fallGravityMult; // Multiplier for player gravity while falling
    public float maxFallSpeed; // Terminal velocity of the player;

    [Space(5)]
    public float jumpHangGravityMult; // how much floatier the character is at the peak of their jump

    [Space(20)]

    [Header("Run")]
    public float runMaxSpeed; // player's desired movement speed;
    public float runAcceleration; // acceleration of player until reaching max speed PER 1 second
    [HideInInspector] public float runAccelAmount; // The force to apply through Unity to achieve the desired acceleration;


    [Space(20)]

    [Header("Jump")]
    public float jumpHeight; // Desired jump height
    public float jumpTimeToApex; // How long it should take to reach the peak of the jump.
                             // Gravity and jump force are calculated using these 2 parameters
    public float distanceToApex; // how far along the x axis the player should be during the jump peak (used to calculate height time to apex)(assumes max speed)
    [HideInInspector] public float jumpForce; // The upwards force applied in Unity to achieve the calculated jump parameters

    [Space(5)]
    public float jumpHangThreshold; // speeds at which the character hangs a bit, at the peak of the jump (near 0)
    public float jumpHangAccelMult; // extra acceleration during this time to help with air control

    [Space(5)]
    public int MultiJumps; // amount of air jumps possible
    public float airJumpForceMult; // make the air jumps stronger or weaker than ones from the ground

    [Space(10)]

    [Header("Assists")]
    public float coyoteTime; // grace period while not grounded where you can still jump
    public float jumpInputBufferTime; // period to store an inputted jump for if the conditions are not yet met (i.e pressed just before landing)


    private void OnValidate()
    {
        // uses projectile motion formulas
        jumpTimeToApex = distanceToApex / runMaxSpeed;
        gravityStrength = (-2 * jumpHeight) / (jumpTimeToApex * jumpTimeToApex);

        //Calculate gravity to apply to Rigidbody;
        gravityScale = gravityStrength / Physics2D.gravity.y;

        // convert acceleration from seconds to frames, unity performs 
        // and then some kinda relation to runMaxSpeed, I don't write the code, I just copy it
        runAccelAmount = (runAcceleration / Time.fixedDeltaTime) / runMaxSpeed;

        // projectile motion formula again
        jumpForce = Mathf.Abs(gravityStrength) * jumpTimeToApex;

        runAcceleration = Mathf.Clamp(runAcceleration, 0.01f, runMaxSpeed);

    }

}
