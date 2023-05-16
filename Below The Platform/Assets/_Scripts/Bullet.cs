using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : DamageAbility
{
    public override void Cast()
    {
        print(gameObject.name);
    }
}
