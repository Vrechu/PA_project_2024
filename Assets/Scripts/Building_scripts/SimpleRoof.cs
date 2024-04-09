using UnityEngine;

public class SimpleRoof : Shape
{
	// grammar rule probabilities:
	const float roofContinueChance = 0.5f;

	// shape parameters:
	int Width;
	int Depth;

	GameObject[] roofStyle;
	GameObject[] roofCornerStyle;

	public void Initialize(int Width, int Depth, GameObject[] roofStyle, GameObject[] roofCornerStyle)
	{
		this.Width = Width;
		this.Depth = Depth;
		this.roofStyle = roofStyle;
		this.roofCornerStyle = roofCornerStyle;
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
			newRow.Initialize(i % 2 == 1 ? Width - 2 : Depth - 2, roofStyle);
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
			newCornerBlock.Initialize(roofCornerStyle[0]);
			newCornerBlock.Generate();

		}		
	}

	private void CreateRoofCenter()
    {
		if (Width > 2)
        {
			
        }
    }
}

