using UnityEngine;



public class GridCity : MonoBehaviour
{
	public int rows = 10;
	public int columns = 10;
	public int rowWidth = 10;
	public int columnWidth = 10;
	[Range(0, 100)]
	public float emptySpaceChance = 0;
	public GameObject[] buildingPrefabs;

	public float buildDelaySeconds = 0.1f;

	private void Start()
	{
		Generate();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.G))
		{
			DestroyChildren();
			Generate();
		}
	}

	private void DestroyChildren()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			Destroy(transform.GetChild(i).gameObject);
		}
	}

	private void Generate()
	{
		for (int row = 0; row < rows; row++)
		{
			for (int col = 0; col < columns; col++)
			{
				float random = Random.Range(0, 100);
				if (random > emptySpaceChance)
				{
					// Create a new building, chosen randomly from the prefabs:
					int buildingIndex = Random.Range(0, buildingPrefabs.Length);
					GameObject newBuilding = Instantiate(buildingPrefabs[buildingIndex], transform);

					// Place it in the grid:
					newBuilding.transform.localPosition = new Vector3(col * columnWidth, 0, row * rowWidth);

					// If the building has a Shape (grammar) component, launch the grammar:
					Shape shape = newBuilding.GetComponent<Shape>();
					if (shape != null)
					{
						shape.Generate(0);
					}
				}
			}
		}
	}
}
