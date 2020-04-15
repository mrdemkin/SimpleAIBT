using System;
using Character;
using BT;
using BaseAI;

public class BeginnerEnemyAI : BaseEnemyAiClass
{
	CharacterModel enemyModel;

	public SelectorNode rootNode;

	public BeginnerEnemyAI(CharacterModel model) {
		this.enemyModel = model;
	}

	public override void InitActions () {
	}

	public override int StartAI() {
        //TODO: Implement!
        return 1;
	}

	public void Start() {
	}
	public void InitObjects () {
	}

	public override AiStates GetNextAction (States state) {
        //TODO: Implement
        return AiStates.Idle;
	}
}

