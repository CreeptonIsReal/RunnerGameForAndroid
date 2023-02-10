using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  [SerializeField] private Transform _player;
  private Vector3 _offset;
  // Start is called before the first frame update
  void Start()
  {
    _offset = transform.position - _player.position;
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    Vector3 _newPosition = new Vector3(_player.position.x, transform.position.y, _player.position.z + _offset.z);
    transform.position = _newPosition;
  }
}
