using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    #region Fields

    // SETTINGS //
    public float attackRange = 1.5f;

    // VARIABLES //
    int currentAnchorIndex = 0;

    // REFERENCES //
    MovementGrid movementGrid;
    PlayerData playerData;
    Transform attackPlane;
    Material attackPlaneMaterial;
    GameObject sound;

    #endregion

    #region Unity Functions

    private void Awake()
    {
        // GATHER REFERENCES //
        movementGrid = GameObject.Find("LevelManager").GetComponent<MovementGrid>();
        playerData = movementGrid.GetComponent<PlayerData>();
        attackPlane = transform.Find("Attack Plane");
        sound = GameObject.Find("SoundPlayer");

        attackPlane.localScale = new Vector3(0.75f, 0.004835f, attackRange / 2);
        attackPlane.localPosition = new Vector3(0, attackPlane.localPosition.y, attackRange / 4);

        movementGrid.onStartTransition += OnTransitionStart;
        movementGrid.onEndTransition += OnTransitionEnd;

        attackPlaneMaterial = attackPlane.GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        if (!movementGrid.transitioning)
        {
            // MOVEMENT //
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                currentAnchorIndex--;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                currentAnchorIndex++; 
            }

            // Wrap the values.
            if (currentAnchorIndex < 0)
            {
                currentAnchorIndex = movementGrid.currentGeometryProperties.playerAnchorPoints.Count - 1;
            }
            else if (currentAnchorIndex > movementGrid.currentGeometryProperties.playerAnchorPoints.Count - 1)
            {
                currentAnchorIndex = 0;
            }

            transform.position = Vector3.Lerp(transform.position, new Vector3(movementGrid.GetAnchorPoint(currentAnchorIndex).position.x, movementGrid.GetAnchorPoint(currentAnchorIndex).position.y, 0), Time.deltaTime * 8);

            Vector3 meshCenter = movementGrid.currentMesh.GetComponent<MeshCollider>().bounds.center;
            transform.rotation = Quaternion.LookRotation(transform.forward, new Vector3(meshCenter.x, meshCenter.y, transform.position.z) - transform.position);

            // PLAYER DAMAGE //
            if (Input.GetKeyDown(KeyCode.Space))
            {
                for (int i = 0; i < movementGrid.enemies.Count; i++)
                {
                    // If we're on the same lane and the enenmy is within attack range.
                    if (movementGrid.enemies[i].currentAnchorPoint == currentAnchorIndex && movementGrid.enemies[i].transform.position.z <= attackRange)
                    {
                        movementGrid.enemies[i].Damage();
                        // play success
                        sound.GetComponent<SoundPlayer>().PlayCollect();

                        break;
                    }
                }

                
            }

            // ENEMY DAMAGE //
            for (int i = 0; i < movementGrid.enemies.Count; i++)
            {
                if (movementGrid.enemies[i].transform.position.z <= 0)
                {
                    playerData.Damage();
                    Destroy(movementGrid.enemies[i].gameObject);
                    movementGrid.enemies.RemoveAt(i);
                    // play hit
                    sound.GetComponent<SoundPlayer>().PlayHit();
                    break;
                }
            }
        }
    }

    #endregion

    #region Events

    private void OnTransitionStart()
    {
        attackPlaneMaterial.DOFade(0, 0.25f);
    }

    private void OnTransitionEnd()
    {
        attackPlaneMaterial.DOFade(0.3647059f, 0.25f);
    }

    #endregion
}
