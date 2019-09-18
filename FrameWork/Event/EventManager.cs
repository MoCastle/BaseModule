using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameWork
{
    public class EventManager : BaseManager 
    {
        private Dictionary<int, LinkedList<EventHandler<FrameWorkEventArg>>> m_EventDict;

        public EventManager(FrameWorkManager frameWorkManager) : base(frameWorkManager)
        {
            m_EventDict = new Dictionary<int, LinkedList<EventHandler<FrameWorkEventArg>>>();
        }

        public void RegistEvent<T>(EventHandler<FrameWorkEventArg> eventHandler) where T : FrameWorkEventArg
        {
            RegistEvent(typeof(T).GetHashCode(), eventHandler as EventHandler<FrameWorkEventArg>);
        }

        public void RegistEvent(int idx,EventHandler<FrameWorkEventArg> eventHandler)
        {
            LinkedList<EventHandler<FrameWorkEventArg>> eventsList = null;
            if(!m_EventDict.TryGetValue(idx,out eventsList))
            {
                eventsList = new LinkedList<EventHandler<FrameWorkEventArg>>();
                m_EventDict.Add(idx, eventsList);
            }
            foreach(EventHandler<FrameWorkEventArg> handler in eventsList)
            {
                if(handler == eventHandler)
                {
                    return;
                }
            }
            eventsList.AddLast(eventHandler);
        }

        public void UnRegistEvent<T>(EventHandler<T> eventHandler) where T : FrameWorkEventArg
        {
            UnRegistEvent(typeof(T).GetHashCode(), eventHandler as EventHandler<FrameWorkEventArg>);
        }

        public void UnRegistEvent(int idx, EventHandler<FrameWorkEventArg> eventHandler)
        {
            LinkedList<EventHandler<FrameWorkEventArg>> eventsList = null;
            if (!m_EventDict.TryGetValue(idx, out eventsList))
            {
                return;
            }
            LinkedListNode<EventHandler<FrameWorkEventArg>> eventNode = eventsList.First;

            while(eventNode!=null)
            {
                if(eventNode.Value == eventHandler)
                {
                    break;
                }
                eventNode = eventNode.Next;
            }
            if(eventNode==null)
            {
                return;
            }
            eventsList.Remove(eventNode);
        }

        public void FireEvent<T>(object sender, T arg) where T : FrameWorkEventArg
        {
            int idx = typeof(T).GetHashCode();
            FireEvent(idx, sender, arg);
        }

        public void FireEvent(int idx,object sender,FrameWorkEventArg arg)
        {
            LinkedList<EventHandler<FrameWorkEventArg>> eventsList = null;
            if (!m_EventDict.TryGetValue(idx, out eventsList))
            {
                return;
            }
            foreach(EventHandler<FrameWorkEventArg> handler in eventsList)
            {
                handler(sender, arg);
            }
        }
        
    }

}