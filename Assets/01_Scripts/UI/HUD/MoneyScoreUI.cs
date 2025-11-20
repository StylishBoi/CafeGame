using TMPro;
using UnityEngine;

public class MoneyScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI scoreEffect;
    [SerializeField] UIFadeEffects _uiFadeEffects;

    public void ScoreIncrease()
    {

        if (EffectManager.NegativeEffect)
        {
            scoreEffect.text = ScoreSystem.LastScore.ToString();
            scoreEffect.color = Color.red;
        }
        else if (EffectManager.PositiveEffect)
        {
            scoreEffect.text = ScoreSystem.LastScore.ToString();
            scoreEffect.color = Color.blue;
        }
        else
        {
            scoreEffect.text = ScoreSystem.LastScore.ToString();
            scoreEffect.color = Color.green;
        }

        moneyText.text = ScoreSystem.MoneyScore + "$";
        StartCoroutine(_uiFadeEffects.DoTextFadeMoveDown(scoreEffect));
    }
}