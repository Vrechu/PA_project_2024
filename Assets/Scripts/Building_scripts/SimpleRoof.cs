using UnityEngine;

public class SimpleRoof : Shape
{
	// grammar rule probabilities:
	const float roofContinueChance = 0.5f;

	// shape parameters:
	int Width;
	int Depth;

	private BuildingProfile buildingProfile;

	public void Initialize(int Width, int Depth, BuildingProfile buildingProfile)
	{
		this.Width = Width;
		this.Depth = Depth;

		this.buildingProfile = buildingProfile;
	}


	protected override void Execute()
	{
		if (Width == 0 || Depth == 0)
			return;

		CreateRoofEdge();
		CreateRoofCenter();
	}

	private void CreateRoofEdge()
	{
		// Create four central roof edge parts:
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
			SimpleRow newRow = CreateSymbol<SimpleRow>("CentralRoofPart", localPosition, Quaternion.Euler(0, i * 90, 0));
			newRow.Initialize(i % 2 == 1 ? Width - 2 : Depth - 2, buildingProfile.RoofCenterBlocks);
			newRow.Generate();
		}

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

			Block newCornerBlock = CreateSymbol<Block>("RoofCornerBlock", localPosition, Quaternion.Euler(0, i * 90, 0));


            int index = RandomInt(buildingProfile.RoofCornerBlocks.Length);
            newCornerBlock.Initialize(buildingProfile.RoofCornerBlocks[index]);
			newCornerBlock.Generate();

		}
	}

	private void CreateRoofCenter()
	{
		if (Depth < 3 && Width < 3) return;
		Vector3 localPosition = new Vector3();

		for (int i = 0; i < Width - 2; i++)
		{
			localPosition = Vector3.right * (i - (Width - 3) / 2f);

			SimpleRow newRow = CreateSymbol<SimpleRow>("RoofCenterBlock", localPosition, Quaternion.identity);
			newRow.Initialize(Depth - 2, buildingProfile.RoofTopBlocks);
			newRow.Generate();
		}
	}
}



