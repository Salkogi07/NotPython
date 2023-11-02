using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJungsikPattern1 : MonoBehaviour
{
    Animator animator;
    BossJungsikMove bossMove;
    BossJungsik boss;
    public GameObject attackAni;

    public Transform AttackPos;
    public Vector2 Attackbox;

    public Transform[] spawnPoints;
    public GameObject[] spawnPrefab;

    private float waitTime = 1.3f;

    private bool isAttacking = false;
    private float attackCooldown = 1.5f; // 공격 쿨다운 시간
    private Coroutine attackCoroutine;

    private float spawnCooldown = 10f;
    private float spawntimer = 0.0f;
    private int spawnCount = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        bossMove = GetComponent<BossJungsikMove>();
        boss = GetComponent<BossJungsik>();
    }

    IEnumerator EnemySpawn()
    {
        bossMove.isAttack = true;
        yield return StartCoroutine(SpanwEnemyStart());
        bossMove.isAttack = false;
        spawnCount++;
        spawntimer = 0.0f; // 타이머를 리셋합니다.
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
                    // 쿨다운이 끝나면 공격을 시작
                    if (attackCoroutine == null)
                    {
                        attackCoroutine = StartCoroutine(Attack(collider));
                    }
                }
            }
        }
    }
    private void FixedUpdate()
    {
        spawntimer += Time.deltaTime;
        if (spawntimer >= spawnCooldown)
        {
            if(spawnCount < 3)
            {
                StartCoroutine(EnemySpawn());
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
                collider.GetComponent<Player>().TakeDamge(boss.bossAttackDamge, gameObject);
            }

            attackAni.SetActive (false);
            isAttacking = false;
            bossMove.isAttack = false;

            yield return new WaitForSeconds(attackCooldown);
            attackCoroutine = null;
        }
    }

    IEnumerator SpanwEnemyStart()
    {
        int enemyIndex = Random.Range(0, 3);
        SpawnEnemy(enemyIndex, 0);
        enemyIndex = Random.Range(0, 3);
        SpawnEnemy(enemyIndex, 1);
        yield return null;

    }

    void SpawnEnemy(int enemyNum, int enemyPoint)
    {
        Instantiate(spawnPrefab[enemyNum], spawnPoints[enemyPoint]);
    }

    private void OnDrawGizmos() //범위 표시
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(AttackPos.position, Attackbox);
    }
}
