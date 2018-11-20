using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class MovementGrid : MonoBehaviour
{
    #region Fields

    // SETTINGS //
    public float levelMoveSpeed = 0.1f;
    public float nextLevelDistance = 5;
    public float levelLength = 50;
    public float transitionDuration = 5;
    public int requriedNextLevelGold = 100;
    public int currentLevel = 1;

    public List<BaseEnemy> enemyLibrary; // Enemies will be added to the list of possible enemies on the level of their index.

    // VARIABLES //
    public bool transitioning;
    [SerializeField] public Transform currentMesh;
    Transform nextMesh;
    [SerializeField] List<GameObject> possibleMeshes;
    [System.NonSerialized] public List<BaseEnemy> enemies = new List<BaseEnemy>();
    public List<BaseEnemy> possibleEnemies = new List<BaseEnemy>();
    int textCounter;

    public float currentGameTime = 0;
   
    public GeometryProperties currentGeometryProperties;

    float enemySpawnTimer = 0;

    public System.Action onStartTransition;
    public System.Action onEndTransition;

    // REFERENCES //
    PlayerController player;
    PlayerData playerData;
    GameObject transitionText;


    #endregion

    #region Properties

    public void ChangeMesh()
    {
        GameObject mesh = Instantiate(possibleMeshes[Random.Range(0, possibleMeshes.Count)]);

        mesh.transform.position = new Vector3(currentMesh.transform.position.x, currentMesh.transform.position.y, 10f);

        Destroy(currentMesh.gameObject);

        // Swap to the next mesh.
        currentMesh = mesh.transform;
        Camera.main.transform.position = new Vector3(currentMesh.GetComponent<Collider>().bounds.center.x, currentMesh.GetComponent<Collider>().bounds.center.y, -1.25f);

        currentGeometryProperties = currentMesh.GetComponent<GeometryProperties>();

        currentGeometryProperties.CalculateGeometries();
    }

    #endregion

    #region Unity Functions

    private void Awake()
    {
        // GATHER REFERENCES //
        player = GameObject.Find("player").GetComponent<PlayerController>();
        playerData = GetComponent<PlayerData>();
        transitionText = GameObject.Find("Transition Text");
        transitionText.SetActive(false);

        // INITIALIZE //
        currentGeometryProperties = currentMesh.GetComponent<GeometryProperties>();
        Camera.main.transform.position = new Vector3(currentMesh.GetComponent<Collider>().bounds.center.x, currentMesh.GetComponent<Collider>().bounds.center.y, -1.25f);
        currentGeometryProperties.CalculateGeometries();

        currentMesh.transform.position = new Vector3(currentMesh.transform.position.x, currentMesh.transform.position.y, -1f);
        possibleEnemies.Add(enemyLibrary[currentLevel - 1]);
    }

    private void Update()
    {
        currentGameTime += Time.deltaTime;

        if (!transitioning)
        {
            if (playerData.CurrentGold >= requriedNextLevelGold)
            {
                StartTransition();
            }

            if (Input.GetKeyDown(KeyCode.U))
            {
                StartTransition();
            }

            if (enemySpawnTimer > 0)
            {
                enemySpawnTimer -= Time.deltaTime;
            }
            else
            {
                // SPAWN ENEMY //
                enemySpawnTimer = GetNextEnemySpawnTimer();

                int position = 0;
                bool goodPosition = false;
                int tries = 0;

                    // Make sure we're not in the same lane as a shooter enenmy;
                do
                {
                    position = Random.Range(0, possibleEnemies.Count);

                    goodPosition = true;

                    foreach (BaseEnemy enemy in enemies)
                    {
                        if (enemy is ShootingEnemy)
                        {
                            if (enemy.currentAnchorPoint == position && (enemy as ShootingEnemy).IsHolding)
                            {
                                goodPosition = false;
                            }
                        }
                    }

                    tries++;
                }
                while (!goodPosition && tries < 30);

                BaseEnemy newEnemy = Instantiate(possibleEnemies[position]);
                newEnemy.currentAnchorPoint = Random.Range(0, currentGeometryProperties.playerAnchorPoints.Count);
                newEnemy.transform.position = new Vector3(0, 0, 10);
            }
        }
        else
        {
            currentMesh.position += -Vector3.forward * levelMoveSpeed * Time.deltaTime;
            if (textCounter % 6 == 0)
            {
                transitionText.GetComponent<Text>().color = new Color(Random.value, Random.value, Random.value);
            }

            textCounter++;

            RectTransform textRectTransform = transitionText.GetComponent<RectTransform>();
           // textRectTransform.DOAnchorPosY(textRectTransform.anchoredPosition.y + 500, 3);
        }
    }

    #endregion

    #region Functions

    public Transform GetAnchorPoint(int _anchorPoint)
    {
        return currentGeometryProperties.playerAnchorPoints[_anchorPoint];
    }

    public void StartTransition()
    {
        transitionText.SetActive(true);
        transitioning = true;
        StartCoroutine(GotoNextLevel());
        RectTransform textRectTransform = transitionText.GetComponent<RectTransform>();
        levelMoveSpeed = 5;

        //textRectTransform.anchoredPosition = new Vector2(textRectTransform.anchoredPosition.x, -178.85f);

        ClearEnemies();

        onStartTransition();
    }

    private IEnumerator GotoNextLevel()
    {
        yield return new WaitForSeconds(transitionDuration);

        ChangeMesh();
        transitionText.SetActive(false);

        currentLevel++;
        requriedNextLevelGold += (int)(500*currentLevel + Mathf.Pow(10, currentLevel / 1.5f));

        while (currentMesh.transform.position.z != -1)
        {
            currentMesh.transform.position = new Vector3(currentMesh.transform.position.x, currentMesh.transform.position.y, currentMesh.transform.position.z - levelMoveSpeed * Time.deltaTime);

            if (currentMesh.transform.position.z <= -1)
            {
                currentMesh.transform.position = new Vector3(currentMesh.transform.position.x, currentMesh.transform.position.y, -1f);
                break;
            }
        
            yield return null;
        }

        transitioning = false;
        enemySpawnTimer = 3 + GetNextEnemySpawnTimer();

        if (currentLevel - 1 < enemyLibrary.Count && enemyLibrary[currentLevel - 1] != null)
        {
            possibleEnemies.Add(enemyLibrary[currentLevel - 1]);
        }

        levelMoveSpeed = Mathf.Min(levelMoveSpeed + (currentLevel * 0.5f), 50f);

        onEndTransition();
    }

    private float GetNextEnemySpawnTimer()
    {
        return Random.Range(3, 8);
    }

    public void ClearEnemies()
    {
        foreach (BaseEnemy enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }

        enemies.Clear();
    }

    #endregion
}
