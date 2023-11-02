using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJungsik : MonoBehaviour
{

    public BossData bossData;

    public int bossHp;
    public int currentHp;
    public int bossAttackDamge;
    public int bossDefense;
    public int bossSpeed;

    public float knockBackForce = 0f;

    private void Awake()
    {
        bossHp = bossData.health;
        bossAttackDamge = bossData.damage;
        bossDefense = bossData.defense;
        bossSpeed = bossData.speed;
        knockBackForce = bossData.knockBackForce;

        currentHp = bossHp;
    }

    public void TakeDamge(int damage, int skill, bool critical)
    {
        int playerAtk = damage;
        float dmg;
        int lastdmg;
        if (critical)
        {
            dmg = (playerAtk * (5f / (5f + bossDefense)) + (10f * skill)) * 2f;
            lastdmg = (int)dmg;
        }
        else
        {
            dmg = playerAtk * (5f / (5f + bossDefense)) + (10f * skill);
            lastdmg = (int)dmg;
        }

        Debug.Log(lastdmg);
        currentHp -= lastdmg;
        if(currentHp < 0)
        {
            currentHp = 0;
        }

        if (currentHp <= 0)
        {
            bossDie();
        }
    }


    private void bossDie()
    {
        gameObject.SetActive(false);
    }
}
