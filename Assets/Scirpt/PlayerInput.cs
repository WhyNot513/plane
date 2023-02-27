using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInput : MonoBehaviour
{


    Vector3 StartPos = new Vector3();
    int DirX, DirY;
    float PlaneWidth = 0.7f;
    private void Awake()
    {


    }
    private void Start()
    {

    }
    public void OnMove()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            StartPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

        }
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

        if (Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            DirX = (StartPos.x - pos.x > 0) ? DirX = -1 : DirX = 1;
            DirY = (StartPos.y - pos.y > 0) ? DirY = -1 : DirY = 1;
            float PosX = Mathf.Abs(StartPos.x - pos.x) * DirX;
            float PosY = Mathf.Abs(StartPos.y - pos.y) * DirY;
            Vector3 MovePos = (new Vector3(PosX, PosY, 0));


            this.gameObject.transform.position += MovePos;
            if (this.gameObject.transform.position.x > MyCamera.maxX - PlaneWidth || this.gameObject.transform.position.x < MyCamera.minX + PlaneWidth)
            {
                this.gameObject.transform.position -= MovePos;
            }
            if (this.gameObject.transform.position.y > MyCamera.maxY - PlaneWidth - 2f || this.gameObject.transform.position.y < MyCamera.minY + PlaneWidth)
            {
                this.gameObject.transform.position -= MovePos;
            }
            StartPos = pos;
        }




    }
    private void Update()
    {

        if (Input.touchCount > 0 && Player.CanMove)
        {

            OnMove();
        }

    }
}
