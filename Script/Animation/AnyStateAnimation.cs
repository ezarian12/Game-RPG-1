using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum RIG { BODY, LEGS};

public class AnyStateAnimation 
{
    public RIG AnimationRig { get; private set; }

    public string[] HigherPrio { get; set; }

    public string Name { get; set; }

    public bool Active { get; set; }

    public AnyStateAnimation(RIG rig, string name, params string[] higherPrio)
    {
        this.AnimationRig = rig;
        this.Name = name;
        this.HigherPrio = higherPrio;

    }
}
