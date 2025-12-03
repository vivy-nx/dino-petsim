using UnityEngine;
using TMPro;  // If you're using TextMeshPro

public class UIManager : MonoBehaviour
{
    public Dinosaur selectedDinosaur;          // reference to the selected dinosaur
    public TextMeshProUGUI healthText;         // reference to the health text box
    public TextMeshProUGUI happinessText;      // reference to the happiness text box
    public TextMeshProUGUI hungerText;         // reference to the hunger text box
    public TextMeshProUGUI energyText;         // reference to the energy text box

    void Update()
    {
        UpdateDinosaurStatsUI();
    }

    // method to update the dinosaur's stats on the UI
    private void UpdateDinosaurStatsUI()
    {
        if (selectedDinosaur != null)
        {
            // update each stat text box with the current values, rounded to the nearest whole number
            healthText.text = "Health: " + Mathf.Round(selectedDinosaur.health).ToString();
            happinessText.text = "Happiness: " + Mathf.Round(selectedDinosaur.happiness).ToString();
            hungerText.text = "Hunger: " + Mathf.Round(selectedDinosaur.hunger).ToString();
            energyText.text = "Energy: " + Mathf.Round(selectedDinosaur.energy).ToString();

        }
    }
}
