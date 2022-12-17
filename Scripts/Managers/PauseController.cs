using UnityEngine;

public class PauseController : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            GameState state = GameManager.Instance.state;
            GameState newState = state != GameState.Pause
            ? GameState.Pause
            : GameState.Running;

            GameManager.Instance.ChangeState(newState);
        }
    }
}
