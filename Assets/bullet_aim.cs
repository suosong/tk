using UnityEngine;
using System.Collections;

public enum launch_timer
{
    one,
    two,
    three,
    four,
    five,
    invalid
}

public class bullet_aim : MonoBehaviour {

    public int bounce_time = 5;
    public ArrayList bounce_points_list;

    public Vector3 aim_pos;
    public Vector3 aim_dir;

    public bool aimed = false;
    public bool one_frame = false;
    public launch_timer bullet_launch_timer = launch_timer.invalid;

    public Transform player_trans = null;
    public Transform bullet_trans = null;
    public Rigidbody2D bullet_rb = null;
    public LineRenderer aim_line_render = null;

	// Use this for initialization
	void Start () {
        bounce_points_list = new ArrayList();
        player_trans = GameObject.Find("player").transform;
        bullet_rb = gameObject.GetComponent<Rigidbody2D>();
        bullet_trans = gameObject.transform;
        aim_line_render = GameObject.Find("aim_line").GetComponent<LineRenderer>();
        aim_line_render.SetWidth(0.1f, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(bullet_trans.position.x) > 20.0f || Mathf.Abs(bullet_trans.position.y) > 20.0f)
        {
            bullet_trans.position = new Vector3(0, 4, 0);
        }

        if (!Input.GetKey(KeyCode.J))
        {
            if (aimed)
            {
                aimed = false;
                bounce_points_list.Clear();
                stop_bullet();
                aim_line_render.enabled = false;
                aim_line_render.SetVertexCount(0);
            }

            return;
        }

        if (!aimed)
        {
            aimed = true;
            bounce_points_list.Clear();
            stop_bullet();
            launch_bullet();
        }
        else if (bullet_launch_timer == launch_timer.one)
        {
            bullet_launch_timer = launch_timer.two;
        }
        else if (bullet_launch_timer == launch_timer.two)
        {
            bullet_launch_timer = launch_timer.three;
        }
        else if (bullet_launch_timer == launch_timer.three)
        {
            bullet_launch_timer = launch_timer.four;
        }
        else if (bullet_launch_timer == launch_timer.four)
        {
            bullet_launch_timer = launch_timer.five;
        }
        else if (bullet_launch_timer == launch_timer.five)
        {
            launch_bullet();
            bullet_launch_timer = launch_timer.invalid;
        }
        else if (!check_aim_valid())
        {
            bounce_points_list.Clear();
            stop_bullet();
            aim_line_render.enabled = false;
            aim_line_render.SetVertexCount(0);
            bullet_launch_timer = launch_timer.one;
        }
        else
        {
            //if (bullet_rb.velocity == Vector2.zero)
            //{
            //    aim_line_render.enabled = false;
            //    aim_line_render.SetVertexCount(0);
            //    launch_bullet();
            //}
            //aim_line_render.enabled = true;
            // 画出瞄准线
            //for (int i=0; i<bounce_points_list.Count; i++)
            //{
            //    if (i == 0)
            //        Gizmos.DrawLine(player_trans.position, (Vector3)bounce_points_list[0]);
            //    else
            //        Gizmos.DrawLine((Vector3)bounce_points_list[i-1], (Vector3)bounce_points_list[i]);
            //}
        }


	}

    bool check_aim_valid()
    {
        return (aim_pos == player_trans.position) && (aim_dir == player_trans.up);
    }

    void launch_bullet()
    {
        // 记录下当时的位置、朝向
        aim_pos = player_trans.position;
        aim_dir = player_trans.up;

        
        bullet_trans.position = player_trans.position + player_trans.up * 2;
        bullet_rb.velocity = Vector2.zero;
        bullet_rb.AddForce(player_trans.up * 1000000000, ForceMode2D.Impulse);

        aim_line_render.enabled = true;
        aim_line_render.SetVertexCount(0);
    }

    void stop_bullet()
    {
        bullet_rb.velocity = Vector2.zero;
        bullet_trans.position = new Vector3(0, 4, 0);
    }

    void draw_aim_line()
    {
        aim_line_render.SetVertexCount(bounce_points_list.Count+1);
        aim_line_render.SetPosition(0, new Vector3(player_trans.position.x, player_trans.position.y, player_trans.position.z-1));
        for (int i = 0; i < bounce_points_list.Count; i++)
        {
            aim_line_render.SetPosition(i + 1, (Vector3)bounce_points_list[i]);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("wall"))
        {
            bounce_points_list.Add(new Vector3(bullet_trans.position.x, bullet_trans.position.y, bullet_trans.position.z-1));
            if (bounce_points_list.Count >= bounce_time)
            {
                stop_bullet();
                draw_aim_line();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("bg"))
        {
            bounce_points_list.Add(new Vector3(bullet_trans.position.x, bullet_trans.position.y, bullet_trans.position.z - 1));
            stop_bullet();
            draw_aim_line();
        }
    }
}
