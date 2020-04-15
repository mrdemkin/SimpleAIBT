using System;
using System.Collections.Generic;
using Character;
using BT;
using BaseAI;

public class BeginnerEnemyAI : BaseEnemyAiClass
{
    ActionNode checkLowHealth;
    ActionNode checkALive;
    ActionNode checkCanMove;
    ActionNode abilityOn;

    //search for player
    public SequenceNode searchForTargetSequence;
    ActionNode checkUnderAttack;
    ActionNode canSearchForTarget;

    public SequenceNode attackTargerSequence;
    //TODO: why it's needed?
    ActionNode checkCanAttack;
    ActionNode AttackAtPoint;
    ActionNode canWeaponReachTarget;

    //move to target
    public SequenceNode moveToTarget;
    //TODO: Inverter node!
    ActionNode checkNotUnderAttack;
    //Invert
    //ActionNode canWeaponReachTarget;
    ActionNode MoveToPoint;

    public SelectorNode rootNode;

    public BeginnerEnemyAI(CharacterModel model) {
		this.cModel = model;
	}


    private bool isInited;
    public override int StartAI()
    {
        if (!isInited)
        {
            InitActions();
        }
        rootNode.Evaluate();
        //GetNextAction(rootNode.state);
        return 1;
    }

    public void Start()
    {
        InitObjects();
    }

    private void InitObjects()
    {

    }

    public override AiStates GetNextAction(States state)
    {
        return AiStates.Idle;
    }

    public override void InitActions()
    {
        checkLowHealth = new ActionNode(isLowHealth, "checkLowHealth");
        checkUnderAttack = new ActionNode(isUnderAttack, "checkUnderAttack");


        rootNode = new SelectorNode(new List<Node>
        {
            
        }, "rootNode");
        isInited = true;
    }
}

