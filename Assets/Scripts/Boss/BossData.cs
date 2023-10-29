using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/BossData", fileName = "BossData Data")]
public class BossData : ScriptableObject
{
    public int health = 100; // ü��
    public int damage = 100; // ���ݷ�
    public int speed = 3; // �̵� �ӵ�
    public int defense = 200;
    public float knockBackForce = 20f;
}
