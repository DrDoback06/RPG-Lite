using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterCreation : MonoBehaviour
{
    public Button createCharacterButton;

    void Start()
    {
        createCharacterButton.onClick.AddListener(CreateRandomCharacter);
    }

    void CreateRandomCharacter()
    {
        Debug.Log("Create Random Character button clicked.");

        string characterName = "John Doe";
        int randomMainClassIndex;
        int randomSubClassIndex;

        // Ensure that the main class and subclass are different
        do
        {
            randomMainClassIndex = UnityEngine.Random.Range(0, 10); // There are 10 classes
            randomSubClassIndex = UnityEngine.Random.Range(0, 10);
        } while (randomMainClassIndex == randomSubClassIndex);

        Character character = CreateCharacterWithMainAndSubClass(characterName, randomMainClassIndex, randomSubClassIndex);

        CharacterManager.Instance.character = character;

        SceneManager.LoadScene("Town");
    }

    Character CreateCharacterWithMainAndSubClass(string characterName, int mainClassIndex, int subclassIndex)
    {
        Character character;

        // Main class creation
        switch (mainClassIndex)
        {
            case 0:
                character = BlademasterClass.CreateBlademaster(characterName);
                break;
            case 1:
                character = DefenderClass.CreateDefender(characterName);
                break;
            // Add cases for the other main classes here
            // ...
            default:
                character = BlademasterClass.CreateBlademaster(characterName);
                break;
        }

        // Subclass assignment
        SkillTree subClassSkillTree;
        switch (subclassIndex)
        {
            case 0:
                subClassSkillTree = BlademasterClass.CreateBlademaster(characterName).mainClassSkillTree;
                character.AssignSubClass("Blademaster", subClassSkillTree);
                break;
            case 1:
                subClassSkillTree = DefenderClass.CreateDefender(characterName).mainClassSkillTree;
                character.AssignSubClass("Defender", subClassSkillTree);
                break;
            // Add cases for the other subclasses here
            // ...
            default:
                subClassSkillTree = DefenderClass.CreateDefender(characterName).mainClassSkillTree;
                character.AssignSubClass("Defender", subClassSkillTree);
                break;
        }

        return character;
    }

    private void OnDestroy()
    {
        if (createCharacterButton != null)
        {
            createCharacterButton.onClick.RemoveListener(CreateRandomCharacter);
        }
    }
}
