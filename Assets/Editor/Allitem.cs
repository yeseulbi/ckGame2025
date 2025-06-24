// FileName: AllItemsGenerator.cs
// Location: Assets/Editor/
// 진짜 최종 완성본: 모든 아이템(무기 34종 포함)의 모든 데이터가 포함되어 있습니다.

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

// 모든 아이템의 데이터를 임시로 담기 위한 단 하나의 공용 구조체
public struct ItemDefinition
{
    public string ItemID;
    public string ItemName;
    public string Description;
    public ItemRarity Rarity;
    public ItemType ItemType;

    // Weapon
    public WeaponType WpnType;
    public float BaseDamage;
    public float BaseAttackDelay;
    public Vector2 AttackPowerIncrease;
    public Vector2 SkillDamageIncrease;
    public Vector2 UniqueStatIncrease;
    public Vector2 StatusEffectChance;

    // Effect-based (Passive, Active, Consumable)
    public List<ItemEffect> Effects;

    // Active
    public float Cooldown;
}


public class AllItemsGenerator
{
    // 각 아이템 타입별 저장 경로
    private const string WEAPON_PATH = "Assets/Resources/Items/Weapons/new/";

    [MenuItem("Game/Generate/Create ALL Items (Final Version)")]
    public static void GenerateAllItems()
    {
        CreateItemsFromDefinitions(GetWeaponDefinitions(), WEAPON_PATH);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("모든 아이템 애셋 생성이 완료되었습니다.");
    }

    private static void CreateItemsFromDefinitions(List<ItemDefinition> definitions, string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        foreach (var def in definitions)
        {
            WeaponItemData item = ScriptableObject.CreateInstance<WeaponItemData>();

            item.itemID = def.ItemID;
            item.itemName = def.ItemName;
            item.description = def.Description;
            item.rarity = def.Rarity;
            item.itemType = def.ItemType;
            item.weaponType = def.WpnType;
            item.baseDamage = def.BaseDamage;
            item.baseAttackDelay = def.BaseAttackDelay;
            item.attackPowerIncrease = def.AttackPowerIncrease;
            item.uniqueStatIncrease = def.UniqueStatIncrease;
            item.statusEffectChance = def.StatusEffectChance;
            item.effects = def.Effects;
            item.cooldown = def.Cooldown;

            item.hideFlags = HideFlags.None;
            AssetDatabase.CreateAsset(item, $"{path}{item.itemID}.asset");
        }
    }

    #region Item Definitions (모든 아이템 데이터 정의)

    private static List<ItemDefinition> GetWeaponDefinitions()
    {
        return new List<ItemDefinition>
        {
            // --- Swords (18) ---
            new ItemDefinition { ItemID = "wp_sword_c_01", ItemName = "모델-7 택티컬 소드", Description = "가장 널리 보급된 모델. 저렴한 가격과 준수한 신뢰성이 특징.", Rarity = ItemRarity.Common, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 10, BaseAttackDelay = 1.0f, AttackPowerIncrease = new Vector2(10, 15), SkillDamageIncrease = new Vector2(15, 25), UniqueStatIncrease = new Vector2(5, 10) },
            new ItemDefinition { ItemID = "wp_sword_c_02", ItemName = "티타늄 합금 나이프", Description = "경량 합금으로 제작되어 휴대성이 좋다.", Rarity = ItemRarity.Common, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 9, BaseAttackDelay = 0.95f, AttackPowerIncrease = new Vector2(10, 15), SkillDamageIncrease = new Vector2(15, 25), UniqueStatIncrease = new Vector2(5, 10) },
            new ItemDefinition { ItemID = "wp_sword_c_03", ItemName = "표준형 전투 단검", Description = "자경단에게 지급되는 표준 장비.", Rarity = ItemRarity.Common, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 11, BaseAttackDelay = 1.0f, AttackPowerIncrease = new Vector2(10, 15), SkillDamageIncrease = new Vector2(15, 25), UniqueStatIncrease = new Vector2(5, 10) },
            new ItemDefinition { ItemID = "wp_sword_c_04", ItemName = "절삭 가공 블레이드", Description = "자동화 공정으로 생산된 날붙이. 내구성은 보장할 수 없다.", Rarity = ItemRarity.Common, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 12, BaseAttackDelay = 1.1f, AttackPowerIncrease = new Vector2(10, 15), SkillDamageIncrease = new Vector2(15, 25), UniqueStatIncrease = new Vector2(5, 10) },
            new ItemDefinition { ItemID = "wp_sword_r_01", ItemName = "고주파 진동 블레이드", Description = "칼날을 미세하게 진동시켜 절삭력을 높였다.", Rarity = ItemRarity.Rare, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 18, BaseAttackDelay = 0.9f, AttackPowerIncrease = new Vector2(15, 25), SkillDamageIncrease = new Vector2(20, 35), UniqueStatIncrease = new Vector2(10, 15) },
            new ItemDefinition { ItemID = "wp_sword_r_02", ItemName = "플라즈마 엣지", Description = "고열의 플라즈마로 장갑을 녹이며 공격한다.", Rarity = ItemRarity.Rare, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 20, BaseAttackDelay = 1.05f, AttackPowerIncrease = new Vector2(15, 25), SkillDamageIncrease = new Vector2(20, 35), UniqueStatIncrease = new Vector2(10, 15) },
            new ItemDefinition { ItemID = "wp_sword_r_03", ItemName = "쇼크 카타나", Description = "고압 전류가 흐르는 동방의 검. 낮은 확률로 적을 마비시킨다.", Rarity = ItemRarity.Rare, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 17, BaseAttackDelay = 1.0f, AttackPowerIncrease = new Vector2(15, 25), SkillDamageIncrease = new Vector2(20, 35), UniqueStatIncrease = new Vector2(10, 15), StatusEffectChance = new Vector2(1, 5) },
            new ItemDefinition { ItemID = "wp_sword_r_04", ItemName = "강화 복합소재 소드", Description = "잊혀진 기술로 제련되어 스킬의 위력을 증폭시킨다.", Rarity = ItemRarity.Rare, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 17, BaseAttackDelay = 1.0f, AttackPowerIncrease = new Vector2(15, 20), SkillDamageIncrease = new Vector2(30, 35) },
            new ItemDefinition { ItemID = "wp_sword_h_01", ItemName = "과열 블레이드", Description = "적중 시 적을 과열시켜 지속적인 화염 피해를 입힌다.", Rarity = ItemRarity.Heroic, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 28, BaseAttackDelay = 1.0f, AttackPowerIncrease = new Vector2(25, 35), SkillDamageIncrease = new Vector2(35, 45), UniqueStatIncrease = new Vector2(15, 20), StatusEffectChance = new Vector2(5, 10) },
            new ItemDefinition { ItemID = "wp_sword_h_02", ItemName = "정지장 커터", Description = "주변 공간을 왜곡하여 적의 움직임을 봉쇄한다.", Rarity = ItemRarity.Heroic, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 26, BaseAttackDelay = 0.95f, AttackPowerIncrease = new Vector2(25, 35), SkillDamageIncrease = new Vector2(35, 45), UniqueStatIncrease = new Vector2(15, 20), StatusEffectChance = new Vector2(7, 10) },
            new ItemDefinition { ItemID = "wp_sword_h_03", ItemName = "글리치 블레이드", Description = "불안정한 데이터로 구성되어 적의 시스템에 오류를 유발한다.", Rarity = ItemRarity.Heroic, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 27, BaseAttackDelay = 1.0f, AttackPowerIncrease = new Vector2(25, 35), SkillDamageIncrease = new Vector2(35, 45), UniqueStatIncrease = new Vector2(15, 20), StatusEffectChance = new Vector2(8, 12) },
            new ItemDefinition { ItemID = "wp_sword_h_04", ItemName = "프로토콜: 슬레이어", Description = "특정 대상을 섬멸하기 위해 설계된 병기. 스킬 공격력이 극대화된다.", Rarity = ItemRarity.Heroic, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 30, BaseAttackDelay = 1.1f, AttackPowerIncrease = new Vector2(25, 35), SkillDamageIncrease = new Vector2(43, 45), UniqueStatIncrease = new Vector2(15, 17) },
            new ItemDefinition { ItemID = "wp_sword_h_05", ItemName = "펄서 세이버", Description = "주기적으로 강력한 에너지 파동을 방출하는 광선검.", Rarity = ItemRarity.Heroic, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 32, BaseAttackDelay = 1.0f, AttackPowerIncrease = new Vector2(30, 35), SkillDamageIncrease = new Vector2(35, 45), UniqueStatIncrease = new Vector2(15, 20) },
            new ItemDefinition { ItemID = "wp_sword_l_01", ItemName = "시공간 절단기", Description = "칼날이 허공을 가르면, 그 궤적을 따라 공간이 비명을 지른다.", Rarity = ItemRarity.Legendary, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 40, BaseAttackDelay = 0.9f, AttackPowerIncrease = new Vector2(35, 40), SkillDamageIncrease = new Vector2(45, 50), UniqueStatIncrease = new Vector2(25, 25), StatusEffectChance = new Vector2(15, 20) },
            new ItemDefinition { ItemID = "wp_sword_l_02", ItemName = "데이터 말소자", Description = "존재의 기록을 지운다. 묵직하고 파괴적인 한 방을 가졌다.", Rarity = ItemRarity.Legendary, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 50, BaseAttackDelay = 1.15f, AttackPowerIncrease = new Vector2(35, 40), SkillDamageIncrease = new Vector2(45, 50), UniqueStatIncrease = new Vector2(20, 22), StatusEffectChance = new Vector2(10, 20) },
            new ItemDefinition { ItemID = "wp_sword_l_03", ItemName = "프로젝트: 오메가", Description = "스킬 사용 시 다른 스킬에도 영향을 미치는 최종 병기.", Rarity = ItemRarity.Legendary, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 42, BaseAttackDelay = 1.0f, AttackPowerIncrease = new Vector2(35, 40), SkillDamageIncrease = new Vector2(50, 50), UniqueStatIncrease = new Vector2(20, 25) },
            new ItemDefinition { ItemID = "wp_sword_l_04", ItemName = "심판의 날", Description = "모든 능력치가 완벽한 균형을 이루는 전설 속의 검.", Rarity = ItemRarity.Legendary, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 45, BaseAttackDelay = 1.0f, AttackPowerIncrease = new Vector2(40, 40), SkillDamageIncrease = new Vector2(50, 50), UniqueStatIncrease = new Vector2(25, 25), StatusEffectChance = new Vector2(20, 20) },
            new ItemDefinition { ItemID = "wp_sword_l_05", ItemName = "싱귤래리티 엣지", Description = "인과율의 특이점. 공격 시 예측 불가능한 효과가 발생한다.", Rarity = ItemRarity.Legendary, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 38, BaseAttackDelay = 1.0f, AttackPowerIncrease = new Vector2(35, 40), SkillDamageIncrease = new Vector2(45, 50), UniqueStatIncrease = new Vector2(20, 25), StatusEffectChance = new Vector2(15, 25) },
            
            // --- Bows (8) ---
            new ItemDefinition { ItemID = "wp_bow_c_01", ItemName = "평범한 활", Description = "구시대의 유물이지만, 여전히 위력적이다.", Rarity = ItemRarity.Common, ItemType = ItemType.Weapon, WpnType = WeaponType.Bow, BaseDamage = 10, BaseAttackDelay = 1.2f, AttackPowerIncrease = new Vector2(5, 10), SkillDamageIncrease = new Vector2(10, 20), UniqueStatIncrease = new Vector2(15, 25) },
            new ItemDefinition { ItemID = "wp_bow_c_02", ItemName = "스탠다드 보우", Description = "자경단에게 표준 지급되는 활.", Rarity = ItemRarity.Common, ItemType = ItemType.Weapon, WpnType = WeaponType.Bow, BaseDamage = 9, BaseAttackDelay = 1.25f, AttackPowerIncrease = new Vector2(5, 10), SkillDamageIncrease = new Vector2(10, 20), UniqueStatIncrease = new Vector2(15, 25) },
            new ItemDefinition { ItemID = "wp_bow_r_01", ItemName = "레일 컴파운드 보우", Description = "전자기 레일의 힘으로 화살을 가속시킨다.", Rarity = ItemRarity.Rare, ItemType = ItemType.Weapon, WpnType = WeaponType.Bow, BaseDamage = 18, BaseAttackDelay = 1.2f, AttackPowerIncrease = new Vector2(10, 20), SkillDamageIncrease = new Vector2(20, 30), UniqueStatIncrease = new Vector2(25, 35) },
            new ItemDefinition { ItemID = "wp_bow_r_02", ItemName = "스팅어 리피터", Description = "반자동 장전 장치로 빠른 연사가 가능하다.", Rarity = ItemRarity.Rare, ItemType = ItemType.Weapon, WpnType = WeaponType.Bow, BaseDamage = 16, BaseAttackDelay = 1.1f, AttackPowerIncrease = new Vector2(10, 20), SkillDamageIncrease = new Vector2(20, 30), UniqueStatIncrease = new Vector2(30, 35) },
            new ItemDefinition { ItemID = "wp_bow_h_01", ItemName = "펄스 스트링 보우", Description = "에너지 활시위가 강력한 펄스를 방출한다.", Rarity = ItemRarity.Heroic, ItemType = ItemType.Weapon, WpnType = WeaponType.Bow, BaseDamage = 30, BaseAttackDelay = 1.2f, AttackPowerIncrease = new Vector2(20, 25), SkillDamageIncrease = new Vector2(30, 35), UniqueStatIncrease = new Vector2(35, 45), StatusEffectChance = new Vector2(5, 10) },
            new ItemDefinition { ItemID = "wp_bow_h_02", ItemName = "이온 차지 보우", Description = "화살에 이온을 코팅하여 시스템 마비를 유발한다.", Rarity = ItemRarity.Heroic, ItemType = ItemType.Weapon, WpnType = WeaponType.Bow, BaseDamage = 28, BaseAttackDelay = 1.2f, AttackPowerIncrease = new Vector2(20, 25), SkillDamageIncrease = new Vector2(30, 35), UniqueStatIncrease = new Vector2(35, 45), StatusEffectChance = new Vector2(10, 15) },
            new ItemDefinition { ItemID = "wp_bow_l_01", ItemName = "가우스 롱보우 '천리안'", Description = "행성 자기장을 이용해 탄자를 발사하는 저격용 병기.", Rarity = ItemRarity.Legendary, ItemType = ItemType.Weapon, WpnType = WeaponType.Bow, BaseDamage = 45, BaseAttackDelay = 1.3f, AttackPowerIncrease = new Vector2(25, 30), SkillDamageIncrease = new Vector2(35, 40), UniqueStatIncrease = new Vector2(45, 60), StatusEffectChance = new Vector2(15, 25) },
            new ItemDefinition { ItemID = "wp_bow_l_02", ItemName = "포톤 런처 '특이점'", Description = "압축된 광자 덩어리를 발사하여 중력 왜곡을 일으킨다.", Rarity = ItemRarity.Legendary, ItemType = ItemType.Weapon, WpnType = WeaponType.Bow, BaseDamage = 42, BaseAttackDelay = 1.2f, AttackPowerIncrease = new Vector2(25, 30), SkillDamageIncrease = new Vector2(35, 40), UniqueStatIncrease = new Vector2(45, 60), StatusEffectChance = new Vector2(20, 25) },

            // --- Hacking Tools (8) ---
            new ItemDefinition { ItemID = "wp_hack_c_01", ItemName = "구형 사이버덱", Description = "암시장에서 거래되는 기본적인 해킹 툴.", Rarity = ItemRarity.Common, ItemType = ItemType.Weapon, WpnType = WeaponType.Hacking, BaseDamage = 8, BaseAttackDelay = 0.85f, AttackPowerIncrease = new Vector2(5, 10), SkillDamageIncrease = new Vector2(10, 20), UniqueStatIncrease = new Vector2(10, 20) },
            new ItemDefinition { ItemID = "wp_hack_c_02", ItemName = "휴대용 터미널", Description = "보안 시스템 우회용으로 제작된 휴대용 단말기.", Rarity = ItemRarity.Common, ItemType = ItemType.Weapon, WpnType = WeaponType.Hacking, BaseDamage = 9, BaseAttackDelay = 0.9f, AttackPowerIncrease = new Vector2(5, 10), SkillDamageIncrease = new Vector2(10, 20), UniqueStatIncrease = new Vector2(10, 20) },
            new ItemDefinition { ItemID = "wp_hack_r_01", ItemName = "ICE 브레이커 v2", Description = "기업의 표준 방화벽을 무력화하기 위한 군용 해킹 툴.", Rarity = ItemRarity.Rare, ItemType = ItemType.Weapon, WpnType = WeaponType.Hacking, BaseDamage = 17, BaseAttackDelay = 0.8f, AttackPowerIncrease = new Vector2(10, 15), SkillDamageIncrease = new Vector2(20, 30), UniqueStatIncrease = new Vector2(20, 30) },
            new ItemDefinition { ItemID = "wp_hack_r_02", ItemName = "뉴로 임플란트 Mk.2", Description = "뇌에 직접 이식하는 신경 인터페이스. 해킹 속도를 높인다.", Rarity = ItemRarity.Rare, ItemType = ItemType.Weapon, WpnType = WeaponType.Hacking, BaseDamage = 15, BaseAttackDelay = 0.7f, AttackPowerIncrease = new Vector2(10, 15), SkillDamageIncrease = new Vector2(20, 30), UniqueStatIncrease = new Vector2(20, 30) },
            new ItemDefinition { ItemID = "wp_hack_h_01", ItemName = "공격용 AI '코어'", Description = "자율적으로 적 시스템을 공격하는 인공지능 바이러스.", Rarity = ItemRarity.Heroic, ItemType = ItemType.Weapon, WpnType = WeaponType.Hacking, BaseDamage = 28, BaseAttackDelay = 0.8f, AttackPowerIncrease = new Vector2(15, 20), SkillDamageIncrease = new Vector2(30, 35), UniqueStatIncrease = new Vector2(30, 40), StatusEffectChance = new Vector2(10, 15) },
            new ItemDefinition { ItemID = "wp_hack_h_02", ItemName = "프로토콜: 오버로드", Description = "대상 시스템에 과부하를 일으켜 내부 회로부터 녹여버린다.", Rarity = ItemRarity.Heroic, ItemType = ItemType.Weapon, WpnType = WeaponType.Hacking, BaseDamage = 32, BaseAttackDelay = 0.9f, AttackPowerIncrease = new Vector2(15, 20), SkillDamageIncrease = new Vector2(30, 35), UniqueStatIncrease = new Vector2(30, 40), StatusEffectChance = new Vector2(15, 20) },
            new ItemDefinition { ItemID = "wp_hack_l_01", ItemName = "'마스터키' 시스템 접근기", Description = "도시의 모든 시스템에 접근할 수 있는 최상위 권한.", Rarity = ItemRarity.Legendary, ItemType = ItemType.Weapon, WpnType = WeaponType.Hacking, BaseDamage = 40, BaseAttackDelay = 0.8f, AttackPowerIncrease = new Vector2(20, 25), SkillDamageIncrease = new Vector2(35, 40), UniqueStatIncrease = new Vector2(40, 50), StatusEffectChance = new Vector2(20, 35) },
            new ItemDefinition { ItemID = "wp_hack_l_02", ItemName = "심층 접속기 '케르베로스'", Description = "정신을 데이터화하여 심층 네트워크로 직접 다이브하기 위한 장치.", Rarity = ItemRarity.Legendary, ItemType = ItemType.Weapon, WpnType = WeaponType.Hacking, BaseDamage = 38, BaseAttackDelay = 0.75f, AttackPowerIncrease = new Vector2(20, 25), SkillDamageIncrease = new Vector2(35, 40), UniqueStatIncrease = new Vector2(40, 50), StatusEffectChance = new Vector2(30, 35) },
        };
    }

    // ... (GetPassiveDefinitions, GetActiveDefinitions 등 나머지 함수들) ...

    #endregion
}