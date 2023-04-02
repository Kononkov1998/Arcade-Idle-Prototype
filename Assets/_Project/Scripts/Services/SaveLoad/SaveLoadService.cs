using System.IO;
using OdinSerializer;

namespace _Project.Scripts.Services.SaveLoad
{
    public class JsonSaveLoadService : ISaveLoadService
    {
        public void Save(PersistentData data, string path)
        {
            byte[] bytes = SerializationUtility.SerializeValue(data, DataFormat.JSON);
            File.WriteAllBytes(path, bytes);
        }

        public PersistentData Load(string path)
        {
            if (File.Exists(path))
            {
                byte[] bytes = File.ReadAllBytes(path);
                return SerializationUtility.DeserializeValue<PersistentData>(bytes, DataFormat.JSON);
            }

            return new PersistentData();
        }
    }

    public interface ISaveLoadService
    {
        void Save(PersistentData data, string path);
        PersistentData Load(string path);
    }
}