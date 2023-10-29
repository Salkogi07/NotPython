using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/BossData", fileName = "BossData Data")]
public class BossData : ScriptableObject
{
    public int health = 100; // 체력
    public int damage = 100; // 공격력
    public int speed = 3; // 이동 속도
    public int defense = 200;
    public float knockBackForce = 20f;
}
