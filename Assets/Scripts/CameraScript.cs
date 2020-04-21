using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float speed = 2.0f;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PLAYER");
    }

    // Update is called once per frame
    void Update()
    {
        float interp = speed * Time.deltaTime;
        Vector3 pos = this.transform.position;
        pos.x = Mathf.Lerp(this.transform.position.x, player.transform.position.x, interp);
        //pos.y = Mathf.Lerp(this.transform.position.y, player.transform.position.y, interp);
        this.transform.position = pos;
    

        
    }


   
}
