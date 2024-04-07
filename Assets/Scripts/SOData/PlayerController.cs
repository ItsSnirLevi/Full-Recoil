using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerController : ScriptableObject {

    [Header("Is Holding")]
    public bool revolver = false;
    public bool shotgun = false;
    public bool machinegun = false;
    public bool assaultRifle = false;


    public void ResetValues()
    {
        revolver = false;
        shotgun = false;
        machinegun = false;
        assaultRifle = false;      
    }

    public bool GetIsActiveByIndex(int index)
    {
        switch (index)
        { 
            case 0:
                return revolver;
            case 1:
                return shotgun;
            case 2:
                return machinegun;
            case 3:
                return assaultRifle;
            default:
                return false;
        }
    }
}
