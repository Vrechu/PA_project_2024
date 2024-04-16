using UnityEngine;


[CreateAssetMenu(fileName = "BuildingProfile", menuName = "ScriptableObjects/BuildingProfile")]
public class BuildingProfile : ScriptableObject
{
    public int MinFloors = 1;
    public int MaxFloors = 1;

    public FloorProfile FirstFloorProfile;
    public FloorProfile[] MiddleFloorProfiles;
    public RoofProfile RoofProfile;
}
