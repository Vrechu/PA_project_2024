using UnityEngine;


[CreateAssetMenu(fileName = "RoofProfile", menuName = "ScriptableObjects/RoofProfile")]
public class RoofProfile : ScriptableObject
{
    public GameObject[] RoofCenterBlocks;
    public GameObject[] RoofCornerBlocks;
    public GameObject[] RoofTopBlocks;
}
