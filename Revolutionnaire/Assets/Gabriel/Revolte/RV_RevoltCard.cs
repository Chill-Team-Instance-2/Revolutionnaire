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

    [SerializeField] public Sprite spriteFront;
    //[SerializeField] private Sprite spriteBack;
    
    //[SerializeField] private Image imageFront;
    //[SerializeField] private Image imageBack;

    public List<int> JetRequirements = new List<int>();
    public List<int> JetInfluences = new List<int>();
    public List<bool> JetAvailable = new List<bool>();
    public List<bool> JetWon = new List<bool>();

    public List<TextMeshProUGUI> TextRequirements = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> TextInfluences = new List<TextMeshProUGUI>();
    public Color TextBaseColor = Color.black;

    public int PointAdded = 0;

    public bool HasBeenPlayed = false;
    private void Awake()
    {
        TextBaseColor = TextRequirements[0].color;
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
            HasBeenPlayed = true;
            JetAvailable[number] = false;
            if (RV_RevoltCard_Manager.Instance.Jet(JetRequirements[number], JetInfluences[number], GetComponent<RV_RevoltCard>())) //win
            {
                StartCoroutine(ApplyColorText(number, RV_DiceManager.Instance.DiceTime, new Color(0, 0.75f, 0)));
                JetWon[number] = true;
            }
            else //jet raté
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
        HasBeenPlayed = false;
        for (int i = 0; i < JetRequirements.Count; i++)
        {
            TextRequirements[i].color = TextBaseColor;
            TextInfluences[i].color = TextBaseColor;
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
            if (!JetAvailable[i] && !JetWon[i])
            {
                JetAvailable[i] = true;
                TextRequirements[i].color = new Color(0, 0, 0);
                TextInfluences[i].color = new Color(0, 0, 0);
            }
        }
    }

    public bool HasLostJet()
    {
        for (int i = 0; i < 3; i++)
        {
            if (!JetWon[i])
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveWonPoints()
    {
        RV_GameManager.Instance.InfluencePlayer -= PointAdded;
    }

    public void GiveBackLostPoints()
    {
        RV_GameManager.Instance.InfluencePlayer += PointAdded;
    }
}
