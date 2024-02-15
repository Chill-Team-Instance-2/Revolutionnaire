using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RV_GoToMenu : MonoBehaviour
{
    [SerializeField] private GameObject PauseCanvas;
    public void GoToPauseMenu()
    {
        PauseCanvas.SetActive(true);
    }
}
