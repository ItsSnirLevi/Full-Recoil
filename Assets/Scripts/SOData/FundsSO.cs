using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FundsSO : ScriptableObject
{

    [SerializeField]
    private int _total;

    public int Value
    {
        get { return _total; }
        set { _total = value; }
    }
}
