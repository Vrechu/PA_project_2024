using Unity.VisualScripting;
using UnityEngine;

public class SimpleStock : Shape
{
	private int floorNumber;

	// shape parameters:
	[SerializeField]
    private int widthMin;
	[SerializeField]
    private int widthMax;
	[SerializeField]
    private int depthMin;
	[SerializeField] 
	private int depthMax;

	private int depth;
	private int width;


    [SerializeField]
	private BuildingProfile buildingProfile;

	[SerializeField]
	private bool firstFloor = false;

	private FloorProfile floorProfile;

	public GameObject[] sidewalkBlocks;
	public GameObject[] streetEdgeBlocks;

	public void Initialize(int floorNumber, int Width, int Depth,BuildingProfile buildingProfile)
	{
		this.width = Width;
		this.depth = Depth;
		this.buildingProfile = buildingProfile;
		this.floorNumber = floorNumber;
	}

	protected override void Execute()
	{
		if (firstFloor)
		{
			SetFloorAmount();
			SetProportions();
			GenerateSidewalks();
			GenerateStreetEdges();
		}

		SetFloorProfile();
		GenerateWallCenters();
		GenerateWallCorners();

		InitializeNextFloor();
	}

	private void SetProportions()
	{
		width = widthMin + RandomInt(widthMax - widthMin);
		depth = depthMin + RandomInt(depthMax - depthMin);
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
					localPosition = new Vector3(-(width - 1) * 0.5f, 0, 0); // left
					break;
				case 1:
					localPosition = new Vector3(0, 0, (depth - 1) * 0.5f); // back
					break;
				case 2:
					localPosition = new Vector3((width - 1) * 0.5f, 0, 0); // right
					break;
				case 3:
					localPosition = new Vector3(0, 0, -(depth - 1) * 0.5f); // front
					break;
			}
			SimpleRow newRow = CreateSymbol<SimpleRow>("CentralWall", localPosition, Quaternion.Euler(0, i * 90, 0));
			newRow.Initialize(i % 2 == 1 ? width - 2 : depth - 2, floorProfile.WallCenterBlocks);
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
					localPosition = new Vector3(-(width - 1) * 0.5f, 0, -(depth - 1) * 0.5f); // left front
					break;
				case 1:
					localPosition = new Vector3(-(width - 1) * 0.5f, 0, (depth - 1) * 0.5f); // left back
					break;
				case 2:
					localPosition = new Vector3((width - 1) * 0.5f, 0, (depth - 1) * 0.5f); // right back
					break;
				case 3:
					localPosition = new Vector3((width - 1) * 0.5f, 0, -(depth - 1) * 0.5f); // right front
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
            nextStock.Initialize(floorNumber - 1, width, depth, buildingProfile);
            nextStock.Generate(buildDelay);
        }
        else
        {
            SimpleRoof nextRoof = CreateSymbol<SimpleRoof>("roof", new Vector3(0, 1, 0));
            nextRoof.Initialize(width, depth, buildingProfile);
            nextRoof.Generate(buildDelay);
        }
    }

	private void GenerateSidewalks()
	{
		for (int i = 0; i < 4; i++)
		{
			Vector3 localPosition = new Vector3();
			switch (i)
			{
				case 0:
					localPosition = new Vector3(-(width + 1) * 0.5f, 0, 0); // left
					break;
				case 1:
					localPosition = new Vector3(0, 0, (depth + 1) * 0.5f); // back
					break;
				case 2:
					localPosition = new Vector3((width + 1) * 0.5f, 0, 0); // right
					break;
				case 3:
					localPosition = new Vector3(0, 0, -(depth + 1) * 0.5f); // front
					break;
			}
			SimpleRow newRow = CreateSymbol<SimpleRow>("sidewalk", localPosition, Quaternion.Euler(0, i * 90, 0));
			newRow.Initialize(i % 2 == 1 ? width : depth, sidewalkBlocks);
			newRow.Generate();
		}

        for (int i = 0; i < 4; i++)
        {
            Vector3 localPosition = new Vector3();
            switch (i)
            {
                case 0:
                    localPosition = new Vector3(-(width + 1) * 0.5f, 0, -(depth + 1) * 0.5f); // left front
                    break;
                case 1:
                    localPosition = new Vector3(-(width + 1) * 0.5f, 0, (depth + 1) * 0.5f); // left back
                    break;
                case 2:
                    localPosition = new Vector3((width + 1) * 0.5f, 0, (depth + 1) * 0.5f); // right back
                    break;
                case 3:
                    localPosition = new Vector3((width + 1) * 0.5f, 0, -(depth + 1) * 0.5f); // right front
                    break;
            }

            Block newCornerBlock = CreateSymbol<Block>("sidewalkCorner" + i, localPosition, Quaternion.Euler(0, i * 90, 0));
			newCornerBlock.Initialize(sidewalkBlocks[RandomInt(sidewalkBlocks.Length)]);
            newCornerBlock.Generate();
        }
    }

	private void GenerateStreetEdges()
	{
        for (int i = 0; i < 4; i++)
        {
            Vector3 localPosition = new Vector3();
            switch (i)
            {
                case 0:
                    localPosition = new Vector3(-(width + 3) * 0.5f, 0, 0); // left
                    break;
                case 1:
                    localPosition = new Vector3(0, 0, (depth + 3) * 0.5f); // back
                    break;
                case 2:
                    localPosition = new Vector3((width + 3) * 0.5f, 0, 0); // right
                    break;
                case 3:
                    localPosition = new Vector3(0, 0, -(depth + 3) * 0.5f); // front
                    break;
            }
            SimpleRow newRow = CreateSymbol<SimpleRow>("streetEdge", localPosition, Quaternion.Euler(0, i * 90, 0));
            newRow.Initialize((i % 2 == 1 ? width : depth) + 2, streetEdgeBlocks);
            newRow.Generate();
        }
    }
}

