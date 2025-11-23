using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : CombatController
{
    public override void Init()
    {
        base.Init();
    }

    public void Init(float speed)
    {
        base.Init();
        attackSpeed += speed;
    }
}
