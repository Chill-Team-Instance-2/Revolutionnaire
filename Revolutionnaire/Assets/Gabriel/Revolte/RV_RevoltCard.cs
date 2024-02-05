using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RV_RevoltCard : MonoBehaviour
{
    public List<int> JetRequirements = new List<int>();
    public List<int> JetInfluences = new List<int>();

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
        if (!RV_DiceManager.Instance.IsLaunching())
        {
            if (RV_RevoltCard_Manager.Instance.Jet(JetRequirements[number], JetInfluences[number]))
            {
                StartCoroutine(ApplySuccessText(number, 2));
            }
        }
    }

    public IEnumerator ApplySuccessText(int number, float wait)
    {
        yield return new WaitForSeconds(wait);
        TextRequirements[number].color = new Color(0, 0.75f, 0);
        TextInfluences[number].color = new Color(0, 0.75f, 0);
        yield return null;
    }
}
