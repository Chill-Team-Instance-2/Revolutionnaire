using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RV_AutoTurnRotation : MonoBehaviour
{
    private void Awake()
    {
        AutoRotate();
    }

    [ContextMenu("Auto Rotate")]
    public void AutoRotate()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetChild(0).localEulerAngles = new Vector3(0, 0, -transform.GetChild(i).localEulerAngles.z);
        }
    }
}
