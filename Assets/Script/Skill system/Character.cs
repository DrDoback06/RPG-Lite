using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum CharacterAttributesType
{
    Strength,
    Agility,
    Intelligence,
    Vitality
}

[System.Serializable]
public class Character
{
    public string characterName;
    public string mainClass;
    public string subClass;
    public string sprite;
    public CharacterAttributes attributes;
    public SkillTree mainClassSkillTree;
    public SkillTree subClassSkillTree;

    public int skillPoints;
    public int statPoints;
    public SkillTree skillTree;

    public delegate void AttributesChangedHandler();
    public event AttributesChangedHandler OnAttributesChanged;

    public int experiencePoints;
    public int nextLevelExperiencePoints;

    public delegate void ExperienceGainedHandler(int amount);
    public event ExperienceGainedHandler OnExperienceGained;

    public delegate void LevelUpHandler();
    public event LevelUpHandler OnLevelUp;

    public delegate void ExperienceChangedHandler();
    public event ExperienceChangedHandler OnExperienceChanged;

    public int requiredExperienceForNextLevel { get; private set; }

    [NonSerialized]
    public Slider experienceSlider;


    public int level
    {
        get;
        private set;
    }

    public int SkillPoints
    {
        get { return skillPoints; }
        set { skillPoints = Mathf.Max(value, 0); } // Ensure skill points don't go below 0
    }

    public int StatPoints
    {
        get { return statPoints; }
        set { statPoints = Mathf.Max(value, 0); } // Ensure stat points don't go below 0
    }

    public Character(string characterName, string mainClass, string sprite, SkillTree mainClassSkillTree, CharacterAttributes attributes)
    {
        this.characterName = characterName;
        this.mainClass = mainClass;
        this.sprite = sprite;
        this.mainClassSkillTree = mainClassSkillTree;
        this.subClassSkillTree = null;
        this.attributes = attributes;
        this.level = 1;
        this.StatPoints = 10;
        this.SkillPoints = 1;

        InitializeExperience();
    }

    private void InitializeExperience()
    {
        experiencePoints = 0;
        requiredExperienceForNextLevel = CalculateNextLevelExperiencePoints();
    }


    public void SetExperienceSlider(Slider slider)
    {
        experienceSlider = slider;
        UpdateExperienceSlider();
    }

    private void UpdateExperienceSlider()
    {
        if (experienceSlider != null)
        {
            experienceSlider.value = (float)experiencePoints / nextLevelExperiencePoints;
        }
    }

    public void AddExperience(int amount)
    {
        experiencePoints += amount;

        if (experiencePoints >= nextLevelExperiencePoints)
        {
            LevelUp();
        }

        UpdateExperienceSlider(); // Update the slider when experience is added
        OnExperienceChanged?.Invoke(); // Raise the event
    }

    private const int maxLevel = 100; // Set your desired level cap

    private void LevelUp()
    {
        if (level < maxLevel)
        {
            level++;
            experiencePoints -= nextLevelExperiencePoints;
            nextLevelExperiencePoints = CalculateNextLevelExperiencePoints();
            StatPoints += 5;
            SkillPoints += 1;

            // Trigger the event
            OnLevelUp?.Invoke();

            // Save the character data when leveling up
            GameController.Instance.SaveCharacterSkills();
        }
    }

    public int CalculateScaledExperiencePoints(int baseExperiencePoints, int sourceLevel)
    {
        // Implement your scaling logic here, e.g., based on level difference between the character and the enemy
        int levelDifference = level - sourceLevel;
        float scalingFactor = Mathf.Clamp(1f + 0.1f * levelDifference, 0.5f, 2f);
        return Mathf.RoundToInt(baseExperiencePoints * scalingFactor);
    }
    private int CalculateNextLevelExperiencePoints()
    {
        // Example of a quadratic experience curve
        return (int)(Mathf.Pow(level, 2) * 100);
    }

    public void AssignSubClass(string subClass, SkillTree subClassSkillTree)
    {
        this.subClass = subClass;
        this.subClassSkillTree = subClassSkillTree;
    }

    public List<Skill> GetMainClassSkills()
    {
        return mainClassSkillTree.GetAllSkills();
    }

    public List<Skill> GetSubClassSkills()
    {
        if (subClassSkillTree != null)
        {
            return subClassSkillTree.GetAllSkills();
        }
        return new List<Skill>();
    }

    public bool CanUpgradeSkill(Skill skill)
    {
        // Check if the player has enough skill points to upgrade the skill
        return SkillPoints >= skill.requiredSkillPoints && skill.currentLevel < skill.maxLevel;
    }

    public void UpgradeSkill(Skill skill)
    {
        if (CanUpgradeSkill(skill))
        {
            skill.Upgrade();
            SkillPoints -= skill.requiredSkillPoints;
            // Update the UI accordingly
        }
    }

    public void ChangeAttribute(CharacterAttributesType attribute, int delta)
    {
        attributes.ChangeAttribute(attribute, delta);
        OnAttributesChanged?.Invoke(); // Raise the event
    }

    public void IncreaseAttribute(string attributeName)
    {
        if (StatPoints <= 0) return;

        switch (attributeName)
        {
            case "Strength":
                attributes.strength++;
                break;
            case "Agility":
                attributes.agility++;
                break;
            case "Intelligence":
                attributes.intelligence++;
                break;
            case "Vitality":
                attributes.vitality++;
                break;
            default:
                Debug.LogWarning("Invalid attribute name: " + attributeName);
                break;
        }

        StatPoints--;
        // Invoke the OnAttributesChanged event to update the UI
        OnAttributesChanged?.Invoke();
    }


    public void SaveSkills()
    {
        string mainClassSkillsJson = JsonUtility.ToJson(mainClassSkillTree);
        string subClassSkillsJson = JsonUtility.ToJson(subClassSkillTree);

        PlayerPrefs.SetString("MainClassSkills", mainClassSkillsJson);
        PlayerPrefs.SetString("SubClassSkills", subClassSkillsJson);

        // Save experience data
        PlayerPrefs.SetInt("ExperiencePoints", experiencePoints);
        PlayerPrefs.SetInt("NextLevelExperiencePoints", nextLevelExperiencePoints);
        PlayerPrefs.SetInt("CharacterLevel", level);

        PlayerPrefs.Save();
    }

    public void LoadSkills()
    {
        if (PlayerPrefs.HasKey("MainClassSkills") && PlayerPrefs.HasKey("SubClassSkills"))
        {
            string mainClassSkillsJson = PlayerPrefs.GetString("MainClassSkills");
            string subClassSkillsJson = PlayerPrefs.GetString("SubClassSkills");

            mainClassSkillTree = JsonUtility.FromJson<SkillTree>(mainClassSkillsJson);
            subClassSkillTree = JsonUtility.FromJson<SkillTree>(subClassSkillsJson);

            // Load experience data
            experiencePoints = PlayerPrefs.GetInt("ExperiencePoints");
            nextLevelExperiencePoints = PlayerPrefs.GetInt("NextLevelExperiencePoints");
            level = PlayerPrefs.GetInt("CharacterLevel");

            UpdateExperienceSlider(); // Update the slider when loading skills
        }
    }
}
