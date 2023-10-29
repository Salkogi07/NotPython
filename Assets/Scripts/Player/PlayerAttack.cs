using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator animator;
    Player player;
    PlayerMove playerMove;
    public Transform AttackPos;
    public Vector2 Attackbox;

    public bool isControl;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
        playerMove = GetComponent<PlayerMove>();
        isControl = true;
    }

    private float curTime;
    public float coolTime = 0.5f;

    void Update()
    {
        if (isControl)
        {
            EnemySearch();
        }
    }

    private void EnemySearch()
    {
        if (curTime <= 0)
        {
            if (Input.GetButtonDown("Attack"))
            {
                animator.SetBool("isIdle",false);
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(AttackPos.position, Attackbox, 0);
                foreach (Collider2D collider in collider2Ds)
                {
                    if (collider.tag == "Enemy")
                    {
                        int critical = player.playerCritical;
                        bool Crit = Dods_ChanceMaker.GetThisChanceResult_Percentage(critical);
                        collider.GetComponent<Enemy>().TakeDamge(player.playerAttackDamge, 0, Crit);
                    }
                    if (collider.tag == "Boss")
                    {
                        int critical = player.playerCritical;
                        bool Crit = Dods_ChanceMaker.GetThisChanceResult_Percentage(critical);
                        collider.GetComponent<BossJungsik>().TakeDamge(player.playerAttackDamge, 0, Crit);
                    }
                }
                playerMove.isAttack = true;
                animator.SetTrigger("isAttack");
                curTime = coolTime;
            }
            else
            {
                playerMove.isAttack = false;
                animator.SetBool("isIdle", true);
            }
        }
        else
        {
            curTime -= Time.deltaTime;
        }
    }

    private void OnDrawGizmos() //범위 표시
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(AttackPos.position, Attackbox);
    }


}
