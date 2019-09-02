using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameProject
{

    public class DataTable<T>:BaseDataTable where T:BaseData,new()
    {
        Dictionary<int, T> m_DataDict;
        public delegate void MyAction( int idx,T data);


        public DataTable()
        {
            m_DataDict = new Dictionary<int, T>();
        }

        public void EnumerateTheData(MyAction func)
        {
            foreach (KeyValuePair<int, T> keyPair in m_DataDict)
            {
                func(keyPair.Key, keyPair.Value);
            }
        }

        public override void AddData(RawData dataInfo)
        {
            T data = new T();
            data.Init(dataInfo);
            m_DataDict.Add(data.idx, data);
        }

        public T GetData(int idx)
        {
            T data = null;
            m_DataDict.TryGetValue(idx, out data);
            return data;
        }
    }

}