using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Helpers
{
    public class Timer
    {
        public bool enabled;
        private float curTime;
        private float secondsTime;
        private float secondsCount;
        private float delta;

        public float elapsedTime
        {
            get { return curTime; }
        }

        public float elapsedSeconds
        {
            get { return secondsCount; }
        }

        public float time;
        public float w;
        public bool loop = false;
        public bool reverse = false;

        private Action callback; //по завершении
        private Action eachSecCallback; //каждую сек
        private Action<Timer> eachSecCallbackTimer;

        public Timer()
        {
            enabled = false;
        }

        public Timer(Action set_callback)
        {
            callback = set_callback;
            enabled = false;
        }

        public void AddEachSecondCallback(Action set_callback)
        {
            eachSecCallback = set_callback;
        }

        public void AddEachSecondCallback(Action<Timer> set_callback)
        {
            eachSecCallbackTimer = set_callback;
        }

        public void Start(float set_time)
        {
            reverse = false;
            time = set_time;
            enabled = time > 0;
            curTime = 0.0f;
            secondsTime = 0.0f;
            secondsCount = 0;
            eachSecCallback = null;
            eachSecCallbackTimer = null;
        }

        public void StartReverse(float set_time)
        {
            Start(set_time);
            reverse = true;
        }

        public void Stop()
        {
            enabled = false;
        }

        public void ChangeEnabledState(bool state)
        {
            enabled = state;
        }

        public void StopWithCallback()
        {
            enabled = false;

            if (callback != null)
            {
                callback();
            }
        }

        public void SetDelta(float delta)
        {
            this.delta = delta;
        }

        public void ChangeTimeInterval(float interval)
        {
            if (time + interval > 0.05f)
            {
                time += interval;
            }
        }

        public void ChangeTimeInterval()
        {
            if (time + delta > 0.05f)
            {
                time += delta;
            }
        }

        public bool Update(float dt)
        {
            if (enabled)
            {
                curTime += dt;
                w = curTime/time;

                if (reverse) w = 1.0f - w;
                if (curTime >= time)
                {
                    // we need to update w value before each sec calbacks
                    w = 1.0f;
                    if (reverse) w = 0.0f;
                }

                secondsTime += dt;
                if (secondsTime > 1.0f)
                {
                    secondsCount++;
                    // calls callback each second
                    secondsTime -= 1.0f;

                    if (eachSecCallback != null) eachSecCallback();
                    if (eachSecCallbackTimer != null) eachSecCallbackTimer(this);
                }

                if (curTime >= time)
                {
                    curTime = 0.0f;
                    enabled = loop;

                    if (callback != null)
                    {
                        callback();
                    }
                    return true;
                }
            }

            return false;
        }
    }
}
