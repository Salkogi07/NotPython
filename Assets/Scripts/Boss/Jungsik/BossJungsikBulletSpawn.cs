using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJungsikBulletSpawn : MonoBehaviour
{
    public float cooltime;
    private float currenttime;
    
    public GameObject bullet;
    public Transform pos;
    public bool isSong = false;
    
    void FixedUpdate()
    {
        if (isSong)
        {
            currenttime = 0;
            if (currenttime <= 0)
            {
                GameObject bulletcopy = Instantiate(bullet,pos.position,transform.rotation);
                currenttime = cooltime;
            }
        }
    }
}
