using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJungsikPattern2 : MonoBehaviour
{
    Animator animator;
    BossJungsik boss;

    [SerializeField] private Rigidbody2D rigid;
    public GameObject attackAni;

    public Transform AttackPos;
    public Vector2 Attackbox;

    public float moveDir;    // Moving direction, Random
    private bool isFacingRight = true;
    public float dis;

    public Transform searchPos;
    public Vector2 searchbox;

    public bool isAttack = false;
    public GameObject dashAttackObj;
    private BossJungsikDash dash;

    public GameObject toObj;

    private Transform target;

    private float waitTime = 1.3f;

    private bool isAttacking = false;
    private float attackCooldown = 1.5f;
    private Coroutine attackCoroutine;

    private bool canDash;
    private bool isDash = false;
    private float dashPower = 30f;
    private float dashTime = 0.2f;
    private float dashCooldown = 8f;
    private float dashtimer = 0.0f;

    private bool canSong;
    private bool isSong = false;
    private float songCooldown = 20f;
    private float songtimer = 0.0f;

    public float cooltimeSong;
    private float currenttimeSong;

    public GameObject bullet;
    public Transform pos;

    private void Awake()
    {
        dash = dashAttackObj.GetComponent<BossJungsikDash>();
        animator = GetComponent<Animator>();
        boss = GetComponent<BossJungsik>();
        rigid = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isDash)
        {
            return;
        }
        Collider2D[] collider2D1s = Physics2D.OverlapBoxAll(AttackPos.position, Attackbox, 0);

        foreach (Collider2D collider in collider2D1s)
        {
            if (collider.tag == "Player")
            {
                if (!isAttacking && dis <= 2)
                {
                    if (attackCoroutine == null)
                    {
                        attackCoroutine = StartCoroutine(Attack(collider));
                    }
                }
            }
        }
        dashAttack();
        songAttack();

        Filp();

        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(searchPos.position, searchbox, 0);

        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Player")
            {
                dis = Vector3.Distance(transform.position, target.position);

                if (isAttack)
                {
                    moveDir = 0;
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
        if (isDash)
        {
            return;
        }
        rigid.velocity = new Vector2(moveDir, rigid.velocity.y);   // no jump monster

        if(!isSong || !isDash)
        {
            dashtimer += Time.deltaTime;
            if (dashtimer >= dashCooldown)
            {
                canDash = true;
                dashtimer = 0f;
            }

            songtimer += Time.deltaTime;
            if (songtimer >= songCooldown)
            {
                canSong = true;
                songtimer = 0f;
            }
        }
        
        if (isSong)
        {
            if (currenttimeSong <= 0)
            {
                GameObject bulletcopy = Instantiate(bullet, pos.position, transform.rotation);
                currenttimeSong = cooltimeSong;
            }
            currenttimeSong -= Time.deltaTime;
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
            dashAttackObj.transform.localScale = localScale;
        }
    }

    private void songAttack()
    {
        if (!canDash)
        {
            if (canSong && !isSong && !isDash && !isAttacking)
            {
                StartCoroutine(songAttackStart());
            }
        }
    }

    private void dashAttack()
    {
        if (canDash && !isSong && !isAttacking)
        {
            dashAttackObj.transform.position = transform.position;
            isAttack = true;
            Invoke("dashStartSet", 1f);
        }
    }

    private void dashStartSet()
    {
        StartCoroutine(dashAttackStart());
    }

    IEnumerator songAttackStart()
    {
        isAttack = true;
        isAttacking = true;
        yield return new WaitForSeconds(1f);
        canSong = false;
        transform.position = toObj.transform.position;
        isSong = true;
        yield return new WaitForSeconds(12f);
        isSong = false;
        isAttacking = false;
        isAttack = false;
    }

    IEnumerator dashAttackStart()
    {
        dash.isDashAttack = true;
        isDash = true;
        canDash = false;
        isAttacking = true;
        isAttack = true;
        float originalGravity = rigid.gravityScale;
        rigid.gravityScale = 0f;
        rigid.velocity = new Vector3(transform.localScale.x * dashPower, 0f);
        yield return new WaitForSeconds(dashTime);
        rigid.gravityScale = originalGravity;
        dash.isDashAttack = false;
        isDash = false;
        yield return new WaitForSeconds(1f);
        isAttack = false;
        isAttacking = false;
    }

    IEnumerator Attack(Collider2D collider)
    {
        if (isAttacking == false)
        {
            attackAni.SetActive(true);
            isAttacking = true;
            isAttack = true;

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

            attackAni.SetActive(false);
            isAttacking = false;
            isAttack = false;

            yield return new WaitForSeconds(attackCooldown);
            attackCoroutine = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(AttackPos.position, Attackbox);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(searchPos.position, searchbox);
    }
}
