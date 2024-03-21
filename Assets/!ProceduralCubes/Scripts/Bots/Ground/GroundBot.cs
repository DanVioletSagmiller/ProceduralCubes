using System.Collections.Generic;
using UnityEngine;

public class GroundBot : MonoBehaviour
{    
    public Cube CubePrefab;
    public Vector3Int Position = new Vector3Int(0, 0, 0);
    private List<Vector3Int> positions = new List<Vector3Int>(capacity: 8);

    public float CreateSpeed = 0.5f;
    private float _timeSinceLastCreate = 0;

    public float MaxMoveSpeed = .1f;
    private float _timeSinceLastMove = 0;

    public float CameraCusioning = .1f;
    private Vector3 target = Vector3.zero;

    private Vector3Int[] HorizontalSlice = new Vector3Int[]
    {
        new Vector3Int(x: -1, y: 0, z: -1),
        new Vector3Int(x: -1, y: 0, z: 0),
        new Vector3Int(x: -1, y: 0, z: 1),
        new Vector3Int(x: 0, y: 0, z: -1),
        new Vector3Int(x: 0, y: 0, z: 1),
        new Vector3Int(x: 1, y: 0, z: -1),
        new Vector3Int(x: 1, y: 0, z: 0),
        new Vector3Int(x: 1, y: 0, z: 1),
    };

    private void Update()
    {
        _timeSinceLastMove += Time.deltaTime;
        if (_timeSinceLastMove >= MaxMoveSpeed)
        {
            _timeSinceLastMove = 0;
            Move();
        }

        _timeSinceLastCreate += Time.deltaTime;
        if (_timeSinceLastCreate >= CreateSpeed)
        {
            _timeSinceLastCreate = 0;
            Create();
        }

        target = Vector3.Lerp(target, Position, CameraCusioning * Time.deltaTime);
        Camera.main.transform.LookAt(target);
    }

    private void Move()
    {
        positions.Clear();
        foreach (Vector3Int offset in HorizontalSlice)
        {
            bool cubeExists = CubeSystem.Instance.HasPosition(Position + offset);
            if (cubeExists) positions.Add(Position + offset);
        }

        if (positions.Count == 0) return;

        int randomIndex = UnityEngine.Random.Range(0, positions.Count);
        Position = positions[randomIndex];
    }

    private void Create()
    {
        positions.Clear();
        foreach (Vector3Int offset in HorizontalSlice)
        {
            bool isNull = !CubeSystem.Instance.HasPosition(Position + offset);
            if (isNull) positions.Add(Position + offset);
        }

        if (positions.Count == 0) return;

        int randomIndex = UnityEngine.Random.Range(minInclusive: 0, positions.Count);
        Position = positions[randomIndex];

        Cube cube = Instantiate(CubePrefab, Position, Quaternion.identity);
        cube.Position = Position;
        CubeSystem.Instance.AddCube(cube);
    }
}
