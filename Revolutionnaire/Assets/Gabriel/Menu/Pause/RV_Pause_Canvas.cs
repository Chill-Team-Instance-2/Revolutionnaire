using UnityEngine;
using UnityEngine.SceneManagement;

public class RV_Pause_Canvas : MonoBehaviour
{
    public void Continue()
    {
        gameObject.SetActive(false);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
