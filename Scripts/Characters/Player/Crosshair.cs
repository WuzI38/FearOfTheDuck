using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private Vector2 crosshairPosition;
    public Vector2 Position {
        get => crosshairPosition;
    }
    private SpriteRenderer spriteRend;
    private Vector2 playerPos;
    Vector2 screenSize;
    void Awake() {
        Cursor.visible = false;
        spriteRend = this.GetComponentInChildren<SpriteRenderer>();
        screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    void Update() {
        playerPos = transform.parent.position;
        // Get the mouse position
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        crosshairPosition = mousePosition;
        // Prevent the crosshair from leaving the screen 
        float minX = playerPos.x - screenSize.x + spriteRend.size.x / 2;
        float maxX = playerPos.x + screenSize.x - spriteRend.size.x / 2;
        float minY = playerPos.y - screenSize.y + spriteRend.size.y / 2;
        float maxY = playerPos.y + screenSize.y - spriteRend.size.y / 2;
        if(mousePosition.x < minX) crosshairPosition.x = minX;
        if(mousePosition.x > maxX) crosshairPosition.x = maxX;
        if(mousePosition.y < minY) crosshairPosition.y = minY;
        if(mousePosition.y > maxY) crosshairPosition.y = maxY;
        /*Debug.Log("Screen pos: " + screenSize);
        Debug.Log("Mouse pos: " + mousePosition);
        Debug.Log("Player pos: " + playerPos);*/
        // Set the crosshair's position to camera position
        transform.position = crosshairPosition;
    }
}