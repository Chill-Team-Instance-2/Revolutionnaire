using UnityEngine;

public class RV_RevoltCard_Manager : MonoBehaviour
{
    public static RV_RevoltCard_Manager Instance;

    [SerializeField] private Transform currentPile;

    private void Awake()
    {
        Instance = this;
    }

    public bool Jet(int requirement, int influenceGain)
    {
        bool success = false;
        int diceResult = RV_DiceManager.Instance.LaunchDice();
        if (diceResult >= requirement)
        {
            success = true;
            RV_GameManager.Instance.AddInfluence(influenceGain);
        }
        RV_GameManager.Instance.NextPlayer();
        return success;
    }
}
