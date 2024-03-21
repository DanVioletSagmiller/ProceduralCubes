using System.Collections.Generic;
using UnityEngine;

public class CubeSystem : MonoBehaviour
{
    public static CubeSystem Instance;

    public CubeSystem() => Instance = this;

    private Dictionary<Vector3Int, Cube> Cubes = new Dictionary<Vector3Int, Cube>();

    private void Awake()
    {
        foreach(Cube cube in FindObjectsOfType<Cube>())
        {
            Cubes.Add(cube.Position, cube);
        }
    }

    public bool HasPosition(Vector3Int position) => Cubes.ContainsKey(position);

    public void AddCube(Cube cube) => Cubes.Add(cube.Position, cube);
}
