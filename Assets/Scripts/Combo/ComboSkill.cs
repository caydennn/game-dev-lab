using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public enum ComboType
{
    electric = 0,
    explosion = 1,
    shine = 2
};

[CreateAssetMenu(fileName = "ComboSkill", menuName = "ScriptableObjects/ComboSkill", order = 10)]
public class ComboSkill : ScriptableObject
{
    // a list of combo inputs that we will cycle through
    public List<SkillKeys> inputs;
    // public ComboAttack comboAttack; // once we got through all inputs, then we summon this attack
    public Attack attack;
    public UnityEvent onInputted;
    public ComboType comboType;

    int curInput = 0;

    public bool continueCombo(SkillKeys i)
    {
        if (currentComboInput().isSameAs(i))
        {
            curInput++;
            if (curInput >= inputs.Count) // finished the inputs and we should cast the combo
            {
                onInputted.Invoke();
                //restart the combo
                curInput = 0;
            }
            return true;
        }
        else
        {
            //reset combo
            ResetCombo();
            return false;
        }
    }

    public SkillKeys currentComboInput()
    {
        if (curInput > inputs.Count) return null;
        else return inputs[curInput];
    }

    public void ResetCombo()
    {
        curInput = 0;
    }
}