using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/EnemyData", fileName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public int health = 100; // ü��
    public int defense = 200; //����
    public int damage = 100; // ���ݷ�
    public float attackSpeed = 1.3f; //���� ���ð�
    public float attackCooldown = 1.5f; //���� ��Ÿ��
    public int speed = 3; // �̵� �ӵ�
    public float knockBackForce = 20f;
}
