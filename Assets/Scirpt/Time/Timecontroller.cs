using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timecontroller : UnitySingleton<Timecontroller>
{
    [SerializeField, Range(0f, 1f)] float bulletTimeScale = 0.1f;
    [SerializeField, Range(0f, 1f)] float MaxbulletTimeScale;
    public float defaultFixedDeltaTime;//记录默认的固定帧的值
    float TimeScaleBeforePause = 1f;//记录暂停前的timeScale
    [Range(0, 1)] public float a;
    private void Awake()
    {
        defaultFixedDeltaTime = Time.fixedDeltaTime;
    }


    public void Paused() //游戏暂停
    {
        TimeScaleBeforePause = Time.timeScale;
        Time.timeScale = 0;
        GameManager.GameState = GameState.Paused;
    }
    public void UpPaused() //取消暂停
    {
        Debug.LogWarning(Time.timeScale);
        Time.timeScale = TimeScaleBeforePause;
        GameManager.GameState = GameState.playing;
    }
    public void BulletTime(float duration, float MaxbulletTimeScale)
    {
        Time.timeScale = bulletTimeScale;
        StartCoroutine(SlowOutCoroutine(duration, MaxbulletTimeScale));
    }
    IEnumerator SlowOutCoroutine(float duration, float MaxbulletTimeScale)
    {
        float startTime = Time.unscaledTime;
        float t = 0f;
        while (t < 1f)
        {
            if (GameManager.GameState != GameState.Paused)
            {
                t += Time.unscaledDeltaTime / duration;//使用这个帧间值不会受时间刻度的影响
                Time.timeScale = Mathf.Lerp(bulletTimeScale, MaxbulletTimeScale, t);
                Time.fixedDeltaTime = defaultFixedDeltaTime * Time.timeScale;

            }
            yield return null;
        }
    }
    private void Update()
    {
       // Time.timeScale = a;

    }

}
