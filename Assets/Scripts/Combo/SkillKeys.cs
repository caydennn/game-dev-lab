using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[
    CreateAssetMenu(
        fileName = "SkillKeys",
        menuName = "ScriptableObjects/SkillKeys",
        order = 0)
]
public class SkillKeys : ScriptableObject
{
    [Header("Inputs")]
    public KeyCode key;

    public bool isSameAs(SkillKeys k)
    {
        return key == k.key;
    }
}
