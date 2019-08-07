using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;
using System;

public class SaveAndLoad : Singleton<SaveAndLoad>
{
    public void DeleteKey(string _key)
    {
        ObscuredPrefs.DeleteKey(_key);
    }

    public void DeleteAll()
    {
        ObscuredPrefs.DeleteAll();
    }

    public void HasData(string _key, Action<bool> _callback = null)
    {
        var result = ObscuredPrefs.HasKey(_key);
        _callback?.Invoke(result);
    }
 
    public void SaveIntData(string _key, int _data)
    {
        ObscuredPrefs.SetInt(_key, _data);
    }

    public void SaveFloatData(string _key, float _data)
    {
        ObscuredPrefs.SetFloat(_key, _data);
    }

    public void SaveStringData(string _key, string _data)
    {
        ObscuredPrefs.SetString(_key, _data);
    }

    public void SaveIntArrayData(string _key, List<int> _data)
    {
        string saveData = "";

        for(int i = 0; i < _data.Count; i++)
        {
            saveData += _data[i].ToString();

            if (i != _data.Count - 1)
                saveData += "/";
        }

        ObscuredPrefs.SetString(_key, saveData);
    }
    
    public void SaveFloatArrayData(string _key, List<float> _data)
    {
        string saveData = "";

        for(int i = 0; i < _data.Count; i++)
        {
            saveData += _data[i].ToString();

            if (i != _data.Count - 1)
                saveData += "/";
        }

        ObscuredPrefs.SetString(_key, saveData);
    }

    public int LoadIntData(string _key)
    {
        int result = 0;

        HasData(_key, (bool exist) =>
        {
            if(exist)
            {
                result = ObscuredPrefs.GetInt(_key);
            }
            else
            {

            }
        });

        return result;
    }

    public float LoadFloatData(string _key)
    {
        float result = 0;

        HasData(_key, (bool exist) =>
        {
            if (exist)
            {
                result = ObscuredPrefs.GetFloat(_key);
            }
            else
            {

            }
        });

        return result;
    }

    public string LoadStringData(string _key)
    {
        string result = "";

        HasData(_key, (bool exist) =>
        {
            if (exist)
            {
                result = ObscuredPrefs.GetString(_key);
            }
            else
            {

            }
        });

        return result;
    }

    public List<int> LoadIntArrayData(string _key)
    {
        string loadedString = ObscuredPrefs.GetString(_key);

        var splitLoadedStringData = loadedString.Split('/');
        List<int> result = new List<int>();

        for(int i = 0; i < splitLoadedStringData.Length; i++)
        {
            result.Add(int.Parse(splitLoadedStringData[i]));
        }

        return result;
    }

    public List<float> LoadFloatArrayData(string _key)
    {
        string loadedString = ObscuredPrefs.GetString(_key);

        var splitLoadedStringData = loadedString.Split('/');
        List<float> result = new List<float>();

        for (int i = 0; i < splitLoadedStringData.Length; i++)
        {
            result.Add(float.Parse(splitLoadedStringData[i]));
        }

        return result;
    }
}
