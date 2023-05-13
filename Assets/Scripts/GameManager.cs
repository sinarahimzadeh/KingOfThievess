using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instamce;
    public enum GameState {pregame, game ,finish}
    public GameState _gameState;
    public TextMeshProUGUI tp;
    public float originalSpeed;
    public int Score,timer;
    public float counter;
    private void Awake()
    {
        counter = 90; 
        originalSpeed = CharacterMovement.instance.speed;
        Instamce = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        _gameState = GameState.pregame; 
    }
    private void OnLevelWasLoaded(int level)
    {
        _gameState = GameState.pregame;
    }

    // Update is called once per frame
    void Update()
    {
        counter -= Time.deltaTime;
       //  tp.text = CharacterMovement.instance.state2.ToString();
        tp.text = "Coins:" + Score.ToString();
        if (Input.GetMouseButtonDown(0)) 
        {
            if(counter<=87)
            _gameState = GameState.game;
        }
    }

    public void Reset()
    {
        SceneManager.LoadScene(1);
    }
}
