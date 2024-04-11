using UnityEngine;


[CreateAssetMenu(fileName = "BuildingProfile", menuName = "ScriptableObjects/BuildingProfile")]
public class BuildingProfile : ScriptableObject
{
    public GameObject[] WallCenterBlocks;
    public GameObject[] WallCornerBlocks;
    public GameObject[] RoofCenterBlocks;
    public GameObject[] RoofCornerBlocks;
    public GameObject[] RoofTopBlocks;
}
