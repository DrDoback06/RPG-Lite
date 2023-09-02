using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public GameObject[] mainClassSkillButtons;
    public GameObject[] subClassSkillButtons;
    public GameObject enemyPrefab;

    [SerializeField] private TMP_Text experienceText;
    [SerializeField] private TMP_Text skillPointsText;
    [SerializeField] private TMP_Text attributePointsText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Slider experienceBar;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Character character = CharacterManager.Instance.character;
        CharacterManager.Instance.character.OnLevelUp += UpdateLevelText; //updates skills to skills tree
        CharacterManager.Instance.character.OnAttributesChanged += UpdateAttributePointsUI; // updates attributes to skill tree

        // Update the level display initially
        UpdateLevelText();
        UpdateSkillPointsUI();
        UpdateAttributePointsUI();
        UpdateExperienceUI();
        UpdateExperienceBar();


        character.LoadSkills();

        List<Skill> mainClassSkills = character.GetMainClassSkills();
        List<Skill> subClassSkills = character.GetSubClassSkills();

        Debug.Log("Main class skills count: " + mainClassSkills.Count);
        Debug.Log("Sub class skills count: " + subClassSkills.Count);
        Debug.Log("Main class skill buttons length: " + mainClassSkillButtons.Length);
        Debug.Log("Sub class skill buttons length: " + subClassSkillButtons.Length);

        for (int i = 5; i < mainClassSkills.Count; i++)
        {
            SkillButton skillButton = mainClassSkillButtons[i].GetComponent<SkillButton>();
            skillButton.character = character;
            skillButton.Initialize(mainClassSkills[i]);
        }

        int subClassSkillButtonsLength = Mathf.Min(subClassSkills.Count, subClassSkillButtons.Length);

        for (int i = 5; i < subClassSkillButtonsLength; i++)
        {
            SkillButton skillButton = subClassSkillButtons[i].GetComponent<SkillButton>();
            skillButton.character = character;
            skillButton.Initialize(subClassSkills[i]);
        }
    }

    public void UpdateSkillPointsUI()
    {
        skillPointsText.text = "Skill Points: " + CharacterManager.Instance.character.skillPoints;
    }

    public void UpdateAttributePointsUI()
    {
        attributePointsText.text = "Attribute Points: " + CharacterManager.Instance.character.StatPoints;
    }


    public void SaveCharacterSkills()
    {
        CharacterManager.Instance.character.SaveSkills();
    }

    private void SpawnEnemy()
    {
        GameObject enemyInstance = Instantiate(enemyPrefab);
        EnemyController enemyController = enemyInstance.GetComponent<EnemyController>();

        // Subscribe to the OnEnemyDefeated event
        enemyController.OnEnemyDefeated += HandleEnemyDefeated;
    }

    private void HandleEnemyDefeated(int experienceValue)
    {
        // Grant the experience points to the player's character
        CharacterManager.Instance.character.AddExperience(experienceValue);

        // Update the UI
        UpdateExperienceUI();
        UpdateExperienceBar();
    }

    public void UpdateExperienceUI()
    {
        experienceText.text = "Experience: " + CharacterManager.Instance.character.experiencePoints;
    }
    public void UpdateExperienceBar()
    {
        Character character = CharacterManager.Instance.character;
        float currentExperience = character.experiencePoints;
        float requiredExperience = character.requiredExperienceForNextLevel; // Assuming you have this value in the Character script

        experienceBar.value = currentExperience / requiredExperience;
    }

    private void UpdateLevelText()
    {
        levelText.text = "Level: " + CharacterManager.Instance.character.level;
    }

    private void OnDestroy()
    {
        CharacterManager.Instance.character.OnLevelUp -= UpdateLevelText;
        CharacterManager.Instance.character.OnAttributesChanged -= UpdateAttributePointsUI;
    }

}

