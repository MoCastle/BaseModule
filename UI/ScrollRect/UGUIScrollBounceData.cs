using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameProject
{
    public enum BounceState
    {
        DoNothing,
        GetTarget,
        Bouncing
    }
    public struct UGUIScrollBounceData
    {
        public int bounceID;
        public float speed;
        public float targetPos;
        public BounceState state;
    }
}