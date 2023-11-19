using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BossJungsikBullet : MonoBehaviour
{
    public float speed;
    public float distance;
    public LayerMask isLayer;

    void Start()
    {
        Invoke("DestroyBullet", 5);
    }

    void Update()
    {
        transform.Translate(transform.right * speed * -1 *  Time.deltaTime);
    }

    void DestroyBullet()
    {
        Destroy(gameObject); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            DestroyBullet();
            collision.GetComponent<Player>().TakeDamge(1000, gameObject);
        }
    }
}
