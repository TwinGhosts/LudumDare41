using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentBehaviour : MonoBehaviour
{
    public SegmentBehaviour upperNeighbour;

    // Update is called once per frame
    private void Update()
    {
        var height = 2f * Camera.main.orthographicSize;

        var spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (transform.position.y < Camera.main.transform.position.y - height)
        {
            var newSegment = Instantiate(gameObject);
            newSegment.transform.position = new Vector2(transform.position.x, upperNeighbour.upperNeighbour.transform.position.y + spriteRenderer.bounds.size.y);
            upperNeighbour.upperNeighbour.upperNeighbour = newSegment.GetComponent<SegmentBehaviour>();
            Destroy(gameObject);
        }
    }
}
