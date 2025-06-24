using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public Transform shakeTr;
    public bool shakeRotate = false;
    private Vector3 originPos = Vector3.zero;
    private Quaternion originRot = Quaternion.identity;
    void Start()
    {
        shakeTr = transform;
        originPos = shakeTr.transform.position;
        originRot = shakeTr.transform.rotation;
    }
    public IEnumerator ShakeCamera(float duration = 0.05f,float magnitudePos = 0.03f, float magnitudeRot = 0.1f)
    {
        float passTime = 0.0f;
        while(passTime < duration)
        {
            Vector3 shakePos = Random.insideUnitSphere;
            shakeTr.transform.position = shakePos * magnitudePos;
            if(shakeRotate)
            {
                Vector3 shakeRot = new Vector3(0f, 0f, Mathf.PerlinNoise(Time.time * magnitudeRot, 0f));
                shakeTr.rotation = Quaternion.Euler(shakeRot);
            }
            passTime += Time.deltaTime;
            yield return null;
        }
        shakeTr.position = originPos;
        shakeTr.rotation = originRot;
    }
}
