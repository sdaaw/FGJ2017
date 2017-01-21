using UnityEngine;
using System.Collections;

public class SinTest : MonoBehaviour
{
    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    public int lengthOfLineRenderer = 20;

    public float speed;

    public float howSadAreYou;

    void Start()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        //lineRenderer.SetColors(c1, c2);
        lineRenderer.startColor = c1;
        lineRenderer.endColor = c2;
        lineRenderer.startWidth = 0.03f;
        lineRenderer.endWidth = 0.03f;
        lineRenderer.numPositions = lengthOfLineRenderer;
        lineRenderer.useWorldSpace = false;
    }
    void Update()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        int i = 0;
        while (i < lengthOfLineRenderer)
        {
            Vector3 pos = new Vector3(i * howSadAreYou, Mathf.Sin(i + Time.time * speed), 0);
            lineRenderer.SetPosition(i, pos);
            i++;
        }
    }
}