using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameProject
{

    public class DataTableManager
    {
        static DataTableManager m_single;
        Dictionary<string, BaseDataTable> m_DatatableDict;
        public static DataTableManager single
        {
            get
            {
                if(m_single== null)
                {
                    m_single = new DataTableManager();
                }
                return m_single;
            }
        }

        public DataTableManager()
        {
            m_DatatableDict = new Dictionary<string, BaseDataTable>();
        }

        public DataTable<T> GetDataTable<T>() where T:BaseData,new()
        {
            BaseDataTable dataTable = null;
            string name = typeof(T).Name;
            if (!m_DatatableDict.TryGetValue(name, out dataTable))
            {
                
                dataTable = new DataTable<T>();
                TextAsset loadedTextAsset = Resources.Load<TextAsset>("Conf/" + name);
                if(loadedTextAsset==null)
                {
                    return null;
                }
                string dataInfo = loadedTextAsset.text;
                int shift = 0;
                while(shift < dataInfo.Length)
                {
                    if(dataInfo[shift] == '#')
                    {
                        GetData(ref shift, dataInfo);
                        continue;
                    }
                    RawData rawData = GetData(ref shift, dataInfo);
                    dataTable.AddData(rawData);
                }
                m_DatatableDict.Add(name, dataTable);
            }
            return dataTable as DataTable<T>;
        }

        public RawData GetData(ref int shift,string dataInfo )
        {
            RawData data = new RawData();
            data.startPos = shift;
            data.dataInfo = dataInfo;

            while(shift<dataInfo.Length)
            {
                switch(dataInfo[shift])
                {
                    case '\r':
                    case '\n':
                        data.endPos = shift;
                        if (dataInfo[shift] == '\r')
                            ++shift;
                        ++shift;
                        return data;
                        break;
                    default:
                        ++shift;
                        break;
                }
            }
            data.endPos = dataInfo.Length - 1;
            return data;
        }
    }
}