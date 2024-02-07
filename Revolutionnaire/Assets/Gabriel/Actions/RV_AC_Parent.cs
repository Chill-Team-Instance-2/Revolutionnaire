using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RV_AC_Parent : MonoBehaviour
{
    public bool CanBePickup = true;

    public virtual void OnReveal()
    {

    }

    public virtual void OnPickUp()
    {
        print("default on pickup");
    }

    public virtual void Action()
    {
        print("default action");
    }

    public virtual void EndAction()
    {
        print("default end action");
    }

    public virtual void OnDiscard()
    {
        print("default on discard");
    }
}
