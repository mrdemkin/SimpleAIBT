using UnityEngine;

namespace Character {
    public class EnemyPresenter : CharacterPresenter
    {
        public EnemyPresenter(EnemyView view)
        {
            _view = view;
			_model = new PlayerModel();
            _isInited = false;
        }

        public override void Deinit()
        {

        }

        public override Transform GetNearestEnemy()
        {
            throw new System.NotImplementedException();
        }

        public override void Init()
        {

        }

        public override void NextAction()
        {
            throw new System.NotImplementedException();
        }

		public override void SetPlayerAction() {
			
		}
    }
}
