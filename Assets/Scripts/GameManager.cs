using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class GameManager : MonoBehaviour
{
    public static GameManager Instamce;
    public enum GameState {pregame, game ,finish}
    public GameState _gameState;
    public TextMeshProUGUI tp;
    private void Awake()
    {
        Instamce = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        _gameState = GameState.pregame; 
    }

    // Update is called once per frame
    void Update()
    {
        tp.text = CharacterMovement.instance.state2.ToString() + ",,," +CharacterMovement.instance.pm.dynamicFriction.ToString();
        if (Input.GetMouseButtonDown(0)) 
        {
            _gameState = GameState.game;
        }
    }
}
