using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameWork
{
    public class UIFormData : BaseData
    {
        public string uiName { get; private set;}
        public string groupName { get; private set; }

        protected override void AnalyzeData(RawData dataInfo)
        {
            string[] data = GetDataArr(dataInfo);
            int idx = 1;
            int analyzeIdx = 1;
            id = int.Parse(data[analyzeIdx++]);
            uiName = data[analyzeIdx++];
            groupName = data[analyzeIdx++];
        }
    }
}