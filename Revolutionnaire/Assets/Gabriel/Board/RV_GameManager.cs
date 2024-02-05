using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RV_GameManager : MonoBehaviour
{
    public static RV_GameManager Instance;
    public RV_PickACardOnEndTour PickACardOnEndTour;

    public UnityEvent onendturn;

    [SerializeField] private Transform turnPawn;
    [SerializeField] private Transform turnTiles;

    public int Turn = 0;
    public int MaxTurn = 15;
    public int Bonus = 0;
    public float Multiplier = 0;

    public int InfluencePlayer = 0;
    public int InfluenceKing = 100;

    public int PlayerTurn = 0;

    public List<int> PlayersClass = new List<int>(); //0 = millice, 1 = commerçant, 2 = intellectuel
    public List<string> PlayersName = new List<string>();


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
    }

    public void EndTurn()
    {
        PickACardOnEndTour.PickACard();
        Turn++;
        NextPlayer();
        PickACardOnEndTour.ActualToDiscard();
        onendturn?.Invoke();
    }

    public int AddInfluence(float adding)
    {
        adding = (adding - Bonus) * Multiplier;
        InfluencePlayer += ((int)System.Math.Floor(adding));
        return InfluencePlayer;
    }
}
