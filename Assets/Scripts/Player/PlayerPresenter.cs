using System.Collections;
using System.Collections.Generic;
using BaseAI;
using MapScanner;
using Weapons;

namespace Character
{
    public class PlayerPresenter : CharacterPresenter
    {
        PlayerAI AI;
        SearchForGameobject mapScanner;

		delegate void PlayerActionDelegate();
		private PlayerActionDelegate playerActionDelegate;

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

        //TODO: interface?
        public PlayerPresenter(PlayerView view)
        {
            _view = view;
			_model = new PlayerModel();
			this.AI = new PlayerAI (_model);
            this.mapScanner = new SearchForGameobject();
            _isInited = false;
            //this.AI = ai;// new PlayerAI();
			//if (playerActionDelegate == null) {
			//	playerActionDelegate = SetPlayerAction;
			//}
        }

        public override void Init()
        {
            if (_isInited) return;
            _model.SetSpeed(1f);
            _isInited = true;
			#if DEBUG_MODE
			//TestMapScan ();
			#endif
        }

		#if DEBUG_MODE
		void TestMapScan() {
			this.mapScanner.TryFindGameobject (_view.transform.position, 10f, 24, "Enemy");
		}
		#endif

        //TODO: NO! shouldn't use UnityEngine in presenter
        public override UnityEngine.Transform GetNearestEnemy()
        {
            //TODO: use tags from manager
            return this.mapScanner.FindNearest(_view.transform.position, this.mapScanner.TryFindGameobject(_view.transform.position, 10f, 24, "Enemy"));
        }

        public override void Deinit()
        {

        }

        public override void NextAction()
        {
#if DEBUG_MODE
            UnityEngine.Debug.Log("!!! PRESENTER NextAction");
#endif
			AI.StartAI();
			AiActionCurrent = AI.GetNextAction(AI.rootNode.state);
        }
			
		public override void SetAction() {
			SetPlayerAction (AiActionCurrent);
		}

        private void SetPlayerAction(AiStates actionCode)
        {
#if DEBUG_MODE
#if UNITY_2017_4_OR_NEWER
            UnityEngine.Debug.Log($"SetPlayerAction {actionCode}");
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
                case AiStates.Move:
					this.SendMoveAction();
                    break;
                case AiStates.Backoff:
                case AiStates.CounterAttack:
					this.SendBackoffAction();
                    break;
            }
        }


		//TODO: interface IMovebaleByAi
		private void SendMoveAction() {
			_view.Move ();
		}

		private void SendBackoffAction() {
			_view.Backoff ();
		}
    }
}
