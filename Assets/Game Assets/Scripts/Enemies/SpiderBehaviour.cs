using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class SpiderBehaviour : MonoBehaviour
{
    #region References & Constants
    [SerializeField]private GameObject spiderBody;
    private Animator animator;

    private float origin;
    private float centerToFront;
    #endregion

    #region Dropping parameters
    [SerializeField] private float castDist = 75f, bumpDist = 0.2f;
    [SerializeField] private Vector2 castSize = new Vector2(5f, 5f);
    [SerializeField] private LayerMask playersLayer, obstacleLayer;
    [SerializeField] private float descentSpeed = 30f, ascentSpeed = 15f;
    #endregion

    #region Horizontal movement parameters
    [SerializeField] private bool moving = false;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float allowedDistLeft = 0f, allowedDistRight = 0f;
    private float leftBorder, rightBorder;

    enum FacingDirection
    {
        Left = -1,
        Right = 1
    }

    FacingDirection facing = FacingDirection.Right;
    #endregion

    enum State
    {
        Idling,
        Swooping,
        Returning
    }

    private State state = State.Idling;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        origin = spiderBody.transform.position.y;
        centerToFront = spiderBody.GetComponent<Collider2D>().bounds.extents.y;

        leftBorder = transform.position.x - allowedDistLeft;
        rightBorder = transform.position.x + allowedDistRight;

        StartTurn();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 spiderPosition = spiderBody.transform.position;

        switch (state)
        {
            case State.Idling:
                if (moving) Move(moveSpeed, facing, leftBorder, rightBorder);
                RaycastHit2D hit = Physics2D.BoxCast(transform.position, castSize, 0f, Vector2.down, castDist, playersLayer);
                    if (!(hit.collider == null) && hit.transform.CompareTag("Player")) {
                        state = State.Swooping;
                        animator.SetBool("Swooping", true);
                    }
                
                break;

            case State.Returning:
                if (spiderPosition.y < origin) spiderBody.transform.Translate(Vector2.up * ascentSpeed * Time.deltaTime);
                else
                {
                    state = State.Idling;
                }

                break;

            case State.Swooping:
                if (Physics2D.Raycast(spiderPosition - new Vector2(0, centerToFront), Vector2.down, bumpDist, obstacleLayer) || (origin - spiderPosition.y >= castDist)) {
                    state = State.Returning;
                    animator.SetBool("Swooping", false);
                } else {
                    spiderBody.transform.Translate(Vector2.down * descentSpeed * Time.deltaTime);
                }

                break;
        }
    }

    private void Move(float moveSpeed, FacingDirection direction, float leftBorder, float rightBorder) {
        if ((direction == FacingDirection.Left && transform.position.x > leftBorder)
            || (direction == FacingDirection.Right && transform.position.x < rightBorder)) {
            transform.Translate(Vector2.right * (int)direction * moveSpeed * Time.deltaTime);
        } else {
            Turn();
        }

    }

    private void Turn()
    {
        // flip the sprite for turning, and store the current direction
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        facing = (facing.Equals(FacingDirection.Right)) ? FacingDirection.Left : FacingDirection.Right;
    }

    private void StartTurn() {
        if (FacingDirection.Left == facing)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void Catch() {
        Debug.Log("Spider got her! Climb back up");
    }

    #region Debug methods
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(spiderBody.transform.position, castSize);
        Gizmos.DrawWireCube(spiderBody.transform.position - new Vector3(0, castDist), castSize);

        Gizmos.color = Color.black;
        Gizmos.DrawLine(new Vector2(leftBorder, transform.position.y + 20f), new Vector2(leftBorder, transform.position.y - 20f));
        Gizmos.DrawLine(new Vector2(rightBorder, transform.position.y + 20f), new Vector2(rightBorder, transform.position.y - 20f));

        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector2(transform.position.x - allowedDistLeft, transform.position.y + 20f), new Vector2(transform.position.x - allowedDistLeft, transform.position.y - 20f));
        Gizmos.DrawLine(new Vector2(transform.position.x + allowedDistRight, transform.position.y + 20f), new Vector2(transform.position.x + allowedDistRight, transform.position.y - 20f));
    }
    #endregion
}
