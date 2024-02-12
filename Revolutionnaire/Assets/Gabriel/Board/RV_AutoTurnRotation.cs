using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RV_AutoTurnRotation : MonoBehaviour
{
    private void Awake()
    {
        AutoRotate();
        AutoScale();
    }

    [ContextMenu("Auto Rotate")]
    public void AutoRotate()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetChild(0).localEulerAngles = new Vector3(0, 0, -transform.GetChild(i).localEulerAngles.z);
        }
    }

    [ContextMenu("Auto Scale")]
    public void AutoScale()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetChild(0).localScale = new Vector3(1 + (1 - transform.GetChild(i).localScale.x), 1, 1);
        }
    }
}
