using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public abstract class CharacterModel
    {
        public float attack;
        public float defence;
        public float hp;
        public float speed;

        public bool canRespawn;
        public bool canMove;
        public bool canMeleeAttack;
        public bool canDistantAttack;
        public bool isInited;

        public abstract void Init();
        public abstract void Attack();
        public abstract void OpenShield();
        public abstract void SetHp(float newValue);
        public abstract void SetSpeed(float newValue);
        public abstract void SetDefence(float newValue);

		public abstract bool isLowHealth ();
		//TODO: get-set
		public abstract bool isUnderAttack();
    }


    [RequireComponent(typeof(CharacterController))]
    public abstract class CharacterView : MonoBehaviour
    {
		// TODO: new class absctract BasicPlayerView?
		[HideInInspector]
		public Vector3 exitPoint;
        // TODO: show in inspector non-public?
        //public Rigidbody _rb;
        //public Collider _collider;
        public CharacterController _cController;
        public CharacterPresenter _presenter;

        public void InitComponents()
        {
            if (_cController == null)
            {
                _cController = this.GetComponent<CharacterController>();
            }
        }

        public abstract void ChangeHealth();

        //armor
        public abstract void ChangeDefence();
        public abstract void ChangeSpeed();
        private void OnDestroy()
        {
            _presenter.Deinit();
        }
        public abstract void Move();
        public abstract void Move(Vector3 targetPoint);
        public abstract void Backoff();
        public abstract void OpenAbilityShield();
    }

    public abstract class CharacterPresenter
    {
        public CharacterView _view;
        public CharacterModel _model;
        [HideInInspector]
        public bool _isInited;
		public bool isCanActivateAiAction;
        public abstract void Init();
        public abstract void Deinit();

		private int _health;
		public int health {
			get {
				return _health;
			}
			set {
				_model.SetHp (value);
			}
		}

        public float speed
        {
            get { return _model.speed; }
        }

        // TODO: Need getter
        /*public bool canMove
        {
            get { return _model.canMove; }
        }*/

        public abstract void NextAction();
		public abstract void SetPlayerAction ();
        //public abstract void InitAI();
    }
}
