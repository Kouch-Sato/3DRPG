using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;
    Animator animator;

    public int maxHp = 100;
    int hp;

    public Collider weaponCollider;
    public EnemyUIManager enemyUIManager;
    public GameObject gameClearText;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.destination = target.position;

        hp = maxHp;
        enemyUIManager.init(this);

        HideWeaponCollider();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.position;
        animator.SetFloat("Distance", agent.remainingDistance);
    }

    void GetDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            hp = 0;
            animator.SetTrigger("Die");
            Destroy(gameObject, 2);
            gameClearText.SetActive(true);
        }
        enemyUIManager.updateHp(hp);
    }

    public void LookAtTarget()
    {
        transform.LookAt(target);
    }

    private void OnTriggerEnter(Collider other) {
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
