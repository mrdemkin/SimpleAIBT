using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public interface IWeapon
    {
        GameObject TryAttack(Vector3 startPosition, Vector3 direction, string targetTag);
        bool isCanDistantAttack
        {
            get;
        }
        bool isCanMeleeAttack
        {
            get;
        }

        float attackRange
        {
            get;
        }


    }
}