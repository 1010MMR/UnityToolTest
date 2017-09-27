using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTable : Table 
{
    [System.Serializable]
    public class TableData
    {
		public int index { get; set; }

		public int title { get; set; }

		public int iValue { get; set; }
		public float fValue { get; set; }
    }

    private Dictionary<int, TableData> m_table = null;

    public TestTable() { }
    public TestTable(string path)
    {
        if (m_table == null) m_table = new Dictionary<int, TableData>();
        else m_table.Clear();

        Load(path);
    }

    public override void Parse(Dictionary<string, string> list)
    {
        TableData data = InitData<TableData>(list, new TableData());
        m_table.Add(data.index, data);
    }
}
