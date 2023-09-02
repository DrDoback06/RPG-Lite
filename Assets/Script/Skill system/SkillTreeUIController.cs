using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillTreeUIController : MonoBehaviour
{
    public GameObject skillButtonPrefab;
    public Transform skillButtonContainer;

    public GameObject skillButtonsPanel;
    public SkillDetailsToggle skillDetailsToggle;

    public TMP_Text strengthText;
    public TMP_Text agilityText;
    public TMP_Text intelligenceText;
    public TMP_Text vitalityText;

    private SkillTree skillTree;
    private int _skillPoints;
    private Character character;

    public event Action OnSkillPointsChanged;

    public int skillPoints
    {
        get { return _skillPoints; }
        private set { _skillPoints = Mathf.Max(value, 0); } // Ensure skill points don't go below 0
    }

    public void InitializeFromCharacter(Character character)
    {
        this.character = character;
        PopulateSkillButtons(character.mainClassSkillTree);
        PopulateSkillButtons(character.subClassSkillTree);
        UpdateCharacterAttributesUI();

        character.OnAttributesChanged += UpdateCharacterAttributesUI; // Subscribe to the event
    }

    public void PopulateSkillButtons(SkillTree skillTree)
    {
        List<Skill> allSkills = skillTree.GetAllSkills();

        foreach (Skill skill in allSkills)
        {
            GameObject button = Instantiate(skillButtonPrefab, skillButtonContainer);
            SkillButtonUI skillButtonUI = button.GetComponent<SkillButtonUI>();
            skillButtonUI.Initialize(skill, this, GetComponentInChildren<SkillDetailsToggle>());
        }
    }

    public void AddSubClassSkills(SkillTree subClassSkillTree)
    {
        foreach (Skill skill in subClassSkillTree.skills)
        {
            GameObject skillButton = Instantiate(skillButtonPrefab, skillButtonsPanel.transform);
            SkillButtonUI skillButtonUI = skillButton.GetComponent<SkillButtonUI>();
            skillButtonUI.Initialize(skill, this, skillDetailsToggle);
        }
    }

    public void AwardSkillPoints(int points)
    {
        skillPoints += points;
        OnSkillPointsChanged?.Invoke(); // Raise the event
    }

    public void OnSkillButtonUpgrade(Skill skill)
    {
        if (skillPoints >= skill.requiredSkillPoints && skill.currentLevel < skill.maxLevel)
        {
            skill.Upgrade();
            AwardSkillPoints(-skill.requiredSkillPoints);
        }
    }

    public void UpdateCharacterAttributesUI()
    {
        strengthText.text = "" + character.attributes.strength;
        agilityText.text = "" + character.attributes.agility;
        intelligenceText.text = "" + character.attributes.intelligence;
        vitalityText.text = "" + character.attributes.vitality;
    }
}
