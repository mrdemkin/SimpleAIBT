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
	ActionNode checkTargetFinded;
	ActionNode checkTargetNotFinded;

    //search for player
    public SequenceNode searchForTargetSequence;
    ActionNode checkUnderAttack;
    ActionNode canSearchForTarget;

    public SequenceNode attackTargerSequence;
    //TODO: why it's needed?
    ActionNode checkCanAttack;
    ActionNode AttackAtPoint;
    ActionNode canWeaponReachTarget;
    ActionNode canNotWeaponReachTarget;

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

	public BT.States isTargetFinded()
	{
		//TODO: isTargetFinded
		return cModel.distanceToTarget != -1f ? States.SUCCESS : States.FAILED;
	}

	public BT.States isTargetNotFinded()
	{
        //TODO: isTargetFinded
#if DEBUG_MODE
        UnityEngine.Debug.Log($"<color=blue>isTargetNotFinded {cModel.distanceToTarget}</color>");
#endif
        return cModel.distanceToTarget != -1f ? States.FAILED : States.SUCCESS;
	}

    public BT.States isNotCanWeaponReachTarget()
    {
		return cModel.weaponAttackRange < cModel.distanceToTarget ? States.SUCCESS : States.FAILED;
    }

    public BT.States abilityOnAction()
    {
		//TODO: abilityOn
        return cModel.isLowHealth() ? States.SUCCESS : States.FAILED;
	}

	public BT.States isCanSearchForTarget()
	{
		//TODO: isCanSearchForTarget
		return States.SUCCESS;
	}

	public BT.States checkCanAttackAction() {
		//TODO: isCanWeaponReachRange?
		return (cModel.weaponAttackRange >= cModel.distanceToTarget) ? States.SUCCESS : States.FAILED;
	}

	public BT.States isCanWeaponReachTarget() {
		return cModel.weaponAttackRange >= cModel.distanceToTarget ? States.SUCCESS : States.FAILED;
        
	}

	public BT.States AttackAtPointAction() {
		//TODO: AttackAtPointAction
		return cModel.isLowHealth() ? States.SUCCESS : States.FAILED;
	}

    public BT.States moveToPointAction()
    {
        //TODO: moveToPointAction
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
		if (state == States.SUCCESS)
		{
			if (searchForTargetSequence.state == States.SUCCESS)
			{
				//TODO: search for target sequence, add to states
				return AiStates.ScanMap;
			}
			if (moveToTarget.state == States.SUCCESS)
			{
				UnityEngine.Debug.Log ("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
				return AiStates.Move;
			}
			if (attackTargerSequence.state == States.SUCCESS) {
				UnityEngine.Debug.Log ("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
				return AiStates.Attack;
			}
		}
		return AiStates.Idle;
    }

    public override void InitActions()
    {
		checkTargetFinded = new ActionNode (isTargetFinded, "isTargetFinded");
		checkTargetNotFinded = new ActionNode (isTargetNotFinded, "isTargetNotFinded");
        checkLowHealth = new ActionNode(isLowHealth, "checkLowHealth");
        checkUnderAttack = new ActionNode(isUnderAttack, "checkUnderAttack");
		checkCanMove = new ActionNode(canMoveAction, "checkCanMove");
		abilityOn = new ActionNode(abilityOnAction, "abilityOn");
		canSearchForTarget = new ActionNode (isCanSearchForTarget, "isCanSearchForTarget");

        checkNotUnderAttack = new ActionNode(isNotUnderAttack, "checkNotUnderAttack");
        MoveToPoint = new ActionNode(moveToPointAction, "moveToPoint");


        checkCanAttack = new ActionNode(checkCanAttackAction, "checkCanAttack");
        canWeaponReachTarget = new ActionNode(isCanWeaponReachTarget, "canWeaponReachTarget");
        canNotWeaponReachTarget = new ActionNode(isNotCanWeaponReachTarget, "canNotWeaponReachTarget");
        AttackAtPoint = new ActionNode(AttackAtPointAction, "AttackAtPoint");

        //search for player
        searchForTargetSequence = new SequenceNode (new List<Node> {
			//TODO: check it's don't busy by any another sequence
			checkNotUnderAttack,
			checkTargetNotFinded,
			canSearchForTarget
		}, "searchForTargetSequence");

		attackTargerSequence = new SequenceNode (new List<Node> {
			checkTargetFinded,
			checkCanAttack,
			canWeaponReachTarget,
			AttackAtPoint
		}, "attackTargerSequence");
        moveToTarget = new SequenceNode(new List<Node> {
			checkTargetNotFinded,
            //checkNotUnderAttack,
            canNotWeaponReachTarget,
            moveToTarget }, "moveToTarget");
		
        rootNode = new SelectorNode(new List<Node>
        {
            searchForTargetSequence,
            moveToTarget,
            attackTargerSequence
        }, "rootNode");
        isInited = true;
    }
}

