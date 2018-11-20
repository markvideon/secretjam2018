using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometryProperties : MonoBehaviour {

    public List<Transform> playerAnchorPoints = new List<Transform>();
    private GameObject emptyPool;

    public MovementGrid movementGrid;

    public bool flipAnchors = false;

    public void Awake()
    {
        movementGrid = GameObject.Find("LevelManager").GetComponent<MovementGrid>();
        emptyPool = GameObject.Find("EmptyPool");

    }

    public void CalculateGeometries()
    {
        if (!flipAnchors)
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                emptyPool.transform.GetChild(i).transform.position = this.transform.GetChild(i).GetComponent<MeshCollider>().bounds.center;
                playerAnchorPoints.Add(emptyPool.transform.GetChild(i).transform);
            }
        }
        else
        {
            for (int i = this.transform.childCount - 1; i >= 0 ; i--)
            {
                emptyPool.transform.GetChild(i).transform.position = this.transform.GetChild(i).GetComponent<MeshCollider>().bounds.center;
                playerAnchorPoints.Add(emptyPool.transform.GetChild(i).transform);
            }
        }
    }

    private void Update()
    {
    }
}
