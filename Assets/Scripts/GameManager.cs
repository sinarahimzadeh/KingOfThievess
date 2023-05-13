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
    public TextMeshProUGUI tp,tpTimer;
    public float originalSpeed;
    public int Score,timer;
    public float counter;
    [SerializeField] private GameObject panel;
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
        timer = Convert.ToInt32(counter);
       //  tp.text = CharacterMovement.instance.state2.ToString();
        tp.text = "Coins:" + Score.ToString();
        tpTimer.text = timer.ToString();
        if (Input.GetMouseButtonDown(0)) 
        {
            if(counter<=87)
            _gameState = GameState.game;
        }
        if (_gameState == GameState.finish)     
        {
            Invoke("Die",1);
            
        }
        if (timer == 0) { Invoke("Finish", 3); }
    }
    void Die() 
    {
        panel.SetActive(true);
        Invoke("Finish", 2);
    }
    public void Reset()
    {
        SceneManager.LoadScene(1);
    }
    public void Finish()
    {
        SceneManager.LoadScene(0);

    }
}
