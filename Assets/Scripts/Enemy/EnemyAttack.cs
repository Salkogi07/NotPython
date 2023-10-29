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
    private Coroutine attackCoroutine; // 코루틴 참조 변수 추가

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyMove = GetComponent<EnemyMove>();
        enemy = GetComponent<Enemy>();
    }


    void Update()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(AttackPos.position, Attackbox, 0);
        bool playerFound = false; // 플레이어를 발견했는지 여부를 추적하기 위한 변수

        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Player")
            {
                //enemyMove.isAttack = true;
                playerFound = true; // 플레이어를 발견했음을 표시
                if (!isAttacking)
                {
                    attackCoroutine = StartCoroutine(Attack(collider));
                }
            }
        }

        // 플레이어를 발견하지 못하면 공격 코루틴을 중지
        if (!playerFound && attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            isAttacking = false;
            enemyMove.isAttack = false;
        }
    }

    IEnumerator Attack(Collider2D collider)
    {
        if (isAttacking == false)
        {
            isAttacking = true;
            yield return new WaitForSeconds(waitTime);
            collider.GetComponent<Player>().TakeDamge(enemy.enemyAttackDamge);
            isAttacking = false;
        }
    }

    private void OnDrawGizmos() //범위 표시
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(AttackPos.position, Attackbox);
    }
}
