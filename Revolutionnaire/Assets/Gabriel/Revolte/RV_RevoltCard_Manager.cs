using System.Collections;
using UnityEngine;

public class RV_RevoltCard_Manager : MonoBehaviour
{
    public static RV_RevoltCard_Manager Instance;

    [SerializeField] private Transform currentPile;

    public int LastTryInfluenceGain = 0;
    public bool LastTrySuccesResult = false;

    private void Awake()
    {
        Instance = this;
    }

    public bool Jet(int requirement, int influenceGain, RV_RevoltCard currentCard)
    {
        bool success = false;
        int diceResult = RV_DiceManager.Instance.LaunchDice();
        if (diceResult >= requirement)
        {
            success = true;
            currentCard.PointAdded +=  RV_GameManager.Instance.AddInfluenceWithDelay(influenceGain, RV_DiceManager.Instance.DiceTime);
        }
        StartCoroutine(JetDelayed(success, influenceGain, currentCard));
        LastTryInfluenceGain = influenceGain;
        LastTrySuccesResult = success;
        return success;
    }

    private IEnumerator JetDelayed(bool success, int influenceGain, RV_RevoltCard currentCard)
    {
        yield return new WaitForSeconds(RV_DiceManager.Instance.DiceTime);
        


        if (success)
        {
            bool cardCompleted = true;
            for (int i = 0; i < 3; i++)
            {
                if (currentCard.JetAvailable[i])
                    cardCompleted = false;
            }
            if (cardCompleted)
            {
                yield return new WaitForSeconds(0.5f);
                RV_GameManager.Instance.EndTurn();
            }
            else
            {
                RV_GameManager.Instance.NextPlayer();
            }
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            //RV_GameManager.Instance.EndTurn();
            currentCard.RemoveWonPoints();
            RV_GameManager.Instance.NextPlayer();
        }

        yield return null;
    }
}
