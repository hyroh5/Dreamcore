using UnityEngine;

[System.Serializable]
public class DialogueTurn
{
    public Speaker speaker;
    public string[] lines;
}

public enum Speaker
{
    Player,
    NPC
}
