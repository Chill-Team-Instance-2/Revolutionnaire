using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RV_AC_Parent : MonoBehaviour
{
    public int CardID = 0;
    public bool CanBePickup = true;
    public bool IsActive = false;


    public string TitleText = "Default Title Text";

    public string MilText = "Default Mil Text";
    public string ComText = "Default Com Text";
    public string IntText = "Default Int Text";

    public virtual void OnReveal()
    {
        print("default on reveal");
    }

    public virtual void OnFinishedReveal()
    {
        print("default on finished reveal");
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
        IsActive = false;
        print("default on discard");
    }

    public virtual void RefreshVisual()
    {

    }
}
