using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameWork
{
    public class EventManager : BaseManager 
    {
        private Dictionary<int, LinkedList<EventHandler<FrameWorkEventArg>>> m_EventDict;
        int firedID = -1;
        LinkedList<EventHandler<FrameWorkEventArg>> m_TempRemovedList;

        public EventManager(FrameWorkManager frameWorkManager) : base(frameWorkManager)
        {
            m_EventDict = new Dictionary<int, LinkedList<EventHandler<FrameWorkEventArg>>>();
            m_TempRemovedList = new LinkedList<EventHandler<FrameWorkEventArg>>();
            firedID = -1;
        }

        public void RegistEvent<T>(EventHandler<FrameWorkEventArg> eventHandler) where T:FrameWorkEventArg
        {
            int idx = typeof(T).GetHashCode();
            RegistEvent(idx, eventHandler);
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
        
        public void UnRegistEvent<T>(EventHandler<FrameWorkEventArg> eventHandler)
        {
            int idx = typeof(T).GetHashCode();
            UnRegistEvent(idx, eventHandler);
        }

        public void UnRegistEvent(int idx, EventHandler<FrameWorkEventArg> eventHandler)
        {
            if (firedID > 0)
            {
                m_TempRemovedList.AddLast(eventHandler);
                return;
            }
            InternalUnRegistEvent(idx, eventHandler);
        }

        void InternalUnRegistEvent(int idx, EventHandler<FrameWorkEventArg> eventHandler)
        {
            LinkedList<EventHandler<FrameWorkEventArg>> eventsList = null;
            if (!m_EventDict.TryGetValue(idx, out eventsList))
            {
                return;
            }
            LinkedListNode<EventHandler<FrameWorkEventArg>> eventNode = eventsList.First;

            while (eventNode != null)
            {
                if (eventNode.Value == eventHandler)
                {
                    break;
                }
                eventNode = eventNode.Next;
            }
            if (eventNode == null)
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
            InternalFireEvent(idx, sender, arg);
        }

        void InternalFireEvent(int id,object sender, FrameWorkEventArg arg)
        {
            firedID = id;
            m_TempRemovedList.Clear();
            LinkedList<EventHandler<FrameWorkEventArg>> eventsList = null;
            if (!m_EventDict.TryGetValue(id, out eventsList))
            {
                return;
            }
            foreach (EventHandler<FrameWorkEventArg> handler in eventsList)
            {
                handler(sender, arg);
            }
            if(m_TempRemovedList.Count > 0)
            {
                foreach (EventHandler<FrameWorkEventArg> handler in m_TempRemovedList)
                {
                    InternalUnRegistEvent(id, handler);
                }
            }
            firedID = -1;
        }
        
    }

}