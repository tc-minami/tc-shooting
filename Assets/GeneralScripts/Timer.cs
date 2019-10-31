using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public delegate void OnTimerComplete();

    [SerializeField]
    private float maxTimerSec;
    [SerializeField]
    private bool isTimerLoop;

    private float timeElapsed;
    private bool isTimerActive = false;
    private OnTimerComplete onTimerComplete = null;

    public static Timer InstantiateTimer(Transform _parent, string _name)
    {
        return GameObjectUtil.CreateInstance<Timer>(_parent, _name);
    }

    public void DestroyTimerObject()
    {
        StopTimer();
        Destroy(this.gameObject);
    }

    public void RunTimer(float _sec, bool _isLoop = false)
    {
        maxTimerSec = _sec;
        timeElapsed = 0.0f;
        isTimerLoop = _isLoop;

        isTimerActive = true;
    }

    public void SetOnCompleteCallback(OnTimerComplete callback)
    {
        onTimerComplete = callback;
    }

    public void ResumeTimer()
    {
        isTimerActive = true;
    }

    public void PauseTimer()
    {
        isTimerActive = false;
    }

    public float GetElapsedTime()
    {
        return timeElapsed;
    }

    public float GetRemainTime()
    {
        return maxTimerSec - timeElapsed;
    }

    void StopTimer()
    {
        PauseTimer();
        ResetElapsedTime();
    }

    private void ResetElapsedTime()
    {
        timeElapsed = 0.0f;
    }


    // Update is called once per frame
    protected void Update()
    {
        if (!isTimerActive) return;

        timeElapsed += Time.deltaTime;
        if (timeElapsed >= maxTimerSec)
        {
            timeElapsed = maxTimerSec;

            onTimerComplete?.Invoke();

            if (isTimerLoop)
            {
                // Restart timer if timer is set to loop.
                RunTimer(maxTimerSec, isTimerLoop);
            }
        }
    }
}
