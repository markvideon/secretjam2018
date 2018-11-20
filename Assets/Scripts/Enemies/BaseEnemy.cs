using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    #region Fields

    // SETTINGS //

    // VARIABLES //
    public int currentAnchorPoint = 0;
    public int goldIncrement = 10;

    // REFERENCES //
    protected MovementGrid movementGrid;
    protected PlayerData playerData;

    #endregion

    #region Unity Functions

    protected virtual void Awake()
    {
        // GATHER REFERENCES //
        movementGrid = GameObject.Find("LevelManager").GetComponent<MovementGrid>();
        playerData = movementGrid.GetComponent<PlayerData>();

        movementGrid.enemies.Add(this);
    }

    protected virtual void Update()
    {
        // Wrap the values.
        if (currentAnchorPoint < 0)
        {
            currentAnchorPoint = movementGrid.currentGeometryProperties.playerAnchorPoints.Count - 1;
        }
        else if (currentAnchorPoint > movementGrid.currentGeometryProperties.playerAnchorPoints.Count - 1)
        {
            currentAnchorPoint = 0;
        }

        MoveUpdate();

        Vector3 meshCenter = movementGrid.currentMesh.GetComponent<MeshCollider>().bounds.center;
        transform.rotation = Quaternion.LookRotation(transform.forward, new Vector3(meshCenter.x, meshCenter.y, transform.position.z) - transform.position);

    }

    #endregion

    #region Functions

    protected virtual void MoveUpdate()
    {
        Transform anchorPointTransform = movementGrid.GetAnchorPoint(currentAnchorPoint);

        transform.position = new Vector3(anchorPointTransform.position.x, anchorPointTransform.position.y, transform.position.z - movementGrid.levelMoveSpeed * Time.deltaTime);
    }

    public virtual void Damage()
    {
        playerData.CurrentGold += goldIncrement;
        movementGrid.enemies.Remove(this);
        Destroy(gameObject);
    }

    #endregion
}
