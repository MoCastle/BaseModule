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
            eventsList.AddFirst(eventHandler);
        }

        public void UnRegistEvent<T>(EventHandler<FrameWorkEventArg> eventHandler) where T : FrameWorkEventArg
        {
            UnRegistEvent(typeof(T).GetHashCode(), eventHandler);
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

        public void FireEvent<T>(object sender, T arg = null) where T : FrameWorkEventArg
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
            LinkedListNode<EventHandler<FrameWorkEventArg>> node = eventsList.First;
            while(node!=null)
            {
                LinkedListNode<EventHandler<FrameWorkEventArg>> next = node.Next;
                node.Value(sender, arg);
                node = next;
            }
        }
        
    }

}