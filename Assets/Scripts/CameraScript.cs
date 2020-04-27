using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private float speed;
    private GameObject player;
    public float positionBiasX = 20f;
    public float positionBiasY = 25f;
    public float lowerLimit = 8f;
    public float upperLimit = 20f;
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
        pos.x = Mathf.Lerp(this.transform.position.x, player.transform.position.x , interp) + positionBiasX * Time.deltaTime;
        pos.y = Mathf.Lerp(this.transform.position.y, player.transform.position.y, interp) + positionBiasY * Time.deltaTime;
        this.transform.position = pos;
    
        if(this.transform.position.y <= lowerLimit)
        {
            this.transform.position = new Vector3(this.transform.position.x, lowerLimit, this.transform.position.z);
        }
        
        else if(this.transform.position.y <= lowerLimit)
        {
            this.transform.position = new Vector3(this.transform.position.x, upperLimit, this.transform.position.z);

        }

    }


   
}
