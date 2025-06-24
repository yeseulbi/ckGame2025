// FileName: AllItemsGenerator.cs
// Location: Assets/Editor/
// ��¥ ���� �ϼ���: ��� ������(���� 34�� ����)�� ��� �����Ͱ� ���ԵǾ� �ֽ��ϴ�.

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

// ��� �������� �����͸� �ӽ÷� ��� ���� �� �ϳ��� ���� ����ü
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
    // �� ������ Ÿ�Ժ� ���� ���
    private const string WEAPON_PATH = "Assets/Resources/Items/Weapons/new/";

    [MenuItem("Game/Generate/Create ALL Items (Final Version)")]
    public static void GenerateAllItems()
    {
        CreateItemsFromDefinitions(GetWeaponDefinitions(), WEAPON_PATH);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("��� ������ �ּ� ������ �Ϸ�Ǿ����ϴ�.");
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

    #region Item Definitions (��� ������ ������ ����)

    private static List<ItemDefinition> GetWeaponDefinitions()
    {
        return new List<ItemDefinition>
        {
            // --- Swords (18) ---
            new ItemDefinition { ItemID = "wp_sword_c_01", ItemName = "��-7 ��Ƽ�� �ҵ�", Description = "���� �θ� ���޵� ��. ������ ���ݰ� �ؼ��� �ŷڼ��� Ư¡.", Rarity = ItemRarity.Common, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 10, BaseAttackDelay = 1.0f, AttackPowerIncrease = new Vector2(10, 15), SkillDamageIncrease = new Vector2(15, 25), UniqueStatIncrease = new Vector2(5, 10) },
            new ItemDefinition { ItemID = "wp_sword_c_02", ItemName = "ƼŸ�� �ձ� ������", Description = "�淮 �ձ����� ���۵Ǿ� �޴뼺�� ����.", Rarity = ItemRarity.Common, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 9, BaseAttackDelay = 0.95f, AttackPowerIncrease = new Vector2(10, 15), SkillDamageIncrease = new Vector2(15, 25), UniqueStatIncrease = new Vector2(5, 10) },
            new ItemDefinition { ItemID = "wp_sword_c_03", ItemName = "ǥ���� ���� �ܰ�", Description = "�ڰ�ܿ��� ���޵Ǵ� ǥ�� ���.", Rarity = ItemRarity.Common, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 11, BaseAttackDelay = 1.0f, AttackPowerIncrease = new Vector2(10, 15), SkillDamageIncrease = new Vector2(15, 25), UniqueStatIncrease = new Vector2(5, 10) },
            new ItemDefinition { ItemID = "wp_sword_c_04", ItemName = "���� ���� ���̵�", Description = "�ڵ�ȭ �������� ����� ������. �������� ������ �� ����.", Rarity = ItemRarity.Common, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 12, BaseAttackDelay = 1.1f, AttackPowerIncrease = new Vector2(10, 15), SkillDamageIncrease = new Vector2(15, 25), UniqueStatIncrease = new Vector2(5, 10) },
            new ItemDefinition { ItemID = "wp_sword_r_01", ItemName = "������ ���� ���̵�", Description = "Į���� �̼��ϰ� �������� ������� ������.", Rarity = ItemRarity.Rare, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 18, BaseAttackDelay = 0.9f, AttackPowerIncrease = new Vector2(15, 25), SkillDamageIncrease = new Vector2(20, 35), UniqueStatIncrease = new Vector2(10, 15) },
            new ItemDefinition { ItemID = "wp_sword_r_02", ItemName = "�ö�� ����", Description = "���� �ö���� �尩�� ���̸� �����Ѵ�.", Rarity = ItemRarity.Rare, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 20, BaseAttackDelay = 1.05f, AttackPowerIncrease = new Vector2(15, 25), SkillDamageIncrease = new Vector2(20, 35), UniqueStatIncrease = new Vector2(10, 15) },
            new ItemDefinition { ItemID = "wp_sword_r_03", ItemName = "��ũ īŸ��", Description = "��� ������ �帣�� ������ ��. ���� Ȯ���� ���� �����Ų��.", Rarity = ItemRarity.Rare, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 17, BaseAttackDelay = 1.0f, AttackPowerIncrease = new Vector2(15, 25), SkillDamageIncrease = new Vector2(20, 35), UniqueStatIncrease = new Vector2(10, 15), StatusEffectChance = new Vector2(1, 5) },
            new ItemDefinition { ItemID = "wp_sword_r_04", ItemName = "��ȭ ���ռ��� �ҵ�", Description = "������ ����� ���õǾ� ��ų�� ������ ������Ų��.", Rarity = ItemRarity.Rare, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 17, BaseAttackDelay = 1.0f, AttackPowerIncrease = new Vector2(15, 20), SkillDamageIncrease = new Vector2(30, 35) },
            new ItemDefinition { ItemID = "wp_sword_h_01", ItemName = "���� ���̵�", Description = "���� �� ���� �������� �������� ȭ�� ���ظ� ������.", Rarity = ItemRarity.Heroic, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 28, BaseAttackDelay = 1.0f, AttackPowerIncrease = new Vector2(25, 35), SkillDamageIncrease = new Vector2(35, 45), UniqueStatIncrease = new Vector2(15, 20), StatusEffectChance = new Vector2(5, 10) },
            new ItemDefinition { ItemID = "wp_sword_h_02", ItemName = "������ Ŀ��", Description = "�ֺ� ������ �ְ��Ͽ� ���� �������� �����Ѵ�.", Rarity = ItemRarity.Heroic, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 26, BaseAttackDelay = 0.95f, AttackPowerIncrease = new Vector2(25, 35), SkillDamageIncrease = new Vector2(35, 45), UniqueStatIncrease = new Vector2(15, 20), StatusEffectChance = new Vector2(7, 10) },
            new ItemDefinition { ItemID = "wp_sword_h_03", ItemName = "�۸�ġ ���̵�", Description = "�Ҿ����� �����ͷ� �����Ǿ� ���� �ý��ۿ� ������ �����Ѵ�.", Rarity = ItemRarity.Heroic, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 27, BaseAttackDelay = 1.0f, AttackPowerIncrease = new Vector2(25, 35), SkillDamageIncrease = new Vector2(35, 45), UniqueStatIncrease = new Vector2(15, 20), StatusEffectChance = new Vector2(8, 12) },
            new ItemDefinition { ItemID = "wp_sword_h_04", ItemName = "��������: �����̾�", Description = "Ư�� ����� �����ϱ� ���� ����� ����. ��ų ���ݷ��� �ش�ȭ�ȴ�.", Rarity = ItemRarity.Heroic, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 30, BaseAttackDelay = 1.1f, AttackPowerIncrease = new Vector2(25, 35), SkillDamageIncrease = new Vector2(43, 45), UniqueStatIncrease = new Vector2(15, 17) },
            new ItemDefinition { ItemID = "wp_sword_h_05", ItemName = "�޼� ���̹�", Description = "�ֱ������� ������ ������ �ĵ��� �����ϴ� ������.", Rarity = ItemRarity.Heroic, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 32, BaseAttackDelay = 1.0f, AttackPowerIncrease = new Vector2(30, 35), SkillDamageIncrease = new Vector2(35, 45), UniqueStatIncrease = new Vector2(15, 20) },
            new ItemDefinition { ItemID = "wp_sword_l_01", ItemName = "�ð��� ���ܱ�", Description = "Į���� ����� ������, �� ������ ���� ������ ����� ������.", Rarity = ItemRarity.Legendary, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 40, BaseAttackDelay = 0.9f, AttackPowerIncrease = new Vector2(35, 40), SkillDamageIncrease = new Vector2(45, 50), UniqueStatIncrease = new Vector2(25, 25), StatusEffectChance = new Vector2(15, 20) },
            new ItemDefinition { ItemID = "wp_sword_l_02", ItemName = "������ ������", Description = "������ ����� �����. �����ϰ� �ı����� �� ���� ������.", Rarity = ItemRarity.Legendary, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 50, BaseAttackDelay = 1.15f, AttackPowerIncrease = new Vector2(35, 40), SkillDamageIncrease = new Vector2(45, 50), UniqueStatIncrease = new Vector2(20, 22), StatusEffectChance = new Vector2(10, 20) },
            new ItemDefinition { ItemID = "wp_sword_l_03", ItemName = "������Ʈ: ���ް�", Description = "��ų ��� �� �ٸ� ��ų���� ������ ��ġ�� ���� ����.", Rarity = ItemRarity.Legendary, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 42, BaseAttackDelay = 1.0f, AttackPowerIncrease = new Vector2(35, 40), SkillDamageIncrease = new Vector2(50, 50), UniqueStatIncrease = new Vector2(20, 25) },
            new ItemDefinition { ItemID = "wp_sword_l_04", ItemName = "������ ��", Description = "��� �ɷ�ġ�� �Ϻ��� ������ �̷�� ���� ���� ��.", Rarity = ItemRarity.Legendary, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 45, BaseAttackDelay = 1.0f, AttackPowerIncrease = new Vector2(40, 40), SkillDamageIncrease = new Vector2(50, 50), UniqueStatIncrease = new Vector2(25, 25), StatusEffectChance = new Vector2(20, 20) },
            new ItemDefinition { ItemID = "wp_sword_l_05", ItemName = "�ַ̱���Ƽ ����", Description = "�ΰ����� Ư����. ���� �� ���� �Ұ����� ȿ���� �߻��Ѵ�.", Rarity = ItemRarity.Legendary, ItemType = ItemType.Weapon, WpnType = WeaponType.Sword, BaseDamage = 38, BaseAttackDelay = 1.0f, AttackPowerIncrease = new Vector2(35, 40), SkillDamageIncrease = new Vector2(45, 50), UniqueStatIncrease = new Vector2(20, 25), StatusEffectChance = new Vector2(15, 25) },
            
            // --- Bows (8) ---
            new ItemDefinition { ItemID = "wp_bow_c_01", ItemName = "����� Ȱ", Description = "���ô��� ����������, ������ �������̴�.", Rarity = ItemRarity.Common, ItemType = ItemType.Weapon, WpnType = WeaponType.Bow, BaseDamage = 10, BaseAttackDelay = 1.2f, AttackPowerIncrease = new Vector2(5, 10), SkillDamageIncrease = new Vector2(10, 20), UniqueStatIncrease = new Vector2(15, 25) },
            new ItemDefinition { ItemID = "wp_bow_c_02", ItemName = "���Ĵٵ� ����", Description = "�ڰ�ܿ��� ǥ�� ���޵Ǵ� Ȱ.", Rarity = ItemRarity.Common, ItemType = ItemType.Weapon, WpnType = WeaponType.Bow, BaseDamage = 9, BaseAttackDelay = 1.25f, AttackPowerIncrease = new Vector2(5, 10), SkillDamageIncrease = new Vector2(10, 20), UniqueStatIncrease = new Vector2(15, 25) },
            new ItemDefinition { ItemID = "wp_bow_r_01", ItemName = "���� ���Ŀ�� ����", Description = "���ڱ� ������ ������ ȭ���� ���ӽ�Ų��.", Rarity = ItemRarity.Rare, ItemType = ItemType.Weapon, WpnType = WeaponType.Bow, BaseDamage = 18, BaseAttackDelay = 1.2f, AttackPowerIncrease = new Vector2(10, 20), SkillDamageIncrease = new Vector2(20, 30), UniqueStatIncrease = new Vector2(25, 35) },
            new ItemDefinition { ItemID = "wp_bow_r_02", ItemName = "���þ� ������", Description = "���ڵ� ���� ��ġ�� ���� ���簡 �����ϴ�.", Rarity = ItemRarity.Rare, ItemType = ItemType.Weapon, WpnType = WeaponType.Bow, BaseDamage = 16, BaseAttackDelay = 1.1f, AttackPowerIncrease = new Vector2(10, 20), SkillDamageIncrease = new Vector2(20, 30), UniqueStatIncrease = new Vector2(30, 35) },
            new ItemDefinition { ItemID = "wp_bow_h_01", ItemName = "�޽� ��Ʈ�� ����", Description = "������ Ȱ������ ������ �޽��� �����Ѵ�.", Rarity = ItemRarity.Heroic, ItemType = ItemType.Weapon, WpnType = WeaponType.Bow, BaseDamage = 30, BaseAttackDelay = 1.2f, AttackPowerIncrease = new Vector2(20, 25), SkillDamageIncrease = new Vector2(30, 35), UniqueStatIncrease = new Vector2(35, 45), StatusEffectChance = new Vector2(5, 10) },
            new ItemDefinition { ItemID = "wp_bow_h_02", ItemName = "�̿� ���� ����", Description = "ȭ�쿡 �̿��� �����Ͽ� �ý��� ���� �����Ѵ�.", Rarity = ItemRarity.Heroic, ItemType = ItemType.Weapon, WpnType = WeaponType.Bow, BaseDamage = 28, BaseAttackDelay = 1.2f, AttackPowerIncrease = new Vector2(20, 25), SkillDamageIncrease = new Vector2(30, 35), UniqueStatIncrease = new Vector2(35, 45), StatusEffectChance = new Vector2(10, 15) },
            new ItemDefinition { ItemID = "wp_bow_l_01", ItemName = "���콺 �պ��� 'õ����'", Description = "�༺ �ڱ����� �̿��� ź�ڸ� �߻��ϴ� ���ݿ� ����.", Rarity = ItemRarity.Legendary, ItemType = ItemType.Weapon, WpnType = WeaponType.Bow, BaseDamage = 45, BaseAttackDelay = 1.3f, AttackPowerIncrease = new Vector2(25, 30), SkillDamageIncrease = new Vector2(35, 40), UniqueStatIncrease = new Vector2(45, 60), StatusEffectChance = new Vector2(15, 25) },
            new ItemDefinition { ItemID = "wp_bow_l_02", ItemName = "���� ��ó 'Ư����'", Description = "����� ���� ����� �߻��Ͽ� �߷� �ְ��� ����Ų��.", Rarity = ItemRarity.Legendary, ItemType = ItemType.Weapon, WpnType = WeaponType.Bow, BaseDamage = 42, BaseAttackDelay = 1.2f, AttackPowerIncrease = new Vector2(25, 30), SkillDamageIncrease = new Vector2(35, 40), UniqueStatIncrease = new Vector2(45, 60), StatusEffectChance = new Vector2(20, 25) },

            // --- Hacking Tools (8) ---
            new ItemDefinition { ItemID = "wp_hack_c_01", ItemName = "���� ���̹���", Description = "�Ͻ��忡�� �ŷ��Ǵ� �⺻���� ��ŷ ��.", Rarity = ItemRarity.Common, ItemType = ItemType.Weapon, WpnType = WeaponType.Hacking, BaseDamage = 8, BaseAttackDelay = 0.85f, AttackPowerIncrease = new Vector2(5, 10), SkillDamageIncrease = new Vector2(10, 20), UniqueStatIncrease = new Vector2(10, 20) },
            new ItemDefinition { ItemID = "wp_hack_c_02", ItemName = "�޴�� �͹̳�", Description = "���� �ý��� ��ȸ������ ���۵� �޴�� �ܸ���.", Rarity = ItemRarity.Common, ItemType = ItemType.Weapon, WpnType = WeaponType.Hacking, BaseDamage = 9, BaseAttackDelay = 0.9f, AttackPowerIncrease = new Vector2(5, 10), SkillDamageIncrease = new Vector2(10, 20), UniqueStatIncrease = new Vector2(10, 20) },
            new ItemDefinition { ItemID = "wp_hack_r_01", ItemName = "ICE �극��Ŀ v2", Description = "����� ǥ�� ��ȭ���� ����ȭ�ϱ� ���� ���� ��ŷ ��.", Rarity = ItemRarity.Rare, ItemType = ItemType.Weapon, WpnType = WeaponType.Hacking, BaseDamage = 17, BaseAttackDelay = 0.8f, AttackPowerIncrease = new Vector2(10, 15), SkillDamageIncrease = new Vector2(20, 30), UniqueStatIncrease = new Vector2(20, 30) },
            new ItemDefinition { ItemID = "wp_hack_r_02", ItemName = "���� ���ö�Ʈ Mk.2", Description = "���� ���� �̽��ϴ� �Ű� �������̽�. ��ŷ �ӵ��� ���δ�.", Rarity = ItemRarity.Rare, ItemType = ItemType.Weapon, WpnType = WeaponType.Hacking, BaseDamage = 15, BaseAttackDelay = 0.7f, AttackPowerIncrease = new Vector2(10, 15), SkillDamageIncrease = new Vector2(20, 30), UniqueStatIncrease = new Vector2(20, 30) },
            new ItemDefinition { ItemID = "wp_hack_h_01", ItemName = "���ݿ� AI '�ھ�'", Description = "���������� �� �ý����� �����ϴ� �ΰ����� ���̷���.", Rarity = ItemRarity.Heroic, ItemType = ItemType.Weapon, WpnType = WeaponType.Hacking, BaseDamage = 28, BaseAttackDelay = 0.8f, AttackPowerIncrease = new Vector2(15, 20), SkillDamageIncrease = new Vector2(30, 35), UniqueStatIncrease = new Vector2(30, 40), StatusEffectChance = new Vector2(10, 15) },
            new ItemDefinition { ItemID = "wp_hack_h_02", ItemName = "��������: �����ε�", Description = "��� �ý��ۿ� �����ϸ� ������ ���� ȸ�κ��� �쿩������.", Rarity = ItemRarity.Heroic, ItemType = ItemType.Weapon, WpnType = WeaponType.Hacking, BaseDamage = 32, BaseAttackDelay = 0.9f, AttackPowerIncrease = new Vector2(15, 20), SkillDamageIncrease = new Vector2(30, 35), UniqueStatIncrease = new Vector2(30, 40), StatusEffectChance = new Vector2(15, 20) },
            new ItemDefinition { ItemID = "wp_hack_l_01", ItemName = "'������Ű' �ý��� ���ٱ�", Description = "������ ��� �ý��ۿ� ������ �� �ִ� �ֻ��� ����.", Rarity = ItemRarity.Legendary, ItemType = ItemType.Weapon, WpnType = WeaponType.Hacking, BaseDamage = 40, BaseAttackDelay = 0.8f, AttackPowerIncrease = new Vector2(20, 25), SkillDamageIncrease = new Vector2(35, 40), UniqueStatIncrease = new Vector2(40, 50), StatusEffectChance = new Vector2(20, 35) },
            new ItemDefinition { ItemID = "wp_hack_l_02", ItemName = "���� ���ӱ� '�ɸ����ν�'", Description = "������ ������ȭ�Ͽ� ���� ��Ʈ��ũ�� ���� ���̺��ϱ� ���� ��ġ.", Rarity = ItemRarity.Legendary, ItemType = ItemType.Weapon, WpnType = WeaponType.Hacking, BaseDamage = 38, BaseAttackDelay = 0.75f, AttackPowerIncrease = new Vector2(20, 25), SkillDamageIncrease = new Vector2(35, 40), UniqueStatIncrease = new Vector2(40, 50), StatusEffectChance = new Vector2(30, 35) },
        };
    }

    // ... (GetPassiveDefinitions, GetActiveDefinitions �� ������ �Լ���) ...

    #endregion
}