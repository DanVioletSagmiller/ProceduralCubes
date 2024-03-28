using Unity.VisualScripting;
using UnityEngine;

public class StoneBot : MonoBehaviour
{
    public float PerlinFrequency = 0.1f;
    public float PerlinAmplitude = 5f;
    public float RiseTime = 2f;
    public AnimationCurve PerlinCurve;
    public CameraTracking CameraTracking;
    public Cube Prefab;
    
    void Update()
    {
        // Get Target Position
        Vector3Int target = CameraTracking.Target;

        // Get Cube Position
        Cube cube = CubeSystem.Instance.GetCube(target);

        // Cube requirements
        if (cube == null) return;
        if (cube.Id != 1 && cube.Id != 2) return;   

        // Dettermine Perlin value
        float perlin = Mathf.PerlinNoise(target.x * PerlinFrequency, target.z * PerlinFrequency);

        // Apply Curve to Perlin
        float curve = PerlinCurve.Evaluate(perlin);
        float amplitude = curve * PerlinAmplitude;
        if (((int) amplitude) < 1) return; // nothing to rise;

        // Instantiate Rise
        SetupRise(cube, amplitude);
    }

    private void SetupRise(Cube cube, float amplitude)
    {
        int count = (int) amplitude;
        Riser r = cube.AddComponent<Riser>();
        r.Origin = cube.Position;
        r.Target = cube.Position + Vector3Int.up * count;
        r.TotalTime = RiseTime;

        for (int i = 1; i < count + 1; i++)
        {
            Cube c = Instantiate(
                original: Prefab,
                position: cube.Position + Vector3Int.down * i,
                rotation: Quaternion.identity,
                parent: cube.transform);
        }
    }
}
