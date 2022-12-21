using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    private int startingHealth;
    private int currentHealth;
    public int CurrentHealth {
        get => currentHealth;
    }
    private GameObject lifeContainer;
    private Sprite empty;
    private Sprite full;
    public int StartingHealth {
        get => startingHealth;
        set {
            if(startingHealth < 0 && value > 0) {
                startingHealth = value;
            }
        }
    }
    void Awake()
    {
        startingHealth = -1;
        lifeContainer = GameObject.FindGameObjectWithTag("Life");
        // Load images
        empty = Resources.Load <Sprite>("Images/UI/heart_empty");
        full = Resources.Load <Sprite>("Images/UI/heart");
    }

    public void SetStartingHealth(int amount) {
        startingHealth = amount;
        currentHealth = startingHealth;
        // Set every health container image to full
        foreach(Transform child in lifeContainer.transform) {
            Sprite currentSprite;
            if(amount > 0) {
                currentSprite = full;
                amount--;
            }
            else {
                currentSprite = empty;
            }
            child.GetComponent<Image>().sprite = currentSprite;
        }
    }

    public void GainHealth(int amount) {
        // Gain health only if you 
        amount = Mathf.Min(currentHealth + amount, startingHealth) - currentHealth;
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
    }
}
