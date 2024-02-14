using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeIndexTutorial : MonoBehaviour
{
    [SerializeField] RV_ImageManager imageManager;

    public void SkipTutorial()
    {
        imageManager.Index = 3;
    }
    public void ChangeIndex()
    {
        imageManager.Index += 1;
    }
}
