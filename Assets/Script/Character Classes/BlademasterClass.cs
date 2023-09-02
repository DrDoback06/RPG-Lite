using System.Collections.Generic;
using UnityEngine;

public class BlademasterClass : MonoBehaviour
{
    public static Sprite slashIcon = Resources.Load<Sprite>("SkillIcons/slashIcon");
    public static Sprite spinAttackIcon = Resources.Load<Sprite>("SkillIcons/spinAttackIcon");
    public static Sprite focusIcon = Resources.Load<Sprite>("SkillIcons/focusIcon");
    public static Sprite parryIcon = Resources.Load<Sprite>("SkillIcons/parryIcon");
    public static Sprite ultimateBladeIcon = Resources.Load<Sprite>("SkillIcons/ultimateBladeIcon");

    public static Character CreateBlademaster(string characterName)
    {
        SkillTree blademasterSkillTree = new SkillTree();
        blademasterSkillTree.AddSkill(new Skill("Slash", "A quick sword slash.", Skill.SkillType.Active, 1, null, 5, slashIcon, 1, 0));
        blademasterSkillTree.AddSkill(new Skill("Spin Attack", "Spins around, hitting all nearby enemies.", Skill.SkillType.Active, 1, null, 5, spinAttackIcon, 5, 20));
        blademasterSkillTree.AddSkill(new Skill("Focus", "Increases attack speed temporarily.", Skill.SkillType.Passive, 1, null, 5, focusIcon, 10, 30));
        blademasterSkillTree.AddSkill(new Skill("Parry", "Block an incoming attack and counterattack.", Skill.SkillType.Active, 1, null, 5, parryIcon, 15, 0));
        blademasterSkillTree.AddSkill(new Skill("Ultimate Blade", "Unleash a powerful sword attack that deals massive damage.", Skill.SkillType.Ultimate, 1, null, 3, ultimateBladeIcon, 20, 60));

        CharacterAttributes blademasterAttributes = new CharacterAttributes(100, 50, 10, 5);

        return new Character(characterName, "Blademaster", "blademaster_sprite", blademasterSkillTree, blademasterAttributes);
    }
}