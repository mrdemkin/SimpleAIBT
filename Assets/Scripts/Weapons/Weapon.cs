using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void TryDistantAttack(Vector3 direction);
    void TryMeleeAttack(Vector3 direction);


}
