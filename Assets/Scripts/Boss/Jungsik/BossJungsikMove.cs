using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJungsikMove : MonoBehaviour
{
    Animator animator;
    BossJungsik boss;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    public float moveDir;    // Moving direction, Random
    private bool isFacingRight = true;

    public Transform searchPos;
    public Vector2 searchbox;

    public bool isAttack = false;

    private Transform target;

    void Awake()
    {
        animator = GetComponent<Animator>();
        boss = GetComponent<BossJungsik>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Filp();

        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(searchPos.position, searchbox, 0);

        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Player")
            {
                float dis = Vector3.Distance(transform.position, target.position);
                if (dis <= 2)
                {
                    isAttack = true;
                    moveDir = 0f;
                }
                else
                {
                    isAttack = false;
                }
                if (!isAttack)
                {
                    Vector3 playerPos = collider.transform.position;
                    if (playerPos.x > transform.position.x)
                    {
                        moveDir = 5.5f;     // speed up
                    }
                    else if (playerPos.x < transform.position.x)
                    {
                        moveDir = -5.5f;
                    }
                }
            }
        }
    }
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(moveDir, rigid.velocity.y);   // no jump monster
    }
    private void Filp()
    {
        if (isFacingRight && rigid.velocity.x < 0f || !isFacingRight && rigid.velocity.x > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnDrawGizmos() //범위 표시
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(searchPos.position, searchbox);
    }
}
