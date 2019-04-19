using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    public float speed = 4f;
    public bool onPlatform = false;
    public bool onOblique30Platform = false;
    public int status = 0;
    private  Vector3 startPos;
    private Vector3 newPos;
    private float angle;
    private BoxCollider2D playercol;
    private float lenth;
    private float height;
    private int l;
    private int h;
    public float density=0.1f;
    Ray2D[] downRay;
    Ray2D[] upRay;
    Ray2D[] rightRay;
    Ray2D[] leftRay;

    private void Start()
    {
        angle = 0f;
        startPos = transform.position;
        playercol = gameObject.GetComponent<BoxCollider2D>();
        lenth = playercol.bounds.size.x;
        height = playercol.bounds.size.y;
        l = (int)(lenth / density);
        h = (int)(height/ density);

    }
    private void FixedUpdate()
    {
        InitRay();
        RayDetection();
        newPos = transform.position;
        float h = Input.GetAxis("Horizontal");
        if (h < 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
        if (h > 0)
            transform.eulerAngles = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W)&&status!=3)
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, 20f, 0f), speed * Time.deltaTime);
        Vector2 movement = new Vector2(h, 0);
        Move(movement);
        startPos = newPos;

    }
    public void Move(Vector2 deltaPos)
    {
        //RayDetection();
        float lerp = newPos.y - startPos.y;
        if(lerp>=0)
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(deltaPos.x * Mathf.Cos(Mathf.PI*angle/180), Mathf.Abs(deltaPos.x) * Mathf.Sin(Mathf.PI*angle / 180), 0f), speed * Time.deltaTime);
        else if(lerp<0)
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(deltaPos.x * Mathf.Cos(Mathf.PI * angle / 180),- Mathf.Abs(deltaPos.x) * Mathf.Sin(Mathf.PI * angle / 180), 0f), speed * Time.deltaTime);
        if (status == 3)
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(deltaPos.x, -1f, 0f), speed * Time.deltaTime);

    }
    public void InitRay()
    {
        downRay = new Ray2D[l];
        upRay = new Ray2D[l];
        rightRay = new Ray2D[h];
        leftRay = new Ray2D[h];
        InitRayDir(downRay, Vector2.down);
        InitRayDir(upRay, Vector2.up);
        InitRayDir(rightRay, Vector2.right);
        InitRayDir(leftRay, Vector2.left);
        downRay[0].origin = transform.position + new Vector3(-lenth / 2, -height / 2, 0f);
        downRay[downRay.Length - 1].origin = transform.position + new Vector3(lenth / 2, -height / 2, 0f);
        float interval = lenth / (downRay.Length - 1);
        for(int i=1; i < downRay.Length - 1; i++)
        {
            downRay[i].origin = downRay[i - 1].origin + new Vector2(interval, 0f);
        }
        upRay[0].origin = transform.position + new Vector3(-lenth / 2, height/ 2, 0f);
        upRay[upRay.Length - 1].origin = transform.position + new Vector3(lenth / 2, height / 2, 0f);
        for (int i = 1; i < upRay.Length - 1; i++)
        {
            upRay[i].origin = upRay[i - 1].origin + new Vector2(interval, 0f);
        }
        leftRay[0].origin = transform.position + new Vector3(-lenth / 2, -height / 2, 0f);
        leftRay[leftRay.Length - 1].origin = transform.position + new Vector3(-lenth / 2,height / 2, 0f);
        interval = height / (leftRay.Length - 1);
        for (int i = 1; i <leftRay.Length - 1; i++)
        {
            leftRay[i].origin = leftRay[i - 1].origin + new Vector2(0f,interval);
        }
        rightRay[0].origin = transform.position + new Vector3(lenth/ 2, -height / 2, 0f);
        rightRay[rightRay.Length - 1].origin = transform.position + new Vector3(lenth / 2, height / 2, 0f);
        for (int i = 1; i < rightRay.Length - 1; i++)
        {
            rightRay[i].origin = rightRay[i - 1].origin + new Vector2(0f, interval);
        }
        DrawRay(upRay, Color.red);
        DrawRay(downRay, Color.red);
        DrawRay(leftRay, Color.red);
        DrawRay(rightRay, Color.red);

    }
    private void RayDetection()
    {        //RaycastHit2D hit=Physics2D.Linecast(ray.origin,ray.origin+new Vector2(0,-0.1f));
        //if (hit.collider == null)
        //{
        //    status = 3;
        //}
        //else
        //{
        //    angle = Vector2.Angle(hit.normal, Vector2.up);
        //    status = 0;
        //    Debug.DrawRay(ray.origin, ray.direction, Color.red);
        //    Debug.Log(angle);

        //}
        //Debug.Log(status);
        RaycastHit2D[] downHit = new RaycastHit2D[l];
        RaycastHit2D[] upHit = new RaycastHit2D[l];
        RaycastHit2D[] leftHit = new RaycastHit2D[h];
        RaycastHit2D[] rightHit = new RaycastHit2D[h];
        int[] downHitStatus = new int[l];
        int[] upHitStatus = new int[l];
        int[] leftHitStatus = new int[h];
        int[] rightHitStatus = new int[h];
        for (int i = 0; i < downHit.Length; i++)
        {
            downHit[i] = Physics2D.Linecast(downRay[0].origin, downRay[0].origin + new Vector2(0, -0.1f));
            Debug.Log(downHit[0].collider.tag);
            if (downHit[i].collider == null)
                downHitStatus[i] = 3;
            if (i < downHit.Length - 1)
            {
                Debug.Log(downHitStatus[i]);
                if (downHitStatus[i] == 3 && downHitStatus[i] == downHitStatus[i + 1])
                    status = 3;
            }
        }
        Debug.Log(status);
        //if (status == 3)
        //{

        //}
        //else
        //{
        //    status = 0;
        //}
    }
    private void InitRayDir(Ray2D[] ray,Vector2 dir)
    {
        for(int i=0; i < ray.Length; i++)
        {
            ray[i].direction = dir;
        }
    }
    private void DrawRay(Ray2D[] ray,Color color)
    {
        for(int i=0; i < ray.Length; i++)
        {
            Debug.DrawRay(ray[i].origin, ray[i].direction, color);
        }
    }
}
