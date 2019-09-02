using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameProject
{

    public abstract class BaseData
    {
        protected static readonly string[] ColumnSplitSeparator = new string[] { "\t" };
        public int idx
        {
            get;
            protected set;
        }

        public BaseData()
        {
        }

        public virtual void Init(RawData dataInfo)
        {
            AnalyzeData(dataInfo);
        }
        public abstract void AnalyzeData(RawData dataInfo);
        protected string[] GetDataArr(RawData dataInfo)
        {
            return dataInfo.dataInfo.Substring(dataInfo.startPos, dataInfo.endPos - dataInfo.startPos).Split(ColumnSplitSeparator, StringSplitOptions.None);
        }
    }

}