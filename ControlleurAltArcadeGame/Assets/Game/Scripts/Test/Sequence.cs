using System.Collections.Generic;
using UnityEngine;

public class Sequence
{
    public int sequenceLength;
    public float sequenceTime = 3f;
    private List<Cord> cords;
    private int currentInputIndex = 0;

    public Sequence(int length, bool includeHold)
    {
        sequenceLength = length;
        cords = new List<Cord>();
        for (int i = 0; i < length; i++)
        {
            bool hold = includeHold && Random.value < 0.5f;
            cords.Add(new Cord(RandomColor(), hold));
        }
    }

    private Color RandomColor()
    {
        Color[] colors = { Color.red, Color.blue, Color.green, Color.yellow };
        return colors[Random.Range(0, colors.Length)];
    }

    public string GetSequenceDisplay()
    {
        string s = "";
        foreach (Cord c in cords)
        {
            s += c.display + " ";
        }
        return s.Trim();
    }

    public void Input(Color color, bool hold = false)
    {
        if (currentInputIndex >= cords.Count) return;

        if (cords[currentInputIndex].Matches(color, hold))
        {
            currentInputIndex++;
        }
        else
        {
            currentInputIndex = cords.Count; // séquence échouée
        }
    }

    public bool IsCompleted() => currentInputIndex >= cords.Count;

    public bool IsAnyInput() => currentInputIndex > 0;

    public float ReactionMultiplier()
    {
        return 1f; // ici tu peux ajouter un facteur basé sur la rapidité
    }
}

public class Cord
{
    public Color color;
    public bool isHold;
    public string display;

    public Cord(Color c, bool hold)
    {
        color = c;
        isHold = hold;
        display = hold ? "[●]" : "●";
    }

    public bool Matches(Color inputColor, bool holdInput)
    {
        return color == inputColor && (!isHold || holdInput);
    }
}