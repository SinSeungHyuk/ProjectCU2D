using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDetails_", menuName = "Scriptable Objects/Weapon/Weapon")]
public class WeaponDetailsSO : ScriptableObject
{
    [Header("weapon base details")]
    public string weaponName;
    [SerializeReference, SubclassSelector] public WeaponType weaponType; // �ٰŸ� , ���Ÿ�
    public Sprite weaponSprite;

    [Header("weapon base stats")]
    public int weaponBaseDamage = 20;   // �⺻������
    public int weaponCriticChance = 10; // ġ��Ÿ Ȯ�� (%)
    public int weaponCriticDamage = 150; // ġ��Ÿ ���� (%)
    public float weaponFireRate = 0.5f; // ���ݼӵ�
    public int weaponRange = 0; // ��Ÿ�
    public int weaponAmmoSpeed = 0; // ź �ӵ�
    public int weaponKnockback = 0; // �˹�Ÿ�

    //[Header("weapon configuration")]
    //public List<GameObject> weaponAmmo;
    //[TextArea] public string upgradeDescription;
    //public bool isTrail; // Trail ������ ����
    //public Material ammoTrailMaterial;
    //public float ammoTrailStartWidth;
    //public float ammoTrailEndWidth;
    //public float ammoTrailTime;
    //public SoundEffectSO weaponFiringSoundEffect;
}