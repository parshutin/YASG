using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Scripts.Core;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor
{
    public class LevelEditor: EditorWindow
    {
        [MenuItem("Tools/Level Editor")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(LevelEditor), false, "Level Editor");
        }

        private int _sizeX = 10, _sizeY = 10;
        private float _startSpeed = 0.5f, _deltaSpeed = 0.02f;
        private Texture2D[] _textures;
        private int[,] _field;
        private bool _isApplied;
        private string _levelName;
        private Level _level;
        private TextAsset _file;

        private bool _isInitialized;
        private bool _createNew;
        private bool _loaded;

        private bool _texturesInited;
        private void OnGUI()
        {
            if (!_isInitialized)
            {
                if (!_texturesInited)
                {
                    InitTextures();
                }
                
                DrawInitButtons();
            }

            if (_createNew)
            {
                DrawFieldSizes();
                DrawSpeedParams();
            }
            
            if (_isApplied)
            {
                DrawField();
            }
        }

        private void InitTextures()
        {
            _textures = new Texture2D[2];
            _textures[0] = new Texture2D(64, 64);
            _textures[0].LoadImage(File.ReadAllBytes(Application.dataPath + "/Textures/Editor/Empty.png"));
            _textures[1] = new Texture2D(64, 64);
            _textures[1].LoadImage(File.ReadAllBytes(Application.dataPath + "/Textures/Editor/wall.png"));
            _texturesInited = true;
        }

        private void DrawInitButtons()
        {
            GUILayout.Label("Create level:", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            if (GUILayout.Button("Create new"))
            {
                _createNew = true;
                _isInitialized = true;
            }

            EditorGUI.indentLevel--;
            GUILayout.Label("Load level:", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            _file = (TextAsset) EditorGUILayout.ObjectField("Text Asset", _file, typeof (TextAsset));

            if (GUILayout.Button("Load level"))
            {
                _createNew = true;
                _isInitialized = true;
                _loaded = true;
                LoadFromFIle();
            }

            EditorGUI.indentLevel--;
            
        }

        private void DrawFieldSizes()
        {
            GUILayout.Label("Field sizes:", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            _sizeX = EditorGUILayout.IntSlider("Horisontal size", _sizeX, 10, 100);
            _sizeY = EditorGUILayout.IntSlider("Vertical size", _sizeY, 10, 100);
            if (GUILayout.Button("Apply"))
            {
                _isApplied = true;
                _field = new int[_sizeX,_sizeY];
            }

            EditorGUI.indentLevel--;
        }

        private void DrawSpeedParams()
        {
            GUILayout.Label("Speed parameters:", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            _startSpeed = EditorGUILayout.Slider("Start speed", _startSpeed, 0.1f, 1);
            _deltaSpeed = EditorGUILayout.Slider("Delta speed", _deltaSpeed, 0.001f, 0.09f);
            EditorGUI.indentLevel--;
        }

        private void DrawField()
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < _sizeX; x++)
            {
                EditorGUILayout.BeginVertical();
                for (int y = 0; y < _sizeY; y++)
                {
                    if (GUILayout.Button(_textures[_field[x,y]], GUILayout.Width(50), GUILayout.Height(50)))
                    {
                        _field[x, y] = _field[x, y] == 0 ? 1 : 0;
                    }
                }

                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel++;
            if (!_loaded)
            {
                GUILayout.Label("File name:", EditorStyles.boldLabel);
                _levelName = EditorGUILayout.TextField(_levelName);
            }
            
            if (!string.IsNullOrEmpty(_levelName))
            {
                if (GUILayout.Button("Save level"))
                {
                    SaveToFile();
                }
            }
            
            EditorGUI.indentLevel--;
        }

        private void SaveToFile()
        {
            _level = new Level();
            _level.DeltaSpeed = _deltaSpeed;
            _level.Field = _field;
            _level.SizeX = _sizeX;
            _level.SizeY = _sizeY;
            _level.StartSpeed = _startSpeed;
            var formatter = new BinaryFormatter();
            using (var stream = new FileStream(Application.dataPath + "/Gamedata/Levels/" + _levelName + ".bytes",
                FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                formatter.Serialize(stream, _level);
                stream.Close();
            }
        }

        private void LoadFromFIle()
        {
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream(_file.bytes))
            {
                _level = (Level)formatter.Deserialize(stream);
                stream.Close();
            }

            _levelName = _file.name;
            _sizeX = _level.SizeX;
            _sizeY = _level.SizeY;
            _deltaSpeed = _level.DeltaSpeed;
            _startSpeed = _level.StartSpeed;
            _field = _level.Field;
            _isApplied = true;
        }

    }
}