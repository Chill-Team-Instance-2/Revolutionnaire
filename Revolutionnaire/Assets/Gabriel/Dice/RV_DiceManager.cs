using UnityEngine;

public class RV_DiceManager : MonoBehaviour
{
    public static RV_DiceManager Instance;

    [SerializeField] private RV_Dice_Model dice;

    public int DiceResult;

    public float DiceTime = 2;

    public float ResultMultiplier = 1;
    public int ResultBonus = 0;

    private void Awake()
    {
        Instance = this;
    }

    public int LaunchDice()
    {
        DiceResult = dice.LaunchDice(DiceTime, true, true);
        DiceResult = ((int)(DiceResult * ResultMultiplier));
        DiceResult += ResultBonus;
        return DiceResult;
    }

    public bool IsLaunching()
    {
        return dice.IsLaunching;
    }
}
