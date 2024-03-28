using System;
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
            Cubes.Add(cube.Position, cube);
    }

    public bool HasPosition(Vector3Int position) => Cubes.ContainsKey(position);

    public void AddCube(Cube cube) => Cubes.Add(cube.Position, cube);

    public Cube GetCube(Vector3Int offset)
    {
        if (HasPosition(offset)) return Cubes[offset];
        return null;
    }

    public void Remove(Vector3Int pos) => Cubes.Remove(pos);

    internal void MoveCube(Vector3Int originalPosition, Vector3Int newPosition, Cube cube)
    {
        Cubes.Remove(originalPosition);
        Cubes.Add(newPosition, cube);
    }
}
