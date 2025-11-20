using TMPro;
using UnityEngine;

public class MoneyScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI scoreEffect;
    [SerializeField] UIFadeEffects _uiFadeEffects;

    private int _score = 0;
    private int _itemScore;

    public void ScoreIncrease(int score)
    {

        if (EffectManager.NegativeEffect)
        {
            _itemScore = score - 2;
            _score += score - 2;
            scoreEffect.text = _itemScore.ToString();
            scoreEffect.color = Color.red;
        }
        else if (EffectManager.PositiveEffect)
        {
            _itemScore = score + 2;
            _score += score + 2;
            scoreEffect.text = _itemScore.ToString();
            scoreEffect.color = Color.blue;
        }
        else
        {
            _itemScore = score;
            _score += score;
            scoreEffect.text = _itemScore.ToString();
            scoreEffect.color = Color.green;
        }

        moneyText.text = _score + "$";
        StartCoroutine(_uiFadeEffects.DoTextFadeMoveDown(scoreEffect));
    }
}