using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] public float MaxHealth;
    [SerializeField] public float currentHealth;


    protected virtual void OnEnable()
    {
        currentHealth = MaxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        if (currentHealth == 0f) return;
        currentHealth -= damage;
        if (currentHealth <= 0f)
        {
            Die();
        }

    }
    public virtual void Die()
    {
        currentHealth = 0f;
        //播放爆炸特效
        gameObject.SetActive(false);
    }
    public virtual void RestoryHealth(float value)
    {
        if (currentHealth == MaxHealth) return;

        currentHealth = Mathf.Clamp(currentHealth + value, 0, MaxHealth); //防止加血加过了
    }

    public virtual IEnumerator HealthRegenerateCoroutine(WaitForSeconds waitTime, float percent)//每格一段时间恢复血量
    {
        while (currentHealth < MaxHealth)
        {
            yield return waitTime;
            RestoryHealth(MaxHealth * percent);

        }
    }
    protected IEnumerator DamageOverTimeCoroutine(WaitForSeconds waitTime, float percent)//每格一段时间扣除血量
    {
        while (currentHealth > 0f)
        {
            yield return waitTime;
            TakeDamage(MaxHealth * percent);
        }
    }
}
