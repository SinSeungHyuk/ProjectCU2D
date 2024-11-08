using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MonsterDetails_", menuName = "Scriptable Objects/Monster/Monster Details")]
public class MonsterDetailsSO : ScriptableObject
{
    [Header("Base Monster Details")]
    public string enemyName;
    public Sprite sprite; // 적의 스프라이트
    public Color spriteColor; // 스프라이트 색상
    public RuntimeAnimatorController runtimeAnimatorController; // 적의 애니메이터

    [Header("Base Monster Stats")]
    public int speed; // 이동속도
    public float chaseDistance = 50f; // 플레이어와의 최대 간격
    public int contactDamageAmount = 10; // 플레이어 접촉 데미지
    public float maxHp = 10; // 최대체력

    //[Header("Material")]
    //public Material enemyStandardMaterial;
    //public float enemyMaterializeTime;
    //public Shader enemyMaterializeShader;
    //public Color enemyMaterializeColor;
}
