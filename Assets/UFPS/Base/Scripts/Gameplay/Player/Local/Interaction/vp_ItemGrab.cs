/////////////////////////////////////////////////////////////////////////////////
//
//	vp_ItemGrab.cs
//	© Opsive. All Rights Reserved.
//	https://twitter.com/Opsive
//	http://www.opsive.com
//
//	description:	assign this script to an item pickup prefab to allow the player
//					to pick it up from a distance by pressing the 'Interact' button
//
//					USAGE:
//						1) make sure the pickup prefab has a 'vp_ItemPickup' component
//							on it that can be picked up by walking over it and gives
//							you the desired result when picked up
//						2) add a vp_ItemGrab component to the gameobject
//						3) assign a texture as 'Interact Crosshair', for example the
//							provided 'Interact64x64' icon
//						4) (optional) tweak the 'Interact Distance' to decide how far
//							away from the object the player should be able to grab it
//						5) (optional) if you don't want players to be able to pick up
//							the object by walking over it, locate the 'vp_ItemPickup'
//							component's 'Item' foldout and uncheck 'Give On Contact'
//
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class vp_ItemGrab : vp_Interactable
{
    /*
     * Revolver - 500
     * Shotgun - 1000
     * Machinegun - 1250
     * Assault Rifle - 1500
     */

    //
    public string item;
    //

	vp_ItemPickup m_ItemPickup = null;
	vp_ItemPickup ItemPickup
	{
		get
		{
			if (m_ItemPickup == null)
				m_ItemPickup = transform.GetComponent<vp_ItemPickup>();
			if (m_ItemPickup == null)
			{
				Debug.LogError("Error ("+this+") This component requires a vp_ItemPickup (disabling self).");
			}
			return m_ItemPickup;
		}
	}


	/// <summary>
	/// 
	/// </summary>
	protected override void Start()
	{

		base.Start();

		if(InteractDistance == 0.0f)
			InteractDistance = 2.5f;

	}


	/// <summary>
	/// 
	/// </summary>
	public override bool TryInteract(vp_PlayerEventHandler player)
	{       
		if(ItemPickup == null)
			return false;
        //////////////////

        switch (item)
        {
            case "Revolver":
                if (BattleController.instance.funds.Value < 500)
                {
                    Debug.Log("Insufficent funds 500");
                    return true;
                }
                else
                {
                    BattleController.instance.HasRevolver(true);
                    BattleController.instance.UsedFunds(500);
                }
                break;
            case "Shotgun":
                if (BattleController.instance.funds.Value < 1000)
                {
                    Debug.Log("Insufficent funds 1000");
                    return true;
                }  
                else
                {
                    BattleController.instance.HasShotgun(true);
                    BattleController.instance.UsedFunds(1000);
                }
                break;

            case "Machinegun":
                if (BattleController.instance.funds.Value < 1250)
                {
                    Debug.Log("Insufficent funds 1200");
                    return true;
                }
                else
                {
                    BattleController.instance.HasMachinegun(true);
                    BattleController.instance.UsedFunds(1250);
                }
                break;

            case "Assault Rifle":
                if (BattleController.instance.funds.Value < 1500)
                {
                    Debug.Log("Insufficent funds 1400");
                    return true;
                }
                else
                {
                    BattleController.instance.HasAR(true);
                    BattleController.instance.UsedFunds(1500);
                }
                break;
        }
        
        //////////////////
        if (m_Player == null)
			m_Player = player;

		ItemPickup.TryGiveTo(m_Player.GetComponent<Collider>());

		return true;

	}
	

}
