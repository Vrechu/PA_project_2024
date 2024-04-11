using UnityEngine;


[CreateAssetMenu(fileName = "BuildingProfile", menuName = "ScriptableObjects/BuildingProfile")]
public class BuildingProfile : ScriptableObject
{
    public FloorProfile FirstFloorProfile;
    public FloorProfile[] MiddleFloorProfiles;
    public RoofProfile RoofProfile;
}
