using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    Animator animator;
    EnemyMove enemyMove;
    Enemy enemy;
    public Transform AttackPos;
    public Vector2 Attackbox;

    private float waitTime = 1.3f;

    private bool isAttacking = false;
    private float attackCooldown = 1.5f; // 공격 쿨다운 시간
    private Coroutine attackCoroutine; // 코루틴 참조 변수 추가

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyMove = GetComponent<EnemyMove>();
        enemy = GetComponent<Enemy>();
        waitTime = enemy.enemyAttackSpeed;
        attackCooldown = enemy.enemyAttackCooldonw;
    }


    void Update()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(AttackPos.position, Attackbox, 0);

        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Player")
            {
                if (!isAttacking && enemyMove.dis <= 2)
                {
                    // 쿨다운이 끝나면 공격을 시작
                    if (attackCoroutine == null)
                    {
                        attackCoroutine = StartCoroutine(Attack(collider));
                    }
                }
            }
        }
    }

    IEnumerator Attack(Collider2D collider)
    {
        if (isAttacking == false)
        {
            isAttacking = true;
            enemyMove.isAttack = true;

            yield return new WaitForSeconds(waitTime);

            Collider2D[] updatedCollider2Ds = Physics2D.OverlapBoxAll(AttackPos.position, Attackbox, 0);
            bool playerStillInRange = false;

            foreach (Collider2D updatedCollider in updatedCollider2Ds)
            {
                if (updatedCollider.CompareTag("Player"))
                {
                    playerStillInRange = true;
                    break;
                }
            }
            if (playerStillInRange)
            {
                collider.GetComponent<Player>().TakeDamge(enemy.enemyAttackDamge,gameObject);
            }

            isAttacking = false;
            enemyMove.isAttack = false;

            yield return new WaitForSeconds(attackCooldown);
            attackCoroutine = null;
        }
    }

    private void OnDrawGizmos() //범위 표시
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(AttackPos.position, Attackbox);
    }
}
