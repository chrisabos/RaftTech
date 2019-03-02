using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using UnityEngine;
using Harmony;
using System.Reflection;
using System.IO;

[ModTitle("RaftTech")]
[ModDescription("Technical additions to Raft")]
[ModAuthor("CoCo")]
[ModVersion("0.1")]
[RaftVersion("Update 5")]
public class RaftTech : Mod
{
    Network_Player p;

    private void Start()
    {
        RConsole.Log("RaftTech loaded!");

        var raftTechAssets = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "rafttechassets"));

        ModItem itemTinIngot = new ModItem();
        ModItem itemBronzeIngot = new ModItem();
        ModItem itemSteelIngot = new ModItem();

        ModItem itemCharcoal = new ModItem();
        ModItem itemTinOre = new ModItem();

        ModItem itemMetalDust = new ModItem();
        ModItem itemCopperDust = new ModItem();
        ModItem itemTinDust = new ModItem();
        ModItem itemBronzeDust = new ModItem();
        ModItem itemSteelDust = new ModItem();

        //ModItem itemChickenRaw = new ModItem();
        //itemChickenRaw.Create("Chicken_Raw", ItemType.Drinkable);
        //itemChickenRaw.setCookableSettings(1, 1f, new Cost(ItemManager.GetItemByName("Cooked_Shark"), 1));
        //RAPI.addItem(itemChickenRaw);

        //itemCharcoal.Create("Charcoal", ItemType.Inventory);
        //itemCharcoal.setCookableSettings

        //### INGOTS ###
        itemTinIngot.Create("TinIngot", ItemType.Inventory);
        itemTinIngot.setInventorySettings("Tin Ingot", "Plain old tin", raftTechAssets.LoadAsset<Sprite>("Assets/Item_TinIngot.png"), "Item/TinIngot", 20);
        RAPI.addItem(itemTinIngot);

        itemBronzeIngot.Create("BronzeIngot", ItemType.Inventory);
        itemBronzeIngot.setInventorySettings("Bronze Ingot", "Shiny Alloy", raftTechAssets.LoadAsset<Sprite>("Assets/Item_BronzeIngot.png"), "Item/BronzeIngot", 20);
        RAPI.addItem(itemBronzeIngot);

        itemSteelIngot.Create("SteelIngot", ItemType.Inventory);
        itemSteelIngot.setInventorySettings("Steel Ingot", "Harder than the other metals", raftTechAssets.LoadAsset<Sprite>("Assets/Item_SteelIngot.png"), "Item/SteelIngot", 20);
        RAPI.addItem(itemSteelIngot);

        //### ORE ###
        itemTinOre.Create("TinOre", ItemType.Inventory);
        itemTinOre.setInventorySettings("Tin Ore", "Plain ole tin", raftTechAssets.LoadAsset<Sprite>("Assets/Item_TinOre.png"), "Item/TinOre", 20);
        itemTinOre.setCookableSettings(1, 80, new Cost(itemTinIngot, 1));
        RAPI.addItem(itemTinOre);

        //### DUST ###
        itemMetalDust.Create("MetalDust", ItemType.Inventory);
        itemMetalDust.setInventorySettings("Metal Dust", "Pulverized metal", raftTechAssets.LoadAsset<Sprite>("Assets/Item_IronDust.png"), "Item/MetalDust", 20);
        itemMetalDust.setCookableSettings(1, 55, new Cost(ItemManager.GetItemByName("MetalIngot"), 1));
        RAPI.addItem(itemMetalDust);

        itemCopperDust.Create("CopperDust", ItemType.Inventory);
        itemCopperDust.setInventorySettings("Copper Dust", "Pulverized copper", raftTechAssets.LoadAsset<Sprite>("Assets/Item_CopperDust.png"), "Item/CopperDust", 20);
        itemCopperDust.setCookableSettings(1, 55, new Cost(ItemManager.GetItemByName("CopperIngot"), 1));
        RAPI.addItem(itemCopperDust);

        itemTinDust.Create("TinDust", ItemType.Inventory);
        itemTinDust.setInventorySettings("Tin Dust", "Pulverized tin", raftTechAssets.LoadAsset<Sprite>("Assets/Item_TinDust.png"), "Item/TinDust", 20);
        itemTinDust.setCookableSettings(1, 55, new Cost(itemTinIngot, 1));
        RAPI.addItem(itemTinDust);

        itemBronzeDust.Create("BronzeDust", ItemType.Inventory);
        itemBronzeDust.setInventorySettings("Bronze Dust", "Pulverized bronze", raftTechAssets.LoadAsset<Sprite>("Assets/Item_BronzeDust.png"), "Item/BronzeDust", 20);
        itemBronzeDust.setCookableSettings(1, 55, new Cost(itemBronzeIngot, 1));
        itemBronzeDust.setRecipeSettings(CraftingCategory.Resources, false, false, "MetalDust", 0, 4, new CostMultiple[]
        {
            new CostMultiple(new Item_Base[]{ itemCopperDust}, 3),
            new CostMultiple(new Item_Base[]{ itemTinDust}, 1)
        });
        RAPI.addItem(itemBronzeDust);

        itemSteelDust.Create("SteelDust", ItemType.Inventory);
        itemSteelDust.setInventorySettings("Steel Dust", "Pulverized steel", raftTechAssets.LoadAsset<Sprite>("Assets/Item_SteelDust.png"), "Item/SteelDust", 20);
        itemSteelDust.setCookableSettings(1, 55, new Cost(itemSteelIngot, 1));
        RAPI.addItem(itemSteelDust);

        ItemManager.GetItemByName("Raw_Shark").settings_cookable = new ItemInstance_Cookable(1, 1f, new Cost(ItemManager.GetItemByName("Hinge"), 1));

        RConsole.registerCommand("give", "give yourself an item", "give", give);
        
        OnWorldLoad();
    }

    public void give()
    {
        RAPI.GiveItem(ItemManager.GetItemByName("TinOre"), 99);
        RAPI.GiveItem(ItemManager.GetItemByName("MetalDust"), 99);
        RAPI.GiveItem(ItemManager.GetItemByName("CopperDust"), 99);
        RAPI.GiveItem(ItemManager.GetItemByName("TinDust"), 99);
        RAPI.GiveItem(ItemManager.GetItemByName("BronzeDust"), 99);
        RAPI.GiveItem(ItemManager.GetItemByName("SteelDust"), 99);
        RAPI.GiveItem(ItemManager.GetItemByName("TinIngot"), 99);
        RAPI.GiveItem(ItemManager.GetItemByName("BronzeIngot"), 99);
        RAPI.GiveItem(ItemManager.GetItemByName("SteelIngot"), 99);

        RAPI.GiveItem(ItemManager.GetItemByName("MetalIngot"), 99);
        RAPI.GiveItem(ItemManager.GetItemByName("MetalOre"), 99);
        RAPI.GiveItem(ItemManager.GetItemByName("CopperIngot"), 99);
        RAPI.GiveItem(ItemManager.GetItemByName("CopperOre"), 99);
    }

    public void Update()
    {
        
    }

    public void OnWorldLoad()
    {
        
    }

    public void OnModUnload()
    {
        RConsole.Log("RaftTech has been unloaded!");
        Destroy(gameObject);
    }
}