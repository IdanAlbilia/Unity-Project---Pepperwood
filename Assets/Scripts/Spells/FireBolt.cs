using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBolt : Spell
{
    public int damage = 25;

    public override int getDamage()
    {
        return damage;
    }
}
