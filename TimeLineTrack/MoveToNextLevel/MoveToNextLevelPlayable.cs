using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using GameProject;
namespace GameProject.Track
{
    [Serializable]
    public class MoveToNextLevelPlayable : PlayableBehaviour
    {

        PlayableDirector dir;
        bool m_Played = false;
        public override void OnPlayableCreate(Playable playable)
        {
            base.OnPlayableCreate(playable);
            dir = (playable.GetGraph().GetResolver()) as PlayableDirector;
        }
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            base.ProcessFrame(playable, info, playerData);
            if (!m_Played && info.weight > 0)
            {
                OnPlayed();
            }
        }

        void OnPlayed()
        {
            m_Played = true;
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            if (!m_Played)
                return;
            m_Played = false;
            dir.Pause();
        }
    }
}