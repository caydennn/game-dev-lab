using System.Collections.Generic;
using UnityEngine;

public class ComboManager : ComboManagerBase
{
    [Header("Setup")]
    public float comboLeeway = 0.2f; // how fast should the delay between each key press be?

    public List<RegularSkill> skills; // a list of ALL basic skills player can do

    public List<ComboSkill> combos; // a list of ALL combo we can do, each combo has an id based on its location in the list

    // for visual effects
    private Animator animator;

    private Dictionary<AttackType, ParticleSystem>
        skillDictionary = new Dictionary<AttackType, ParticleSystem>();

    private Dictionary<ComboType, ParticleSystem>
        comboDictionary = new Dictionary<ComboType, ParticleSystem>();

    // logic
    private Attack curAttack = null; // currently executed attack, can be regular attack or combo attack

    private RegularSkill lastInput;

    private List<int> currentPossibleCombosID = new List<int>(); // keep track of id of the combos combos that could be accessed by the same current starting attack

    private float timer = 0; // to keep track how long current combo will play

    private bool skipFrame = false;

    private float currentComboLeeway; //to keep track time passed between each skill press that makes a combo

    protected override void doComboAttack(ComboSkill c)
    {
        // animator.SetTrigger("CastCombo");
        curAttack = c.attack;
        timer = c.attack.length;

        // particle cast
        comboDictionary[c.comboType].Play();
    }

    protected override void doRegularAttack(RegularSkill r)
    {
        // animator.SetTrigger("CastBasic");

        // Attack(r.attack);
        curAttack = r.attack;
        timer = r.attack.length;

        // particle cast
        skillDictionary[r.skillType].Play();
    }

    protected override void ResetCurrentCombos()
    {
        currentComboLeeway = 0;

        //loop through all current combos and reset each of them
        for (int i = 0; i < currentPossibleCombosID.Count; i++)
        {
            ComboSkill c = combos[currentPossibleCombosID[i]];
            c.ResetCombo();
        }
        currentPossibleCombosID.Clear();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        InitializeCombosEffects();
        InitializeRegularSkillsEffects();
    }

    private void Update()
    {
        // if current attack is playing, we dont want to disturb it
        if (curAttack != null)
        {
            if (timer > 0)
                timer -= Time.deltaTime;
            else
                curAttack = null;
            return; // end it right here if there's a current attack playing
        }

        // if current combo is not empty, increase leeway count
        if (currentPossibleCombosID.Count > 0)
        {
            // increase leeway, this means we are waiting for the next sequence
            currentComboLeeway += Time.deltaTime;
            if (currentComboLeeway >= comboLeeway)
            {
                // if time's up, combo is not happening
                // cast last input if any
                if (lastInput != null)
                {
                    doRegularAttack (lastInput);
                    lastInput = null;
                }
                ResetCurrentCombos();
            }
        }
        else
        {
            // no combos currently registered, reset leeway to ensure we don't have unclean old values
            currentComboLeeway = 0;
        }

        RegularSkill input = null;

        // loop through current skills and see if the key pressed matches
        foreach (RegularSkill r in skills)
        {
            if (Input.GetKeyDown(r.key.key))
            {
                input = r;
                break;
            }
        }

        // return if there's no input currently that matches any skill
        if (input == null)
        {
            return;
        }

        // set current input as last known input
        lastInput = input;

        List<int> remove = new List<int>();

        // loop through our current combos to see if it continues existing combos
        for (int i = 0; i < currentPossibleCombosID.Count; i++)
        {
            // get the actual combo from the combo ids stored in currentCombos
            ComboSkill c = combos[currentPossibleCombosID[i]];

            // if this input is the next thing to press
            if (c.continueCombo(lastInput.key))
            {
                currentComboLeeway = 0;
            }
            else
            {
                // this combo isn't happening, we need to remove it
                // take note of the combo id
                remove.Add(currentPossibleCombosID[i]);
            }
        }

        if (skipFrame)
        {
            skipFrame = false;
            return;
        }

        // adding new combos to the currentCombo list with this current last known input

        for (int i = 0; i < combos.Count; i++)
        {
            if (currentPossibleCombosID.Contains(i)) continue;

            // if it's not being checked already, attempt to add combos into current combos that START with this current last known input

            if (combos[i].continueCombo(lastInput.key))
            {
                currentPossibleCombosID.Add (i);
                currentComboLeeway = 0;
            }
        }

        // remove stale combos from current combos
        // recall 'remove' contains combo IDs to remove
        foreach (int i in remove)
        {
            currentPossibleCombosID.Remove (i);
        }

        // do basic attack if there's no combo
        if (currentPossibleCombosID.Count <= 0)
        {
            doRegularAttack (lastInput);
        }
    }

    void InitializeCombosEffects()
    {
        // loop through the combos
        for (int i = 0; i < combos.Count; i++)
        {
            ComboSkill c = combos[i];

            // register callback for this combo on this manager
            c
                .onInputted
                .AddListener(() =>
                {
                    Debug.Log("Listener triggered");
                    // Call attack function with the combo's attack
                    skipFrame = true; // skip a frame before we attack
                    doComboAttack (c);
                    ResetCurrentCombos();
                });

            // instantiate
            GameObject comboEffect =
                Instantiate(c.attack.effect.particleEffect,
                Vector3.zero,
                Quaternion.identity);
            comboEffect.transform.parent = this.transform; // make mario the parent

            // reset its local transform
            comboEffect.transform.localPosition = new Vector3(1, 0, 0);

            // add to particle system list
            comboDictionary
                .Add(c.comboType, comboEffect.GetComponent<ParticleSystem>());
        }
    }

    void InitializeRegularSkillsEffects()
    {
        // loop through regular skills effect
        for (int i = 0; i < skills.Count; i++)
        {
            RegularSkill r = skills[i];
            GameObject skillEffect =
                Instantiate(r.attack.effect.particleEffect,
                Vector3.zero,
                Quaternion.identity);
            skillEffect.transform.parent = this.transform; // make mario the parent

            // reset its local transform
            skillEffect.transform.localPosition = new Vector3(1, 0, 0);

            // add to particle system list
            skillDictionary
                .Add(r.skillType, skillEffect.GetComponent<ParticleSystem>());
        }
    }
}
