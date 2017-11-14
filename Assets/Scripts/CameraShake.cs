using UnityEngine;
using System.Collections;


public class CameraShake : MonoBehaviour
{
    public float magnitude;
    public float duration;

    private bool shaking;
    private Vector3 originalCamPos;


    void Start()
    {
        Vector3 originalCamPos = transform.localPosition;
    }
    

    public IEnumerator Shake(float _magnitude, float _duration)
    {

        float elapsed = 0.0f;

        while (elapsed < _duration)
        {
           
            elapsed += Time.deltaTime;

            float percentComplete = elapsed / _duration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;
            x *= _magnitude * damper;
            y *= _magnitude * damper;

           transform.localPosition = new Vector3(x, y, originalCamPos.z);
            shaking = false;
            
            yield return null;
        }

        transform.localPosition = originalCamPos;
        
    }
   
}