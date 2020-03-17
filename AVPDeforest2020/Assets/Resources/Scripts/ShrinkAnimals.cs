using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkAnimals : MonoBehaviour
{
    bool isScaling = false;
    public void Shrink()
    {
        StartCoroutine(scaleOverTime(transform, new Vector3(0, 0, 0), 5));
    }


    IEnumerator scaleOverTime(Transform objectToScale, Vector3 toScale, float duration)
    {
        objectToScale.parent = null;
        //Make sure there is only one instance of this function running
        if (isScaling)
        {
            yield break; ///exit if this is still running
        }
        isScaling = true;

        float counter = 0;

        //Get the current scale of the object to be moved
        Vector3 startScaleSize = objectToScale.localScale;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            objectToScale.localScale = Vector3.Lerp(startScaleSize, toScale, counter / duration);
            yield return null;
        }

        isScaling = false;
        Destroy(this.gameObject);
    }
}
