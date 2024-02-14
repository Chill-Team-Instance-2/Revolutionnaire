using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RV_Influence_UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI kingInfluenceText;
    [SerializeField] private TextMeshProUGUI playerInfluenceText;

    private int influenceKing = 0;
    private int influencePlayer = 0;

    private void Start()
    {
        influenceKing = RV_GameManager.Instance.InfluenceKing;
        influencePlayer = RV_GameManager.Instance.InfluencePlayer;
    }

    private void Update()
    {
        if (influencePlayer != RV_GameManager.Instance.InfluencePlayer)
        {
            int adding = RV_GameManager.Instance.InfluencePlayer - influencePlayer;
            StartCoroutine(AddingInfluenceAnimation(adding.ToString(), 1.5f, 5));
        }

        influenceKing = RV_GameManager.Instance.InfluenceKing;
        influencePlayer = RV_GameManager.Instance.InfluencePlayer;

        kingInfluenceText.text = "Roi: " + RV_GameManager.Instance.InfluenceKing.ToString();
        playerInfluenceText.text = "Peuple: " + RV_GameManager.Instance.InfluencePlayer.ToString();
    }

    private IEnumerator AddingInfluenceAnimation(string text, float time, float startOffset)
    {
        Transform newText = Instantiate(playerInfluenceText, playerInfluenceText.transform.parent).transform;
        newText.position = playerInfluenceText.transform.position + (Vector3.down * startOffset) + (Vector3.right * 6);
        newText.GetComponent<TextMeshProUGUI>().text = text;
        Color colorText = newText.GetComponent<TextMeshProUGUI>().color;
        float timer = 0;
        while (timer < time)
        {
            newText.position += new Vector3(0, timer * 2, 0) * Time.deltaTime;
            newText.GetComponent<TextMeshProUGUI>().color = new Color(colorText.r, colorText.g, colorText.b, Mathf.Abs((timer / time)-1));
            timer += Time.deltaTime;
            yield return null;
        }
        Destroy(newText.gameObject);
        yield return null;
    }
}
