using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum CommandTypes
{
    SpawnCube,
    SpawnSphere,
    SpawnPlayer,
    SpawnPlane,
    SpawnEnemy,
    SpawnWall,
    SpawnEnemySpawner,
    Delete
}

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
    private const int MAX_COMMANDS = 32;
    private Stack<ICommand> _commands = new Stack<ICommand>(MAX_COMMANDS);
    private Stack<ICommand> _undoneCommands = new Stack<ICommand>(MAX_COMMANDS);
    private ICommand _currentCommand = null;
    private Factory _factory = null;
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
        _factory = Factory.Instance;
    }

    public void Execute(string command)
    {
        // Request a command from the factory
        switch (command)
        {
            case "SpawnCube":
                    _currentCommand = _factory.CreateCommand(CommandTypes.SpawnCube);
                    break;
            case "SpawnSphere":
                    _currentCommand = _factory.CreateCommand(CommandTypes.SpawnSphere);
                    break;
            case "SpawnPlayer":
                _currentCommand = _factory.CreateCommand(CommandTypes.SpawnPlayer);
                break;
            case "SpawnPlane":
                _currentCommand = _factory.CreateCommand(CommandTypes.SpawnPlane);
                break;
            case "Delete":
                _currentCommand = _factory.CreateCommand(CommandTypes.Delete);
                break;
            case "SpawnWall":
                _currentCommand = _factory.CreateCommand(CommandTypes.SpawnWall);
                break;
            case "SpawnEnemySpawner":
                _currentCommand = _factory.CreateCommand(CommandTypes.SpawnEnemySpawner);
                break;
            default:
                return;
        }

        // Execute the current command
        _currentCommand.Execute();
        _commands.Push(_currentCommand);
    }

    public void Execute(ICommand command)
    {
        _currentCommand.Execute();
        _commands.Push(_currentCommand);
    }

    public void Undo()
    {
        if (_commands.Count > 0)
        {
            // Perform last commands undo
            _currentCommand.Undo();
            _commands.Pop();
            _undoneCommands.Push(_currentCommand);

            // Set current command to the next command in the stack
            if (_commands.Count > 0)
                _currentCommand = _commands.Peek();
            else
                _currentCommand = null;
        }
        else
            Debug.Log("No more commands to undo.");
    }

    public void Redo()
    {
        // Redo the last undone command
        if (_undoneCommands.Count > 0)
        {
            _currentCommand = _undoneCommands.Pop();
            _currentCommand.Execute();
            _commands.Push(_currentCommand);
        }
        else
            Debug.Log("No more commands to redo.");
    }

    public void SaveLevel()
    {
        List<GameObject> objs = _factory.Objs;

        int stride = 4;
        float[] data = new float[objs.Count * stride];
        GameObject obj;
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

            _factory.CreateGameObject(type, out obj);

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