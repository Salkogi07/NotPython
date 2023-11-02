using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    EnemyMove enemyMove;
    private KillEnemyCount killEnemyCount;

    public EnemyData enemyData;
    
    [SerializeField]
    Image hpbar;

    public int enemyHp;
    public int currentHp;
    public int enemyAttackDamge;
    public float enemyDefense;
    public int enemySpeed;
    public float enemyAttackSpeed;
    public float enemyAttackCooldonw;

    public float knockBackForce = 20f;

    float imsiSlider;

    private void Awake()
    {
        killEnemyCount = GameObject.Find("GameManager").GetComponent<KillEnemyCount>();
        enemyMove = GetComponent<EnemyMove>();
        enemyHp = enemyData.health;
        enemyAttackDamge = enemyData.damage;
        enemyDefense = enemyData.defense;
        enemySpeed = enemyData.speed;
        knockBackForce = enemyData.knockBackForce;
        enemyAttackSpeed = enemyData.attackSpeed;
        enemyAttackCooldonw = enemyData.attackCooldown;

        currentHp = enemyHp;
        imsiSlider = (float)currentHp / (float)enemyHp;
    }

    

    void Update()
    {
        imsiSlider = (float)currentHp / (float)enemyHp;
        HandleHp();
    }

    public void TakeDamge(int damage,int skill, bool critical)
    {
        int playerAtk = damage;
        float dmg;
        int lastdmg;
        if (critical)
        {
            dmg = (playerAtk * (5f / (5f + enemyDefense)) + (10f * skill)) * 2f;
            lastdmg = (int) dmg;
        }
        else
        {
            dmg = playerAtk * (5f / (5f + enemyDefense)) + (10f * skill);
            lastdmg = (int) dmg;
        }

        Debug.Log(lastdmg);
        currentHp -= lastdmg;
        if (currentHp <= 0)
        {
            EnemyDie();
        }
        else
        {
            enemyMove.KnockBack();
        }
    }

    private void HandleHp()
    {
        hpbar.fillAmount = Mathf.Lerp(hpbar.fillAmount, imsiSlider, Time.deltaTime * 10);
    }


    private void EnemyDie()
    {
        gameObject.SetActive(false);
        killEnemyCount.killCount++;
    }
}
