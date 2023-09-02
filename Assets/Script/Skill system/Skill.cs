using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
    public string name;
    public string description;
    public SkillType skillType;
    public int currentLevel;
    public int? maxLevel; // Change this to a nullable int
    public int requiredSkillPoints;
    public Sprite icon;
    public int requiredLevel;
    public int cooldown;

    public bool IsUpgradable
    {
        get
        {
            return CanLevelUp(); // Use the CanLevelUp method instead of directly comparing the levels
        }
    }

    public Skill(string name, string description, SkillType skilltype, int currentLevel, int? maxLevel, int requiredSkillPoints, Sprite icon, int requiredLevel, int cooldown)
    {
        this.name = name;
        this.description = description;
        this.skillType = skilltype;
        this.currentLevel = currentLevel;
        this.maxLevel = maxLevel;
        this.requiredSkillPoints = requiredSkillPoints;
        this.icon = icon;
        this.requiredLevel = requiredLevel;
        this.cooldown = cooldown;
    }

    public enum SkillType
    {
        Active,
        Passive,
        Ultimate
    }

    public void Upgrade()
    {
        if (CanLevelUp())
        {
            currentLevel++;
        }
    }

    public string GetStatsText()
    {
        string statsText = "";

        // Add code here to generate a string representation of the skill's base stats.
        // For example:
        // statsText = $"Damage: {baseDamage} \nCooldown: {baseCooldown}s";

        return statsText;
    }

    // Add this method to handle the nullable maxLevel property
    public bool CanLevelUp()
    {
        if (!maxLevel.HasValue || currentLevel < maxLevel.Value)
        {
            return true;
        }
        return false;
    }
}
