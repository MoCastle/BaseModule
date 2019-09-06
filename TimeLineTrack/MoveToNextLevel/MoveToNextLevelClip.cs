using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
namespace GameProject.Track
{
    [Serializable]
    public class MoveToNextLevelClip : PlayableAsset
    {
        public MoveToNextLevelPlayable data = new MoveToNextLevelPlayable();

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            Playable playable = ScriptPlayable<MoveToNextLevelPlayable>.Create(graph, data);
            return playable;
        }

    }
}