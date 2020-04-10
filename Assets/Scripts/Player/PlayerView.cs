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
			_presenter = new PlayerPresenter (this);
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
        {
#if DEBUG_MODE
			Debug.Log("Move " + exitPoint);
#endif
            Move(exitPoint);
            return;
            //TODO: rotate by direction
            transform.Rotate(0, Input.GetAxis("Horizontal") * _presenter.speed, 0);
            //now moving only forward
            Vector3 moving = transform.TransformDirection(Vector3.forward);
            _cController.SimpleMove(moving * Time.deltaTime);
        }

        public override void Move(Vector3 targetPoint)
        {
            //now moving only forward
            Vector3 moveVector = targetPoint - transform.position;
            //TODO: correct speed moving at end of moving
            float x = (moveVector.x > 1f ? 1f : moveVector.x) * _presenter.speed;
            float z = (moveVector.z > 1f ? 1f : moveVector.z) * _presenter.speed;
            Vector3 moving = new Vector3(x, this.transform.position.y, z);
			_cController.Move(moving * Time.deltaTime);
        }

        public override void Backoff()
        {
            //TODO: Implement
            // move negative from attacked and attack enemy
#if DEBUG_MODE
            Debug.Log($"<color=red>Backoff</color>");
#endif
            Attack(exitPoint);
            Move();
        }

        private void Attack(Vector3 attackDirection)
        {
            //TODO: create weapon class and shoot by that object
            //TODO: why it's in visual presentation?
            int bulletSpeed = 3;
            Vector3 startPos = this.transform.position;
            Vector3 direction = (attackDirection - this.transform.position).normalized;
            Debug.DrawRay(startPos, direction, Color.blue, 0.2f);
            RaycastHit hit;
            if (Physics.Raycast(startPos, direction, out hit, 20f))
            {
#if DEBUG_MODE
                Debug.Log($"<color=red>{hit.transform.name} was hit</color>");
#endif
            }

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
