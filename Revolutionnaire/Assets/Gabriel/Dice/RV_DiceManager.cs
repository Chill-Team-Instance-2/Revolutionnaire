using UnityEngine;

public class RV_DiceManager : MonoBehaviour
{
    public static RV_DiceManager Instance;

    [SerializeField] private RV_Dice_Model dice;

    public int DiceResult;

    private void Awake()
    {
        Instance = this;
    }

    public int LaunchDice()
    {
        DiceResult = dice.LaunchDice(true, true);
        return DiceResult;
    }

    public bool IsLaunching()
    {
        return dice.IsLaunching;
    }
}
