using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayerAnchors : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int j = this.transform.childCount;


        Debug.Log(this.transform.GetComponent<GeometryProperties>().playerAnchorPoints.Count);

        for (int i = 0; i < j; i++) {
            Debug.Log(this.transform.GetChild(i).GetComponent<MeshCollider>().bounds);
           // this.transform.GetChild(i).GetComponent<MeshCollider>().bounds.center;
        }
	}
	

}
