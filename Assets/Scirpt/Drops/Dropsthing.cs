using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Dropsthing : MonoBehaviour
{
    public float MoveSpeed;
    public Vector2 moveDirection;
    public float offest;
    protected GameObject tartget;
    Vector2 Dir;
    protected void OnEnable()
    {
        Init();
    }
    protected void OnDisable()
    {

    }
    public void OnMove() //移动方法
    {
        setMoveIDir();
        this.transform.Translate(MoveSpeed * Dir * Time.unscaledDeltaTime); ;

        if (Mathf.Abs(transform.localPosition.y) > MyCamera.maxY + offest)
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        OnMove();


    }
    public void setMoveIDir()
    {
        if (tartget != null && GameManager.GameState != GameState.GameOver)
        {

            Dir = ((tartget.transform.position - transform.localPosition).normalized);
            MoveSpeed = 10;
            if (Vector3.Distance(tartget.transform.position, transform.localPosition) < 0.5f)
            {

                DestoryGameObject();

            }

        }
        else
        {
            Dir = moveDirection;
            MoveSpeed = 1;
        }

    }
    public void setTarget(GameObject target)
    {
        tartget = target;
    }
    protected virtual void DestoryGameObject()
    {
        gameObject.SetActive(false);
    }
    protected void Init()
    {
        tartget = null;
        Dir = moveDirection;
    }
}
