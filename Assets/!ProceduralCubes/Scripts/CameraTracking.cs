using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    public static CameraTracking Instance;
    public CameraTracking() => Instance = this;

    public Transform CameraObject;
    public Vector3Int Target = new Vector3Int(0, 0, 0);


    public float TimeToLerpToTarget = .5f;

    public float MinRotationTime = 1f;
    public float MaxRotationTime = 5f;
    private float OriginalRotation = 0f;
    private float TargetRotation = 0f;
    private float TimeForRotation = 0f;
    private float RotationCounter = 0f;
    public AnimationCurve RotationStrengthCurve;

    private void Start()
    {
        CameraObject.transform.LookAt(this.transform);
        SetNextRotation();
    }

    private void SetNextRotation()
    {
        OriginalRotation = transform.rotation.eulerAngles.y;
        TargetRotation = UnityEngine.Random.Range(minInclusive: 0, maxExclusive: 360);
        RotationCounter = 0f;
        TimeForRotation = UnityEngine.Random.Range(MinRotationTime, MaxRotationTime);
    }

    private void Update()
    {
        // Lerp camera towards the target position
        transform.position = Vector3.Lerp(transform.position, Target, TimeToLerpToTarget * Time.deltaTime);

        // Rotate the camera
        RotationCounter += Time.deltaTime;
        float t = Mathf.Clamp01(RotationCounter / TimeForRotation);
        t = RotationStrengthCurve.Evaluate(t);
        float rotation = Mathf.LerpAngle(OriginalRotation, TargetRotation, t); 
        Vector3 eulerAngles = transform.eulerAngles;
        eulerAngles.y = rotation;
        transform.eulerAngles = eulerAngles;
        if (RotationCounter >= TimeForRotation) SetNextRotation();
    }
}
