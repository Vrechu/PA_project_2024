using Unity.VisualScripting;
using UnityEngine;

public class SimpleStock : Shape
{
	private int floorNumber;

	// shape parameters:
	[SerializeField]
	int Width;
	[SerializeField]
	int Depth;

	[SerializeField]
	private BuildingProfile buildingProfile;

	[SerializeField]
	private bool firstFloor = false;

	private FloorProfile floorProfile;

	public void Initialize(int floorNumber, int Width, int Depth,BuildingProfile buildingProfile)
	{
		this.Width = Width;
		this.Depth = Depth;
		this.buildingProfile = buildingProfile;
		this.floorNumber = floorNumber;
	}

	protected override void Execute()
	{
		SetFloorProfile();
		GenerateWallCenters();
		GenerateWallCorners();
		if (firstFloor) SetFloorAmount();

		InitializeNextFloor();
	}

	/// <summary>
	/// Sets the total amount of non-roof layers to the building based on the min and max specified in the building profile.
	/// </summary>
	private void SetFloorAmount()
	{
		floorNumber = buildingProfile.MinFloors + RandomInt(buildingProfile.MaxFloors + 1 - buildingProfile.MinFloors);
	}

	/// <summary>
	/// Picks a random floor profile from the building profile.
	/// </summary>
	private void SetFloorProfile()
	{
		if (firstFloor) floorProfile = buildingProfile.FirstFloorProfile;
		else
		{
            int index = RandomInt(buildingProfile.MiddleFloorProfiles.Length);
			floorProfile = buildingProfile.MiddleFloorProfiles[index];
        }
	}

	/// <summary>
	/// Generates the walls of the building excluding the wall corners.
	/// </summary>
	private void GenerateWallCenters()
	{
		// Create four central walls:
		for (int i = 0; i < 4; i++)
		{
			Vector3 localPosition = new Vector3();
			switch (i)
			{
				case 0:
					localPosition = new Vector3(-(Width - 1) * 0.5f, 0, 0); // left
					break;
				case 1:
					localPosition = new Vector3(0, 0, (Depth - 1) * 0.5f); // back
					break;
				case 2:
					localPosition = new Vector3((Width - 1) * 0.5f, 0, 0); // right
					break;
				case 3:
					localPosition = new Vector3(0, 0, -(Depth - 1) * 0.5f); // front
					break;
			}
			SimpleRow newRow = CreateSymbol<SimpleRow>("CentralWall", localPosition, Quaternion.Euler(0, i * 90, 0));
			newRow.Initialize(i % 2 == 1 ? Width - 2 : Depth - 2, floorProfile.WallCenterBlocks);
			newRow.Generate();
		}
	}


	/// <summary>
	/// Generates the wall corners of this floor layer.
	/// </summary>
	private void GenerateWallCorners()
    {
		// Create four corner blocks:
		for (int i = 0; i < 4; i++)
		{
			Vector3 localPosition = new Vector3();
			switch (i)
			{
				case 0:
					localPosition = new Vector3(-(Width - 1) * 0.5f, 0, -(Depth - 1) * 0.5f); // left front
					break;
				case 1:
					localPosition = new Vector3(-(Width - 1) * 0.5f, 0, (Depth - 1) * 0.5f); // left back
					break;
				case 2:
					localPosition = new Vector3((Width - 1) * 0.5f, 0, (Depth - 1) * 0.5f); // right back
					break;
				case 3:
					localPosition = new Vector3((Width - 1) * 0.5f, 0, -(Depth - 1) * 0.5f); // right front
					break;
			}

			Block newCornerBlock = CreateSymbol<Block>("WallCornerBlock", localPosition, Quaternion.Euler(0, i * 90, 0));

            int index = RandomInt(floorProfile.WallCornerBlocks.Length);
            newCornerBlock.Initialize(floorProfile.WallCornerBlocks[index]);
			newCornerBlock.Generate();
		}
	}


	/// <summary>
	/// Generates the next floor or roof layer.
	/// </summary>
	private void InitializeNextFloor()
	{
        if (floorNumber > 1)
        {
            SimpleStock nextStock = CreateSymbol<SimpleStock>("stock", new Vector3(0, 1, 0));
            nextStock.Initialize(floorNumber - 1, Width, Depth, buildingProfile);
            nextStock.Generate(buildDelay);
        }
        else
        {
            SimpleRoof nextRoof = CreateSymbol<SimpleRoof>("roof", new Vector3(0, 1, 0));
            nextRoof.Initialize(Width, Depth, buildingProfile);
            nextRoof.Generate(buildDelay);
        }
    }
}

