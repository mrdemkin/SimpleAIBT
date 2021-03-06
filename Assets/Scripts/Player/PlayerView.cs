﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using Weapons;

namespace Character
{
    //TODO: abstract class Character
    public class PlayerView : CharacterView
    {
        //TODO: This not for visual presentation!
        DistantWeapon weapon;

        Timer timer;
        delegate void RepeatAiAction();
        private RepeatAiAction AIAction;
        private int RepeatAims = 1000;
        //TODO: events like jump, startAttack or something else
        //public EventHandler 

        private void Awake()
        {
			_presenter = new PlayerPresenter (this);
            _presenter.Init();
            InitComponents();
			weapon = new DistantWeapon (10f);
            AIAction = _presenter.NextAction;
#if DEBUG_MODE
            Invoke ("loweringHealth", 5f);
#endif
        }

#if DEBUG_MODE
        private void loweringHealth() {
            Debug.Log("<color=blue>loweringHealth</color>");
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
            Move(exitPoint);
        }

        private void Move(Vector3 targetPoint)
        {
            //now moving only forward
            Vector3 moveVector = targetPoint - transform.position;
            //TODO: correct speed moving at end of moving
            float x = (moveVector.x > 1f ? 1f : moveVector.x) * _presenter.speed;
            float z = (moveVector.z > 1f ? 1f : moveVector.z) * _presenter.speed;
            Vector3 moving = new Vector3(x, this.transform.position.y, z);
			_cController.Move(moving * Time.deltaTime);
        }

        private void BackMove(Transform attacked)
        {
            _cController.Move((-GetDirectionToAttacked(attacked)) * Time.deltaTime);
        }

        public override void Backoff()
        {
            // move negative from attacked and attack enemy
#if DEBUG_MODE
			Debug.Log("<color=red>Backoff</color>");
#endif
            //TODO: distance should be less then attack range!
            Transform _tr = GetTransformToAttack();
            if (_tr != null)
            {
                Attack(_tr.position);
                BackMove(_tr);
            }
        }

        public override void ScanMap()
        {
            //TODO: need for enemy, but ni player.. for why?
        }

        private Vector3 GetDirectionToAttacked(Transform attacked)
        {
            return (attacked.position - this.transform.position).normalized;
        }

        private Transform GetTransformToAttack()
        {
            return _presenter.GetNearestEnemy();
        }

        private void Attack(Vector3 attackDirection)
        {
            //TODO: use weapon for this
            //TODO: why it's in visual presentation?
            Vector3 direction = (attackDirection - this.transform.position).normalized;
            GameObject hitObj = weapon.TryAttack(this.transform.position, direction, "Enemy");
            if (hitObj)
            {
                //TODO: enemy set new hp and others
            }
        }

        private void Attack(Vector3 attackDirection, float distance)
        {
            //TODO: use weapon for this
            //TODO: why it's in visual presentation?
            if (distance > weapon.attackRange)
            {
#if DEBUG_MODE
                Debug.Log("Attack range less, then distance to enemy");
#endif
                return;
            }
            Vector3 direction = (attackDirection - this.transform.position).normalized;
            GameObject hitObj = weapon.TryAttack(this.transform.position, direction, "Enemy");
            if (hitObj)
            {
                //TODO: enemy set new hp and others
            }
        }

        public override void OpenAbilityShield()
        {
            //TODO: Implement
        }

		private void Update() {
            if (_presenter.isCanActivateAiAction) {
				_presenter.SetAction ();
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
