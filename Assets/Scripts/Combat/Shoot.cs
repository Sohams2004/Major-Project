using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private bool isCollectable;

    private int ammoLayer;
    
    [SerializeField] TextMeshProUGUI currentAmmoText;
    [SerializeField] TextMeshProUGUI magazineSizeText;

    [SerializeField] private Texture2D crosshair;


    private void Start()
    {
        currentAmmo = magazineSize;
        ammoLayer = LayerMask.NameToLayer("Ammo");
        
        currentAmmoText = GameObject.Find("CurrentAmmoText").GetComponent<TextMeshProUGUI>();
        magazineSizeText = GameObject.Find("MagazineSizeText").GetComponent<TextMeshProUGUI>();
        
        currentAmmoText.text = currentAmmo.ToString();
        magazineSizeText.text = "/ " + magazineSize.ToString();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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
            currentAmmoText.text = currentAmmo.ToString();
        }

        if (currentAmmo >= magazineSize)
        {
            isCollectable = false;
        }
        else
            isCollectable = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == ammoLayer && isCollectable)
        {
            Debug.Log("destoryeddddddddddddddddd");
            currentAmmo++;
            currentAmmoText.text = currentAmmo.ToString();
            Destroy(other.gameObject);
        }
    }

    void OnEnable()
    {
        Cursor.SetCursor(crosshair, Vector2.zero, CursorMode.Auto);
    }

    private void OnDisable()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }


    void Update()
    {
        ShootProjectile();
    }
}
