using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Projectile_", menuName = "Scriptable Objects/Weapon/Projectile/Projectile")]
public class ProjectileDetailsSO : ScriptableObject
{
    // ����ü ���߽� ȿ�� (����, ���� ��)
    public ProjectileEffectSO projectileEffect;
    // ����ü�� ���� �߰�ȿ�� (�����, ��Ʈ������ ��)
    public List<BonusEffectSO> bonusEffects;
    // ����ü ��� ����
    public bool isPiercing;
    // ����ü �ӵ�
    public int projectileSpeed = 10;
}