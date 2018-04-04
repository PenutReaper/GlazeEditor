using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Core
{
    class Program
    {
        static void Main(string[] args)
        {
            List<SaveFile> EditableData = new List<SaveFile>();
            List<FileInfo> Saves = GetSaveFiles();
            FileParser parser;

            foreach (FileInfo Save in Saves)
            {
                SaveFile curFile = new SaveFile();
                string RawSave = ReadSave(Save);

                parser = new FileParser(RawSave);
                parser.Parse();

                curFile.SetInfo(parser.Info);
                curFile.SetEquipment(parser.Equip);
                curFile.SetInventory(parser.Inv);
                curFile.extra = parser.ExtraData;
                EditableData.Add(curFile);
            }

            bool exit = false;
            int command_mode = 0;

            while (!exit)
            {
                Console.Clear();
                string inp = "";
                string output = "";

                if (command_mode == 0)
                {
                    Console.WriteLine("Display, Edit or Save:");
                    inp = Console.ReadLine().Trim().ToLower();

                    switch (inp)
                    {
                        case "display":
                            command_mode = 1;
                            break;

                        case "edit":
                            command_mode = 2;
                            break;

                        case "save":
                            command_mode = 3;
                            break;

                        default:
                            output = "Invalid Input, Press Enter to Continue";
                            break;
                    }
                }
                else if (command_mode == 1)
                {
                    Console.WriteLine("Param to Display");
                    inp = Console.ReadLine().Trim().ToLower();
                    switch (inp)
                    {
                        case "name":
                            output = $"Player Name: {EditableData[0].player_info.Name}";
                            break;

                        case "level":
                            output = $"Player Level: {EditableData[0].player_info.Level}";
                            break;

                        case "credits":
                            output = $"Credits: {EditableData[0].player_info.Credits}";
                            break;

                        case "xp":
                            output = $"XP: {EditableData[0].player_info.XP}";
                            break;

                        case "perks":
                            foreach ((Affix name, float level) perk in EditableData[0].player_info.Perks)
                            {
                                output += $"{perk.name}: {perk.level}{Environment.NewLine}";
                            }
                            break;

                        case "perkpoints":
                            output = $"PerkPoints: {EditableData[0].player_info.PerkPoints}";
                            break;

                        case "hardcore":
                            output = $"Hardcore: {EditableData[0].player_info.Hardcore}";
                            break;

                        case "newcharacter":
                            output = $"NewCharacter: {EditableData[0].player_info.NewCharacter}";
                            break;

                        case "talents":
                            foreach ((Affix name, float level) talents in EditableData[0].player_info.Talents)
                            {
                                output += $"{talents.name}: {talents.level}{Environment.NewLine}";
                            }
                            break;

                        case "talentpoints":
                            output = $"levelPoints: {EditableData[0].player_info.TalentPoints}";
                            break;

                        default:
                            output = "Invalid Input, Press Enter to Continue";
                            break;
                    }

                    Console.WriteLine(output);
                    Console.Read();
                    command_mode = 0;
                }

                else if (command_mode == 2)
                {
                    Console.WriteLine("Param to Modify:");
                    inp = Console.ReadLine().Trim().ToLower();
                    switch (inp)
                    {
                        case "name":
                            Console.Clear();
                            Console.WriteLine("Enter New Value For Name");
                            inp = Console.ReadLine().Trim();
                            EditableData[0].player_info.Name = inp;
                            break;

                        case "level":
                            Console.Clear();
                            Console.WriteLine("Enter New Value For Level");
                            inp = Console.ReadLine().Trim();
                            EditableData[0].player_info.Level = int.Parse(inp);
                            break;

                        case "credits":
                            Console.Clear();
                            Console.WriteLine("Enter New Value For Credits");
                            inp = Console.ReadLine().Trim();
                            EditableData[0].player_info.Credits = int.Parse(inp);
                            break;

                        case "xp":
                            Console.Clear();
                            Console.WriteLine("Enter New Value For XP");
                            inp = Console.ReadLine().Trim();
                            EditableData[0].player_info.XP = int.Parse(inp);
                            break;

                        case "perks":
                            Console.Clear();
                            foreach ((Affix name, float level) perk in EditableData[0].player_info.Perks)
                            {
                                output += $"{perk.name}: {perk.level}{Environment.NewLine}";
                            }
                            Console.WriteLine(output);
                            output = "";
                            Console.WriteLine("Choose Perk to Edit: ");
                            inp = Console.ReadLine().Trim();
                            Affix toEdit;
                            bool foundAffix = false;
                            if (Enum.TryParse(inp, out toEdit))
                            {
                                for (int i = 0; i < EditableData[0].player_info.Perks.Count; i++)
                                {
                                    (Affix name, float level) perk;
                                    perk.name = EditableData[0].player_info.Perks[i].Item1;
                                    perk.level = EditableData[0].player_info.Perks[i].Item2;

                                    if (perk.name == toEdit)
                                    {
                                        Console.WriteLine($"Enter New Value For {perk.name}");
                                        inp = Console.ReadLine().Trim();
                                        EditableData[0].player_info.Perks[i] = Tuple.Create(perk.name, float.Parse(inp)).ToValueTuple();
                                        foundAffix = true;
                                    }
                                }

                                if (!foundAffix)
                                {
                                    Console.WriteLine("Perk Not Found");
                                    Console.Read();
                                }
                            }

                            break;

                        case "perkpoints":
                            Console.Clear();
                            Console.WriteLine("Enter New Value For PerkPoints");
                            inp = Console.ReadLine().Trim();
                            EditableData[0].player_info.PerkPoints = int.Parse(inp);
                            break;

                        case "hardcore":
                            Console.Clear();
                            Console.WriteLine("Enter New Value For Hardcore");
                            inp = Console.ReadLine().Trim();
                            EditableData[0].player_info.Hardcore = bool.Parse(inp);
                            break;

                        case "newcharacter":
                            Console.Clear();
                            Console.WriteLine("Enter New Value For NewCharacter");
                            inp = Console.ReadLine().Trim();
                            EditableData[0].player_info.NewCharacter = bool.Parse(inp);
                            break;

                        case "talents":
                            Console.Clear();
                            foreach ((Affix name, float level) perk in EditableData[0].player_info.Talents)
                            {
                                output += $"{perk.name}: {perk.level}{Environment.NewLine}";
                            }
                            Console.WriteLine(output);
                            output = "";
                            Console.WriteLine("Choose Talent to Edit: ");
                            inp = Console.ReadLine().Trim();
                            Affix toEdit2;
                            bool foundAffix2 = false;
                            if (Enum.TryParse(inp, out toEdit2))
                            {
                                for (int i = 0; i < EditableData[0].player_info.Talents.Count; i++)
                                {
                                    (Affix name, float level) perk;
                                    perk.name = EditableData[0].player_info.Talents[i].Item1;
                                    perk.level = EditableData[0].player_info.Talents[i].Item2;

                                    if (perk.name == toEdit2)
                                    {
                                        Console.WriteLine($"Enter New Value For {perk.name}");
                                        inp = Console.ReadLine().Trim();
                                        EditableData[0].player_info.Talents[i] = Tuple.Create(perk.name, float.Parse(inp)).ToValueTuple();
                                        foundAffix2 = true;
                                    }
                                }

                                if (!foundAffix2)
                                {
                                    Console.WriteLine("Talent Not Found");
                                    Console.Read();
                                }
                            }
                            break;

                        case "talentpoints":
                            Console.Clear();
                            Console.WriteLine("Enter New Value For TalentPoints");
                            inp = Console.ReadLine().Trim();
                            EditableData[0].player_info.TalentPoints = int.Parse(inp);
                            break;

                        default:
                            output = "Invalid Input, Press Enter to Continue";
                            command_mode = 0;
                            break;
                    }

                    command_mode = 0;
                }

                else if (command_mode == 3)
                {
                    for (int i = 0; i < EditableData.Count; i++)
                    {
                        SaveFile cur_file = EditableData[i];

                        using (StreamWriter sw = new StreamWriter($@"{Directory.GetCurrentDirectory()}\Black Ice_Data\Saves\PlayerData{i}.save"))
                        {
                            sw.WriteLine("{");
                            sw.WriteLine($" \"$type\": \"PlayerSaveProxy, Assembly-CSharp\",");
                            sw.WriteLine($" \"Name\": {cur_file.player_info.Name},");
                            sw.WriteLine($" \"XP\": {cur_file.player_info.XP},");
                            sw.WriteLine($" \"Level\": {cur_file.player_info.Level},");
                            sw.WriteLine($" \"TalentPoints\": {cur_file.player_info.TalentPoints},");
                            sw.WriteLine($" \"Talents\": {{");
                            sw.WriteLine($"    \"$type\": \"System.Collections.Generic.Dictionary`2[[Affix, Assembly-CSharp],[System.Single, mscorlib]], mscorlib\",");

                            for (int x = 0; x < cur_file.player_info.Talents.Count; x++)
                            {
                                (Affix name, float level) tal = (
                                    cur_file.player_info.Talents[x].Item1,
                                    cur_file.player_info.Talents[x].Item2);

                                int max = cur_file.player_info.Talents.Count - 1;
                                string do_comma = x < max ? "," : "";

                                sw.WriteLine($"    \"{tal.name}\": {tal.level.ToString("0.0")}{do_comma}");
                            }
                            sw.WriteLine("  },");

                            sw.WriteLine($" \"PerkPoints\": {cur_file.player_info.PerkPoints},");
                            sw.WriteLine($" \"Perks\": {{");
                            sw.WriteLine($"    \"$type\": \"System.Collections.Generic.Dictionary`2[[Affix, Assembly-CSharp],[System.Single, mscorlib]], mscorlib\",");

                            for (int x = 0; x < cur_file.player_info.Perks.Count; x++)
                            {
                                (Affix name, float level) perk = (
                                    cur_file.player_info.Perks[x].Item1,
                                    cur_file.player_info.Perks[x].Item2);

                                int max = cur_file.player_info.Perks.Count - 1;
                                string do_comma = x < max ? "," : "";

                                sw.WriteLine($"    \"{perk.name}\": {perk.level.ToString("0.0")}{do_comma}");
                            }
                            sw.WriteLine("  },");

                            sw.WriteLine($" \"Hardcore\": {cur_file.player_info.Hardcore.ToString().ToLower()},");
                            sw.WriteLine($" \"NewCharacter\": {cur_file.player_info.NewCharacter.ToString().ToLower()},");
                            sw.WriteLine($" \"Credits\": {cur_file.player_info.Credits},");

                            sw.Write(GetExtra(cur_file.extra));
                            sw.Write("}5fa68df1015c9b40baf44fb421b90307");
                            command_mode = 0;
                            exit = true;
                        }

                    }
                }
            }

        }

        private static string GetExtra(List<JSONItem> extra)
        {
            string output = "";
            foreach (JSONItem data in extra)
            {
                output += $"    \"{data.Key}\": {data.Value},{Environment.NewLine}"; 
            }

            output = output.Substring(0, output.LastIndexOf(","));
            output += $" }}{Environment.NewLine}";

            return output;
        }

        private static List<FileInfo> GetSaveFiles()
        {
            DirectoryInfo SaveDir = new DirectoryInfo($@"{Directory.GetCurrentDirectory()}\Black Ice_Data\Saves");
            FileInfo[] Files = SaveDir.GetFiles();
            List<FileInfo> Saves = RemoveNonPlayerSaves(Files);

            return Saves;
        }

        private static List<FileInfo> RemoveNonPlayerSaves(FileInfo[] saves)
        {
            List<FileInfo> TempSaves = new List<FileInfo>();

            for (int i = 0; i < saves.Count(); i++)
            {
                if (saves[i].Extension == ".save" && saves[i].Name.Contains("PlayerData"))
                {
                    TempSaves.Add(saves[i]);
                }
            }

            return TempSaves;
        }

        private static string ReadSave(FileInfo save) => File.ReadAllText($"{save.FullName}");
        
    }

       

    class SaveFile
    {
        public gPlayerInfo player_info { get; set; } 
        public gEquipment player_equipment { get; set; }
        public gInventory player_inventory { get; set; }
        public List<JSONItem> extra { get; set; }

        public void SetInventory(gInventory newInv) => player_inventory = newInv;
        public void SetEquipment(gEquipment newEquip) => player_equipment = newEquip;
        public void SetInfo(gPlayerInfo newInfo) => player_info = newInfo;
    }

    class gPlayerInfo
    {
        public List<(Affix, float)> Talents = new List<(Affix, float)>();
        public List<(Affix, float)> Perks = new List<(Affix, float)>();
        public int TalentPoints;
        public int PerkPoints;
        public int Level;
        public int XP;
        public string Name;
        public bool Hardcore;
        public bool NewCharacter;
        public int Credits;
    }

    class gInventory
    {
        public List<gItem> Items = new List<gItem>();
    }

    class gEquipment
    {
        public List<gItem> Items = new List<gItem>();
    }

    class gItem
    {
        protected string _name;
        protected int _value;
        protected RarityType _rarity;
        protected Dictionary<Affix, float> _affixes;
        protected IconIndex _icon = IconIndex.Laser;
        protected int _ramConsumed;
        protected float _effect = 1f;
        protected float _cooldown = 0.1f;
        protected int _levelRequirement;
        protected int _itemLevel = 1;
        protected string _specialProperties = string.Empty;
        protected bool _favorite;
        protected string _flavorText = string.Empty;

        public gItem(string name, int value, RarityType rarity, Dictionary<Affix, float> affixes, IconIndex icon, int ramConsumed, float effect, float cooldown, int levelRequirement, int itemLevel, string specialProperties, bool favorite, string flavorText)
        {
            _name = name;
            _value = value;
            _rarity = rarity;
            _affixes = affixes;
            _icon = icon;
            _ramConsumed = ramConsumed;
            _effect = effect;
            _cooldown = cooldown;
            _levelRequirement = levelRequirement;
            _itemLevel = itemLevel;
            _specialProperties = specialProperties;
            _favorite = favorite;
            _flavorText = flavorText;
        }
    }
}
