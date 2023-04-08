using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeemsManager : MonoBehaviour
{
    [SerializeField] private List<Color> possibleColorTemm = new();
    private List<Color> accessColor = new();
    private Dictionary<GameObject, Color> characterColors = new();
    private Dictionary<Color, List<GameObject>> teems = new();
    private static TeemsManager instance;
    public Dictionary<GameObject, Color> CharacterColors => characterColors;
    public Dictionary<Color, List<GameObject>> Teems => teems;
    public static TeemsManager Instance => instance;

    private void Awake()
    {
        instance = this;
        accessColor = possibleColorTemm;
    }
    public List<GameObject> GetEnemy(GameObject character)
    {
        Color colorCharacter = characterColors[character];
        List<GameObject> enemy = new();

        foreach (var item in teems)
        {
            if (item.Key != characterColors[character])
            {
                enemy.AddRange(item.Value);
            }
        }
        return enemy;
    }
    public List<GameObject> GetAllies(GameObject character)
    {
        return teems[characterColors[character]];
    }
    public Color AddCharacterToTeem(GameObject character)
    {
        var color = accessColor[Random.Range(0, accessColor.Count)];
        accessColor.Remove(color);
        characterColors.Add(character,color);
        teems.Add(color, new List<GameObject>());
        teems[color].Add(character);
        return color;
    }
    public void AddCharacterToTeem(GameObject character, Color color)
    {
        if (!teems.ContainsKey(color))
        {
            teems.Add(color, new List<GameObject>());
            accessColor.Remove(color);
        }
        teems[color].Add(character);
        characterColors.Add(character, color);
    }
}
