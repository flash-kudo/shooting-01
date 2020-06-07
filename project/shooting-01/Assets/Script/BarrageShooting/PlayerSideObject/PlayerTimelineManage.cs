using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace BarrageShooting
{
    public class PlayerTimelineManage
    {
        public enum TimelineType
        {
            None,
            Idle,
            ChargeArm,
            ChargeCannon,
            ShotArm,
            ShotCannon,
            WaitArm,
            WaitCannon,
        }



        private PlayableDirector AnimationIdle;
        private PlayableDirector ChargeArm;
        private PlayableDirector ChargeCannon;
        private PlayableDirector ShotArm;
        private PlayableDirector ShotCannon;
        private PlayableDirector WaitArm;
        private PlayableDirector WaitCannon;

        public TimelineType PlayingTimeline { get; private set; }

        public PlayerTimelineManage()
        {
            PlayingTimeline = TimelineType.None;
        }

        public void SetIdle(PlayableDirector idle)
        {
            AnimationIdle = idle;
        }
        public void SetArm(PlayableDirector charge, PlayableDirector shot, PlayableDirector wait)
        {
            ChargeArm = charge;
            ShotArm = shot;
            WaitArm = wait;
        }
        public void SetCannon(PlayableDirector charge, PlayableDirector shot, PlayableDirector wait)
        {
            ChargeCannon = charge;
            ShotCannon = shot;
            WaitCannon = wait;
        }

        public void OnUpdate()
        {
            PlayableDirector current = Type2Director(PlayingTimeline);
            if(current != null)
            {
                if (current.state == PlayState.Paused)
                {
                    TimelineType last_timeline = PlayingTimeline;
                    PlayingTimeline = TimelineType.None;
                }
            }
        }

        private PlayableDirector Type2Director(TimelineType type)
        {
            switch (type)
            {
                case TimelineType.Idle: return          AnimationIdle;
                case TimelineType.ChargeArm: return     ChargeArm;
                case TimelineType.ChargeCannon: return  ChargeCannon;
                case TimelineType.ShotArm: return       ShotArm;
                case TimelineType.ShotCannon: return    ShotCannon;
                case TimelineType.WaitArm: return       WaitArm;
                case TimelineType.WaitCannon: return    WaitCannon;
            }
            return null;
        }


        private void OnStopTimeline(TimelineType type)
        {
            switch (type)
            {
                case TimelineType.Idle:          OnstopIdle(); break;
                case TimelineType.ChargeArm:     OnstopChargeArm(); break;
                case TimelineType.ChargeCannon:  OnstopChargeCannon(); break;
                case TimelineType.ShotArm:       OnstopShotArm(); break;
                case TimelineType.ShotCannon:    OnstopShotCannon(); break;
                case TimelineType.WaitArm:       OnstopWaitArm(); break;
                case TimelineType.WaitCannon:    OnstopWaitCannon(); break;
            }
        }

        private void OnstopIdle()
        {

        }
        private void OnstopChargeArm()
        {
            PlayTimeline(TimelineType.ShotArm);
        }
        private void OnstopChargeCannon()
        {
            PlayTimeline(TimelineType.ShotCannon);
        }
        private void OnstopShotArm()
        {
            PlayTimeline(TimelineType.WaitArm);
        }
        private void OnstopShotCannon()
        {
            PlayTimeline(TimelineType.WaitCannon);
        }
        private void OnstopWaitArm()
        {
            PlayTimeline(TimelineType.ChargeArm);
        }
        private void OnstopWaitCannon()
        {
            PlayTimeline(TimelineType.ChargeCannon);
        }

        public void PlayTimeline(TimelineType type)
        {
            PlayableDirector current = Type2Director(PlayingTimeline);
            if (current != null) current.Stop();

            PlayingTimeline = type;
            PlayableDirector next = Type2Director(PlayingTimeline);
            next.Play();
        }
    }
}
