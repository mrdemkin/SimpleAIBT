using UnityEngine;
using MapScanner;
using BaseAI;

namespace Character {
    public class EnemyPresenter : CharacterPresenter
    {
        //TODO: distance to target to MODEL!
		//TODO: abstract class enemyAI and variations - beginner, middle, expert
		BeginnerEnemyAI AI;
		SearchForGameobject mapScanner;

		delegate void EnemyActionDelegate();
		private EnemyActionDelegate enemyActionDelegate;

		private AiStates _aiActionCurrent;
		public AiStates AiActionCurrent {
			get {
				return _aiActionCurrent;
			}
			set {
				_aiActionCurrent = value;
				isCanActivateAiAction = true;
			}
		}

        //it's for data!
        public float attackRange
        {
            get
            {
                return _view.weapon.attackRange;
            }
        }

        public EnemyPresenter(EnemyView view)
        {
            _view = view;
			_model = new EnemyModel();

			this.AI = new BeginnerEnemyAI (_model);
			this.mapScanner = new SearchForGameobject();
			_isInited = false;
        }

        public override void Deinit()
        {

        }

        public override Transform GetNearestEnemy()
        {
            //TODO: use tags from manager
            Transform nearest = this.mapScanner.FindNearest(_view.transform.position, this.mapScanner.TryFindGameobject(_view.transform.position, 7f, 24, "Player"));
            if (nearest != null)
            {
                this.distanceToTarget = mapScanner.LengthBetween(nearest.position, _view.transform.position);
                return this.mapScanner.FindNearest(_view.transform.position, this.mapScanner.TryFindGameobject(_view.transform.position, 7f, 24, "Player"));
            }
            return null;
        }

        public override void Init()
        {
			if (_isInited) return;
			_model.SetSpeed(1f);
            this.distanceToTarget = -1f;
			_isInited = true;
        }

		public override void NextAction()
        {
#if DEBUG_MODE
            UnityEngine.Debug.Log("<color=red>!!!!WENEMY NextAction</color>");
#endif
            AI.StartAI();
			AiActionCurrent = AI.GetNextAction(AI.rootNode.state);
		}

		public override void SetAction() {
			SetAction (AiActionCurrent);
		}

		private void SetAction(AiStates actionCode)
		{
			#if DEBUG_MODE
			#if UNITY_2017_4_OR_NEWER
			UnityEngine.Debug.Log($"<color=red>!!!!WENEMY SetPlayerAction {actionCode}</color>");
			#else 
			UnityEngine.Debug.Log("SetPlayerAction " + actionCode);
			#endif
			#endif
			switch (actionCode)
			{
			    case AiStates.Idle:
			    default:
				    //stop
				    break;
                case AiStates.ScanMap:
                    ScanMapAction();
                    break;
			    case AiStates.Move:
				    //this.SendMoveAction();
				    break;
			    case AiStates.Backoff:
			    case AiStates.CounterAttack:
				    //this.SendBackoffAction();
				    break;
			}
		}

        private void ScanMapAction()
        {
            _view.ScanMap();
        }
    }
}
