using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RV_ChangeIndex : MonoBehaviour
{
    [SerializeField] int index;
    [SerializeField] RV_AC_Card5 card5;
    [SerializeField] RV_AC_Card9 card9;
    [SerializeField] bool isCard5;

    public void ChangeIndex()
    {
        if (isCard5)
        {
            card5.cardIndex = index;
        }
        else
        {
            card9.cardIndex = index;
        }
    }
}
