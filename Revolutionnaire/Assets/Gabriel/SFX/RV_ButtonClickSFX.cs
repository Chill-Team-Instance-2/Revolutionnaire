using UnityEngine;
using UnityEngine.UI;

public class RV_ButtonClickSFX : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(PlayClickSound);
    }

    private void PlayClickSound()
    {
        GameObject newObject = new GameObject();
        newObject.AddComponent<AudioSource>();
        newObject.GetComponent<AudioSource>().playOnAwake = false;
        newObject = Instantiate(newObject);
        AudioSource newAudioSource = newObject.GetComponent<AudioSource>();
        newAudioSource.clip = Resources.Load<AudioClip>("normalClick");
        newAudioSource.volume = 0.1f;
        newAudioSource.Play();
        Destroy(newObject, 2);
    }
}
