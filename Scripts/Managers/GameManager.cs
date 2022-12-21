using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState state {get; private set;} // Store game's global state
    public delegate void GameStateChangeHandler(GameState newGameState);
    public event GameStateChangeHandler OnGameStateChanged;

    protected override void Awake() { // Currently set opening state to generation, then running
        base.Awake();
        state = GameState.None;
        GameManager.Instance.ChangeState(GameState.Generation);
    }

    public void ChangeState(GameState newState) { 
        if(newState == state) return;

        state = newState;
        OnGameStateChanged?.Invoke(newState);
        
        switch(newState) {
            case GameState.Generation: // (not implemented)
                break;
            case GameState.SpawnHero: // (not implemented)
                // Spawn hero, give him starting equipment, set base stats
                break;
            case GameState.Running: // (not implemented)
                // The game is running 
                Time.timeScale = 1;
                break;
            case GameState.EnterRoom: // (not implemented)
                // Spawn enemies, save the number of enemies to kill before leaving the room, close the exits
                break;
            case GameState.ClearRoom: // (not implemented)
                // Open the exits, give player reward (optional)
                break;
            case GameState.Pause: // (not implemented)
                // The game is paused
                Time.timeScale = 0;
                break;
            case GameState.Death: // (not implemented)
                // Show ending screen, return to menu
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}

public enum GameState {
    Generation = 0,
    SpawnHero = 1,
    Running = 2,
    EnterRoom = 3,
    ClearRoom = 4,
    Pause = 5,
    Death = 6,
    None = 7 // Empty state
}

/*GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
private void OnGameStateChanged(GameState newGameState) {
        enabled = newGameState == GameState.Running;
    }*/
