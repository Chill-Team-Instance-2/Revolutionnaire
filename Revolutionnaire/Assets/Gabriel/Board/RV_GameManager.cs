using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RV_GameManager : MonoBehaviour
{
    public static RV_GameManager Instance;
    public RV_PickACardOnEndTour PickACardOnEndTour;
    public UnityEvent onendturn;
    public UnityEvent OnChangePlayer;
    [SerializeField] private Image milicia_Image;
    [SerializeField] private Image intellectual_Image;
    [SerializeField] private Image merchant_Image;
    [SerializeField] private Sprite unselected_Image;
    [SerializeField] private Sprite selected_Image;
    [SerializeField] private Transform turnPawn;
    [SerializeField] private Transform turnTiles;

    [SerializeField] private GameObject canvasGuillotine;
    [SerializeField] private TextMeshProUGUI textDescriptionGuillotine;

    [SerializeField] private GameObject canvasGameOver;
    [SerializeField] private TextMeshProUGUI textGameResult;
    [SerializeField] private TextMeshProUGUI textDescriptionResult;
    [SerializeField] private TextMeshProUGUI textEndInfluence;

    [SerializeField] private int cheatUsePlayerClass = -1; // -1 = not activated

    public int Turn = 0;
    public int MaxTurn = 15;
    public int Bonus = 0;
    public float Multiplier = 1;

    public int InfluencePlayer = 0;
    public int InfluenceKing = 100;

    public int PlayerTurn = 0;

    public List<int> PlayersClass = new List<int>(); //0 = millice, 1 = commerçant, 2 = intellectuel
    public List<string> PlayersName = new List<string>();

    public bool CanEndTurn = true;




    private void Awake()
    {
        Instance = this;
        SetupPlayers();
    }
    private void Start()
    {
        PickACardOnEndTour.PickACard();
    }

    private void Update()
    {
        ChangeImageColorOnTurn();
        turnPawn.position = Vector3.Lerp(turnPawn.position, turnTiles.GetChild(Turn).position, 8 * Time.deltaTime);
    }

    private void SetupPlayers()
    {
        PlayersName.Add("Player 1");
        PlayersName.Add("Player 2");
        PlayersName.Add("Player 3");

        List<int> classGiven = new List<int>();
        for (int i = 0; i < 3;)
        {
            int givingClass = Random.Range(0, 3);
            if (!classGiven.Contains(givingClass))
            {
                PlayersClass.Add(givingClass);
                classGiven.Add(givingClass);
                i++;
            }
        }
    }

    public void NextPlayer()
    {
        PlayerTurn++;
        if (PlayerTurn > 2) PlayerTurn = 0;
        if (cheatUsePlayerClass != -1) PlayerTurn = cheatUsePlayerClass; //CHEAT
        OnChangePlayer.Invoke();
    }

    public void EndTurn()
    {
        if (CanEndTurn && Turn >= 15) //&& (influenceKing - influencePlayer) < 20
        {
            ActivateJetGuillotine();
            return;
        }

        if (RV_PickACardOnEndTour.Instance.CurrentCard.TryGetComponent<RV_ActionCard>(out RV_ActionCard actionCard)) // action card
        {
            PassTurn();
            return;
        }
        if (CanEndTurn) // revolt
        {
            Turn++;
            if (RV_PickACardOnEndTour.Instance.CurrentCard.TryGetComponent<RV_RevoltCard>(out RV_RevoltCard revoltCard))
            {
                if (!revoltCard.HasBeenPlayed)
                    NextPlayer();
            }
            PickACardOnEndTour.ActualToDiscard();
            PickACardOnEndTour.PickACard();
            onendturn?.Invoke();
        }
    }

    public void ActivateJetGuillotine()
    {
        canvasGuillotine.SetActive(true);
        textDescriptionGuillotine.text = "Faites " + (InfluenceKing - InfluencePlayer + 1).ToString() + " ou plus pour détrôner le roi !";
    }

    public void LaunchJetGuillotine()
    {
        canvasGuillotine.SetActive(false);
        int result = RV_DiceManager.Instance.LaunchDice();
        InfluencePlayer += result;
        if (InfluencePlayer > InfluenceKing)
        {
            RV_DiceManager.Instance.onDiceEnd.AddListener(EndJetGuillotineVictory);
        }
        else
        {
            RV_DiceManager.Instance.onDiceEnd.AddListener(EndJetGuillotineDefeat);
        }
    }

    public void EndJetGuillotineVictory()
    {
        print("victory!");

        canvasGameOver.SetActive(true);
        textDescriptionResult.text = "Victoire !";
        textEndInfluence.text = "Influence: " + InfluencePlayer.ToString();
    } 
    
    public void EndJetGuillotineDefeat()
    {
        print("defeat!");

        canvasGameOver.SetActive(true);
        textDescriptionResult.text = "Défaite";
        textEndInfluence.text = "Influence: " + InfluencePlayer.ToString();
    }

    public void PassTurn() //when ending turn on action card
    {
        if (CanEndTurn)
        {
            PickACardOnEndTour.ActualToDiscard();
            PickACardOnEndTour.PickACard();
            NextPlayer();
            onendturn?.Invoke();
        }
    }

    public int AddInfluence(float adding)
    {
        adding = (adding + Bonus) * Multiplier;
        adding = ((int)System.Math.Floor(adding));
        InfluencePlayer += (int)adding;
        return (int)adding;
    }

    public int AddInfluenceWithDelay(float adding, float delay)
    {
        adding = (adding + Bonus) * Multiplier;
        adding = ((int)System.Math.Floor(adding));
        StartCoroutine(CoroutineAddInfluenceWithDelay(adding, delay));
        return (int)adding;
    }

    private IEnumerator CoroutineAddInfluenceWithDelay(float adding, float delay)
    {
        yield return new WaitForSeconds(delay);
        InfluencePlayer += (int)adding;
    }

    public void ChangeImageColorOnTurn()
    {
        switch (PlayerTurn)
        {
            case 0:
                if (milicia_Image && merchant_Image && intellectual_Image)
                {
                    milicia_Image.sprite = selected_Image;
                    milicia_Image.color = new Vector4(milicia_Image.color.r, milicia_Image.color.g, milicia_Image.color.b, 1f);

                    merchant_Image.sprite = unselected_Image;
                    merchant_Image.color = new Vector4(merchant_Image.color.r, merchant_Image.color.g, merchant_Image.color.b, 0.75f);

                    intellectual_Image.sprite = unselected_Image;
                    intellectual_Image.color = new Vector4(intellectual_Image.color.r, intellectual_Image.color.g, intellectual_Image.color.b, 0.75f);
                }
                break;
            case 1:
                if (milicia_Image && merchant_Image && intellectual_Image)
                {
                    milicia_Image.sprite = unselected_Image;
                    milicia_Image.color = new Vector4(milicia_Image.color.r, milicia_Image.color.g, milicia_Image.color.b, 0.75f);

                    merchant_Image.sprite = selected_Image;
                    merchant_Image.color = new Vector4(merchant_Image.color.r, merchant_Image.color.g, merchant_Image.color.b, 1f);

                    intellectual_Image.sprite = unselected_Image;
                    intellectual_Image.color = new Vector4(intellectual_Image.color.r, intellectual_Image.color.g, intellectual_Image.color.b, 0.75f);
                }
                break;
            case 2:
                if (milicia_Image && merchant_Image && intellectual_Image)
                {
                    milicia_Image.sprite = unselected_Image;
                    milicia_Image.color = new Vector4(milicia_Image.color.r, milicia_Image.color.g, milicia_Image.color.b, 0.75f);

                    merchant_Image.sprite = unselected_Image;
                    merchant_Image.color = new Vector4(merchant_Image.color.r, merchant_Image.color.g, merchant_Image.color.b, 0.75f);

                    intellectual_Image.sprite = selected_Image;
                    intellectual_Image.color = new Vector4(intellectual_Image.color.r, intellectual_Image.color.g, intellectual_Image.color.b, 1f);
                }
                break;
            default:
                break;
        }
    }

    public void EnableEndTurn()
    {
        CanEndTurn = true;
    }

    public void DisableEndTurn()
    {
        CanEndTurn = false;
    }
}
