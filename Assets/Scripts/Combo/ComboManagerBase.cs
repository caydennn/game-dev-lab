using UnityEngine;

public abstract class ComboManagerBase : MonoBehaviour
{
    protected abstract void doRegularAttack(RegularSkill r);
    protected abstract void doComboAttack(ComboSkill c);
    protected abstract void ResetCurrentCombos();
}