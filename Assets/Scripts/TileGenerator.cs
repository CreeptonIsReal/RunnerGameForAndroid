using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
  // Start is called before the first frame update
  public GameObject[] _tilePrefabs;
  private List<GameObject> _activeTile = new List<GameObject>();
  private float _spawnPos = 0;
  private float _tileL = 100;

  [SerializeField] private Transform _player;
  private int _startTiles = 6;
  void Start()
  {
    for (int i = 0; i < _startTiles; i++)
    {
      if (i == 0)
      {
        SpawnTile(3);
      }
      SpawnTile(Random.Range(0, _tilePrefabs.Length - 1));
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (_player.position.z - 40 > _spawnPos - (_startTiles * _tileL))
    {
      SpawnTile(Random.Range(0, _tilePrefabs.Length));
      DeleteTile();
    }
  }

  private void SpawnTile(int tileIndex)
  {
    GameObject _nextTile = Instantiate(_tilePrefabs[tileIndex], transform.forward * _spawnPos, transform.rotation);
    _activeTile.Add(_nextTile);
    _spawnPos += _tileL;
  }

  private void DeleteTile()
  {
    Destroy(_activeTile[0]);
    _activeTile.RemoveAt(0);
  }
}
