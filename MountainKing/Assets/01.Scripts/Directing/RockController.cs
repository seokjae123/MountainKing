using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RockController : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Color originColor;
    private MeshCollider meshCollider;
    public int Hit_Count = 0;

    private Vector3 originPosition;
    private Quaternion originRotation;
    public float shake_decay;
    public float shake_intensity;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originColor = meshRenderer.material.color;
        meshCollider = GetComponent<MeshCollider>();
    }

    private void Update()
    {
        if (shake_intensity > 0) {
            transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
            shake_intensity -= shake_decay;
        }
    }

    public void HitRock(int count)
    {       
        Hit_Count += count;
        Debug.Log("바위 타격 횟수" + Hit_Count + "회");
        StartCoroutine("OnHitColor");
        StartCoroutine("OneHit");
        Shake();

        if (Hit_Count == 6)
        {
            StartCoroutine("Destroy");
        }
    }

    private IEnumerator OneHit()
    {
        this.tag = "Untagged";
        yield return new WaitForSeconds(0.2f);
        this.tag = "Rock";
    }

    private IEnumerator OnHitColor()
    {
        meshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        meshRenderer.material.color = originColor;
    }

    private IEnumerator Destroy()
    {
        meshCollider.enabled = false;
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject, 0.0f);
    }

    void Shake()
    {
        originPosition = transform.position;
        originRotation = transform.rotation;
        shake_intensity = 0.3f;
        shake_decay = 0.01f;
    }
}
