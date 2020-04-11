using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Weapons
{
    class DistantWeapon : IWeapon
    {
        private bool _isCanDistantAttack;
        public bool isCanDistantAttack
        {
            get
            {
                return _isCanDistantAttack;
            }
        }

        private bool _isCanMeleeAttack;
        public bool isCanMeleeAttack
        {
            get
            {
                return _isCanMeleeAttack;
            }
        }

        private float _attackRange;
        public float attackRange
        {
            get
            {
                return _attackRange;
            }
        }

        public DistantWeapon(float attackRange)
        {
            this._attackRange = attackRange;
        }
            

        public GameObject TryAttack(Vector3 startPosition, Vector3 direction, string targetTag)
        {
            //Vector3 direction = (attackDirection - this.transform.position).normalized;
#if DEBUG_MODE
            Debug.DrawRay(startPosition, direction, Color.blue, 0.2f);
#endif
            RaycastHit hit;
            if (Physics.Raycast(startPosition, direction, out hit, attackRange))
            {
                if (hit.transform.CompareTag(targetTag))
                {
#if DEBUG_MODE
                    Debug.Log($"<color=red>{hit.transform.name} was hit</color>");
#endif
                    //TODO: Draw bullet something
                    return hit.transform.gameObject;
                }
            }
            return null;
        }
    }
}
