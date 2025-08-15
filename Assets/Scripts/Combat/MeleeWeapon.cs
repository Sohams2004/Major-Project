using System;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    int wallLayer;
    int enemyLayer;
    

    private void Start()
    {
        wallLayer = LayerMask.NameToLayer("Wall");
        enemyLayer = LayerMask.NameToLayer("Enemy");
        
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == enemyLayer)
        {
            Destroy(other.gameObject);
        }
    }
}
