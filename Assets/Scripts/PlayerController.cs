using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
  const float MAX_SPEED = 100;

  private CharacterController _controller;
  private CapsuleCollider _col;
  private Animator _anim;
  private Vector3 _dir;
  private TextScript _score;
  [SerializeField] private Text _moneyText;
  [SerializeField] private TextScript _recordText;
  [SerializeField] private float _speed = 40;
  [SerializeField] private float _jump;
  [SerializeField] private float _gravity;
  [SerializeField] private GameObject losePanel;
  [SerializeField] private int _coins;
  [SerializeField] private GameObject scoreText;

  private bool _Slide;

  private int _lineToMove = 1; //линия по которой движемся 0 - левая, 1 - середина, 2 - правая
  public float _lineDistance = 6; //расстояние между линиями
  // Start is called before the first frame update
  void Start()
  {
    _anim = GetComponentInChildren<Animator>();
    _col = GetComponent<CapsuleCollider>();
    _controller = GetComponent<CharacterController>();
    _score = scoreText.GetComponent<TextScript>();
    StartCoroutine(SpeedIncrease());
    Time.timeScale = 1;
    _score.scoreMultiplier = 1;
    _coins = PlayerPrefs.GetInt("_coins");
  }

  private void Update()
  {
    if (SwipeController.swipeRight)
    {
      if (_lineToMove < 2)
      {
        _lineToMove++;
        _anim.SetTrigger("Right");
      }

    }

    if (SwipeController.swipeLeft)
    {
      if (_lineToMove > 0)
      {
        _lineToMove--;
        _anim.SetTrigger("Left");
      }
    }

    if (SwipeController.swipeUp)
    {
      if (_controller.isGrounded)
        Jump();
    }

    if (SwipeController.swipeDown)
    {
      StartCoroutine(Slide());
    }

    if (_controller.isGrounded && !_Slide)
    {
      _anim.SetBool("isRunning", true);
    }
    else
    {
      _anim.SetBool("isRunning", false);
    }

    Vector3 _targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
    if (_lineToMove == 0)
      _targetPosition += Vector3.left * _lineDistance;
    else if (_lineToMove == 2)
      _targetPosition += Vector3.right * _lineDistance;
    if (transform.position == _targetPosition)
      return;

    Vector3 diff = _targetPosition - transform.position;
    Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
    if (moveDir.sqrMagnitude < diff.sqrMagnitude)
      _controller.Move(moveDir);
    else
      _controller.Move(diff);
  }

  void Jump()
  {
    _dir.y = _jump;
    _anim.SetTrigger("Jump");
  }

  void FixedUpdate()
  {
    _dir.z = _speed;
    _dir.y += _gravity * Time.fixedDeltaTime;
    _controller.Move(_dir * Time.fixedDeltaTime);
  }

  private void OnControllerColliderHit(ControllerColliderHit hit)
  {
    if (hit.gameObject.tag == "obstacle")
    {
      losePanel.SetActive(true);
      int lastRunScore = int.Parse(_recordText._scoreText.text.ToString());
      PlayerPrefs.SetInt("lastRunScore", lastRunScore);
      Time.timeScale = 0;
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == "GoldCoin")
    {
      _coins += 5;
      PlayerPrefs.SetInt("_coins", _coins);
      _moneyText.text = _coins.ToString();
      Destroy(other.gameObject);
    }
    if (other.gameObject.tag == "SilverCoin")
    {
      _coins += 3;
      PlayerPrefs.SetInt("_coins", _coins);
      _moneyText.text = _coins.ToString();
      Destroy(other.gameObject);
    }
    if (other.gameObject.tag == "BronzeCoin")
    {
      _coins += 1;
      PlayerPrefs.SetInt("_coins", _coins);
      _moneyText.text = _coins.ToString();
      Destroy(other.gameObject);
    }

    if (other.gameObject.tag == "BonusX2")
    {
      StartCoroutine(Bonus2x());
      Destroy(other.gameObject);
    }
  }

  private IEnumerator SpeedIncrease()
  {
    yield return new WaitForSeconds(2);
    if (_speed < MAX_SPEED)
    {
      _speed += 1;
      StartCoroutine(SpeedIncrease());
    }
  }

  private IEnumerator Slide()
  {
    _col.center = new Vector3(0, 0.38f, 0);
    _col.height = 0.8f;
    _Slide = true;
    _anim.SetTrigger("Slide");

    yield return new WaitForSeconds(1);

    _col.center = new Vector3(0, 0.9f, 0);
    _col.height = 1.85f;
    _Slide = false;
  }

  private IEnumerator Bonus2x()
  {
    _score.scoreMultiplier = 2;
    yield return new WaitForSeconds(5);
    _score.scoreMultiplier = 1;
  }
}
