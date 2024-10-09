using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
/*
public class FileDataHandler
{
    private string dataDirPath ="";
    private string dataFileName = "";
    private bool UseEntryption = false;
    private readonly string encryptionCode = "ALISNDUEBNCVB213K!@3X$KOSKD$@!@KOKOCODE";
    public FileDataHandler(string dataDirPath, string DataFileName, bool useEncrypt)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = DataFileName;
        this.UseEntryption = useEncrypt;
    }

    public GameData LoadInfoToScriptableOrMonoBehaviour()
    {
        //combine and generate path
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        PlayerSquad _SquadData = new PlayerSquad();
        GameData loadedData = new GameData(_SquadData);
        if(File.Exists(fullPath))
        {
            try
            {
                //Load data
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if(UseEntryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                //loads all the information to a Scriptable Object
                JsonUtility.FromJsonOverwrite(dataToLoad, loadedData);
               // loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch(Exception e)
            {
                Debug.LogError("Error trying to load data " + fullPath + "/n" + e);
            }
        }

        return loadedData;
    }

    public GameData LoadInfoToClass()
    {
        //combine and generate path
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                //Load data
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if (UseEntryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error trying to load data " + fullPath + "/n" + e);
            }
        }

        return loadedData;
    }


    public void Save(GameData data)
    {
        //combine and generate path
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            //create directory thaty will store the file
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            //serialize the c# game data object
            string dataToStore = JsonUtility.ToJson(data, true);

            if (UseEntryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }
            Debug.Log("File saved at: " + fullPath);
            //write the file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogError("Error guardando data" + fullPath + "--" + e);
        }
    }

    private string EncryptDecrypt(string data)
    {
        string modifiedData = "";
        for(int x=0;x< data.Length;x++)
        {
            modifiedData += (char)(data[x] ^ encryptionCode[x % encryptionCode.Length]);
        }
        return modifiedData;
    }
}
*/