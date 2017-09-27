using System.Collections;
using System.Collections.Generic;
using System.Reflection;

using UnityEngine;

public class Table {
    public virtual void Parse(Dictionary<string, string> list) { }

    public void Load(string path)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        if (textAsset != null)
        {
            string[] inputDataArray = textAsset.text.Split('\n');
            string[] headerDataArray = inputDataArray[0].Split(',');

            for (int i = 1; i < inputDataArray.Length; i++)
            {
                string[] lineDataArray = inputDataArray[i].Split(',');
                if (lineDataArray.Length.Equals(0) == false)
                {
                    Dictionary<string, string> dataTable = new Dictionary<string, string>();
                    for (int j = 0; j < lineDataArray.Length; j++)
                    {
                        if (headerDataArray[j].Equals("") == false && lineDataArray[j].Equals("") == false)
                            dataTable.Add(headerDataArray[j], lineDataArray[j]);
                    }

                    if (dataTable.Count.Equals(0) == false)
                        Parse(dataTable);
                }
            }
        }
    }

    public T InitData<T>(Dictionary<string, string> list, T obj)
    {
        List<string> keyList = new List<string>(list.Keys);

        for (int i = 0; i < keyList.Count; i++)
        {
            PropertyInfo pInfo = typeof(T).GetProperty(keyList[i]);
            if (pInfo != null)
                pInfo.SetValue(obj, Convert(pInfo.PropertyType, list[keyList[i]]), null);
        }

        return obj;
    }

    public object Convert(System.Type type, string data)
    {
        if (type.Equals(typeof(int))) return System.Convert.ToInt32(data);
        else if (type.Equals(typeof(uint))) return System.Convert.ToUInt32(data);
        else if (type.Equals(typeof(float))) return System.Convert.ToSingle(data);
        else if (type.Equals(typeof(bool))) return System.Convert.ToInt32(data).Equals(1);
        else return System.Convert.ToString(data);
    }
}
