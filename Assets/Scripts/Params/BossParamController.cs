using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossParamController : ParamController
{
    [SerializeField] protected string bossName;
    [SerializeField] protected Sprite bossIcon;
    [SerializeField] protected BossUI bossUI;
    [SerializeField] private MainEvents events;

    private bool isFirstCheck = true;

    protected override void CheckTypeAndValues(DamagebleParam.ParamType type, float value, float maxValue)
    {
        switch(type)
        {
            case DamagebleParam.ParamType.Health:
                if (isFirstCheck)
                {
                    bossUI.InitializeBossView(bossName, bossIcon, maxValue);
                    isFirstCheck = false;
                }
                bossUI.ViewHealth(value);
                if (value <= 0)
                {
                    events.OnAnimEvent(AnimationController.AnimationType.Death);
                }
                break;
        }
    }

    
}
