using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Core
{
    class FileParser
    {
        string RawSave;
        public gEquipment Equip = new gEquipment();
        public gInventory Inv = new gInventory();
        public gPlayerInfo Info = new gPlayerInfo();

        public FileParser(string rawSave) => RawSave = rawSave;

        public void Parse()
        {
            JObject saveData = (JObject)JsonConvert.DeserializeObject(RawSave.Substring(0, RawSave.LastIndexOf("}")+1));

            Info.Hash = RawSave.Substring(RawSave.LastIndexOf("}"));
            Info.Credits = (int) saveData.GetValue("Credits");
            Info.Hardcore = (bool) saveData.GetValue("Hardcore");
            Info.Level = (int) saveData.GetValue("Level");
            Info.Name = (string) saveData.GetValue("Name");
            Info.NewCharacter = (bool) saveData.GetValue("NewCharacter");
            Info.PerkPoints = (int) saveData.GetValue("PerkPoints");

            foreach (JProperty perk in saveData.GetValue("Perks").Children())
            {
                Affix toAdd;
                if (Enum.TryParse<Affix>(perk.Name, out toAdd))
                {
                    Info.Perks.Add((toAdd, float.Parse((string)perk.Value)));
                }
            }

            Info.TalentPoints = (int) saveData.GetValue("TalentPoints");

            foreach (JProperty perk in saveData.GetValue("Talents").Children())
            {
                Affix toAdd;
                if (Enum.TryParse<Affix>(perk.Name, out toAdd))
                {
                    Info.Talents.Add((toAdd, float.Parse((string)perk.Value)));
                }
            }

            Info.XP = (int) saveData.GetValue("XP");

            //The actual item information is a few levels deep, so we have to do this to get at it
            JObject e_setup = (JObject)saveData.GetValue("Equipment");
            JArray e_setup2 = (JArray)e_setup.GetValue("$values");

            foreach (JObject item in e_setup2)
            {
                gItem new_item = new gItem();

                new_item._type = (string)item.GetValue("$type");
                new_item._name = (string)item.GetValue("Name");
                new_item._cooldown = (float)item.GetValue("Cooldown");
                new_item._effect = (float)item.GetValue("Effect");
                new_item._favorite = (bool)item.GetValue("Favorite");
                new_item._flavorText = (string)item.GetValue("FlavorText");
                new_item._icon = (IconIndex)Enum.Parse(typeof(IconIndex), (string)item.GetValue("Icon"));
                new_item._itemLevel = (int)item.GetValue("ItemLevel");
                new_item._levelRequirement = (int)item.GetValue("LevelRequirement");
                new_item._ramConsumed = (int)item.GetValue("RamConsumed");
                new_item._rarity = (RarityType)Enum.Parse(typeof(RarityType), (string)item.GetValue("Rarity"));
                new_item._specialProperties = (string)item.GetValue("SpecialProperties");
                new_item._value = (int)item.GetValue("Value");

                //new_item._affixes 
                new_item._affixes = new List<(Affix, float)>();
                foreach (JProperty affix in item.GetValue("Affixes"))
                {
                    Affix toAdd;
                    if (Enum.TryParse<Affix>(affix.Name, out toAdd))
                    {
                        new_item._affixes.Add((toAdd, float.Parse((string)affix.Value)));
                    }
                }

                if (item.GetValue("ProjectileType") != null)
                {
                    gWeapon new_wep = new gWeapon(new_item);

                    new_wep._minDamage = (int)item.GetValue("MinDamage");
                    new_wep._maxDamage = (int)item.GetValue("MaxDamage");
                    new_wep._projectileType = (PayloadIndex)Enum.Parse(typeof(PayloadIndex), (string)item.GetValue("ProjectileType"));
                    new_wep._projectileSpeed = (int)item.GetValue("ProjectileSpeed");
                    new_wep._range = (int)item.GetValue("Range");
                    new_wep._accuracy = (int)item.GetValue("Accuracy");
                    new_wep._projectileCount = (int)item.GetValue("ProjectileCount");
                    new_wep._pitch = (float)item.GetValue("Pitch");
                    new_wep._weaponSound = (LaserSoundIndex)Enum.Parse(typeof(LaserSoundIndex), (string)item.GetValue("WeaponSound"));
                    new_wep._damageFalloff = (bool)item.GetValue("DamageFalloff");

                    JObject spread = (JObject)item.GetValue("WeaponSpread");
                    new_wep._spread = new List<(string, float)>
                    {
                        ("MaxSpread", (float)spread.GetValue("MaxSpread")),
                        ("SpreadPerShot", (float)spread.GetValue("SpreadPerShot")),
                        ("SecondsToNormal", (float)spread.GetValue("SecondsToNormal"))
                    };

                    new_wep._weaponModel = (WeaponModelIndex)Enum.Parse(typeof(WeaponModelIndex), (string)item.GetValue("WeaponModel"));

                    Equip.Items.Add(new_wep);
                }
                else if (item.GetValue("EnemyToSpawn") != null)
                {
                    gMinion new_min = new gMinion(new_item);
                    new_min._minDamage = (int)item.GetValue("MinDamage");
                    new_min._maxDamage = (int)item.GetValue("MaxDamage");
                    new_min._enemyToSpawn = (EnemyIndex)Enum.Parse(typeof(EnemyIndex), (string)item.GetValue("EnemyToSpawn"));
                    new_min._health = (int)item.GetValue("Health");
                }
                else
                {
                    Equip.Items.Add(new_item);
                }
            }

            //The actual item information is a few levels deep, so we have to do this to get at it
            JObject i_setup = (JObject)saveData.GetValue("Inventory");
            JArray i_setup2 = (JArray)e_setup.GetValue("$values");

            foreach (JObject item in i_setup2)
            {
                gItem new_item = new gItem();

                new_item._type = (string)item.GetValue("$type");
                new_item._name = (string)item.GetValue("Name");
                new_item._cooldown = (float)item.GetValue("Cooldown");
                new_item._effect = (float)item.GetValue("Effect");
                new_item._favorite = (bool)item.GetValue("Favorite");
                new_item._flavorText = (string)item.GetValue("FlavorText");
                new_item._icon = (IconIndex)Enum.Parse(typeof(IconIndex), (string)item.GetValue("Icon"));
                new_item._itemLevel = (int)item.GetValue("ItemLevel");
                new_item._levelRequirement = (int)item.GetValue("LevelRequirement");
                new_item._ramConsumed = (int)item.GetValue("RamConsumed");
                new_item._rarity = (RarityType)Enum.Parse(typeof(RarityType), (string)item.GetValue("Rarity"));
                new_item._specialProperties = (string)item.GetValue("SpecialProperties");
                new_item._value = (int)item.GetValue("Value");

                //new_item._affixes 
                new_item._affixes = new List<(Affix, float)>();
                foreach (JProperty affix in item.GetValue("Affixes"))
                {
                    Affix toAdd;
                    if (Enum.TryParse<Affix>(affix.Name, out toAdd))
                    {
                        new_item._affixes.Add((toAdd, float.Parse((string)affix.Value)));
                    }
                }

                Inv.Items.Add(new_item);
            }

            Info.Cheater = (bool)saveData.GetValue("Cheater");
            Info.HighestServerHacked = (int)saveData.GetValue("HighestServerHacked");
            Info.TimeSinceLastLevel = (float)saveData.GetValue("TimeSinceLastLevel");

            JObject state = (JObject)saveData.GetValue("WorldState");
            Info.WorldState = new List<(string, int)>
            {
                ("Door_Portcullis", (int?) state.GetValue("Door_Portcullis") ?? 0) ,
                ("Door_Finality1", (int?) state.GetValue("Door_Finality1") ?? 0) 
            };
        }
    }
}
