using System.Collections;
using TMPro;
using UnityEngine;

public class RV_Dice_Model : MonoBehaviour
{
    private Quaternion baseRotation;
    private float baseHeight;

    [SerializeField] private TextMeshProUGUI resultText;

    [SerializeField] private AnimationCurve diceCurve;

    public bool IsLaunching = false;

    

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
        }
    }

    public IEnumerator ChangeTextWithDelay(int number, float delay)
    {
        yield return new WaitForSeconds(delay);
        resultText.text = number.ToString();
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
