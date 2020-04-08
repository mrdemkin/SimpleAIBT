using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;
using BaseAI;
using Character;

//TODO: interface for AI with InitActions() and StartAI()
public class PlayerAI : BasePlayerAiClass
{
    ActionNode checkLowHealth;
    ActionNode checkALive;
    ActionNode checkCanMove;
    ActionNode counterAttack;
    ActionNode abilityOn;

    //counterattack
    public SequenceNode counterAttackCheckSequence;
    ActionNode checkUnderAttack;
    ActionNode MoveFromAttack;
    ActionNode AttackAtPoint;

    //move to exit
    public SequenceNode moveToExitSequence;
    //TODO: Inverter node!
    ActionNode checkNotUnderAttack;
    ActionNode MoveToExit;

    public SelectorNode rootNode;

	public PlayerAI(CharacterModel model) {
		this.cModel = model;
	}

    /*public BT.States MoveFromAttack()
    {
        //TODO: Implement!
        return cModel.canMove ? States.SUCCESS : States.FAILED;
    }*/

    public override void InitActions()
    {
        /* First, the AI checks its own health. If it's low, it will want to heal */
        checkLowHealth = new ActionNode(isLowHealth, "checkLowHealth");
        checkUnderAttack = new ActionNode(isUnderAttack, "checkUnderAttack");
        //counter Attack
        counterAttack = new ActionNode(madeCounterAttack, "counterAttack");

        //Sequence for counterAttack
        counterAttackCheckSequence = new SequenceNode(new List<Node> {
            checkLowHealth,
            checkUnderAttack,
            counterAttack
        }, "counterAttackCheckSequence");

        //Sequence for move to exit
        checkNotUnderAttack = new ActionNode(isNotUnderAttack, "checkNotUnderAttack");
        checkCanMove = new ActionNode(canMoveAction, "checkCanMove");
        MoveToExit = new ActionNode(moveToExitAction, "MoveToExit");
        moveToExitSequence = new SequenceNode(new List<Node> {
            checkNotUnderAttack,
            checkCanMove,
            MoveToExit
        }, "moveToExitSequence");

        rootNode = new SelectorNode(new List<Node>
        {
            moveToExitSequence,
            counterAttackCheckSequence
        }, "rootNode");
        isInited = true;
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
#if DEBUG_MODE
      //  Debug.Log($"<color=yellow>GetNextAction {state}</color>");
#endif
        if (state == States.SUCCESS)
        {
            if (moveToExitSequence.state == States.SUCCESS)
            {
                return AiStates.Move;
            }
            if (counterAttackCheckSequence.state == States.SUCCESS)
            {
                return AiStates.CounterAttack;
            }
        }
        return AiStates.Idle;
    }
}
