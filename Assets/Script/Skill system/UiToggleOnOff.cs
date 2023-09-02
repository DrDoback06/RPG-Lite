using UnityEngine;

public class UiToggleOnOff : MonoBehaviour
{
    [SerializeField] private GameObject skillTreeUI;
    [SerializeField] private KeyCode toggleKey = KeyCode.P;

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleSkillTreeUI();
        }
    }

    public void ToggleSkillTreeUI()
    {
        skillTreeUI.SetActive(!skillTreeUI.activeSelf);
    }
}
