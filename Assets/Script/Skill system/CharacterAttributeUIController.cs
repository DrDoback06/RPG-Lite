using UnityEngine;
using UnityEngine.UI;

public class CharacterAttributesUIController : MonoBehaviour
{
    public Character character;
    public Text strengthText;
    public Text agilityText;
    public Text intelligenceText;
    public Text vitalityText;

    private void Start()
    {
        UpdateCharacterAttributesUI();
        character.OnAttributesChanged += UpdateCharacterAttributesUI; // Subscribe to the event
    }

    public void UpdateCharacterAttributesUI()
    {
        strengthText.text = "Strength: " + character.attributes.strength;
        agilityText.text = "Agility: " + character.attributes.agility;
        intelligenceText.text = "Intelligence: " + character.attributes.intelligence;
        vitalityText.text = "Vitality: " + character.attributes.vitality;
    }

    private void OnDestroy()
    {
        character.OnAttributesChanged -= UpdateCharacterAttributesUI; // Unsubscribe from the event
    }
}


