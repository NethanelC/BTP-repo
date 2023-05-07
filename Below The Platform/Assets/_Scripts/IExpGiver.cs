using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExpGiver
{
    public static event Action<int> OnExperienceAcquired;
    public int _expPoints { get; set; }
}
