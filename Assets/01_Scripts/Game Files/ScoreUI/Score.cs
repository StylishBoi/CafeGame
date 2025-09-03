using System;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] GameObject _scoreEffect;
    private static bool _effect;

    private ScoreEffect effect;
    
    private static TextMeshProUGUI _scoreText;
    private static int _score;
    private static int _itemScore;

    private void Start()
    {
        if(TryGetComponent(out _scoreText))
        {
            Debug.Log("Score attached");
        }
    }

    private void Update()
    {
        if (_effect)
        {
            Instantiate(_scoreEffect, new Vector3(transform.position.x, transform.position.y+150,0), Quaternion.identity, transform);
            effect = GetComponentInChildren<ScoreEffect>();
            effect.effectScore=_itemScore;
            _effect = false;
        }
    }

    public static void ScoreIncrease(int score)
    {
        if (EffectManager.NegativeEffect)
        {
            _itemScore = score-2;
            _score += score-2;
        }
        else if (EffectManager.PositiveEffect)
        {
            _itemScore = score+2;
            _score += score+2;
        }
        else
        {
            _itemScore = score;
            _score += score;
        }
        
        
        _scoreText.text=_score.ToString("0");
        _effect = true;
    }
}
