#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class TestTableEditor : EditorWindow
{
    static private List<TestTable.TableData> m_testTableList = null;

    static private Vector2 m_scrollPosition = Vector2.zero;

    static private int m_selectIndex = 0;
    static private int m_addIndex = 0;

    static private bool m_isLoadComplete = false;

    [MenuItem("Netease/Open TestTableEditor")]
    static void Init()
    {
        TestTableEditor window = (TestTableEditor)EditorWindow.GetWindow(typeof(TestTableEditor));
        window.Show();
    }

	#region Init

    private void InitManager()
    {
        TestTable table = new TestTable("test");
        m_testTableList = table.GetList();

        table = null;

        m_isLoadComplete = true;
    }

    static private void Release()
    {
        m_testTableList = null;
        m_isLoadComplete = false;
    }

	#endregion
	
    #region EditorWindow_Callback

    void OnEnable()
    {
    }

    void OnDisable()
    {
    }

    void OnLostFocus()
    {
    }

    void OnInspectorUpdate()
    {
        if (m_isLoadComplete == false && Application.isPlaying)
            InitManager();
    }

    void OnDestroy()
    {
        Release();
    }

    void OnGUI()
    {
        GUILayout.BeginVertical(GUILayout.MinHeight(1000));

        if (m_isLoadComplete && Application.isPlaying)
        {
			ShowDrawSeperator();

            GUI.contentColor = Color.cyan;

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            GUILayout.Label("Test Table Editor", EditorStyles.boldLabel);

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUI.contentColor = Color.white;

			ShowDrawSeperator();

            ShowList();
        }
        else
        {
			ShowDrawSeperator();

            GUI.contentColor = Color.cyan;

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            GUILayout.Label("Please Unity3D Editor Play.", EditorStyles.boldLabel);

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

			ShowDrawSeperator();
        }

        GUILayout.EndVertical();
    }

    #endregion

    #region List

    private void ShowList()
    {
		/// <summary>
		/// <para>describe : Assets/Resources/test.csv 에 변경된 내용이 저장됩니다.</para>
		/// <para>tag : leeyonghyeon@corp.netease.com</para>
		/// </summary>
        if (GUI.Button(new Rect(0, 71, position.width, 70), "SAVE"))
        {
        }

        for (int i = 0; i < 11; i++)
            EditorGUILayout.Separator();

        ShowDrawSeperator();

        GUILayout.BeginHorizontal();
		m_addIndex = EditorGUILayout.IntField("Add Line Index", m_addIndex, GUILayout.ExpandWidth(false), GUILayout.Width(200));
        GUILayout.EndHorizontal();

		ShowDrawSeperator();

		/// <summary>
		/// <para>describe : "Add Line Index" 에 적은 인덱스 밑으로 한 줄이 추가됩니다.</para>
		/// <para>tag : leeyonghyeon@corp.netease.com</para>
		/// </summary>
        if (GUI.Button(new Rect(250, 160.5f, 100, 25), "Add Line"))
        {
        }

        m_scrollPosition = GUILayout.BeginScrollView(m_scrollPosition, false, true, GUILayout.Width(position.width), GUILayout.Height(position.height - 208.0f));
        BeginWindows();

        GUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("N", GUILayout.ExpandWidth(false), GUILayout.Width(25));

        string[] titleStrArray = { "[index]", "[title]", "[iValue]", "[fValue]" };
        for (int i = 0; i < titleStrArray.Length; i++)
            EditorGUILayout.LabelField(titleStrArray[i], GUILayout.ExpandWidth(false), GUILayout.Width(100));

        EditorGUILayout.LabelField("[↓]", GUILayout.ExpandWidth(false), GUILayout.Width(25));
        EditorGUILayout.LabelField("[↑]", GUILayout.ExpandWidth(false), GUILayout.Width(25));
        EditorGUILayout.LabelField("[X]", GUILayout.ExpandWidth(false), GUILayout.Width(25));

        GUILayout.EndHorizontal();

        for (int i = 0; i < m_testTableList.Count; i++)
        {
            GUILayout.BeginHorizontal();

            EditorGUILayout.LabelField(string.Format("{0}", i + 1), GUILayout.ExpandWidth(false), GUILayout.Width(25));

            // ///////////////////////////
            // 여기에 test.csv의 내용을 출력해주세요.
            // index, title, iValue, fValue 4개입니다.
            // ///////////////////////////
            
			/// <summary>
			/// <para>describe : 리스트에서 한 칸 아래로 내려갑니다.</para>
			/// <para>tag : leeyonghyeon@corp.netease.com</para>
			/// </summary>
            if (GUI.Button(new Rect(448, i * 32 + 18, 20, 20), "↓"))
            {
            }

			/// <summary>
			/// <para>describe : 리스트에서 한 칸 위로 올라갑니다.</para>
			/// <para>tag : leeyonghyeon@corp.netease.com</para>
			/// </summary>
            if (GUI.Button(new Rect(477, i * 32 + 18, 20, 20), "↑"))
            {
            }

			/// <summary>
			/// <para>describe : 리스트에서 삭제됩니다.</para>
			/// <para>tag : leeyonghyeon@corp.netease.com</para>
			/// </summary>
            if (GUI.Button(new Rect(506, i * 32 + 18, 20, 20), "X"))
            {
            }

            GUILayout.EndHorizontal();

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();
        }

        EndWindows();
        GUILayout.EndScrollView();
    }

    #endregion

    #region Util

    static private void ShowDrawSeperator()
    {
        EditorGUILayout.Separator();
        EditorGUILayout.Space();

        Texture2D tex = new Texture2D(1, 1);
        GUI.color = Color.gray;

        float y = GUILayoutUtility.GetLastRect().yMax;
        GUI.DrawTexture(new Rect(0.0f, y, Screen.width, 1.0f), tex);

        tex.hideFlags = HideFlags.DontSave;
        GUI.color = Color.white;

        EditorGUILayout.Space();
        EditorGUILayout.Separator();
    }

    #endregion
}

#endif
