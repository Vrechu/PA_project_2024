using UnityEngine;

public class SimpleRow : Shape
{
	int Number;
	GameObject[] prefabs = null;
	Vector3 direction;

	public void Initialize(int Number, GameObject[] centerprefabs, Vector3 dir = new Vector3())
	{
		this.Number = Number;
		this.prefabs = centerprefabs;
		if (dir.magnitude != 0)
		{
			direction = dir;
		}
		else
		{
			direction = new Vector3(0, 0, 1);
		}
	}

	protected override void Execute()
	{
		if (Number <= 0)
			return;
		for (int i = 0; i < Number; i++)
		{   // spawn the prefabs, randomly chosen
			int index = RandomInt(prefabs.Length); // choose a random prefab index

			Block newBlock = CreateSymbol<Block>("RowBlock", direction * (i - (Number - 1) / 2f), Quaternion.identity);

			newBlock.Initialize(prefabs[index]);
			newBlock.Generate();
		}
	}
}

