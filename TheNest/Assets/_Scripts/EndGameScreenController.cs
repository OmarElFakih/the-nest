using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGameScreenController : MonoBehaviour
{

    [SerializeField] private GameObject _endScreen = null;
    [SerializeField] private TextMeshProUGUI _endGameText = null;


    [SerializeField] private string _gameWinMessage = "";
    [SerializeField] private string _gameOverMessage = "";
    [SerializeField] private float _fadeInDelay = 0;

    [SerializeField] Animator[] _animators;

    private void OnEnable()
    {
        GameManager.EndTheGame += StartEndGameRoutine;
    }

    private void OnDisable()
    {
        GameManager.EndTheGame -= StartEndGameRoutine;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartEndGameRoutine()
    {
        StartCoroutine(EndGameRoutine());

    }

    IEnumerator EndGameRoutine()
    {
        _endScreen.SetActive(true);
        _endGameText.text = (PlayerEnergyControl.PlayerIsDead || NestControl.NestIsDead) ? _gameOverMessage : _gameWinMessage;
        yield return new WaitForSeconds(_fadeInDelay);
        foreach (Animator anim in _animators)
        {
            anim.SetTrigger("FadeIn");
        }
    }
}
