using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour
{
    Animator animator;
    Enemy enemy;
    Rigidbody2D rigid;
    public Image img;
    SpriteRenderer spriteRenderer;
    public int moveDir;    // Moving direction, Random
    private bool isFacingRight = true;

    public Transform searchPos;
    public Vector2 searchbox;
    private bool playerDetected = false; // 플레이어 감지 여부를 나타내는 변수를 추가합니다.

    public bool isAttack = false;

    private Transform target;
    private bool isKnockBack = false;
    private bool isMoving = false; // 움직임을 체크하는 변수를 추가합니다.

    public float dis;

    void Awake()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        StartCoroutine("monsterAI");
    }

    void Update()
    {
        Filp();

        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(searchPos.position, searchbox, 0);

        // 감지 여부를 초기화합니다.
        playerDetected = false;

        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Player")
            {
                playerDetected = true; // 플레이어를 감지한 경우 변수를 true로 설정합니다.
                dis = Vector3.Distance(transform.position, target.position);
                if (isAttack)
                {
                    moveDir = 0;
                }
                if (!isAttack && !isKnockBack)
                {
                    if (!isMoving) // 움직임을 체크하여 한 번만 실행되도록 합니다.
                    {
                        startMove();
                        isMoving = true;
                    }
                    Vector3 playerPos = collider.transform.position;
                    if (playerPos.x > transform.position.x)
                    {
                        moveDir = enemy.enemySpeed;     // speed up
                    }
                    else if (playerPos.x < transform.position.x)
                    {
                        moveDir = enemy.enemySpeed*-1;
                    }
                }
            }
        }

        // 플레이어를 감지하지 못한 경우 디버그 메시지 출력
        if (!playerDetected)
        {
            isMoving = false; // 플레이어를 감지하지 못한 경우 움직임 상태를 초기화합니다.
        }
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
        img.transform.localScale = new Vector3(isFacingRight ? 1 : -1, 1, 1);
    }

    void FixedUpdate()
    {
        if (!isKnockBack)
        {
            rigid.velocity = new Vector2(moveDir, rigid.velocity.y);   // no jump monster
        }
    }

    IEnumerator monsterAI()
    {
        moveDir = Random.Range(-1, 2);   // -1<= ranNum <2
        yield return new WaitForSeconds(4f);
        StartCoroutine("monsterAI");
    }

    public void startMove()
    {
        StartCoroutine("monsterAI");
    }

    public void stopMove()
    {
        StopCoroutine("monsterAI");
    }

    public void KnockBack()
    {
        Invoke("KnockBack1", 0.2f);
    }
    public void KnockBack1()
    {
        isKnockBack = true;
        Vector2 knockBackDirection = transform.position - target.position;
        knockBackDirection.Normalize();
        rigid.velocity = knockBackDirection * enemy.knockBackForce;
        StartCoroutine(ResetKnockback());
    }

    private IEnumerator ResetKnockback()
    {
        // 0.1초 후에 rigid.velocity를 초기화하여 넉백 효과를 중지합니다.
        yield return new WaitForSeconds(0.1f);
        rigid.velocity = Vector2.zero;
        yield return new WaitForSeconds(1f);
        isKnockBack = false;
    }

    private void OnDrawGizmos() //범위 표시
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(searchPos.position, searchbox);
    }
}
