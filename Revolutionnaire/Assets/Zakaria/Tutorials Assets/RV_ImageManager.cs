using UnityEngine;
using UnityEngine.UI;

public class RV_ImageManager : MonoBehaviour
{
    [SerializeField] private Sprite[] Sprites;
    public int Index = 0;
    private Image _Image;
    void Start()
    {
        PlayerPrefs.GetInt("TutorialPassed", 0);
        print(PlayerPrefs.GetInt("TutorialPassed"));

        if (PlayerPrefs.GetInt("TutorialPassed") == 0)
        {
            gameObject.SetActive(true);
            Time.timeScale = 0f;
            _Image = GetComponentInChildren<Image>();
            _Image.sprite = Sprites[0];
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Index < 3)
        {
            _Image.sprite = Sprites[Index];
        }
        else
        {
            Time.timeScale = 1.0f;
            PlayerPrefs.SetInt("TutorialPassed", 1);
            gameObject.SetActive(false);
        }
    }
}
