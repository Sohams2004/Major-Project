using System;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject projectilePrefab; 
    public Transform firePoint; 
    public float fireRate = 1f; 
    private float nextFireTime = 0f;
    

    void ShootProjectile()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Debug.Log("Shooting projectile");
            Vector2 mousePos = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position).normalized;

            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            
            Debug.Log(projectile);
            projectile.GetComponent<MoveProjectile>().MoveObject(mousePos);
            Debug.Log(projectile + "Shot");
            nextFireTime = Time.time + 1f / fireRate;
        }
    }
    
    void Update()
    {
        ShootProjectile();
    }
}
