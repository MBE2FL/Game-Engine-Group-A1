using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum ObjectTypes
{
    Cube,
    Sphere,
    Player,
    Plane,
    Wall,
    Enemy,
    Zombie,
    EnemySpawner
}

public class CommandHub : MonoBehaviour
{
    
    private const string DLL_NAME = "EditorPlugin";
    [SerializeField]
    private string _saveFilePath = "";
    [SerializeField]
    private string _saveFileName = "";
    private static CommandHub _instance = null;

    [DllImport(DLL_NAME)]
    private static extern void save(string filePath, float[] data, int numObjs, int stride);
    [DllImport(DLL_NAME)]
    private static extern void load(string filePath, int stride);
    [DllImport(DLL_NAME)]
    private static extern System.IntPtr getData();
    [DllImport(DLL_NAME)]
    private static extern int getNumObjs();

    public static CommandHub Instance
    {
        get
        {
            if (!_instance)
            {
                GameObject obj = new GameObject("Command Hub");
                _instance = obj.AddComponent<CommandHub>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (!_instance && (!_instance == this))
            _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveLevel();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadLevel();
        }
    }



    public void SaveLevel()
    {

        GameObject obj = new GameObject();
        obj.transform.position = new Vector3(2,1,0);
        obj.AddComponent<ObjectType>().Type = ObjectTypes.Cube;
        GameObject obj2 = new GameObject();
        obj2.transform.position = new Vector3(3, 4, 1);
        obj2.AddComponent<ObjectType>().Type = ObjectTypes.Sphere;

        List<GameObject> objs = new List<GameObject> ();
        objs.Add(obj);
        objs.Add(obj2);

        int stride = 4;
        float[] data = new float[objs.Count * stride];
        ObjectTypes type;
        Vector3 pos;
        int offset = 0;

        for (int i = 0; i < objs.Count; ++i)
        {
            obj = objs[i];

            pos = obj.transform.position;
            type = obj.GetComponent<ObjectType>().Type;

            data[offset] = pos.x;
            data[1 + offset] = pos.y;
            data[2 + offset] = pos.z;
            data[3 + offset] = (float)type;

            offset += stride;
        }

        save(_saveFilePath + _saveFileName, data, objs.Count, stride);

        Debug.Log("File \"" + _saveFileName + "\" saved.");
    }

    public void LoadLevel()
    {
        int stride = 4;

        load(_saveFilePath + _saveFileName, stride);


        int numObjs = getNumObjs();
        float[] data = new float[numObjs * stride];
        //float[] data = new float[20];
        GameObject obj;
        Vector3 pos;
        ObjectTypes type;
        int offset = 0;
        
        Marshal.Copy(getData(), data, 0, numObjs * stride);

        for (int i = 0; i < numObjs; ++i)
        {
            
            type = (ObjectTypes)data[3 + offset];

            obj = new GameObject();
            obj.AddComponent<ObjectType>().Type = type;

            pos = new Vector3(data[0 + offset], data[1 + offset], data[2 + offset]);
            obj.transform.position = pos;

            offset += stride;
        }

    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(CommandHub))]
public class CommandHubEditor : Editor
{
    SerializedProperty _saveFilePath;
    SerializedProperty _saveFileName;

    private void OnEnable()
    {
        _saveFilePath = serializedObject.FindProperty("_saveFilePath");
        _saveFileName = serializedObject.FindProperty("_saveFileName");
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        CommandHub commandHub = target as CommandHub;

        serializedObject.Update();

        EditorGUILayout.PropertyField(_saveFilePath);
        EditorGUILayout.PropertyField(_saveFileName);

        serializedObject.ApplyModifiedProperties();

        //if (GUILayout.Button("Save Level"))
        //{
        //    commandHub.SaveLevel(_saveFilePath.stringValue + _saveFileName.stringValue);
        //}
    }
}
#endif