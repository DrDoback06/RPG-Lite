using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SkillButtonUI : MonoBehaviour
{
    public Image skillIcon;
    public TMP_Text skillLevel;
    public Button upgradeButton;

    private Skill skill;
    private SkillTreeUIController skillTreeUIController;
    private SkillDetailsToggle skillDetailsToggle;

    public void Initialize(Skill skill, SkillTreeUIController skillTreeUIController, SkillDetailsToggle skillDetailsToggle)
    {
        this.skill = skill;
        this.skillTreeUIController = skillTreeUIController;
        this.skillDetailsToggle = skillDetailsToggle;
        skillTreeUIController.OnSkillPointsChanged += UpdateUI;

        UpdateUI();
    }

    public void OnUpgradeButtonClick()
    {
        skillTreeUIController.OnSkillButtonUpgrade(skill);
        UpdateUI();
    }

    public void UpdateUI()
    {
        // Load the sprite dynamically from the "Resources/skillicons" folder
        Sprite icon = Resources.Load<Sprite>("skillicons/" + skill.name.ToLower());
        if (icon != null)
        {
            skillIcon.sprite = icon;
        }
        else
        {
            Debug.LogError("Icon not found for skill: " + skill.name);
        }

        skillLevel.text = skill.currentLevel.ToString() + " / " + skill.maxLevel.ToString();
        upgradeButton.interactable = skill.IsUpgradable;
    }

    private void Start()
    {
        // Add event triggers for pointer enter and exit
        EventTrigger trigger = gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry pointerEnterEntry = new EventTrigger.Entry();
        pointerEnterEntry.eventID = EventTriggerType.PointerEnter;
        pointerEnterEntry.callback.AddListener((eventData) => { skillDetailsToggle.ShowSkillDetails(skill); });
        trigger.triggers.Add(pointerEnterEntry);

        EventTrigger.Entry pointerExitEntry = new EventTrigger.Entry();
        pointerExitEntry.eventID = EventTriggerType.PointerExit;
        pointerExitEntry.callback.AddListener((eventData) => { skillDetailsToggle.HideSkillDetails(); });
        trigger.triggers.Add(pointerExitEntry);
    }

    private void OnDestroy()
    {
        if (skillTreeUIController != null)
        {
            skillTreeUIController.OnSkillPointsChanged -= UpdateUI;
        }
    }
}
