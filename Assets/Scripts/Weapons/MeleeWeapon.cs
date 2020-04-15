using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using UnityEngine;

namespace Weapons
{
	class MeleeWeapon : IWeapon
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

		private float _damage;
		public float damage
		{
			get
			{
				return _damage;
			}
		}

		//TODO: attack damage enum
		//TODO: damage coeff, calculate actual damage by angle, distance
		public MeleeWeapon(float attackRange, float damage)
		{
			this._attackRange = attackRange > 1f ? 1f : attackRange;
			this._damage = damage;
		}


		public GameObject TryAttack(Vector3 startPosition, Vector3 direction, string targetTag)
		{
			//Vector3 direction = (attackDirection - this.transform.position).normalized;
			#if DEBUG_MODE
			Debug.DrawRay(startPosition, direction, Color.yellow, 0.4f);
			#endif
			RaycastHit hit;
			if (Physics.Raycast(startPosition, direction, out hit, attackRange))
			{
				if (hit.transform.CompareTag(targetTag))
				{
					#if DEBUG_MODE
					#if UNITY_2017_4_OR_NEWER
					Debug.Log($"<color=red>{hit.transform.name} was hit</color>");
					#else
					Debug.Log("<color=red>" + hit.transform.name + " was hit</color>");
					#endif
					#endif
					//TODO: Draw bullet something
					return hit.transform.gameObject;
				}
			}
			return null;
		}
	}
}
