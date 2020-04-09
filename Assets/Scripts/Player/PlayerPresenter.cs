using System.Collections;
using System.Collections.Generic;
using BaseAI;

namespace Character
{
    public class PlayerPresenter : CharacterPresenter
    {
        PlayerAI AI;

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
            _isInited = false;
            //this.AI = ai;// new PlayerAI();
			//if (playerActionDelegate == null) {
			//	playerActionDelegate = SetPlayerAction;
			//}
        }

        public override void Init()
        {
            if (_isInited) return;
            _isInited = true;
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
			//SetPlayerAction(AI.GetNextAction(AI.rootNode.state));
        }
			
		public override void SetPlayerAction() {
			SetPlayerAction (AiActionCurrent);
		}

        private void SetPlayerAction(AiStates actionCode)
        {
            switch (actionCode)
            {
                case AiStates.Idle:
                default:
                    //stop
                    break;
                case AiStates.Move:
                    _view.Move();
                    break;
                case AiStates.Backoff:
                    _view.Backoff();
                    break;
            }
        }
    }
}
