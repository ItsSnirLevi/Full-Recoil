using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScoreSOLegion : ScriptableObject
{

    [SerializeField]
    private int _score;

    public int Value
    {
        get { return _score; }
        set { _score = value; }
    }
}
