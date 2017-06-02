using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : CharacterHealth
{
    private bool isSubdued;

    public bool IsSubdued
    {
        get { return isSubdued; }
        set { isSubdued = value; }
    }

    // Use this for initialization
    void Start()
    {
        isSubdued = false;
    }

    // Update is called once per frame
    protected override void Update()
    {

    }
}