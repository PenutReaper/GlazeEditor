using System.Collections.Generic;

namespace Core
{
    public enum Affix
    {
        HackSpeed,
        HackTimeFlat,
        HackRange,
        MovementSpeed,
        LootFind,
        RAM,
        RAMPerSecond = 7,
        MaxHealth,
        AttackSpeed,
        Accuracy,
        CritChance,
        CritMultiplier,
        WeaponDamagePercentage,
        WeaponDamageFlat,
        WeaponRange,
        Thorns,
        Defense = 22,
        RAMPerSecondPercentage = 24,
        MaxHealthPercentage,
        RecoilReduction = 42,
        SpreadReduction,
        LevelRequirementReduced = 26,
        ChanceToPierce = 17,
        Drunk,
        Knockback,
        Homing,
        ChanceToRicochet,
        ChanceToColorize = 23,
        ChanceToBurn = 45,
        ChanceToFreeze,
        ChanceToSlow,
        PercentRAM = 6,
        ChanceToTeleport = 27,
        XPPercentage,
        PerkSniperHoming,
        PerkLowHPDamageBonus,
        PerkMineMovementSpeed,
        PerkLasgunPiercing,
        PerkMoreDiscs,
        PerkDrunkenMaster,
        PerkStandingStillDamageBonus,
        PerkMovingForwardDamageBonus,
        PerkColorizeConverts,
        PerkGITSfishDoubler,
        PerkAimbotProjectiles,
        PerkThornsPower,
        PerkShotgunDoubleBarrel,
        PerkMachineGun = 44
    }

    public enum PayloadIndex
    {
        Laser,
        PlasmaBall,
        GreenLaserEnemy,
        Missile,
        LasgunLaser,
        Hammer,
        Disc,
        Fireball,
        FireLasgunLaser,
        Frostbolt,
        Slowbolt,
        SlowingLasgunLaser,
        Railgun,
        RainbowShot,
        Mine,
        EnemyLasgunLaser,
        SpiderMine,
        FireEnemyLasgunLaser,
        SlowingEnemyLasgunLaser,
        BouncyBall,
        Dodgeball,
        BouncyFireball,
        DiscoDeathball,
        GreenLaser,
        OrangeLaser,
        BlueLaser,
        PurpleLaser,
        SlowBall
    }

    public enum RarityType
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Unique,
        Any,
        UncommonOrBetter,
        RareOrBetter,
        EpicOrBetter,
        NotUnique
    }

    public enum WeaponModelIndex
    {
        MachineGun,
        Shotgun,
        Sniper,
        Lasgun,
        Shotgun2,
        MachineGun2,
        Bubblegun,
        RayPistol,
        RocketLauncher,
        TeslaCannon,
        Healgun,
        Prismgun,
        Disc,
        Shotgun3,
        Cat,
        Dodgeball
    }

    public enum LaserSoundIndex
    {
        Laser,
        Railgun,
        BouncyBall,
        PlasmaBall,
        Disc,
        Lasgun,
        Mine,
        EnemyLaser,
        Missile,
        Dodgeball,
        Frostbolt,
        Fireball,
        RainbowShot,
        SilverHammer,
        FireBouncyBall
    }

    public enum AbilityIcon
    {
        Laser,
        Knockback,
        Spider,
        Jump,
        Sprint,
        Hack,
        Mine,
        Sniper,
        Shotgun
    }

    public enum IconIndex
    {
        EmptySlot,
        Laser,
        Icebreaker,
        Mod,
        Spider,
        PlasmaBall,
        Missile,
        Jump,
        Sprint,
        Scope,
        Heal,
        Mine,
        Disc,
        Fireball,
        Frostbolt,
        RainbowShot,
        EMP,
        Railgun,
        Lasgun,
        Text,
        Lasbreaker,
        Popup,
        Aimbot,
        GITSfish,
        ActiveSlot,
        HackOLantern,
        Scorpion,
        Cat,
        BouncyBall,
        Dodgeball,
        DiscoDeathball,
        Hammer,
        SubIcon_Machinegun,
        SubIcon_Shotgun,
        SubIcon_Sniper,
        Lock,
        Teleport,
        HealOverTime,
        Frostbiter,
        IceAKill,
        Kokinator,
        Oersted,
        Porta,
        REM,
        Scorchporation,
        Meatball,
        SpiderMine,
        Valentyn,
        SubIcon_Spiral,
        JumpOrig
    }

    public enum EnemyIndex
    {
        Spider,
        Probe,
        Hopper,
        Shark,
        Crab,
        Scorpion,
        Cat,
        CrabMustache,
        CrabSD,
        Snail,
        Mom
    }
}
