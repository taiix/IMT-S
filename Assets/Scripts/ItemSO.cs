using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Objects/ItemSO")]
public class ItemSO : ScriptableObject
{
    public Sprite image;
    public new string name;
    public int orderIndex;
    [TextArea(2,5)] public string description;
}
