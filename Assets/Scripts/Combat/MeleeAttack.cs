using System;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public GameObject meleeWeaponPrefab;
    [SerializeField] float damage = 5f;
    [SerializeField] float attackCooldown = 0.5f;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] private float attackTimer;
    [SerializeField] bool isAttacking = false;
    [SerializeField] bool isHit = false;
    
    [SerializeField] Animator animator;
    [SerializeField] Animator enemyAnimator;
    
    [SerializeField] LayerMask Enemy;
    
    [SerializeField] AudioSource meleeAudioSource;

    private void Awake()
    {
        meleeAudioSource = GameObject.Find("MeleeWeaponPrefab").GetComponent<AudioSource>();
    }

    private void Update()
    {
        MeleeTimer();
        if(Input.GetMouseButtonDown(0))
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
            meleeAudioSource.Play();
            Attack();
            attackTimer = attackCooldown;
        }
        
        if(Input.GetMouseButtonUp(0))
        {
            isAttacking = false;
            //meleeWeaponPrefab.SetActive(false);
        }
    }

    void Attack()
    {
        if (isAttacking)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleeWeaponPrefab.transform.position, attackRange, Enemy);

            foreach (Collider2D hit in hitEnemies)
            {
                Debug.Log("Hit " + hit.name);
                hit.GetComponent<Animator>().SetTrigger("Hit");
                hit.GetComponent<Enemy>().enemyHealth -= damage;
            }
        }
    }
    
    
    void MeleeTimer()
    {
        if (isAttacking)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCooldown)
            {
                isAttacking = false;
                //meleeWeaponPrefab.SetActive(false);
                attackTimer = 0f; 
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(meleeWeaponPrefab.transform.position, attackRange);
    }
}
