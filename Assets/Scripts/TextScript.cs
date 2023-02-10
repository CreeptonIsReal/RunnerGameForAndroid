using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
  // Start is called before the first frame update
  [SerializeField] private Transform _player;
  [SerializeField] public Text _scoreText;

  private int totalScore;
  public int scoreMultiplier;

  private void FixedUpdate()
  {
    totalScore += scoreMultiplier;
    _scoreText.text = totalScore.ToString();
  }
}
