using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollider : MonoBehaviour
{

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            GameObject.Find("LevelManager").GetComponent<PlayerData>().Damage();
            Destroy(transform.parent.gameObject);
        }
    }
}
