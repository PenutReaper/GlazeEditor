using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class PlayerSaveProxy
    {

        // Token: 0x06001069 RID: 4201 RVA: 0x00065028 File Offset: 0x00063428
        public PlayerSaveProxy()
        {
            this.Equipment = new Item[9];
            this.Inventory = new Item[45];
        }

        // Token: 0x04000DB6 RID: 3510
        [JsonProperty("Name")]
        public string Name = string.Empty;

        // Token: 0x04000DB7 RID: 3511
        [JsonProperty("XP")]
        public int XP;

        // Token: 0x04000DB8 RID: 3512
        [JsonProperty("Level")]
        public int Level;

        // Token: 0x04000DB9 RID: 3513
        [JsonProperty("TalentPoints")]
        public int TalentPoints;

        // Token: 0x04000DBA RID: 3514
        [JsonProperty("Talents")]
        public Dictionary<Affix, float> Talents = new Dictionary<Affix, float>(new AffixComparer());

        // Token: 0x04000DBB RID: 3515
        [JsonProperty("PerkPoints")]
        public int PerkPoints;

        // Token: 0x04000DBC RID: 3516
        [JsonProperty("Perks")]
        public Dictionary<Affix, float> Perks = new Dictionary<Affix, float>(new AffixComparer());

        // Token: 0x04000DBD RID: 3517
        [JsonProperty("Hardcore")]
        public bool Hardcore;

        // Token: 0x04000DBE RID: 3518
        [JsonProperty("NewCharacter")]
        public bool NewCharacter = true;

        // Token: 0x04000DBF RID: 3519
        [JsonProperty("Credits")]
        public int Credits = 5;

        // Token: 0x04000DC0 RID: 3520
        [JsonProperty("Equipment")]
        public Item[] Equipment;

        // Token: 0x04000DC1 RID: 3521
        [JsonProperty("Inventory")]
        public Item[] Inventory;

        // Token: 0x04000DC2 RID: 3522
        [JsonProperty("Cheater")]
        public bool Cheater;

        // Token: 0x04000DC3 RID: 3523
        [JsonProperty("HighestServerHacked")]
        public int HighestServerHacked;

        // Token: 0x04000DC4 RID: 3524
        [JsonProperty("TimeSinceLastLevel")]
        public float TimeSinceLastLevel;

        // Token: 0x04000DC5 RID: 3525
        [JsonProperty("WorldState")]
        public Dictionary<WorldState, int> WorldState = new Dictionary<WorldState, int>();
    }
}