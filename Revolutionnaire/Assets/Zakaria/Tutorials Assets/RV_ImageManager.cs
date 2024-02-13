using UnityEngine;
using UnityEngine.UI;

public class RV_ImageManager : MonoBehaviour
{
    [SerializeField] private Sprite[] Sprites;
    public int Index = 0;
    private Image _Image;
    void Start()
    {
        Time.timeScale = 0f;
        _Image = GetComponentInChildren<Image>();
        _Image.sprite = Sprites[0];
    }

    void Update()
    {
        if (Index < 3)
        {
            _Image.sprite = Sprites[Index];
        }
        else
        {
            gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }
}
