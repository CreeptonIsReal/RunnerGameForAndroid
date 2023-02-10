using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCoinScript : MonoBehaviour
{
  void Update()
  {
    transform.Rotate(70 * Time.deltaTime, 0, 0);
  }
}
