using System.Collections.Generic;

[System.Serializable]
public class SkillTree
{
    public List<Skill> skills;

    public SkillTree()
    {
        skills = new List<Skill>();
    }

    public void AddSkill(Skill skill)
    {
        skills.Add(skill);
    }

    public List<Skill> GetAllSkills()
    {
        return skills;
    }
}
