using UnityEngine;
using UnityEngine.UI;

public class RV_AC_Card1 : MonoBehaviour
{
    private RV_GameManager gameManager;
    private bool CardUsed = false;
    [SerializeField] private Canvas CanvaOddOrEven;    
    public int OddOrEven = 0; //0 = pair 1 = impair
    public void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<RV_GameManager>();
    }
    public void Action()
    {
        switch(gameManager.PlayersClass[gameManager.PlayerTurn])
        {
            case 0:
                gameManager.Multiplier = gameManager.Multiplier + 1;
                break;
            case 1:
                gameManager.InfluencePlayer -= 2;
                //diceManager.diceResult += 2;
                break;
            case 2:
                CanvaOddOrEven.enabled = true;
                break;
            default:
                break;
        }
    }

    public void OddOrEvenCheck()
    {
        CanvaOddOrEven.enabled = false;
        if ((OddOrEven == 0 && diceManager.diceResult % 2 == 0) || (OddOrEven == 1 && diceManager.diceResult % 2 != 0))
        {
            gameManager.InfluencePlayer += 6;
        }
        else
        {
            gameManager.InfluencePlayer -= 6;
        }
    }

    public void EndAction()
    {
        switch (gameManager.PlayersClass[gameManager.PlayerTurn])
        {
            case 0:
                gameManager.Multiplier = gameManager.Multiplier - 1;
                break;
            case 1:
                break;
            case 2:
                break;
            default:
                break;
        }
    }
}
