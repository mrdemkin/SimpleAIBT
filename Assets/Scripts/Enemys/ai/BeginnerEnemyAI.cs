using System;
using System.Collections.Generic;
using Character;
using BT;
using BaseAI;

public class BeginnerEnemyAI : BaseEnemyAiClass
{
    ActionNode checkLowHealth;
    ActionNode checkAlive;
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
	
	public BT.States abilityOnAction()
    {
		//TODO: abilityOn
        return cModel.isLowHealth() ? States.SUCCESS : States.FAILED;
	}

	public BT.States isCanSearchForTarget()
	{
		//TODO: isCanSearchForTarget
		return cModel.isLowHealth() ? States.SUCCESS : States.FAILED;
	}

	public BT.States checkCanAttackAction() {
		//TODO: checkCanAttackAction
		return cModel.isLowHealth() ? States.SUCCESS : States.FAILED;
	}

	public BT.States isCanWeaponReachTarget() {
		//TODO: isCanWeaponReachTarget
		return cModel.isLowHealth() ? States.SUCCESS : States.FAILED;
	}

	public BT.States AttackAtPointAction() {
		//TODO: AttackAtPointAction
		return cModel.isLowHealth() ? States.SUCCESS : States.FAILED;
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
		checkCanMove = new ActionNode(canMoveAction, "checkCanMove");
		abilityOn = new ActionNode(abilityOnAction, "abilityOn");
		canSearchForTarget = new ActionNode (isCanSearchForTarget, "isCanSearchForTarget");
    //search for player
		searchForTargetSequence = new SequenceNode (new List<Node> {
			//TODO: check it's don't busy by any another sequence
			checkUnderAttack,
			canSearchForTarget
		}, "searchForTargetSequence");

		checkCanAttack = new ActionNode (checkCanAttackAction, "checkCanAttack");
		canWeaponReachTarget = new ActionNode (isCanWeaponReachTarget, "canWeaponReachTarget");
		AttackAtPoint = new ActionNode (AttackAtPointAction, "AttackAtPoint");
		attackTargerSequence = new SequenceNode (new List<Node> {
			checkCanAttack,
			canWeaponReachTarget,
			AttackAtPoint
		}, "attackTargerSequence");

    //move to target
    public SequenceNode moveToTarget;
    //TODO: Inverter node!
    ActionNode checkNotUnderAttack;
    //Invert
    //ActionNode canWeaponReachTarget;
    ActionNode MoveToPoint;

        rootNode = new SelectorNode(new List<Node>
        {
            
        }, "rootNode");
        isInited = true;
    }
}

