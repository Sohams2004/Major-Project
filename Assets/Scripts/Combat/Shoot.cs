using System;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject projectilePrefab; 
    public Transform firePoint; 
    public float fireRate = 1f; 
    [SerializeField] float nextFireTime = 0f;
    Vector2 shootDirection;

    [SerializeField] private int magazineSize;
    [SerializeField] private int currentAmmo;
    [SerializeField] private float reloadTime = 1.5f;
    [SerializeField] private bool autoReload = true;
    [SerializeField] private bool isReloading = false;

    private void Start()
    {
        currentAmmo = magazineSize;
    }

    void ShootProjectile()
    {
        shootDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position;
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg - 90f;
        firePoint.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            if (currentAmmo <= 0)
            {
                return;
            }
            nextFireTime = Time.time + 1f / fireRate;
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            currentAmmo--;
        }
    }
    
    void Update()
    {
        ShootProjectile();
    }
}
