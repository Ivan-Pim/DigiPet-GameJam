using System;
using UnityEngine;

public class WormBehaviour : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    enum FacingDirection {
        Right=1,
        Left=-1,
    }
    private FacingDirection facing = FacingDirection.Right; // 1 is facing right, -1 is facing left;

    private Collider2D collider;
    [SerializeField] private float rayDist = 0.2f;
    [SerializeField] private LayerMask wormsWalkOnThis;

    public bool reporting = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider = GetComponent<Collider2D>();
        if (facing.Equals(FacingDirection.Left) && transform.localScale.x > 0) Turn();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 face = new Vector2(collider.bounds.center.x + collider.bounds.extents.x * (int)facing, collider.bounds.min.y);

        if (!Physics2D.Raycast(face, Vector2.down, rayDist, wormsWalkOnThis) // steeping into a pit
            || Physics2D.Raycast(face, Vector2.right * (int)facing, rayDist, wormsWalkOnThis)) // stepping into a wall
        {
            Turn();
        }
        transform.Translate(Vector2.right * (int)facing * moveSpeed * Time.deltaTime);

        if (reporting) Debug.Log(face);
    }

    private void Turn()
    {
        // flip the sprite for turning, and store the current direction
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        facing = (facing.Equals(FacingDirection.Right)) ? FacingDirection.Left : FacingDirection.Right;
    }

    #region Debug methods
    #endregion
}
