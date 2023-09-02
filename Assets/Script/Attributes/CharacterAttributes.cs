using System;

[System.Serializable]
public class CharacterAttributes
{
    public int strength;
    public int agility;
    public int intelligence;
    public int vitality;

    public event Action OnAttributesChanged;

    public CharacterAttributes(int strength, int agility, int intelligence, int vitality)
    {
        this.strength = strength;
        this.agility = agility;
        this.intelligence = intelligence;
        this.vitality = vitality;
    }

    public void ChangeAttribute(CharacterAttributesType attribute, int delta)
    {
        switch (attribute)
        {
            case CharacterAttributesType.Strength:
                strength += delta;
                break;
            case CharacterAttributesType.Agility:
                agility += delta;
                break;
            case CharacterAttributesType.Intelligence:
                intelligence += delta;
                break;
            case CharacterAttributesType.Vitality:
                vitality += delta;
                break;
            default:
                break;
        }

        OnAttributesChanged?.Invoke();
    }
}
