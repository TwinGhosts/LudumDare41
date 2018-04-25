using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteY : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        if (Time.frameCount % 10 == 0)
        {
            if (transform.position.x < -8f || transform.position.x > 8f)
            {
                Destroy(gameObject);
            }

            if (transform.position.y < Camera.main.transform.position.y + -8f || transform.position.y > Camera.main.transform.position.y + 25f)
            {
                Destroy(gameObject);
            }
        }
    }
}
