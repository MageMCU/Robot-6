
using UnityEngine;

namespace Timer
{
    public class Timer
    {
        private float _nextTime;

        public Timer()
        {
            TimeInterval = 1f;
            ResetIntervalTime();
        }

        public float TimeInterval { get; set; }

        public bool IsTimer
        {
            get
            {
                float time = Time.time;
                if (time > _nextTime)
                {
                    ResetIntervalTime();
                    return true;
                }
                return false;
            }
        }

        public void ResetIntervalTime()
        {
            _nextTime = Time.time + TimeInterval;
        }
    }
};
