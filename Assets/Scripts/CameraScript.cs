using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private float speed;
    private GameObject player;
    public float positionBias;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PLAYER");
        speed = player.GetComponent<PlayerScript>().speed;
    }

    // Update is called once per frame
    void Update()
    {
        float interp = speed * Time.deltaTime;
        Vector3 pos = this.transform.position;
        pos.x = Mathf.Lerp(this.transform.position.x, player.transform.position.x , interp) + positionBias * Time.deltaTime;
        //pos.y = Mathf.Lerp(this.transform.position.y, player.transform.position.y, interp);
        this.transform.position = pos;
    

        
    }


   
}
