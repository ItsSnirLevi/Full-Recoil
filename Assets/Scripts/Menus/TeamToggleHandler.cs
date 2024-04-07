using System;
using UnityEngine;
using DG.Tweening;

public class TeamToggleHandler : MonoBehaviour {

    //private int toggleState = 1;    
    public GameObject toggleBtn;
    public PlayMenu playMenu;

    public void OnSwitchButtonClicked()
    {
        toggleBtn.transform.DOLocalMoveX(-toggleBtn.transform.localPosition.x, 0.2f);
        //toggleState = Math.Sign(-toggleBtn.transform.localPosition.x);
        playMenu.ChangeTeams();
    }

}
