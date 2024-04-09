using UnityEngine;

public class SimpleStock : Shape
{
	// grammar rule probabilities:
	const float stockContinueChance = 0.5f;

	// shape parameters:
	[SerializeField]
	int Width;
	[SerializeField]
	int Depth;

	[SerializeField]
	private GameObject[] wallStyle;
	[SerializeField]
	private GameObject[] wallCornerStyle;
	[SerializeField]
	private GameObject[] roofStyle;
	[SerializeField]
	private GameObject[] roofCornerStyle;
	[SerializeField]
	private GameObject[] roofCenterStyle;

	public void Initialize(int Width, int Depth, GameObject[] wallStyle, GameObject[] wallCornerStyle, GameObject[] roofStyle, GameObject[] roofCornerStyle, GameObject[] roofCenterStyle)
	{
		this.Width = Width;
		this.Depth = Depth;
		this.wallStyle = wallStyle;
		this.wallCornerStyle = wallCornerStyle;
		this.roofStyle = roofStyle;
		this.roofCornerStyle = roofCornerStyle;
		this.roofCenterStyle = roofCenterStyle;
	}

	protected override void Execute()
	{
		GenerateWallCenters();
		GenerateWallCorners();

		// Continue with a stock or with a roof (random choice):
		float randomValue = RandomFloat();
		if (randomValue < stockContinueChance)
		{
			SimpleStock nextStock = CreateSymbol<SimpleStock>("stock", new Vector3(0, 1, 0));
			nextStock.Initialize(Width, Depth, wallStyle, wallCornerStyle, roofStyle, roofCornerStyle, roofCenterStyle);
			nextStock.Generate(buildDelay);
		}
		else
		{
			SimpleRoof nextRoof = CreateSymbol<SimpleRoof>("roof", new Vector3(0, 1, 0));
			nextRoof.Initialize(Width, Depth, roofStyle, roofCornerStyle, roofCenterStyle);
			nextRoof.Generate(buildDelay);
		}
	}

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
			newRow.Initialize(i % 2 == 1 ? Width - 2 : Depth - 2, wallStyle);
			newRow.Generate();
		}
	}

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
			newCornerBlock.Initialize(wallCornerStyle[0]);
			newCornerBlock.Generate();
		}
	}
}

