using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomFlickeringController : MonoBehaviour
{
    public bool isFlickering = false;
    public float minStrength;
    public float maxStrength;

    void Update()
    {
        if (isFlickering == false)
        {
            StartCoroutine(FlickeringLight());
        }
    }

    IEnumerator FlickeringLight()
    {
        isFlickering = true;
        int flickNo = Random.Range(5, 10);
        for (int i = 0; i < flickNo; i++)
        {
            this.gameObject.GetComponent<Light>().intensity = Random.Range(minStrength, maxStrength);
            yield return new WaitForSeconds(0.1f);
        }
        this.gameObject.GetComponent<Light>().intensity = 1.0f;
        yield return new WaitForSeconds(Random.Range(5f, 10f));
        isFlickering = false;
    }
}
