using UnityEngine;
using System.Collections;

public class player_control : MonoBehaviour {

    public GameObject bullet_pre;

	// Use this for initialization
	void Start () {
        bullet_pre = Resources.Load("prefabs/bullet") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 euler = gameObject.transform.eulerAngles;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        //Debug.LogError(x.ToString() + "  " + y.ToString());
        if (Mathf.Abs(x - 0) > 0.5 || Mathf.Abs(y - 0) > 0.5)
        {
            euler.z = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            euler.z -= 90;
            gameObject.transform.eulerAngles = euler;
            gameObject.transform.Translate(0, 1 * Time.deltaTime, 0, Space.Self);
        }

	    if (Input.GetKey(KeyCode.W))
        {
            euler.z = 0;
            gameObject.transform.eulerAngles = euler;
            gameObject.transform.Translate(0, 1 * Time.deltaTime, 0, Space.Self);
        }

        if (Input.GetKey(KeyCode.S))
        {
            euler.z = 180;
            gameObject.transform.eulerAngles = euler;
            gameObject.transform.Translate(0, 1 * Time.deltaTime, 0, Space.Self);
        }

        if (Input.GetKey(KeyCode.A))
        {
            euler.z = 90;
            gameObject.transform.eulerAngles = euler;
            gameObject.transform.Translate(0, 1 * Time.deltaTime, 0, Space.Self);
        }

        if (Input.GetKey(KeyCode.D))
        {
            euler.z = 270;
            gameObject.transform.eulerAngles = euler;
            gameObject.transform.Translate(0, 1 * Time.deltaTime, 0, Space.Self);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            //GameObject bullet = GameObject.Instantiate(bullet_pre, gameObject.transform.position + gameObject.transform.up * 2,Quaternion.identity) as GameObject;
            ////bullet.transform.position = gameObject.transform.position + gameObject.transform.up * 2;
            //Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            ////rb.velocity = Vector2.zero;
            //rb.AddForce(gameObject.transform.up * 10, ForceMode2D.Impulse);
        }
	}
}
