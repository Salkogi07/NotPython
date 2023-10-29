using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJungsikPattern1 : MonoBehaviour
{
    Animator animator;
    BossJungsikMove bossMove;
    BossJungsik boss;

    public Transform AttackPos;
    public Vector2 Attackbox;

    private float waitTime = 1.3f;

    private bool isAttacking = false;
    private Coroutine attackCoroutine; // 코루틴 참조 변수 추가

    int count = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        bossMove = GetComponent<BossJungsikMove>();
        boss = GetComponent<BossJungsik>();
    }

    IEnumerator EnemySpawn()
    {
        Debug.Log("EnemySpawn");
        yield return new WaitForSeconds(1);
    }

    IEnumerator SongAttack()
    {
        Debug.Log("SongAttack");
        yield return new WaitForSeconds(1);
    }

    void Update()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(AttackPos.position, Attackbox, 0);
        bool playerFound = false; // 플레이어를 발견했는지 여부를 추적하기 위한 변수

        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Player")
            {
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
            bossMove.isAttack = false;
        }
    }

    IEnumerator Attack(Collider2D collider)
    {
        if (isAttacking == false)
        {
            if (count == 3)
            {
                int nextPattern = Random.Range(1, 3); // 1 for SongAttack and 2 for EnemySpawn
                switch (nextPattern)
                {
                    case 1:
                        StartCoroutine(SongAttack());
                        break;
                    case 2:
                        StartCoroutine(EnemySpawn());
                        break;
                }
                count = 0; // reset the count
            }
            else
            {
                isAttacking = true;
                yield return new WaitForSeconds(waitTime);
                Debug.Log("Attack");
                count++;
                collider.GetComponent<Player>().TakeDamge(boss.bossAttackDamge);
                isAttacking = false;
            }
        }
    }

    private void OnDrawGizmos() //범위 표시
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(AttackPos.position, Attackbox);
    }
}