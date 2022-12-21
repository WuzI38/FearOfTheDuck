using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public const int MAX_HEALTH = 3;
    private int currentHealth;
    public int CurrentHealth {
        get => currentHealth;
    }
    private GameObject lifeContainer;
    private Sprite empty;
    private Sprite full;
    void Awake()
    {
        lifeContainer = GameObject.FindGameObjectWithTag("Life");
        currentHealth = MAX_HEALTH;
        // Load images
        empty = Resources.Load <Sprite>("Images/UI/heart_empty");
        full = Resources.Load <Sprite>("Images/UI/heart");
    }

    public void DisplayStartingHealth() {
        // Set every health container image to full
        int startingHealth = MAX_HEALTH;
        foreach(Transform child in lifeContainer.transform) {
            Sprite currentSprite;
            if(startingHealth > 0) {
                currentSprite = full;
                startingHealth--;
            }
            else {
                currentSprite = empty;
            }
            child.GetComponent<Image>().sprite = currentSprite;
        }
    }

    public void GainHealth(int amount) {
        // Gain health only if you are not on max healthstate
        amount = Mathf.Min(currentHealth + amount, MAX_HEALTH) - currentHealth;
        currentHealth += amount;
        foreach(Transform child in lifeContainer.transform) {
            Sprite spr = child.GetComponent<Image>().sprite;
            if(amount > 0) {
                if(spr == empty) {
                    child.GetComponent<Image>().sprite = full;
                    amount--;
                }          
            }
            else {
                break;
            }
        }
    }

    public void LoseHealth(int amount) {
        // Lose the amount of hp equal to the damage, if currentHealth == 0 kill the player
        amount = Mathf.Min(amount, currentHealth);
        currentHealth -= amount;
        foreach(Transform child in lifeContainer.transform) {
            Sprite spr = child.GetComponent<Image>().sprite;
            if(amount > 0) {
                if(spr == full) {
                    child.GetComponent<Image>().sprite = empty;
                    amount--;
                }          
            }
            else {
                break;
            }
        }
        if(currentHealth == 0) {
            Debug.Log("Ur Dead");
        }
    }
}
