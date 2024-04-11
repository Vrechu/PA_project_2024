using UnityEngine;

[CreateAssetMenu(fileName = "FloorProfile", menuName = "ScriptableObjects/FloorProfile")]
public class FloorProfile : ScriptableObject
{
    public GameObject[] WallCenterBlocks;
    public GameObject[] WallCornerBlocks;
}
