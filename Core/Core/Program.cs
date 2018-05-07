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
                            sw.WriteLine($" \"Name\": \"{cur_file.player_info.Name}\",");
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

                            sw.WriteLine($" \"Equipment\": {{");
                            sw.WriteLine($"     \"$type\": \"Item[], Assembly-CSharp\",");
                            sw.WriteLine($"     \"$values\": [");

                            i = writeEquipment(i, cur_file, sw);
                            sw.WriteLine($"     ] {Environment.NewLine} }},");

                            sw.WriteLine($" \"Inventory\": {{");
                            sw.WriteLine($"     \"$type\": \"Item[], Assembly-CSharp\",");
                            sw.WriteLine($"     \"$values\": [");

                            i = writeInventory(i, cur_file, sw);

                            sw.WriteLine($"     ] {Environment.NewLine} }},");

                            sw.WriteLine($" \"Cheater\": {cur_file.player_info.Cheater.ToString().ToLower()},");
                            sw.WriteLine($" \"HighestServerHacked\": {cur_file.player_info.HighestServerHacked},");
                            sw.WriteLine($" \"TimeSinceLastLevel\": {cur_file.player_info.TimeSinceLastLevel},");


                            sw.WriteLine($" \"WorldState\":  {{");
                            sw.WriteLine($" \"$type\": \"System.Collections.Generic.Dictionary`2[[WorldState, Assembly - CSharp],[System.Int32, mscorlib]], mscorlib\",");
                            sw.WriteLine($" \"Door_Portcullis\": {cur_file.player_info.WorldState[0].Item2},");
                            sw.WriteLine($" \"Door_Finality1\": {cur_file.player_info.WorldState[1].Item2}");
                            sw.WriteLine($" }}");

                            sw.Write($"{cur_file.player_info.Hash}");
                            command_mode = 0;
                            exit = true;
                        }

                    }
                }
            }

        }

        private static int writeInventory(int i, SaveFile cur_file, StreamWriter sw)
        {
            for (int x = 0; x < 45; x++)
            {
                try
                {
                    gItem cur_item = cur_file.player_inventory.Items[x];
                    string comma = x < 44 ? "," : "";

                    sw.WriteLine("      {");

                    if (cur_item is gWeapon)
                    {
                        gWeapon cur_wep = (gWeapon)cur_item;
                        sw.WriteLine($"     \"MinDamage\": {cur_wep._minDamage},");
                        sw.WriteLine($"     \"MaxDamage\": {cur_wep._maxDamage},");
                        sw.WriteLine($"     \"ProjectileType\": {(int)cur_wep._projectileType},");
                        sw.WriteLine($"     \"ProjectileSpeed\": {cur_wep._projectileSpeed},");
                        sw.WriteLine($"     \"Range\": {cur_wep._range},");
                        sw.WriteLine($"     \"Accuracy\": {cur_wep._accuracy},");
                        sw.WriteLine($"     \"ProjectileCount\": {cur_wep._projectileCount},");
                        sw.WriteLine($"     \"Pitch\": {cur_wep._pitch},");
                        sw.WriteLine($"     \"WeaponSound\": {(int)cur_wep._weaponSound},");
                        sw.WriteLine($"     \"DamageFalloff\": {cur_wep._accuracy.ToString().ToLower()},");

                        sw.WriteLine($"     \"WeaponSpread\": {{");
                        sw.WriteLine($"         \"$type\": \"WeaponSpread, Assembly - CSharp\",");
                        sw.WriteLine($"         \"MaxSpread\": {cur_wep._spread[0].Item2},");
                        sw.WriteLine($"         \"SpreadPerShot\": {cur_wep._spread[1].Item2},");
                        sw.WriteLine($"         \"SecondsToNormal\": {cur_wep._spread[2].Item2}");
                        sw.WriteLine($"     }},");

                        sw.WriteLine($"     \"WeaponModel\": {(int)cur_wep._weaponModel},");
                    }

                    if (cur_item is gMinion)
                    {
                        gMinion cur_min = (gMinion)cur_item;
                        sw.WriteLine($"     \"MinDamage\": {cur_min._minDamage},");
                        sw.WriteLine($"     \"MaxDamage\": {cur_min._maxDamage},");
                        sw.WriteLine($"     \"EnemyToSpawn\": {(int)cur_min._enemyToSpawn},");
                        sw.WriteLine($"     \"Health\": {cur_min._health},");
                    }

                    sw.WriteLine($"     \"$type\": \"{cur_item._type}\",");
                    sw.WriteLine($"     \"Name\": \"{cur_item._name}\",");
                    sw.WriteLine($"     \"Value\": {cur_item._value},");

                    sw.WriteLine($"     \"Affixes\":  {{");
                    for (int a = 0; a < cur_item._affixes.Count; a++)
                    {
                        (Affix name, float level) perk = (
                           cur_item._affixes[a].Item1,
                           cur_item._affixes[a].Item2);

                        int max = cur_item._affixes.Count - 1;
                        string do_comma = a < max ? "," : "";

                        sw.WriteLine($"    \"{perk.name}\": {perk.level.ToString("0.0")}{do_comma}");
                    }
                    sw.WriteLine($"     }},");

                    sw.WriteLine($"     \"Icon\": {(int)cur_item._icon},");
                    sw.WriteLine($"     \"RamConsumed\": {cur_item._ramConsumed},");
                    sw.WriteLine($"     \"Effect\": {cur_item._effect},");
                    sw.WriteLine($"     \"Cooldown\": {cur_item._cooldown},");
                    sw.WriteLine($"     \"LevelRequirement\": {cur_item._levelRequirement},");
                    sw.WriteLine($"     \"ItemLevel\": {cur_item._itemLevel},");
                    sw.WriteLine($"     \"SpecialProperties\": \"{cur_item._specialProperties.Trim()}\",");
                    sw.WriteLine($"     \"Favorite\": {cur_item._favorite.ToString().ToLower()},");
                    sw.WriteLine($"     \"FlavorText\": \"{cur_item._flavorText.Trim()}\"");

                    sw.WriteLine($"     }}{ comma } ");
                }
                catch
                {
                    string comma = x < 44 ? "," : "";
                    sw.WriteLine($"     null{comma}");
                }
            }

            return i;
        }

        private static int writeEquipment(int i, SaveFile cur_file, StreamWriter sw)
        {
            for (int x = 0; x < 9; x++)
            {
                try
                {
                    gItem cur_item = cur_file.player_equipment.Items[x];
                    string comma = x < 8 ? "," : "";

                    sw.WriteLine("      {");

                    if (cur_item is gWeapon)
                    {
                        gWeapon cur_wep = (gWeapon)cur_item;
                        sw.WriteLine($"     \"MinDamage\": {cur_wep._minDamage},");
                        sw.WriteLine($"     \"MaxDamage\": {cur_wep._maxDamage},");
                        sw.WriteLine($"     \"ProjectileType\": {(int)cur_wep._projectileType},");
                        sw.WriteLine($"     \"ProjectileSpeed\": {cur_wep._projectileSpeed},");
                        sw.WriteLine($"     \"Range\": {cur_wep._range},");
                        sw.WriteLine($"     \"Accuracy\": {cur_wep._accuracy},");
                        sw.WriteLine($"     \"ProjectileCount\": {cur_wep._projectileCount},");
                        sw.WriteLine($"     \"Pitch\": {cur_wep._pitch},");
                        sw.WriteLine($"     \"WeaponSound\": {(int)cur_wep._weaponSound},");
                        sw.WriteLine($"     \"DamageFalloff\": {cur_wep._accuracy.ToString().ToLower()},");

                        sw.WriteLine($"     \"WeaponSpread\": {{");
                        sw.WriteLine($"         \"$type\": \"WeaponSpread, Assembly - CSharp\",");
                        sw.WriteLine($"         \"MaxSpread\": {cur_wep._spread[0].Item2},");
                        sw.WriteLine($"         \"SpreadPerShot\": {cur_wep._spread[1].Item2},");
                        sw.WriteLine($"         \"SecondsToNormal\": {cur_wep._spread[2].Item2}");
                        sw.WriteLine($"     }},");

                        sw.WriteLine($"     \"WeaponModel\": {(int)cur_wep._weaponModel},");
                    }

                    if (cur_item is gMinion)
                    {
                        gMinion cur_min = (gMinion)cur_item;
                        sw.WriteLine($"     \"MinDamage\": {cur_min._minDamage},");
                        sw.WriteLine($"     \"MaxDamage\": {cur_min._maxDamage},");
                        sw.WriteLine($"     \"EnemyToSpawn\": {(int)cur_min._enemyToSpawn},");
                        sw.WriteLine($"     \"Health\": {cur_min._health},");
                    }

                    sw.WriteLine($"     \"$type\": \"{cur_item._type}\",");
                    sw.WriteLine($"     \"Name\": \"{cur_item._name}\",");
                    sw.WriteLine($"     \"Value\": {cur_item._value},");
                    sw.WriteLine($"     \"Rarity\": {(int)cur_item._rarity},");

                    sw.WriteLine($"     \"Affixes\":  {{");
                    for (int a = 0; a < cur_item._affixes.Count; a++)
                    {
                        (Affix name, float level) perk = (
                           cur_item._affixes[a].Item1,
                           cur_item._affixes[a].Item2);

                        int max = cur_item._affixes.Count - 1;
                        string do_comma = a < max ? "," : "";

                        sw.WriteLine($"    \"{perk.name}\": {perk.level.ToString("0.0")}{do_comma}");
                    }
                    sw.WriteLine($"     }},");

                    sw.WriteLine($"     \"Icon\": {(int)cur_item._icon},");
                    sw.WriteLine($"     \"RamConsumed\": {cur_item._ramConsumed},");
                    sw.WriteLine($"     \"Effect\": {cur_item._effect},");
                    sw.WriteLine($"     \"Cooldown\": {cur_item._cooldown},");
                    sw.WriteLine($"     \"LevelRequirement\": {cur_item._levelRequirement},");
                    sw.WriteLine($"     \"ItemLevel\": {cur_item._itemLevel},");
                    sw.WriteLine($"     \"SpecialProperties\": \"{cur_item._specialProperties.Trim()}\",");
                    sw.WriteLine($"     \"Favorite\": {cur_item._favorite.ToString().ToLower()},");
                    sw.WriteLine($"     \"FlavorText\": \"{cur_item._flavorText.Trim()}\"");

                    sw.WriteLine($"     }}{ comma } ");
                }
                catch
                {
                    string comma = x < 8 ? "," : "";
                    sw.WriteLine($"     null{comma}");
                }
            }

            return i;
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

        public bool Cheater;
        public int HighestServerHacked;
        public float TimeSinceLastLevel;
        public List<(string, int)> WorldState = new List<(string, int)>();
        public string Hash;
    }

    class gInventory
    {
        public List<gItem> Items = new List<gItem>();
    }

    class gEquipment
    {
        public List<gItem> Items = new List<gItem>();
    }

    class gMinion : gItem
    {
        public EnemyIndex _enemyToSpawn;
        public int _minDamage = 1;
        public int _maxDamage = 1;
        public int _health = 1;

        public gMinion(gItem base_item)
        {
            _type = base_item._type;
            _name = base_item._name;
            _value = base_item._value;
            _rarity = base_item._rarity;
            _affixes = base_item._affixes;
            _icon = base_item._icon;
            _ramConsumed = base_item._ramConsumed;
            _effect = base_item._effect;
            _cooldown = base_item._cooldown;
            _levelRequirement = base_item._levelRequirement;
            _itemLevel = base_item._itemLevel;
            _specialProperties = base_item._specialProperties;
            _favorite = base_item._favorite;
            _flavorText = base_item._flavorText;
        }
    }

    class gWeapon : gItem
    {
        public int _minDamage = 1;
        public int _maxDamage = 1;
        public PayloadIndex _projectileType;
        public int _projectileSpeed = 80;
        public int _range = 30;
        public int _accuracy = 90;
        public int _projectileCount = 1;
        public float _pitch = 1f;
        public LaserSoundIndex _weaponSound;
        public bool _damageFalloff;
        public List<(string, float)> _spread = new List<(string, float)>();
        public WeaponModelIndex _weaponModel;

        public gWeapon(gItem base_item)
        {
            _type = base_item._type;
            _name = base_item._name;
            _value = base_item._value;
            _rarity = base_item._rarity;
            _affixes = base_item._affixes;
            _icon = base_item._icon;
            _ramConsumed = base_item._ramConsumed;
            _effect = base_item._effect;
            _cooldown = base_item._cooldown;
            _levelRequirement = base_item._levelRequirement;
            _itemLevel = base_item._itemLevel;
            _specialProperties = base_item._specialProperties;
            _favorite = base_item._favorite;
            _flavorText = base_item._flavorText;
        }
    }

    class gItem
    {
        public string _type;
        public string _name;
        public int _value;
        public RarityType _rarity;
        public List<(Affix, float)> _affixes;
        public IconIndex _icon = IconIndex.Laser;
        public int _ramConsumed;
        public float _effect = 1f;
        public float _cooldown = 0.1f;
        public int _levelRequirement;
        public int _itemLevel = 1;
        public string _specialProperties = string.Empty;
        public bool _favorite;
        public string _flavorText = string.Empty;

        public gItem()
        {

        }

        public gItem(string type, string name, int value, RarityType rarity, List<(Affix, float)> affixes, IconIndex icon, int ramConsumed, float effect, float cooldown, int levelRequirement, int itemLevel, string specialProperties, bool favorite, string flavorText)
        {
            _type = type;
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
