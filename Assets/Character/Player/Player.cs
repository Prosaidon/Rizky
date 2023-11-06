using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded; // Tambahkan variabel grounded
    private BoxCollider2D boxCollider; // Tambahkan boxCollider
    private LayerMask groundLayer; // Tambahkan groundLayer

    private void Awake()
    {
        // Ambil referensi untuk rigidbody, animator, dan komponen lainnya
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        groundLayer = LayerMask.GetMask("Ground"); // Ganti "Ground" sesuai dengan nama layer yang Anda gunakan
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        // Flip player saat menghadap kiri atau kanan
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(2, 2, 1);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-2, 2, 1);

        if (Input.GetKey(KeyCode.Space) && grounded)
            Jump();

        // Set parameter animasi
        anim.SetBool("run", Mathf.Abs(horizontalInput) > 0.01f);
        anim.SetBool("grounded", grounded);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
         // Ambil horizontalInput di sini
        return Input.GetAxis("Horizontal") == 0 && grounded;
    }
}
