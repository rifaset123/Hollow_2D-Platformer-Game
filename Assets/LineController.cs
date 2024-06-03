using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [SerializeField]
    private Texture[] textures;

    private int animationStep;

    [SerializeField]
    private float fps = 30f;
    private float fpsCounter;


    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

    }

    private void Update()
    {
        fpsCounter += Time.deltaTime;
        if (fpsCounter >= 1 / fps)
        {
            animationStep = (animationStep + 1) % textures.Length;

            lineRenderer.material.SetTexture("_MainTex", textures[animationStep]);

            fpsCounter = 0;
        }
    }
}
