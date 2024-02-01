using UnityEngine;
using UnityEngine.SceneManagement;

public class RV_Pause_Canvas : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Continue()
    {
        gameObject.SetActive(false);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
