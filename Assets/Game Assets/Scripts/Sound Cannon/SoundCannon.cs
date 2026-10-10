using System.Collections;
using UnityEngine;

public class SoundCannon : MonoBehaviour
{
    #region Cannon Data
    [SerializeField] private bool isActive = false;
    [SerializeField] private float attackFrequency = 1.0f;
    [SerializeField] private float attackDelay = 0;
    private float timeSinceLastShot = 0f;

    [SerializeField] private Vector2 bulletSpawn = Vector2.right;
    [SerializeField] private GameObject bullet;
    #endregion

    [SerializeField] private float bulletSpeed = 1.0f;
    [SerializeField] private float maxDistance = 50f;

    void Start()
    {
        StartCoroutine(WaitingRoutine(attackDelay));
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive) {
            timeSinceLastShot += Time.deltaTime;
            if (timeSinceLastShot > attackFrequency) {
                timeSinceLastShot = 0f;
                Fire();
            }
        }
    }

    void Fire()
    {
        SoundBullet newBullet = Instantiate(bullet, transform, false).GetComponent<SoundBullet>();
        newBullet.transform.localPosition = bulletSpawn;
        newBullet.setSpeed(bulletSpeed);
        newBullet.setMaxDistance(maxDistance);
    }

    public IEnumerator WaitingRoutine(float wait)
    {
        yield return new WaitForSeconds(wait);
        isActive = true;
    }
}
