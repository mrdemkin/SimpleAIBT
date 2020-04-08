using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

namespace Character
{
    //TODO: abstract class Character
    public class PlayerView : CharacterView
    {
        Timer timer;
        delegate void RepeatAiAction();
        private RepeatAiAction AIAction;
        private int RepeatAims = 2000;
        //TODO: events like jump, startAttack or something else
        //public EventHandler 

        private void Awake()
        {
			_presenter = new PlayerPresenter (this, null);//this.GetComponent<PlayerAI>());
            _presenter.Init();
            InitComponents();

            AIAction = _presenter.NextAction;
#if DEBUG_MODE
            Invoke ("loweringHealth", 5f);
#endif
        }

#if DEBUG_MODE
        private void loweringHealth() {
			_presenter.health = 20;
		}
#endif

        private void Start()
        {
            ActionAI();
        }

        public override void ChangeHealth()
        {

        }

        //armor
        public override void ChangeDefence()
        {

        }

        public override void ChangeSpeed()
        {

        }

        public override void Move()
        {   //rotate
            transform.Rotate(0, Input.GetAxis("Horizontal") * _presenter.speed, 0);
            //now moving only forward
            Vector3 moving = transform.TransformDirection(Vector3.forward);
            _cController.SimpleMove(moving);
        }

        public override void Move(Vector3 targetPoint)
        {
            //rotate
            transform.Rotate(0, Input.GetAxis("Horizontal") * _presenter.speed, 0);
            //now moving only forward
            Vector3 moving = transform.TransformDirection(Vector3.forward);
            _cController.SimpleMove(moving);
        }

        public override void Backoff()
        {
            //TODO: Implement
        }

        public override void OpenAbilityShield()
        {
            //TODO: Implement
        }

		private void Update() {
			if (_presenter.isCanActivateAiAction) {
				_presenter.SetPlayerAction ();
			}
		}

        void ActionAI()
        {
#if DEBUG_MODE
            Debug.Log("!!! ActionAI");
#endif
            timer = new System.Timers.Timer(RepeatAims);
            timer.AutoReset = true;
            timer.Elapsed += OnTimerEvent;
            timer.Start();
        }

        void OnDisable()
        {
            timer.Stop();
        }

        private void OnTimerEvent(object sender, ElapsedEventArgs e)
        {
            AIAction();
        }
    }
}
