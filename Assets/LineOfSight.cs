using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    public bool hasLineOfSight = false;
    public List<SkeletonScript> skeletons; // List to hold multiple skeletons
    public PlayerSwitcherScript playerSwitcher;

    [Header("DrawLine")]
    private LineRenderer lr;
    public Transform pos1;
    public Transform pos2;
    public float animationDuration = 1f;
    public LayerMask obstacleLayerMask;
    public SkeletonScript detectedSkeleton = null;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Clear line of sight
        hasLineOfSight = false;

        // Detected skeleton
        
        float closestDistance = Mathf.Infinity;

        // Iterate over all skeletons
        foreach (SkeletonScript skeleton in skeletons)
        {
            // Calculate distance to the skeleton
            float distance = Vector2.Distance(transform.position, skeleton.transform.position);

            // Detect line of sight for each skeleton
            RaycastHit2D ray = Physics2D.Raycast(transform.position, skeleton.transform.position - transform.position, Mathf.Infinity, obstacleLayerMask);
            if (ray.collider != null && ray.collider.CompareTag("Skeleton") && distance < closestDistance)
            {
                hasLineOfSight = true; // Set line of sight to true if any skeleton is detected
                detectedSkeleton = skeleton; // Store the detected skeleton
                closestDistance = distance;
            }
        }

        // Set up line based on the result
        if (playerSwitcher.player1Active)
            setUpLine(hasLineOfSight, detectedSkeleton);
        else
            setUpLine2(hasLineOfSight, detectedSkeleton);
    }


    private void setUpLine(bool status, SkeletonScript skeleton)
    {
        if (status)
        {
            lr.enabled = true;
            lr.SetPosition(0, transform.position); // Start position from the player
            lr.SetPosition(1, skeleton.transform.position); // End position from the detected skeleton
        }
        else
        {
            lr.enabled = false; // Hide the line renderer if no line of sight
        }
    }

    private void setUpLine2(bool status, SkeletonScript skeleton)
    {
        if (status)
        {
            lr.enabled = true;
            lr.SetPosition(0, skeleton.transform.position); // Start position from the detected skeleton
            lr.SetPosition(1, transform.position); // End position from the player
        }
        else
        {
            lr.enabled = false; // Hide the line renderer if no line of sight
        }
    }
    public void ProvideLineOfSightInfo(out bool status, out SkeletonScript skeleton)
    {
        status = hasLineOfSight;
        skeleton = detectedSkeleton;
    }
}