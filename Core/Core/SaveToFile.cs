using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Core
{
    public class SaveToFile
    {
        // Token: 0x0600106B RID: 4203 RVA: 0x000650A4 File Offset: 0x000634A4
        public static void SaveCharacter(PlayerSaveProxy proxy)
        {
            string @string = PlayerPrefs.GetString("CurrentSave", string.Empty);
            string text = SaveToFile.SerializeObject(proxy);
            string str = SaveToFile.Md5Sum(text);
            text += str;
            SaveToFile.SaveCharacterString(@string, text);
        }

        // Token: 0x0600106C RID: 4204 RVA: 0x000650E0 File Offset: 0x000634E0
        private static void SaveCharacterString(string saveName, string saveString)
        {
            SteamCloudSaves.WriteSaveToSteam(saveName, saveString);
            try
            {
                Directory.CreateDirectory(Application.dataPath + "/Saves/");
            }
            catch (Exception arg)
            {
                Debug.Log("Couldn't save to file on directory create: " + arg);
            }
            string path = SaveToFile.SaveNameToFileName(saveName);
            try
            {
                FileStream fileStream = File.Create(path);
                byte[] bytes = new UTF8Encoding(true).GetBytes(saveString);
                fileStream.Write(bytes, 0, bytes.Length);
                fileStream.Close();
            }
            catch (Exception arg2)
            {
                Debug.Log("Couldn't save to file on file save: " + arg2);
            }
        }

        // Token: 0x0600106D RID: 4205 RVA: 0x0006518C File Offset: 0x0006358C
        public static PlayerSaveProxy LoadCharacter()
        {
            string @string = PlayerPrefs.GetString("CurrentSave", string.Empty);
            return SaveToFile.LoadCharacter(@string);
        }

        // Token: 0x0600106E RID: 4206 RVA: 0x000651B0 File Offset: 0x000635B0
        public static PlayerSaveProxy LoadCharacter(string saveName)
        {
            string text = SaveToFile.LoadCharacterString(saveName);
            if (string.IsNullOrEmpty(text))
            {
                Debug.Log("Error loading save!");
                return new PlayerSaveProxy();
            }
            string text2 = text.Substring(0, text.Length - 32);
            string a = text.Substring(text.Length - 32, 32);
            PlayerSaveProxy result;
            try
            {
                PlayerSaveProxy playerSaveProxy = (PlayerSaveProxy)SaveToFile.DeserializeObject(text2);
                if (a != SaveToFile.Md5Sum(text2))
                {
                    playerSaveProxy.Cheater = true;
                }
                result = playerSaveProxy;
            }
            catch
            {
                Debug.Log("Error deserializing player!");
                result = new PlayerSaveProxy();
            }
            return result;
        }

        // Token: 0x0600106F RID: 4207 RVA: 0x00065258 File Offset: 0x00063658
        private static string LoadCharacterString(string saveName)
        {
            string result = string.Empty;
            string path = SaveToFile.SaveNameToFileName(saveName);
            if (File.Exists(path))
            {
                StreamReader streamReader = File.OpenText(path);
                string text = streamReader.ReadToEnd();
                streamReader.Close();
                result = text;
            }
            return result;
        }

        // Token: 0x06001070 RID: 4208 RVA: 0x00065294 File Offset: 0x00063694
        public static void SaveProgress(StoryProgressProxy proxy)
        {
            string @string = PlayerPrefs.GetString("CurrentSave", string.Empty);
            string text = SaveToFile.SerializeObject(proxy);
            string str = SaveToFile.Md5Sum(text);
            text += str;
            text = SaveToFile.Magic(text, "0xc294d1e94e19a3328473fa15f65c39323c833697");
            SaveToFile.SaveProgressString(@string, text);
            PlayerPrefs.Save();
        }

        // Token: 0x06001071 RID: 4209 RVA: 0x000652E0 File Offset: 0x000636E0
        private static void SaveProgressString(string saveName, string saveString)
        {
            SteamCloudSaves.WriteSaveToSteam(saveName, saveString);
            try
            {
                Directory.CreateDirectory(Application.dataPath + "/Saves/");
            }
            catch (Exception arg)
            {
                Debug.Log("Couldn't save to file on directory create: " + arg);
            }
            string path = SaveToFile.SaveNameToProgressFileName(saveName);
            try
            {
                FileStream fileStream = File.Create(path);
                byte[] bytes = new UTF8Encoding(true).GetBytes(saveString);
                fileStream.Write(bytes, 0, bytes.Length);
                fileStream.Close();
            }
            catch (Exception arg2)
            {
                Debug.Log("Couldn't save to file on file save: " + arg2);
            }
        }

        // Token: 0x06001072 RID: 4210 RVA: 0x0006538C File Offset: 0x0006378C
        public static StoryProgressProxy LoadProgress()
        {
            string @string = PlayerPrefs.GetString("CurrentSave", string.Empty);
            return SaveToFile.LoadProgress(@string);
        }

        // Token: 0x06001073 RID: 4211 RVA: 0x000653B0 File Offset: 0x000637B0
        public static StoryProgressProxy LoadProgress(string saveName)
        {
            string text = SaveToFile.LoadProgressString(saveName);
            text = SaveToFile.Magic(text, "0xc294d1e94e19a3328473fa15f65c39323c833697");
            if (string.IsNullOrEmpty(text))
            {
                Debug.Log("Error loading progress or new progress!");
                return new StoryProgressProxy(0);
            }
            string text2 = text.Substring(0, text.Length - 32);
            string a = text.Substring(text.Length - 32, 32);
            StoryProgressProxy result;
            try
            {
                StoryProgressProxy storyProgressProxy = (StoryProgressProxy)SaveToFile.DeserializeObject(text2);
                if (a != SaveToFile.Md5Sum(text2))
                {
                    storyProgressProxy.cheater = true;
                }
                result = storyProgressProxy;
            }
            catch
            {
                Debug.Log("Error deserializing progress!");
                result = new StoryProgressProxy(0);
            }
            return result;
        }

        // Token: 0x06001074 RID: 4212 RVA: 0x00065468 File Offset: 0x00063868
        private static string LoadProgressString(string saveName)
        {
            string result = string.Empty;
            string path = SaveToFile.SaveNameToProgressFileName(saveName);
            if (File.Exists(path))
            {
                StreamReader streamReader = File.OpenText(path);
                string text = streamReader.ReadToEnd();
                streamReader.Close();
                result = text;
            }
            return result;
        }

        // Token: 0x06001075 RID: 4213 RVA: 0x000654A4 File Offset: 0x000638A4
        public static void SaveCacheData(CacheSaveProxy proxy, bool hardcore)
        {
            string text = SaveToFile.SerializeObject(proxy);
            string str = SaveToFile.Md5Sum(text);
            text += str;
            SaveToFile.SaveCacheString(text, hardcore);
        }

        // Token: 0x06001076 RID: 4214 RVA: 0x000654D0 File Offset: 0x000638D0
        private static void SaveCacheString(string saveString, bool hardcore)
        {
            SteamCloudSaves.WriteSaveToSteam("CacheData.save", saveString);
            try
            {
                Directory.CreateDirectory(Application.dataPath + "/Saves/");
            }
            catch (Exception arg)
            {
                Debug.Log("Couldn't save to file on directory create: " + arg);
            }
            string cacheFileName = SaveToFile.GetCacheFileName(hardcore);
            try
            {
                FileStream fileStream = File.Create(cacheFileName);
                byte[] bytes = new UTF8Encoding(true).GetBytes(saveString);
                fileStream.Write(bytes, 0, bytes.Length);
                fileStream.Close();
            }
            catch (Exception arg2)
            {
                Debug.Log("Couldn't save to file on file save: " + arg2);
            }
        }

        // Token: 0x06001077 RID: 4215 RVA: 0x00065580 File Offset: 0x00063980
        public static CacheSaveProxy LoadCacheData(bool hardcore)
        {
            string text = SaveToFile.LoadCacheDataString(hardcore);
            if (string.IsNullOrEmpty(text))
            {
                Debug.Log("Error loading cache or new cache!");
                return new CacheSaveProxy();
            }
            string text2 = text.Substring(0, text.Length - 32);
            string a = text.Substring(text.Length - 32, 32);
            CacheSaveProxy result;
            try
            {
                CacheSaveProxy cacheSaveProxy = (CacheSaveProxy)SaveToFile.DeserializeObject(text2);
                if (a != SaveToFile.Md5Sum(text2))
                {
                    GameStateMachine.instance.GetInventory().SetCheater();
                }
                result = cacheSaveProxy;
            }
            catch
            {
                Debug.Log("Error deserializing Cache Data!");
                result = new CacheSaveProxy();
            }
            return result;
        }

        // Token: 0x06001078 RID: 4216 RVA: 0x00065630 File Offset: 0x00063A30
        private static string LoadCacheDataString(bool hardcore)
        {
            string result = string.Empty;
            string cacheFileName = SaveToFile.GetCacheFileName(hardcore);
            if (File.Exists(cacheFileName))
            {
                StreamReader streamReader = File.OpenText(cacheFileName);
                string text = streamReader.ReadToEnd();
                streamReader.Close();
                result = text;
            }
            return result;
        }

        // Token: 0x06001079 RID: 4217 RVA: 0x0006566C File Offset: 0x00063A6C
        public static bool AbleToLoad()
        {
            for (int i = 0; i < SaveToFile._numSaveSlots; i++)
            {
                string saveName = "PlayerData" + i;
                if (SaveToFile.IsSaveValid(saveName))
                {
                    return true;
                }
            }
            return false;
        }

        // Token: 0x0600107A RID: 4218 RVA: 0x000656B0 File Offset: 0x00063AB0
        public static List<string> GetSaves()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < SaveToFile._numSaveSlots; i++)
            {
                string text = "PlayerData" + i;
                if (SaveToFile.IsSaveValid(text))
                {
                    list.Add(text);
                }
            }
            return list;
        }

        // Token: 0x0600107B RID: 4219 RVA: 0x00065700 File Offset: 0x00063B00
        public static string GetFirstValidSave()
        {
            for (int i = 0; i < SaveToFile._numSaveSlots; i++)
            {
                string text = "PlayerData" + i;
                if (SaveToFile.IsSaveValid(text))
                {
                    return text;
                }
            }
            return string.Empty;
        }

        // Token: 0x0600107C RID: 4220 RVA: 0x00065748 File Offset: 0x00063B48
        public static void DeleteAllSaves()
        {
            for (int i = 0; i < SaveToFile._numSaveSlots; i++)
            {
                string saveName = "PlayerData" + i;
                SaveToFile.DeleteSave(saveName);
            }
        }

        // Token: 0x0600107D RID: 4221 RVA: 0x00065784 File Offset: 0x00063B84
        public static bool IsSaveValid(string saveName)
        {
            string path = SaveToFile.SaveNameToFileName(saveName);
            string text = SaveToFile.LoadCharacterString(saveName);
            return !text.Contains("UnityScript") && File.Exists(path);
        }

        // Token: 0x0600107E RID: 4222 RVA: 0x000657C0 File Offset: 0x00063BC0
        public static string GetCharacterName(string saveName)
        {
            if (saveName == string.Empty)
            {
                return string.Empty;
            }
            PlayerSaveProxy playerSaveProxy = SaveToFile.LoadCharacter(saveName);
            return playerSaveProxy.Name;
        }

        // Token: 0x0600107F RID: 4223 RVA: 0x000657F0 File Offset: 0x00063BF0
        public static string GetCharacterLevel(string saveName)
        {
            if (saveName == string.Empty)
            {
                return string.Empty;
            }
            PlayerSaveProxy playerSaveProxy = SaveToFile.LoadCharacter(saveName);
            return playerSaveProxy.Level.ToString();
        }

        // Token: 0x06001080 RID: 4224 RVA: 0x0006582C File Offset: 0x00063C2C
        public static bool GetCharacterHardcore(string saveName)
        {
            if (saveName == string.Empty)
            {
                return false;
            }
            PlayerSaveProxy playerSaveProxy = SaveToFile.LoadCharacter(saveName);
            return playerSaveProxy.Hardcore;
        }

        // Token: 0x06001081 RID: 4225 RVA: 0x00065858 File Offset: 0x00063C58
        public static bool GetCharacterNew(string saveName)
        {
            if (saveName == string.Empty)
            {
                return true;
            }
            PlayerSaveProxy playerSaveProxy = SaveToFile.LoadCharacter(saveName);
            return playerSaveProxy.NewCharacter;
        }

        // Token: 0x06001082 RID: 4226 RVA: 0x00065884 File Offset: 0x00063C84
        public static string FirstEmptySlotAvailable()
        {
            for (int i = 0; i < SaveToFile._numSaveSlots; i++)
            {
                string text = "PlayerData" + i;
                if (!SaveToFile.IsSaveValid(text))
                {
                    return text;
                }
            }
            return string.Empty;
        }

        // Token: 0x06001083 RID: 4227 RVA: 0x000658CC File Offset: 0x00063CCC
        public static void DeleteCurrentSave()
        {
            string @string = PlayerPrefs.GetString("CurrentSave", string.Empty);
            SaveToFile.DeleteSave(@string);
        }

        // Token: 0x06001084 RID: 4228 RVA: 0x000658EF File Offset: 0x00063CEF
        public static void DeleteSave(string saveName)
        {
            SteamCloudSaves.DeleteSteamSave(saveName);
            SaveToFile.DeleteFile(SaveToFile.SaveNameToFileName(saveName));
            SaveToFile.DeleteFile(SaveToFile.SaveNameToProgressFileName(saveName));
        }

        // Token: 0x06001085 RID: 4229 RVA: 0x00065910 File Offset: 0x00063D10
        private static void DeleteFile(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    Debug.Log("Deleting file: " + path);
                    File.Delete(path);
                }
            }
            catch (Exception arg)
            {
                Debug.Log("Couldn't delete: " + arg);
            }
        }

        // Token: 0x06001086 RID: 4230 RVA: 0x0006596C File Offset: 0x00063D6C
        private static string SaveNameToFileName(string saveName)
        {
            return Application.dataPath + "/Saves/" + saveName + ".save";
        }

        // Token: 0x06001087 RID: 4231 RVA: 0x00065983 File Offset: 0x00063D83
        private static string SaveNameToProgressFileName(string saveName)
        {
            return Application.dataPath + "/Saves/" + saveName + ".bak";
        }

        // Token: 0x06001088 RID: 4232 RVA: 0x0006599A File Offset: 0x00063D9A
        private static string GetCacheFileName(bool hardcore)
        {
            if (hardcore)
            {
                return Application.dataPath + "/Saves/HardcoreCacheData.save";
            }
            return Application.dataPath + "/Saves/CacheData.save";
        }

        private static string Md5Sum(string strToEncrypt)
        {
            UTF8Encoding utf8Encoding = new UTF8Encoding();
            byte[] bytes = utf8Encoding.GetBytes(strToEncrypt);
            MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] array = md5CryptoServiceProvider.ComputeHash(bytes);
            string text = string.Empty;
            for (int i = 0; i < array.Length; i++)
            {
                text += Convert.ToString(array[i], 16).PadLeft(2, '0');
            }
            return text.PadLeft(32, '0');
        }

        // Token: 0x0600108A RID: 4234 RVA: 0x00065A34 File Offset: 0x00063E34
        public static string SerializeObject(object thisObject)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            return JsonConvert.SerializeObject(thisObject, Formatting.Indented, settings);
        }

        public static object DeserializeObject(string objectString)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            return JsonConvert.DeserializeObject(objectString, settings);
        }

        private static string Magic(string a, string b)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < a.Length; i++)
            {
                stringBuilder.Append(a[i] ^ b[i % b.Length]);
            }
            return stringBuilder.ToString();
        }

        private static int _numSaveSlots = 100;
    }
}
