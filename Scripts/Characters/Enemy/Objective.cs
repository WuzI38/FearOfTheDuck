using UnityEngine;

// Class used to overlook the number of remaining enemies + the number of enemies remaining in the room
public static class Objective {
    private static int maxEnemiesInRoom = 7;
    private static int minEnemiesInRoom = 5;
    private static int enemiesInRoom = 0;
    private static int enemiesToBeat = 30;
    private static bool completed = false;
    private static bool stateEnter = true; // Used to prevent the game from double state change

    public static bool Completed {
        get => completed;
    }
    
    // Spawn a random number of enemies in a room
    public static int GetNumberOfEnemiesToSpawn() {
        stateEnter = false;
        enemiesInRoom = Random.Range(minEnemiesInRoom, maxEnemiesInRoom);
        return enemiesInRoom;
    }

    // If enemy is killed check if the objective is completed and if room is cleared
    public static void EnemyKilled() {
        enemiesToBeat -= 1;
        if(enemiesInRoom > 1) enemiesInRoom -= 1;
        else {
            // If the last enemy was killed enable leaving the room
            enemiesInRoom -= 1;
            if(!stateEnter) {
                GameState state = GameManager.Instance.state;
                GameManager.Instance.ChangeState(GameState.ClearRoom);
                stateEnter = true;
            }
        }
        if(enemiesToBeat <= 0) {
            GameState state = GameManager.Instance.state;
            GameManager.Instance.ChangeState(GameState.Completed);
            completed = true; 
        }
        /*Debug.Log("Enemies remaining " + enemiesToBeat);
        Debug.Log("Enemies in room " + enemiesInRoom);*/
    }
}
