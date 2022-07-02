using UnityEngine;

public enum AttackType
{
    heavy = 0,
    light = 1,
    kick = 2,
    //test

}

[System.Serializable]
public class Effect
{
    // change this to your own data structure defining visual or audio effect of this skill
    public GameObject particleEffect;
    // o
}

[System.Serializable]
public class Attack
{
    public float length;

    // change this to your own data structure defining visual or audio effect of this skill
    public Effect effect;
}

[CreateAssetMenu(fileName = "RegularSkill", menuName = "ScriptableObjects/RegularSkill", order = 9)]
public class RegularSkill : ScriptableObject
{
    public AttackType skillType;
    public SkillKeys key;
    public Attack attack;
}