using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class EnemyView : CharacterView
    {
        public override void Backoff()
        {
            throw new System.NotImplementedException();
        }

        public override void ChangeDefence()
        {
            throw new System.NotImplementedException();
        }

        public override void ChangeHealth()
        {
            throw new System.NotImplementedException();
        }

        public override void ChangeSpeed()
        {
            throw new System.NotImplementedException();
        }

        public override void Move()
        {
            throw new System.NotImplementedException();
        }

        public override void Move(Vector3 targetPoint)
        {
            throw new System.NotImplementedException();
        }

        public override void OpenAbilityShield()
        {
            throw new System.NotImplementedException();
        }

        private void Awake()
        {
            _presenter = new EnemyPresenter(this);
            _presenter.Init();
            InitComponents();
        }
    }
}