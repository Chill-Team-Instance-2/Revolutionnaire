using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RV_ChangeIndex : MonoBehaviour
{
    [SerializeField] int index;
    [SerializeField] RV_AC_Card5 card5;

    public void ChangeIndex()
    {
        card5.cardIndex = index;
    }
}
