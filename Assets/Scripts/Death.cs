using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {

    public TacticalAI.TargetScript myTargetScript;
    public TacticalAI.HitBox myHitBox;


    private void OnAIDeath()
    {
        if (myHitBox.GetLastHit() && myTargetScript.myTeamID != BattleController.instance.playerTeamID)
        {
            BattleController.instance.AddKill();
        }
        BattleController.instance.UpdateScore(myTargetScript.myTeamID);
    }
}
