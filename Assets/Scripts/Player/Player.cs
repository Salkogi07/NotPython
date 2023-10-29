using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Canvas dieUI;
    public PlayUI playUI;
    public int playerHp;
    public int currentHp;

    public int playerAttackDamge;
    public int playerDefense;
    public int playerCritical;

    public bool isControl;

    void Start()
    {
        currentHp = playerHp;
        isControl = true;
    }

    public int TakeDamge(int damage)
    {
        int enemyAtk = damage;
        float dmg;
        int lastdmg;

        dmg = enemyAtk * (5f / (5f + playerDefense));
        lastdmg = (int)dmg;

        if (isControl)
        {
            Debug.Log(lastdmg);
            currentHp -= lastdmg;
            if (currentHp < 0)
            {
                currentHp = 0;
            }

            if (currentHp <= 0)
            {
                PlayerDie();
            }
        }
        return lastdmg;
    }

    private void PlayerDie()
    {
        gameObject.SetActive(false);
        Invoke("PlayerDie1", 1f);
    }
    void PlayerDie1()
    {
        playUI.diePlayer();
        dieUI.gameObject.SetActive(true);
    }
}
