using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestBot : MonoBehaviour
{
    public ForestBlock Prefab;
    public float ChanceToSpawnForest = 0.03f;
    public int ForestMinSize = 5;
    public int ForestMaxSize = 30;
    public GroundBot GroundBot;
    public bool IsBuilding = false;
    public int LowTreeChance = -5;

    private void Update()
    {
        if (IsBuilding || Random.value > ChanceToSpawnForest) return;

        if (!HasMinimumSpaceForForest()) return;

        IsBuilding = true;
        StartCoroutine(BuildForest());
    }

    private bool HasMinimumSpaceForForest()
    {
        foreach(var pos in InRadius(GroundBot.Position, ForestMinSize))
        { 
            Cube c = CubeSystem.Instance.GetCube(pos);
            if (c == null || c.Id == 1 || c.Id == 2) continue;
            return false;
        }

        return true;
    }

    private IEnumerator BuildForest()
    {
        Vector3Int center = GroundBot.Position;
        foreach(Vector3Int pos in InRadius(center, ForestMaxSize))
        { 
            ForestBlock forest = Instantiate(Prefab, pos, Quaternion.identity);
            forest.SetTree(LowTreeChance);

            Cube cube = forest.GetComponent<Cube>();
            cube.Position = pos;
            if (CubeSystem.Instance.HasPosition(pos)) 
                CubeSystem.Instance.Remove(pos);

            CubeSystem.Instance.AddCube(cube);

            yield return null;
        }

        IsBuilding = false;
    }

    private IEnumerable<Vector3Int> InRadius(Vector3Int center, int radius)
    {
        int r2 = radius * radius;
        for (int x = -radius; x <= radius; x++)
            for (int z = -radius; z <= radius; z++)
                if (x * x + z * z <= r2)
                    yield return new Vector3Int(center.x + x, center.y, center.z + z);
    }
}
