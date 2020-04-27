using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    void Start()
    {
        speed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-speed * Time.deltaTime, 0f, 0f));

        if(this.transform.position.x >= 6 && this.transform.position.z <= -5)
        {
            this.transform.position = new Vector3(-9, -1, 0);
        }
    }
}
