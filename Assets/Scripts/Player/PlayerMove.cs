using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private Animator animator;
    [SerializeField] private TrailRenderer trilRenderer;

    public Transform groundChkFront;
    public Transform groundChkBack;
    public GameObject jumpEffect;

    private float horizontal;
    public float speed = 6.5f;
    private bool isFacingRight = true;

    public float chkDistance;
    public LayerMask g_Layer;
    public float jumpingPower = 20f;
    public int jumpCount = 2;
    public float knockBackForce = 13f;
    int jumpCnt;
    private bool isGround = false;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 17f;
    private float dashingTime = 0.17f;
    private float dashingCooldown = 1f;
    private float dashtimer = 0.0f;
    private int dashCount = 2;

    public bool isControl;
    public bool isAttack;
    private bool isKnockBack = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        trilRenderer = GetComponent<TrailRenderer>();
        jumpCnt = jumpCount;
        isControl = true;
        isAttack = false;
    }

    private void Update()
    {
        DashCooldown();
        if(isAttack)
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            animator.SetBool("isRun", false);
        }
        else
        {
            if (isDashing)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Z) && canDash)
            {
                StartCoroutine(Dash());
            }

            if (isControl)
            {
                Jump();
                Filp();
            }
            else
            {
                if (!isFacingRight)
                {
                    isFacingRight = !isFacingRight;
                    Vector3 localScale = transform.localScale;
                    localScale.x *= -1f;
                    transform.localScale = localScale;
                }
                rigid.velocity = new Vector2(0, 0);
                animator.SetBool("isRun", false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isAttack)
        {
            if (isDashing)
            {
                return;
            }
            if (isControl)
            {
                horizontal = Input.GetAxisRaw("Horizontal");

                rigid.velocity = new Vector2(horizontal * speed, rigid.velocity.y);

                if (horizontal != 0f)
                {
                    animator.SetBool("isRun", true);
                }
                else
                {
                    animator.SetBool("isRun", false);
                }

                GroundCheck();
            }
        }
    }

    private void GroundCheck()
    {
        bool ground_front = Physics2D.Raycast(groundChkFront.position, Vector2.down, chkDistance, g_Layer);
        bool ground_back = Physics2D.Raycast(groundChkBack.position, Vector2.down, chkDistance, g_Layer);
        if (ground_front || ground_back)
        {
            jumpCnt = jumpCount;
            animator.SetBool("isJump", false);
            jumpEffect.SetActive(false);
            isGround = true;
        }
        else
        {
            animator.SetTrigger("isJump");
            isGround = false;
        }
    }

    private void Jump()
    {
        if (isGround && Input.GetButtonDown("Jump") && jumpCnt > 0)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpingPower);
        }
        if (!isGround && Input.GetButtonDown("Jump") && jumpCnt == 1)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpingPower);
            jumpEffect.SetActive(true);
        }
        if (Input.GetButtonUp("Jump"))
        {
            animator.SetTrigger("isJump");
            jumpCnt--;
        }
    }

    private void Filp()
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        if(dashCount > 0)
        {
            dashCount--;
            canDash = false;
            isDashing = true;
            float originalGravity = rigid.gravityScale;
            rigid.gravityScale = 0f;
            rigid.velocity = new Vector3(transform.localScale.x * dashingPower, 0f);
            trilRenderer.emitting = true;
            yield return new WaitForSeconds(dashingTime);
            trilRenderer.emitting = false;
            rigid.gravityScale = originalGravity;
            isDashing = false;
            canDash = true;
        }
    }

    private void DashCooldown()
    {
        dashtimer += Time.deltaTime;

        // 일정 시간(interval)이 지났을 때 count를 1씩 증가시킵니다.
        if (dashCount == 2)
        {
            dashtimer = 0;
        }
        else
        {
            if (dashtimer >= dashingCooldown)
            {
                if (dashCount < 2)
                {
                    dashCount++;
                    dashtimer = 0.0f; // 타이머를 리셋합니다.
                }
            }
        }
    }

    public void KnockBack(GameObject gameObject1)
    {
        isKnockBack = true;
        Vector2 knockBackDirection = transform.position - gameObject1.transform.position;
        knockBackDirection.Normalize();
        rigid.velocity = knockBackDirection * knockBackForce;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(groundChkFront.position, Vector2.down * chkDistance);
        Gizmos.DrawRay(groundChkBack.position, Vector2.down * chkDistance);
    }
}
