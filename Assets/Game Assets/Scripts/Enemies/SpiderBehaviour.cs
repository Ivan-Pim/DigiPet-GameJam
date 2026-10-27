using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpiderBehaviour : MonoBehaviour
{    
    [SerializeField]private GameObject spiderBody;
    //private Animator animator;

    [SerializeField] private float castDist = 75f, bumpDist = 0.2f;
    [SerializeField] private Vector2 castSize = new Vector2(5f, 5f);
    [SerializeField] private LayerMask playersLayer, obstacleLayer;

    [SerializeField] private float descentSpeed = 30f, ascentSpeed = 15f;
    private float origin;
    private float centerToFront;

    enum State {
        Idling,
        Swooping,
        Returning
    }

    private State state = State.Idling;

    private void Awake()
    {
        //animator = GetComponent<Animator>();
        origin = spiderBody.transform.position.y;
        centerToFront = spiderBody.GetComponent<Collider2D>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 spiderPosition = spiderBody.transform.position;

        switch (state)
        {
            case State.Idling:
                RaycastHit2D hit = Physics2D.BoxCast(transform.position, castSize, 0f, Vector2.down, castDist, playersLayer);
                    if (!(hit.collider == null) && hit.transform.CompareTag("Player")) {
                        state = State.Swooping;
                        //animator.SetBool("Swooping", true);
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
                    //animator.SetBool("Swooping", false);
                } else {
                    spiderBody.transform.Translate(Vector2.down * descentSpeed * Time.deltaTime);
                }

                break;
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
    }
    #endregion
}
