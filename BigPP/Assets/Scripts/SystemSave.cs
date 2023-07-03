using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SystemSave
{
    public static void SaveProgress(Progress current_progress)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/progress.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        Progress data = current_progress;
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static Progress LoadProgress()
    {
        string path = Application.persistentDataPath + "/progress.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Progress data = formatter.Deserialize(stream) as Progress;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("File path not found" + "\nPath is: " + path);
            return null;
        }
    }
}