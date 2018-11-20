using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SideMovingEnemy : BaseEnemy
{
    #region Fields

    // SETTINGS //
    public float moveDelay = 1;

    // VARIABLES //
    bool jumped;
    float moveTimer;
    int moveDirection;

    // REFERENCES //
    Material material;
    Transform model;
    #endregion

    #region Unity Functions

    protected override void Awake()
    {
        base.Awake();
        moveDirection = (Random.value > 0.5) ? 1 : -1;
        moveTimer = moveDelay;
        model = transform.Find("Model");

        foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
        {
            if (material == null)
            {
                material = new Material(renderer.material);
            }

            renderer.material = material;
        }
    }

    protected override void Update()
    {
        if (moveTimer > 0)
        {
            moveTimer -= Time.deltaTime;

            if (moveTimer < 0.5 && !jumped)
            {
                jumped = true;
                model.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.5f);
            }
        }
        else
        {
            jumped = false;
            moveTimer = moveDelay;
            currentAnchorPoint += moveDirection;
        }

        base.Update();

        material.color = new Color(moveTimer / moveDelay, moveTimer / moveDelay, 1);
    }

    #endregion

    #region Functions

    public override void Damage()
    {
        playerData.CurrentGold += goldIncrement;
        movementGrid.enemies.Remove(this);
        Destroy(gameObject);
    }

    #endregion
}
