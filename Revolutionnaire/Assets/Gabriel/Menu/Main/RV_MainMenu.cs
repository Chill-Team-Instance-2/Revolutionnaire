using UnityEngine;
using UnityEngine.SceneManagement;

public class RV_MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject creditMenu;

    private void Start()
    {
        creditMenu.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    
    public void Credit()
    {
        creditMenu.SetActive(!creditMenu.activeSelf);
    }

    public void CloseCredit()
    {
        creditMenu.SetActive(false);
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
