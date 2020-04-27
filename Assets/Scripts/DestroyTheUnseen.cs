using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTheUnseen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Vector3 dist = cam.WorldToScreenPoint(this.transform.position);
        if (dist.x < -1 * Screen.width)
        {
            Destroy(this.gameObject);
        }
    }
}
