using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RV_RevoltCard : MonoBehaviour
{
    [SerializeField] private Material materialFront;
    [SerializeField] private Material materialBack;

    [SerializeField] private MeshRenderer planeFront;
    [SerializeField] private MeshRenderer planeBack;

    //[SerializeField] private Sprite spriteFront;
    //[SerializeField] private Sprite spriteBack;
    
    //[SerializeField] private Image imageFront;
    //[SerializeField] private Image imageBack;

    public List<int> JetRequirements = new List<int>();
    public List<int> JetInfluences = new List<int>();
    public List<bool> JetAvailable = new List<bool>();
    public List<bool> JetWon = new List<bool>();

    public List<TextMeshProUGUI> TextRequirements = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> TextInfluences = new List<TextMeshProUGUI>();

    public int PointAdded = 0;

    private void Awake()
    {
        //if (spriteBack)
        //{
        //    imageBack.sprite = spriteBack;
        //}
        //if (spriteFront)
        //{
        //    imageFront.sprite = spriteFront;
        //}
        if (materialFront && planeFront)
        {
            planeFront.material = materialFront;
        }
        if (materialBack && planeBack) 
        {
            planeBack.material = materialBack;
        }

        for (int i = 0; i < JetRequirements.Count; i++)
        {
            TextRequirements[i].text = JetRequirements[i].ToString();
            TextInfluences[i].text = JetInfluences[i].ToString();
        }
    }

    public void Jet(int number)
    {
        if (!RV_DiceManager.Instance.IsLaunching() && JetAvailable[number] && !JetWon[number])
        {
            JetAvailable[number] = false;
            if (RV_RevoltCard_Manager.Instance.Jet(JetRequirements[number], JetInfluences[number], GetComponent<RV_RevoltCard>())) //win
            {
                StartCoroutine(ApplyColorText(number, RV_DiceManager.Instance.DiceTime, new Color(0, 0.75f, 0)));
                JetWon[number] = true;
            }
            else //jet rat�
            {
                StartCoroutine(ApplyColorText(number, RV_DiceManager.Instance.DiceTime, new Color(0.75f, 0, 0)));
                DisableAllJet();
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
        PointAdded = 0;
        for (int i = 0; i < JetRequirements.Count; i++)
        {
            TextRequirements[i].color = new Color(1, 1, 1);
            TextInfluences[i].color = new Color(1, 1, 1);
            JetAvailable[i] = true;
            JetWon[i] = false;
        }
    }

    public void DisableAllJet()
    {
        for (int i = 0; i < 3; i++)
        {
            JetAvailable[i] = false;
        }
    }

    public void ReanableLostJet()
    {
        for (int i = 0; i < 3; i++)
        {
            if (!JetWon[i])
            {
                JetAvailable[i] = true;
                TextRequirements[i].color = new Color(1, 1, 1);
                TextInfluences[i].color = new Color(1, 1, 1);
            }
        }
    }

    public void RemoveWonPoints()
    {
        RV_GameManager.Instance.InfluencePlayer -= PointAdded;
    }
}
