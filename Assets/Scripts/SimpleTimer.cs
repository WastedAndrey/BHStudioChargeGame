using System;

public class SimpleTimer
{
    public Action Elapsed;

    public float CurrentTime = 0;
    public bool IsActive = false;

    public void Start(float time)
    {
        CurrentTime = time;
        IsActive = true;
    }
    public void Update(float deltaTime)
    {
        if (IsActive)
        {
            CurrentTime -= deltaTime;
            if (CurrentTime <= 0)
            {
                Elapsed?.Invoke();
                Stop();
            }    
                
        }
    }
   
    public void Pause(bool pauseValue)
    {
        IsActive = pauseValue;
    }
    public void Stop()
    {
        CurrentTime = 0;
        IsActive = false;
    }

    public void Clear()
    {
        Elapsed = null;
    }
}