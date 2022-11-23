using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    public GameState state {get; private set;} // Store game's global state
    
    protected override void Awake() { // Currently set opening state to generation, then running
        base.Awake();
        ChangeState(GameState.Generation);
    }

    public void ChangeState(GameState newState) { 
        OnBeforeStateChanged?.Invoke(newState);
        
        switch(newState) {
            case GameState.Generation: // (not implemented)
                // Generating a new level
                break;
            case GameState.SpawnHero: // (not implemented)
                // Spawn hero, give him starting equipment, set base stats
                break;
            case GameState.Running: // (not implemented)
                // The game is running 
                break;
            case GameState.EnterRoom: // (not implemented)
                // Spawn enemies, save the number of enemies to kill before leaving the room, close the exits
                break;
            case GameState.ClearRoom: // (not implemented)
                // Open the exits, give player reward (optional)
                break;
            case GameState.Pause: // (not implemented)
                // Pause the game
                break;
            case GameState.Death: // (not implemented)
                // Show ending screen, return to menu
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke(newState);
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
}
