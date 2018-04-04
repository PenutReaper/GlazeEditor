using System;
using System.Collections.Generic;

namespace Core
{
    class FileParser
    {
        List<JSONItem> ParsedData = new List<JSONItem>();
        public List<JSONItem> ExtraData = new List<JSONItem>();

        string RawSave;
        public gEquipment Equip = new gEquipment();
        public gInventory Inv = new gInventory();
        public gPlayerInfo Info = new gPlayerInfo();

        public FileParser(string rawSave) => RawSave = rawSave;

        public JSONItem GetElementByName(string name)
        {
            foreach (JSONItem j in ParsedData)
            {
                if (j.Key == name) return j;
            }
            return null;
        }

        public void Parse()
        {
            RawSave = RawSave.Substring(0, RawSave.LastIndexOf("}"));
            RawSave = RawSave.Substring(1);

            ParsedData = WorkMagic(RawSave);

            Info.Credits = int.Parse(GetElementByName("Credits").Value);
            Info.Hardcore = bool.Parse(GetElementByName("Hardcore").Value);
            Info.Level = int.Parse(GetElementByName("Level").Value);
            Info.Name = GetElementByName("Name").Value;
            Info.NewCharacter = bool.Parse(GetElementByName("NewCharacter").Value);
            Info.PerkPoints = int.Parse(GetElementByName("PerkPoints").Value);

            foreach (JSONItem perk in GetElementByName("Perks").Children)
            {
                Affix toAdd;
                if (Enum.TryParse<Affix>(perk.Key, out toAdd))
                {
                    Info.Perks.Add((toAdd, float.Parse(perk.Value)));
                }
            }

            Info.TalentPoints = int.Parse(GetElementByName("TalentPoints").Value);

            foreach (JSONItem perk in GetElementByName("Talents").Children)
            {
                Affix toAdd;
                if (Enum.TryParse<Affix>(perk.Key, out toAdd))
                {
                    Info.Talents.Add((toAdd, float.Parse(perk.Value)));
                }
            }

            Info.XP = int.Parse(GetElementByName("XP").Value);

            ExtraData.AddRange(ParsedData.GetRange(11, 6));
        }

        private List<JSONItem> WorkMagic(string input)
        {
            (List<string> _names, List<string> _values) = StringIntoStrings(input);
            return SetupJSONObjects(_names, _values);
        }

        private List<JSONItem> SetupJSONObjects(List<string> names, List<string> values)
        {
            List<JSONItem> tempList = new List<JSONItem>();
            JSONItem temp;
            for (int i = 0; i < names.Count; i++)
            {
                if (values[i].Trim().Substring(0, 1) == "{")
                {
                    temp = new JSONItem(names[i], 0, values[i], new List<JSONItem>());
                    temp.Children = WorkMagic(temp.Value);
                }
                else
                {
                    temp = new JSONItem(names[i], 0, values[i], new List<JSONItem>());
                }

                tempList.Add(temp);
            }

            return tempList;
        }

        private (List<string>, List<string>) StringIntoStrings(string rawSave)
        {
            string value = PrefilterString(rawSave);

            List<string> temp_names = new List<string>();
            List<string> temp_values = new List<string>();

            int f_length = value.Length;
            int sub_pos = 0;

            while (sub_pos < f_length)
            {
                string next_item = GetNextItem(value, sub_pos);

                sub_pos += next_item.Length + 1;

                int split_at = next_item.IndexOf(":");

                if (split_at < 0) break;

                temp_names.Add(next_item.Substring(0, split_at).Trim());
                temp_values.Add(next_item.Substring(split_at + 1).Trim());
            }

            return (temp_names, temp_values);
        }

        private static string PrefilterString(string value)
        {
            int sub_end = value.LastIndexOf("}") > 0 ? value.LastIndexOf("}") : value.Length;            
            value = value.Substring(0, sub_end);
            value = value.Substring(1);
            return value;
        }

        private string GetNextItem(string input, int start_pos)
        {
            string output;
            int bracket_level = 0;
            int speech_level = 0;
            int cur_pos = start_pos;
            int loop_pos = start_pos;
            bool exit = false;

            while (!exit)
            {
                if (loop_pos >= input.Length)
                {
                    return input.Substring(start_pos);
                }
                string s = GetChar(input, loop_pos);

                if (s == "," && bracket_level == 0 && speech_level == 0)
                {
                    exit = true;
                }
                else if (s == "{") { bracket_level++; }
                else if (s == "}") { bracket_level--; }

                else if (s == "\"") { speech_level = 1 - speech_level; }

                if (!exit) loop_pos++;
            }

            output = input.Substring(cur_pos, loop_pos - cur_pos);
            cur_pos = loop_pos + 1;
            loop_pos = cur_pos;

            return output;
        }

        private string GetChar(string input, int charAt) => input.Substring(charAt, 1);
    }

    class JSONItem
    {
        public string Key;
        int Level { get; set; }
        public string Value;
        public List<JSONItem> Children;

        public JSONItem(string key, int level, string value, List<JSONItem> children)
        {
            Key = key.Substring(1, key.Length-2);
            Level = level;
            Value = value;
            Children = children;
        }
    }
}
