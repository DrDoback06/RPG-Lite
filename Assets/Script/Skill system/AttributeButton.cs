using UnityEngine;
using UnityEngine.UI;

public class AttributeButton : MonoBehaviour
{
    public string attributeName;
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(AddAttributePoint);
    }

    private void AddAttributePoint()
    {
        Character character = CharacterManager.Instance.character;

        if (character.StatPoints > 0)
        {
            character.IncreaseAttribute(attributeName);
            // The IncreaseAttribute method should handle the decrement of StatPoints
            GameController.Instance.UpdateAttributePointsUI();
        }
    }
}
