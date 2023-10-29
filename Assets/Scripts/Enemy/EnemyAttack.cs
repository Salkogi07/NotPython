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
    private Coroutine attackCoroutine; // �ڷ�ƾ ���� ���� �߰�

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyMove = GetComponent<EnemyMove>();
        enemy = GetComponent<Enemy>();
    }


    void Update()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(AttackPos.position, Attackbox, 0);
        bool playerFound = false; // �÷��̾ �߰��ߴ��� ���θ� �����ϱ� ���� ����

        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Player")
            {
                //enemyMove.isAttack = true;
                playerFound = true; // �÷��̾ �߰������� ǥ��
                if (!isAttacking)
                {
                    attackCoroutine = StartCoroutine(Attack(collider));
                }
            }
        }

        // �÷��̾ �߰����� ���ϸ� ���� �ڷ�ƾ�� ����
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

    private void OnDrawGizmos() //���� ǥ��
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(AttackPos.position, Attackbox);
    }
}
