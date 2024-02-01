using UnityEngine;
using UnityEngine.SceneManagement;

public class RV_GameOver_Canvas : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
