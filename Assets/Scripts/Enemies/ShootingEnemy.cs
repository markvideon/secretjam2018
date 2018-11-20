using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : BaseEnemy
{
    #region Fields

    // SETTINGS //
    public Bullet bulletPrefab;
    public float holdDistance = 4;
    public float holdDuration = 5;
    public float shootDelay = 1;

    // VARIABLES //
    private float holdTimer;
    private float bulletTimer = 3;

    // REFERENCES //

    #endregion


    public bool IsHolding
    {
        get { return holdTimer > 0; }
    }

    #region Unity Functions

    #endregion

    #region Functions

    protected override void Awake()
    {
        base.Awake();

        holdTimer = holdDuration;
    }

    protected override void MoveUpdate()
    {
        Transform anchorPointTransform = movementGrid.GetAnchorPoint(currentAnchorPoint);

        // Move up to hold distance.
        if (transform.position.z > holdDistance)
        {
            float newZPosition = transform.position.z - movementGrid.levelMoveSpeed * Time.deltaTime;
            newZPosition = Mathf.Max(newZPosition, holdDistance);

            transform.position = new Vector3(anchorPointTransform.position.x, anchorPointTransform.position.y, newZPosition);
        }
        else
        {
            // Shoot and hold at that distance.
            if (holdTimer > 0)
            {
                holdTimer -= Time.deltaTime;

                // Shoot
                if (bulletTimer > 0)
                {
                    bulletTimer -= Time.deltaTime;
                }
                else
                {
                    bulletTimer = shootDelay;
                    Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                    bullet.currentAnchorPoint = currentAnchorPoint;
                }
            }
            else
            {
                // After a certain amount of time move towards the player.
                float newZPosition = transform.position.z - movementGrid.levelMoveSpeed * Time.deltaTime;
                transform.position = new Vector3(anchorPointTransform.position.x, anchorPointTransform.position.y, newZPosition);
            }
        }
    }

    public override void Damage()
    {
        playerData.CurrentGold += goldIncrement;
        movementGrid.enemies.Remove(this);
        Destroy(gameObject);
    }

    #endregion
}
