using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossJungsikPattern1 : MonoBehaviour
{
    Animator animator;
    BossJungsikMove bossMove;
    BossJungsik boss;
    public GameObject attackAni;

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

        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Player")
            {
                if (!isAttacking && bossMove.dis <= 2)
                {
                    attackCoroutine = StartCoroutine(Attack(collider));
                }
            }
        }

        
    }

    IEnumerator Attack(Collider2D collider)
    {
        if (isAttacking == false)
        {
            attackAni.SetActive(true);
            isAttacking = true;
            bossMove.isAttack = true;

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
                Debug.Log("Attack");
                count++;
                collider.GetComponent<Player>().TakeDamge(boss.bossAttackDamge);
            }

            attackAni.SetActive (false);
            isAttacking = false;
            bossMove.isAttack = false;
        }
    }

    private void OnDrawGizmos() //범위 표시
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(AttackPos.position, Attackbox);
    }
}
