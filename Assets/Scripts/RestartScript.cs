using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartScript : MonoBehaviour
{
  [SerializeField] Text _recordText;
  private void Start()
  {
    int lastRunScore = PlayerPrefs.GetInt("lastRunScore");
    int recordScore = PlayerPrefs.GetInt("_recordScore");

    if (lastRunScore > recordScore)
    {
      recordScore = lastRunScore;
      PlayerPrefs.SetInt("_recordScore", recordScore);
      _recordText.text = recordScore.ToString();
    }
    else
    {
      _recordText.text = recordScore.ToString();
    }
  }
  public void RestartLevel()
  {
    SceneManager.LoadScene(1);
  }

  public void MenuGame()
  {
    SceneManager.LoadScene(0);
  }
}
