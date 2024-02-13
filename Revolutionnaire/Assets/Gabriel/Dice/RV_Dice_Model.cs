using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RV_Dice_Model : MonoBehaviour
{
    private Quaternion baseRotation;
    private float baseHeight;

    [SerializeField] private TextMeshProUGUI resultText;

    [SerializeField] private AnimationCurve diceCurve;

    public bool IsLaunching = false;

    public Transform FakeNumbersParent;

    private void Awake()
    {
        baseRotation = transform.rotation;
        baseHeight = transform.position.z;
    }

    public void testButton()
    {
        LaunchDice();
    }

    public void ChangeText(int number, float delay = -1)
    {
        if (delay != -1)
        {
            StartCoroutine(ChangeTextWithDelay(number, delay));
        }
        else
        {
            resultText.text = number.ToString();
            ApplyFakeNumbers(number);
        }
    }

    public void ApplyFakeNumbers(int result) //result = number to avoid
    {
        List<TextMeshProUGUI> allFakeNumbersText = new List<TextMeshProUGUI>();
        for (int i = 0; i < FakeNumbersParent.childCount; i++)
        {
            allFakeNumbersText.Add(FakeNumbersParent.GetChild(i).GetComponent<TextMeshProUGUI>());
        }

        List<int> alreadyAppliedNumbers = new List<int>();
        for (int i = 0; i < 19; i++)
        {
            int number = Random.Range(1, 21);
            if (alreadyAppliedNumbers.Contains(number) || number == result)
            {
                i -= 1;
                continue;
            }
            else
            {
                alreadyAppliedNumbers.Add(number);
                allFakeNumbersText[i].text = number.ToString();
            }
        }
    }

    public IEnumerator ChangeTextWithDelay(int number, float delay)
    {
        yield return new WaitForSeconds(delay);
        resultText.text = number.ToString();
        ApplyFakeNumbers(number);
    }

    public int LaunchDice(float diceTime = 2, bool show = true, bool hide = true)
    {
        transform.rotation = baseRotation;
        //transform.position = new Vector3(0, 0, baseHeight);

        int result = Random.Range(1, 20);

        IsLaunching = true;
        
        StartCoroutine(LaunchingDice(result, diceTime, show, hide));

        return result;
    }

    private IEnumerator LaunchingDice(int result, float diceTime, bool show, bool hide)
    {
        if (show)
        {
            StartCoroutine(ShowDice(1));
            yield return new WaitForSeconds(1);
        }

        StartCoroutine(LaunchDiceRotation(result, diceTime));
        StartCoroutine(LaunchDiceFalling(diceTime));

        if (hide)
        {
            yield return new WaitForSeconds(diceTime);
            StartCoroutine(HideDice(1));
            yield return new WaitForSeconds(1);
        }

        IsLaunching = false;
        yield return null;
    }

    public IEnumerator HideDice(float animTime = 1)
    {
        float timeAnimating = 0;
        while (timeAnimating < animTime)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(0, -70, baseHeight), 5 * Time.deltaTime);
            timeAnimating += Time.deltaTime;
            yield return null;
        }
        //transform.position = new Vector3(0, -70, baseHeight);
    }

    public IEnumerator ShowDice(float animTime = 1)
    {
        float timeAnimating = 0;
        while (timeAnimating < animTime)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(0, 0, baseHeight), 5 * Time.deltaTime);
            timeAnimating += Time.deltaTime;
            yield return null;
        }
        //transform.position = new Vector3(0, 0, baseHeight);
    }

    private IEnumerator LaunchDiceFalling(float timeFall)
    {
        float timeFalling = 0;
        while (timeFalling < timeFall)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, baseHeight + (-70 * diceCurve.Evaluate(timeFalling)));
            timeFalling += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }

    private IEnumerator LaunchDiceRotation(int result, float timeRotation)
    {
        float timeRoll = timeRotation/2f;
        float timeRolling = 0;
        Vector3 rotationRoll = new Vector3(Random.Range(0, 360), Random.Range(0, 360), baseRotation.z);

        while (true)
        {
            transform.Rotate(rotationRoll.normalized * 1300 * Time.deltaTime);
            timeRolling += Time.deltaTime;
            if (timeRolling > timeRoll)
            {
                break;
            }
            yield return null;
        }

        float timeResetRoll = timeRotation/2f;
        float timeResetRolling = 0;
        while (true)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, baseRotation, 15 * Time.deltaTime);
            timeResetRolling += Time.deltaTime;
            if (timeResetRolling > timeResetRoll)
            {
                break;
            }
            yield return null;
        }
    }

    
}
