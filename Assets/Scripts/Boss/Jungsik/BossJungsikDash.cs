using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJungsikDash : MonoBehaviour
{
    public Transform AttackPos;
    public Vector2 Attackbox;

    public bool isDashAttack = false;
    private bool isDamage = false;
    private Coroutine attackCoroutine;

    void FixedUpdate()
    {
        if(isDashAttack)
        {
            Collider2D[] collider2D1s = Physics2D.OverlapBoxAll(AttackPos.position, Attackbox, 0);

            foreach (Collider2D collider in collider2D1s)
            {
                if (collider.tag == "Player")
                {
                    if (attackCoroutine == null)
                    {
                        if (!isDamage)
                        {
                            isDamage = true;
                            collider.GetComponent<Player>().TakeDamge(700, gameObject);
                        }
                    }
                }
            }
        }
        else
        {
            isDamage = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(AttackPos.position, Attackbox);
    }
}
