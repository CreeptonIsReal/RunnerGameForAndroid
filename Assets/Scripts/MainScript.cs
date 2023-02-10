using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
  [SerializeField] private Text _coinsText;

  private void Start()
  {
    int _coins = PlayerPrefs.GetInt("_coins");
    _coinsText.text = "Денег: " + _coins.ToString();
  }


  public void PlayGame()
  {
    SceneManager.LoadScene(1);
  }
}
