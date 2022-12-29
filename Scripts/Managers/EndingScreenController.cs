using UnityEngine;
using UnityEngine.UI;

public class EndingScreenController : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;
    private Text txt;
    private Image img;
    void Start() {
        // At the beggining of the game hide ending screens
        GameManager.Instance.OnGameStateChanged += OnGameStateChanged;
        txt = canvas.GetComponentInChildren<Text>();
        // Find the image of ending screen (not a life container)
        foreach(Image i in canvas.GetComponentsInChildren<Image>()) {
            if(i.transform.parent.tag != "Life") img = i;
        }
        txt.enabled = false;
        // Enabled doesn't work with image, you have to disable the whole image component
        img.gameObject.SetActive(false);
    }

    private void OnGameStateChanged(GameState newGameState) {
        // If player dies or completes the objective and reaches exit show appropriate ending screen
        if(newGameState == GameState.Death) {
            img.gameObject.SetActive(true);
            txt.enabled = true;
            txt.text = "You Died";
        }
        if(newGameState == GameState.Finish) {
            img.gameObject.SetActive(true);
            txt.enabled = true;
            txt.text = "You Win";
        }
    }
}
