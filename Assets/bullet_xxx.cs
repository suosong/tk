using UnityEngine;
using System.Collections;

public class bullet_xxx : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("bg"))
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}
