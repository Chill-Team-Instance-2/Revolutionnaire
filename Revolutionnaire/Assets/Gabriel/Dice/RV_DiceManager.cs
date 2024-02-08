using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RV_DiceManager : MonoBehaviour
{
    public static RV_DiceManager Instance;

    [SerializeField] private RV_Dice_Model dice;

    public int DiceResult;

    public float DiceTime = 2;

    public float ResultMultiplier = 1;
    public int ResultBonus = 0;

    public UnityEvent onDiceLaunch;
    public UnityEvent onDiceEnd;

    private void Awake()
    {
        Instance = this;
    }

    public int LaunchDice()
    {
        DiceResult = dice.LaunchDice(DiceTime, true, true);
        DiceResult = ((int)(DiceResult * ResultMultiplier));
        DiceResult += ResultBonus;
        onDiceLaunch.Invoke();
        return DiceResult;
    }

    public bool IsLaunching()
    {
        return dice.IsLaunching;
    }

    private IEnumerator SendOnDiceEnd()
    {
        yield return new WaitForSeconds(DiceTime + 0.25f);
        onDiceEnd.Invoke();
    }
}
