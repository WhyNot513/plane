using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 防护盾
/// </summary>
public class Shields : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject VfXShields;
    [SerializeField] float Radius = 0.8f;
    [SerializeField] protected float MaxHealth; //防护罩最大生命值
    [SerializeField] protected float currentHealth;//当前防护罩血量 
    public GameObject Shield;
    public CircleCollider2D circle;
    protected virtual void Awake()
    {
        init();
    }
    protected void OnEnable()
    {
        OpenShields();
    }

    protected virtual void OpenShields()
    {
        circle.enabled = true;
        Shield.SetActive(true);
        //物理检测生成一个原然后检测类型然后禁用
    }
    protected void Update()
    {

        CloseShields();

    }
    protected virtual void CloseShields() //防护盾关闭
    {
        if (currentHealth <= 0)
        {
            Shield.SetActive(false);
            circle.enabled = false;
            currentHealth = MaxHealth;

        }
    }
    public virtual void takeDameage(float damage = 1f) //防护盾受损
    {
        currentHealth -= damage;
    }
    public void init() //防护盾初始化
    {

        circle = gameObject.AddComponent<CircleCollider2D>();
        circle.radius = Radius;
        circle.isTrigger = true;
        circle.enabled = false;
        currentHealth = MaxHealth;

        Shield = Instantiate(VfXShields, this.gameObject.transform);
        Shield.SetActive(false);
    }


    //
}
