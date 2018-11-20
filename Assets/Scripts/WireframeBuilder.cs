using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
public class WireframeBuilder: MonoBehaviour
{
    MeshFilter mf;
    MeshRenderer mr; 
    LineRenderer lr1;
    LineRenderer lr2;
    LineRenderer lr3;
    [SerializeField] Material lineMaterial;
    [SerializeField] GameObject dummy;



    public float radiusProximity = 0.1f;
    public float width = 0.1f;

    Vector3 pool_centre = new Vector3();
    Vector3 pool_start = new Vector3();
    Vector3 pool_finish = new Vector3();

    private int vertexCount = 0;

    Color currentColour;

    // Use this for initialization
    void Start()
    {

        mf = GetComponent<MeshFilter>();
        mr = GetComponent<MeshRenderer>();

        // Line Renderer does not function as expected unless using 
        // series of simple two point segments rather than a single chain
        Instantiate(dummy, this.transform);
        Instantiate(dummy, this.transform);
        Instantiate(dummy, this.transform);


        lr1 = this.transform.GetChild(0).gameObject.AddComponent<LineRenderer>();
        lr2 = this.transform.GetChild(1).gameObject.AddComponent<LineRenderer>();
        lr3 = this.transform.GetChild(2).gameObject.AddComponent<LineRenderer>();

        setDefaultLineProperties(lr1);
        setDefaultLineProperties(lr2);
        setDefaultLineProperties(lr3);

        pool_centre = mf.transform.TransformPoint(mf.mesh.bounds.center);

        // De-render mesh, leaving wireframe
        mr.enabled = false;
    }

    void Update()
    {
        DrawWire(0, 1, lr1);
        DrawWire(0, 2, lr2);
        DrawWire(2, 3, lr3);
    }



    void setDefaultLineProperties(LineRenderer lr)
    {
        lr.positionCount = 2;
        lineMaterial.color = this.transform.parent.GetComponent<WireframeColourParent>().currentColour;
        lr.startColor = lineMaterial.color;
        lr.endColor = lineMaterial.color;
        lr.material = lineMaterial;
        lr.startWidth = width;
        lr.endWidth = 1.5f*width;
    }

    void DrawWire(int p0, int p1, LineRenderer lr) {

        pool_start = mf.transform.TransformPoint(mf.mesh.vertices[p0]);
        pool_finish = mf.transform.TransformPoint(mf.mesh.vertices[p1]);

        pool_start.x = pool_start.x + radiusProximity * (pool_centre.x - pool_start.x);
        pool_start.z = pool_start.z + radiusProximity * (pool_centre.z - pool_start.z);

        pool_finish.x = pool_finish.x + radiusProximity * (pool_centre.x - pool_finish.x);
        pool_finish.z = pool_finish.z + radiusProximity * (pool_centre.z - pool_finish.z);

        lr.SetPosition(0, pool_start);
        lr.SetPosition(1, pool_finish);

    }


}
