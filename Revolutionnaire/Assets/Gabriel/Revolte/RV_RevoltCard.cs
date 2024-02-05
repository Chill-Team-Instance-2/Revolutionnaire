using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RV_RevoltCard : MonoBehaviour
{
    public List<int> JetRequirements = new List<int>();
    public List<int> JetInfluences = new List<int>();
    public List<bool> JetAvailable = new List<bool>();

    public List<TextMeshProUGUI> TextRequirements = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> TextInfluences = new List<TextMeshProUGUI>();

    private void Awake()
    {
        for (int i = 0; i < JetRequirements.Count; i++)
        {
            TextRequirements[i].text = JetRequirements[i].ToString();
            TextInfluences[i].text = JetInfluences[i].ToString();
        }
    }

    public void Jet(int number)
    {
        if (!RV_DiceManager.Instance.IsLaunching() && JetAvailable[number])
        {
            JetAvailable[number] = false;
            if (RV_RevoltCard_Manager.Instance.Jet(JetRequirements[number], JetInfluences[number]))
            {
                StartCoroutine(ApplyColorText(number, 2, new Color(0, 0.75f, 0)));
            }
            else
            {
                StartCoroutine(ApplyColorText(number, 2, new Color(0.75f, 0, 0)));
            }
        }
    }

    public IEnumerator ApplyColorText(int number, float wait, Color newColor)
    {
        yield return new WaitForSeconds(wait);
        TextRequirements[number].color = newColor;
        TextInfluences[number].color = newColor;
        yield return null;
    }

    public void ResetCard()
    {
        for (int i = 0; i < JetRequirements.Count; i++)
        {
            TextRequirements[i].color = new Color(1, 1, 1);
            TextInfluences[i].color = new Color(1, 1, 1);
            JetAvailable[i] = true;
        }
    }
}
