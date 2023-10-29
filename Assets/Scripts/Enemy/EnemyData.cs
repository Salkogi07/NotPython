using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/EnemyData", fileName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public int health = 100; // 체력
    public int damage = 100; // 공격력
    public int speed = 3; // 이동 속도
    public int defense = 200;
    public float knockBackForce = 20f;
}
