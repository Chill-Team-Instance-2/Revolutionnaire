using TMPro;
using UnityEngine;

public class RV_Influence_UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI kingInfluenceText;
    [SerializeField] private TextMeshProUGUI playerInfluenceText;

    private void Update()
    {
        kingInfluenceText.text = "Roi: " + RV_GameManager.Instance.InfluenceKing.ToString();
        playerInfluenceText.text = "Peuple: " + RV_GameManager.Instance.InfluencePlayer.ToString();
    }
}
