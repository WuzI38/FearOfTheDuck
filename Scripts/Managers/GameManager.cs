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
            case GameState.Generation:
            // Generate the whole dungeon and spawn player
                AbstractDungeonGenerator generator = GameObject.FindGameObjectWithTag("Generator").GetComponent<AbstractDungeonGenerator>();
                generator.GenerateDungeon();
                GameManager.Instance.ChangeState(GameState.Running);
                break;
            case GameState.Running:
                Time.timeScale = 1;
                break;
            case GameState.EnterRoom: 
                // After entering the room spimply raise the spikes, spawn enemies (enemy spawner script) and come back to Game.Running
                GameManager.Instance.ChangeState(GameState.Running);
                break;
            case GameState.ClearRoom: 
                // After the room was cleared and all of the enemies were killed spimply lower the spikes and come back to Game.Running
                GameManager.Instance.ChangeState(GameState.Running);
                break;
            case GameState.Pause: // The game is paused
                Time.timeScale = 0;
                break;
            case GameState.Finish: // Player won the game
                Time.timeScale = 0;
                break;
            case GameState.Death: // Player died
                Time.timeScale = 0;
                break;
            case GameState.Completed: 
                // Not implemented the objective was completed, so the exit must be opened
                GameManager.Instance.ChangeState(GameState.Running);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }
}

public enum GameState {
    Generation,
    Running,
    EnterRoom,
    ClearRoom,
    Pause,
    Death,
    Finish,
    Completed,
    None // Empty state
}
