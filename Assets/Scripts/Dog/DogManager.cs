using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogManager : MonoBehaviour
{
    Animator animator;
    Rigidbody rb;

    float x;
    float z;

    public int maxHp = 100;
    int hp;
    bool isDead = false;

    public float moveSpeed = 2;
    public Collider weaponCollider;
    public DogUIManager dogUIManager;
    public GameObject gameOverText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        hp = maxHp;
        dogUIManager.init(this);

        HideWeaponCollider();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }

        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate() {
        if (isDead)
        {
            return;
        }

        rb.velocity = new Vector3(x, 0, z) * moveSpeed;
        animator.SetFloat("MoveSpeed", rb.velocity.magnitude);

        Vector3 direction = transform.position + rb.velocity;
        transform.LookAt(direction);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Attack");
        }
    }

    void GetDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            hp = 0;
            animator.SetTrigger("Die");
            isDead = true;
            gameOverText.SetActive(true);
            rb.velocity = Vector3.zero;
        }

        dogUIManager.UpdateHP(hp);
    }

    private void OnTriggerEnter(Collider other) {
        if (isDead)
        {
            return;
        }

        Damager damager = other.GetComponent<Damager>();

        if (damager != null)
        {
            GetDamage(damager.damage);
            animator.SetTrigger("Hurt");
        }
    }

    // アニメーションクリップの関数
    public void HideWeaponCollider()
    {
        weaponCollider.enabled = false;
    }

    public void ShowWeaponCollider()
    {
        weaponCollider.enabled = true;
    }
}
