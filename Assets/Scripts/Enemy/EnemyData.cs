using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/EnemyData", fileName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public int health = 100; // 체력
    public int defense = 200; //방어력
    public int damage = 100; // 공격력
    public float attackSpeed = 1.3f; //공격 대기시간
    public float attackCooldown = 1.5f; //공격 쿨타임
    public int speed = 3; // 이동 속도
    public float knockBackForce = 20f;
}
