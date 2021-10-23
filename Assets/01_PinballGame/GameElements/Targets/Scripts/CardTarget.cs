using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardTarget : MonoBehaviour
{
    [Header("Reference - Write")]
    [SerializeField]
    private IntegerVariable _score;

    [Header("Config")]
    [SerializeField]
    private TMP_Text _cardLevelText;
    [SerializeField]
    private TMP_Text _scoreEffectText;

    public void RandomScore()
    {
        int roll = Random.Range(1, 10);
        _cardLevelText.text = roll.ToString();

        int score = roll * 100;
        _scoreEffectText.text = score.ToString();

        _score.Value += score;
    }
}
