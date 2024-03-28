using System.Collections.Generic;
using UnityEngine;

public class Riser : MonoBehaviour
{
    public Vector3 Target;
    public Vector3 Origin;
    public float TotalTime = 1f;
    private float TimeCounter = 0f;

    private void Update()
    {
        TimeCounter += Time.deltaTime;
        if (TimeCounter > TotalTime) TimeCounter = TotalTime;

        transform.position = Vector3.Lerp(Origin, Target, TimeCounter / TotalTime);
        if (TimeCounter == TotalTime) Destroy(this);
    }
}
