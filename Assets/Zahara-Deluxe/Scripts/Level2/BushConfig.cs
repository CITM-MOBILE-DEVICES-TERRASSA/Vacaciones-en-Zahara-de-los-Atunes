using UnityEngine;

[CreateAssetMenu(fileName = "BushConfig", menuName = "Game/Bush Configuration")]
public class BushConfig : ScriptableObject
{
    public float baseScale = 40f;
    public float depthScaleFactor = 0.93f;
    public float bushSpacing = 3f;
    public int bushesPerRow = 8;
}