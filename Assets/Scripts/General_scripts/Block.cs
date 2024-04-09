using UnityEngine;

public class Block : Shape
{
    public GameObject BlockPrefab;
    public Quaternion BlockRotation;
    private Vector3 blockPosition;

    public void Initialize(GameObject prefab)
    {
        BlockPrefab = prefab;
    }

    protected override void Execute()
    {

        SpawnPrefab(BlockPrefab,
                    blockPosition, // position offset from center
                    Quaternion.identity         // no rotation
                );

    }
}
