using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image skillIcon;
    public Text skillNameText;
    public Text skillDescription;
    public Text skillLevel;

    public Character character;
    private Skill skill;

    public SkillDetailsToggle skillDetailsToggle;

    public void Initialize(Skill skill)
    {
        this.skill = skill;
        skillIcon.sprite = skill.icon;
        skillNameText.text = skill.name;
        skillDescription.text = skill.description;

        // Update the skillLevel text without the slash
        skillLevel.text = $"Level {skill.currentLevel}";
    }


    public void UpgradeSkill()
    {
        if (character.CanUpgradeSkill(skill))
        {
            skill.Upgrade();
            skillLevel.text = $"{skill.currentLevel}";
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (skillDetailsToggle != null && skill != null)
        {
            skillDetailsToggle.ShowSkillDetails(skill);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (skillDetailsToggle != null)
        {
            skillDetailsToggle.HideSkillDetails();
        }
    }
}
