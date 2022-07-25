using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinking : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;

    public float timeBetweenBlinks = 4f;

    private float initialTime;

    // Start is called before the first frame update
    void Start()
    {
        initialTime = timeBetweenBlinks;
    }

    // Update is called once per frame
    void Update()
    {
        timeBetweenBlinks -= Time.deltaTime;

        if(timeBetweenBlinks <= 0f){
            skinnedMeshRenderer.SetBlendShapeWeight(0, 100f);
            skinnedMeshRenderer.SetBlendShapeWeight(1, 100f);
            StartCoroutine("ResetBlink");
        }
    }

    IEnumerator ResetBlink(){
        yield return new WaitForSeconds(0.08f);
        skinnedMeshRenderer.SetBlendShapeWeight(0, 0f);
        skinnedMeshRenderer.SetBlendShapeWeight(1, 0f);
        timeBetweenBlinks = initialTime - (Random.Range(-1f, 1f));

    }
}
