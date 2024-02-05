using System.Collections;
using UnityEngine;

public class RV_RevoltCard_Manager : MonoBehaviour
{
    public static RV_RevoltCard_Manager Instance;

    [SerializeField] private Transform currentPile;

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
        }
        StartCoroutine(JetDelayed(success, influenceGain, currentCard));
        return success;
    }

    private IEnumerator JetDelayed(bool success, int influenceGain, RV_RevoltCard currentCard)
    {
        yield return new WaitForSeconds(RV_DiceManager.Instance.DiceTime);
        
        if (success)
        {
            RV_GameManager.Instance.NextPlayer();
            currentCard.PointAdded += RV_GameManager.Instance.AddInfluence(influenceGain);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            RV_GameManager.Instance.EndTurn();
            RV_GameManager.Instance.InfluencePlayer -= currentCard.PointAdded;
        }

        yield return null;
    }
}
