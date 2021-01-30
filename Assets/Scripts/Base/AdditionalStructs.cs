using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct NamedInt
{
    public string name;
    public int value;
}

[Serializable]
public struct NamedFmodSound
{
    public string name;

    [FMODUnity.EventRef]
    public string value;
}