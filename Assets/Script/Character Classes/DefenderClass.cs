using System.Collections.Generic;
using UnityEngine;

public class DefenderClass : MonoBehaviour
{
    public static Sprite shieldBashIcon = Resources.Load<Sprite>("SkillIcons/shieldBashIcon");
    public static Sprite toughSkinIcon = Resources.Load<Sprite>("SkillIcons/toughSkinIcon");
    public static Sprite shieldWallIcon = Resources.Load<Sprite>("SkillIcons/shieldWallIcon");
    public static Sprite resilienceIcon = Resources.Load<Sprite>("SkillIcons/resilienceIcon");
    public static Sprite earthquakeIcon = Resources.Load<Sprite>("SkillIcons/earthquakeIcon");

    public static Character CreateDefender(string characterName)
    {
        SkillTree defenderSkillTree = new SkillTree();
        defenderSkillTree.AddSkill(new Skill("Shield Bash", "Bash the enemy with your shield, dealing damage and stunning them.", Skill.SkillType.Active, 1, null, 3, shieldBashIcon, 1, 0));
        defenderSkillTree.AddSkill(new Skill("Tough Skin", "Increase armor.", Skill.SkillType.Passive, 1, null, 5, toughSkinIcon, 5, 20));
        defenderSkillTree.AddSkill(new Skill("Shield Wall", "Raise your shield, blocking all incoming damage for a short time.", Skill.SkillType.Active, 1, null, 5, shieldWallIcon, 10, 60));
        defenderSkillTree.AddSkill(new Skill("Resilience", "Increase maximum health.", Skill.SkillType.Passive, 1, null, 5, resilienceIcon, 15, 90));
        defenderSkillTree.AddSkill(new Skill("Earthquake", "Slam the ground, creating a shockwave that damages and knocks down all nearby enemies.", Skill.SkillType.Ultimate, 1, null, 3, earthquakeIcon, 20, 180));

        CharacterAttributes defenderAttributes = new CharacterAttributes(120, 40, 8, 8);

        return new Character(characterName, "Defender", "defender_sprite", defenderSkillTree, defenderAttributes);
    }
}
