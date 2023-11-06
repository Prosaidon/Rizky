using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject fireballPrefab; // Menggunakan prefab tunggal untuk fireball
    private Animator anim;
    private Player player;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && cooldownTimer >= attackCooldown && player.canAttack()) // Menggunakan GetMouseButtonDown untuk mengecek klik mouse
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;

        // Membuat instance fireballPrefab pada posisi firePoint
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);

        // Menetapkan arah fireball berdasarkan arah karakter
        float direction = Mathf.Sign(transform.localScale.x);
        fireball.GetComponent<Projectile>().SetDirection(direction);
    }
}
