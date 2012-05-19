Public Module DatabaseCode
    'this file creates the xsd from scratch
    'we don't want a typed-dataset, because it's less flexible
    'once we have a flexible way to do the GUI, this format should pay off
    Public Const SchemaName As String = "JA2Data"
    Public Const SchemaFileName As String = SchemaName & ".xsd"
    'JMich
    Public ItemSizesRead As Boolean = False
    Public ItemSizeMax As Integer
    Private ModDir As String = ""

    Public Sub ReadItemSizes()
        Dim xr As New Xml.XmlTextReader("XMLEditorInit.xml")
        Dim curNode As String = ""
        Dim curValue As String = ""
        While xr.Read
            If xr.NodeType = Xml.XmlNodeType.Element Then
                curNode = xr.Name
            ElseIf xr.NodeType = Xml.XmlNodeType.Text Then
                curValue = xr.Value
                Select Case curNode
                    Case "Data_Directory"
                        ModDir = curValue
                        If Not ModDir.EndsWith("\") Then ModDir &= "\"
                End Select
            End If
        End While
        xr.Close()
        Dim ja2() As String = System.IO.File.ReadAllLines(ModDir & "ja2_options.ini")
        For Each line As String In ja2
            Dim arr As String() = line.Split("=")
            If arr(0).Trim = "MAX_ITEM_SIZE" Then
                ItemSizeMax = arr(1)
                ItemSizesRead = True
            End If
        Next
        If Not ItemSizesRead Then ItemSizeMax = 34
    End Sub

    Public Sub MakeDB()
        Dim ds As New DataSet(SchemaName)

        'standard tables
        Dim items As DataTable = MakeItemsTable()
        Dim weapons As DataTable = MakeWeaponsTable()
        Dim merges As DataTable = MakeMergeTable()
        Dim magazines As DataTable = MakeMagazineTable()
        Dim launchables As DataTable = MakeLaunchableTable()
        Dim ammoTypes As DataTable = MakeAmmoTypesTable()
        Dim ammoStrings As DataTable = MakeAmmoStringsTable()
        Dim attachments As DataTable = MakeAttachmentTable()
        Dim attachmentInfo As DataTable = MakeAttachmentInfoTable()
        Dim attachmentComboMerges As DataTable = MakeAttachmentComboMergeTable()
        Dim armours As DataTable = MakeArmourTable()
        Dim compatibleFaceItems As DataTable = MakeCompatibleFaceItemTable()
        Dim incompatibleAttachments As DataTable = MakeIncompatibleAttachmentTable()
        Dim explosionData As DataTable = MakeExplosionDataTable()
        Dim explosives As DataTable = MakeExplosiveTable()
        Dim germanAmmoStrings As DataTable = MakeLocalizedAmmoStringsTable("German")
        Dim russianAmmoStrings As DataTable = MakeLocalizedAmmoStringsTable("Russian")
        Dim polishAmmoStrings As DataTable = MakeLocalizedAmmoStringsTable("Polish")
        Dim frenchAmmoStrings As DataTable = MakeLocalizedAmmoStringsTable("French")
        Dim italianAmmoStrings As DataTable = MakeLocalizedAmmoStringsTable("Italian")
        Dim dutchAmmoStrings As DataTable = MakeLocalizedAmmoStringsTable("Dutch")
        Dim chineseAmmoStrings As DataTable = MakeLocalizedAmmoStringsTable("Chinese")
        Dim germanItems As DataTable = MakeLocalizedItemsTable("German")
        Dim russianItems As DataTable = MakeLocalizedItemsTable("Russian")
        Dim polishItems As DataTable = MakeLocalizedItemsTable("Polish")
        Dim frenchItems As DataTable = MakeLocalizedItemsTable("French")
        Dim italianItems As DataTable = MakeLocalizedItemsTable("Italian")
        Dim dutchItems As DataTable = MakeLocalizedItemsTable("Dutch")
        Dim chineseItems As DataTable = MakeLocalizedItemsTable("Chinese")
        Dim sounds As DataTable = MakeSoundsTable()
        Dim burstSounds As DataTable = MakeBurstSoundsTable()
        Dim impItems As DataTable = MakeIMPItemsTable()
        Dim enemyGuns As DataTable = MakeEnemyGunsTable()
        Dim enemyItems As DataTable = MakeEnemyItemsTable()
        Dim enemyAmmo As DataTable = MakeEnemyAmmoTable()
        Dim enemyAmmoDrop As DataTable = MakeEnemyAmmoDropTable()
        Dim enemyArmourDrop As DataTable = MakeEnemyArmourDropTable()
        Dim enemyExplosiveDrop As DataTable = MakeEnemyExplosiveDropTable()
        Dim enemyMiscDrop As DataTable = MakeEnemyMiscItemDropTable()
        Dim enemyWeaponDrop As DataTable = MakeEnemyWeaponDropTable()
        Dim loadBearingEquipment As DataTable = MakeLoadBearingEquipmentTable()
        Dim pockets As DataTable = MakePocketsTable()
        Dim mercStartingGear As DataTable = MakeMercStartingGearTable()
        Dim attachmentSlots As DataTable = MakeAttachmentSlotsTable()
        Dim itemsToExplosives As DataTable = MakeITETable()

        Dim albertoControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Alberto)
        Dim arnieControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Arnie)
        Dim carloControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Carlo)
        Dim devinControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Devin)
        Dim elginControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Elgin)
        Dim frankControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Frank)
        Dim franzControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Franz)
        Dim fredoControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Fredo)
        Dim gabbyControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Gabby)
        Dim herveControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Herve)
        Dim howardControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Howard)
        Dim jakeControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Jake)
        Dim keithControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Keith)
        Dim mannyControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Manny)
        Dim mickeyControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Mickey)
        Dim perkoControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Perko)
        Dim peterControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Peter)
        Dim samControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Sam)
        Dim tonyControl As DataTable = MakeShopKeeperControlTable(ShopKeepers.Tony)

        Dim alberto As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Alberto)
        Dim arnie As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Arnie)
        Dim carlo As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Carlo)
        Dim devin As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Devin)
        Dim elgin As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Elgin)
        Dim frank As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Frank)
        Dim franz As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Franz)
        Dim fredo As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Fredo)
        Dim gabby As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Gabby)
        Dim herve As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Herve)
        Dim howard As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Howard)
        Dim jake As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Jake)
        Dim keith As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Keith)
        Dim manny As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Manny)
        Dim mickey As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Mickey)
        Dim perko As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Perko)
        Dim peter As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Peter)
        Dim sam As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Sam)
        Dim tony As DataTable = MakeShopKeeperInventoryTable(ShopKeepers.Tony)

        ds.Tables.Add(items)
        ds.Tables.Add(weapons)
        ds.Tables.Add(merges)
        ds.Tables.Add(magazines)
        ds.Tables.Add(launchables)
        ds.Tables.Add(ammoTypes)
        ds.Tables.Add(ammoStrings)
        ds.Tables.Add(attachments)
        ds.Tables.Add(attachmentInfo)
        ds.Tables.Add(attachmentComboMerges)
        ds.Tables.Add(armours)
        ds.Tables.Add(compatibleFaceItems)
        ds.Tables.Add(incompatibleAttachments)
        ds.Tables.Add(explosionData)
        ds.Tables.Add(explosives)
        ds.Tables.Add(germanAmmoStrings)
        ds.Tables.Add(russianAmmoStrings)
        ds.Tables.Add(polishAmmoStrings)
        ds.Tables.Add(frenchAmmoStrings)
        ds.Tables.Add(italianAmmoStrings)
        ds.Tables.Add(dutchAmmoStrings)
        ds.Tables.Add(chineseAmmoStrings)
        ds.Tables.Add(germanItems)
        ds.Tables.Add(russianItems)
        ds.Tables.Add(polishItems)
        ds.Tables.Add(frenchItems)
        ds.Tables.Add(italianItems)
        ds.Tables.Add(dutchItems)
        ds.Tables.Add(chineseItems)
        ds.Tables.Add(sounds)
        ds.Tables.Add(burstSounds)
        ds.Tables.Add(albertoControl)
        ds.Tables.Add(arnieControl)
        ds.Tables.Add(carloControl)
        ds.Tables.Add(devinControl)
        ds.Tables.Add(elginControl)
        ds.Tables.Add(frankControl)
        ds.Tables.Add(franzControl)
        ds.Tables.Add(fredoControl)
        ds.Tables.Add(gabbyControl)
        ds.Tables.Add(herveControl)
        ds.Tables.Add(howardControl)
        ds.Tables.Add(jakeControl)
        ds.Tables.Add(keithControl)
        ds.Tables.Add(mannyControl)
        ds.Tables.Add(mickeyControl)
        ds.Tables.Add(perkoControl)
        ds.Tables.Add(peterControl)
        ds.Tables.Add(samControl)
        ds.Tables.Add(tonyControl)
        ds.Tables.Add(alberto)
        ds.Tables.Add(arnie)
        ds.Tables.Add(carlo)
        ds.Tables.Add(devin)
        ds.Tables.Add(elgin)
        ds.Tables.Add(frank)
        ds.Tables.Add(franz)
        ds.Tables.Add(fredo)
        ds.Tables.Add(gabby)
        ds.Tables.Add(herve)
        ds.Tables.Add(howard)
        ds.Tables.Add(jake)
        ds.Tables.Add(keith)
        ds.Tables.Add(manny)
        ds.Tables.Add(mickey)
        ds.Tables.Add(perko)
        ds.Tables.Add(peter)
        ds.Tables.Add(sam)
        ds.Tables.Add(tony)
        ds.Tables.Add(impItems)
        ds.Tables.Add(enemyGuns)
        ds.Tables.Add(enemyItems)
        ds.Tables.Add(enemyAmmo)
        ds.Tables.Add(enemyAmmoDrop)
        ds.Tables.Add(enemyArmourDrop)
        ds.Tables.Add(enemyExplosiveDrop)
        ds.Tables.Add(enemyMiscDrop)
        ds.Tables.Add(enemyWeaponDrop)
        ds.Tables.Add(loadBearingEquipment)
        ds.Tables.Add(pockets)
        ds.Tables.Add(mercStartingGear)
        ds.Tables.Add(attachmentSlots)
        ds.Tables.Add(itemsToExplosives)
        'ds.Tables.Add(spreadPatterns)

        'lookup tables
        Dim mergeTypes As DataTable = MakeLookupTable("MergeType")
        Dim explosionTypes As DataTable = MakeLookupTable("ExplosionType")
        Dim explosionSize As DataTable = MakeLookupTable("ExplosionSize")
        Dim itemClasses As DataTable = MakeLookupTable("ItemClass")
        Dim skillCheckTypes As DataTable = MakeLookupTable("SkillCheckType")
        Dim armourClasses As DataTable = MakeLookupTable("ArmourClass")
        Dim weaponTypes As DataTable = MakeLookupTable("WeaponType")
        Dim weaponClasses As DataTable = MakeLookupTable("WeaponClass")
        Dim cursors As DataTable = MakeLookupTable("Cursor")
        Dim lbeClasses As DataTable = MakeLookupTable("LBEClass")
        Dim silhouettes As DataTable = MakeLookupTable("Silhouette")
        Dim pocketSizes As DataTable = MakeLookupTable("PocketSize")
        Dim AttachmentClasses As DataTable = MakeLookupTable("AttachmentClass")
        Dim AttachmentSystem As DataTable = MakeLookupTable("AttachmentSystem")

        ds.Tables.Add(mergeTypes)
        ds.Tables.Add(explosionTypes)
        ds.Tables.Add(explosionSize)
        ds.Tables.Add(itemClasses)
        ds.Tables.Add(skillCheckTypes)
        ds.Tables.Add(armourClasses)
        ds.Tables.Add(weaponTypes)
        ds.Tables.Add(weaponClasses)
        ds.Tables.Add(cursors)
        ds.Tables.Add(lbeClasses)
        ds.Tables.Add(silhouettes)
        ds.Tables.Add(pocketSizes)
        ds.Tables.Add(AttachmentClasses)
        ds.Tables.Add(AttachmentSystem)

        ' -------------------------
        'relations
        ' -------------------------
        ds.Relations.Add(MakeRelation(items, merges, "uiIndex", "firstItemIndex"))
        ds.Relations.Add(MakeRelation(items, merges, "uiIndex", "secondItemIndex"))
        ds.Relations.Add(MakeRelation(items, merges, "uiIndex", "firstResultingItemIndex"))
        ds.Relations.Add(MakeRelation(items, merges, "uiIndex", "secondResultingItemIndex"))
        ds.Relations.Add(MakeRelation(mergeTypes, merges, "id", "mergeType"))

        ds.Relations.Add(MakeRelation(ammoTypes, magazines, "uiIndex", "ubAmmoType"))
        ds.Relations.Add(MakeRelation(ammoStrings, magazines, "uiIndex", "ubCalibre"))

        ds.Relations.Add(MakeRelation(items, launchables, "uiIndex", "launchableIndex"))
        ds.Relations.Add(MakeRelation(items, launchables, "uiIndex", "itemIndex"))

        ds.Relations.Add(MakeRelation(items, incompatibleAttachments, "uiIndex", "itemIndex"))
        ds.Relations.Add(MakeRelation(items, incompatibleAttachments, "uiIndex", "incompatibleattachmentIndex"))

        ds.Relations.Add(MakeRelation(explosionData, explosives, "uiIndex", "ubAnimationID"))
        ds.Relations.Add(MakeRelation(explosionTypes, explosives, "id", "ubType"))
        ds.Relations.Add(MakeRelation(explosionSize, ammoTypes, "id", "explosionSize"))

        ds.Relations.Add(MakeRelation(sounds, explosionData, "id", "ExplosionSoundID"))

        ds.Relations.Add(MakeRelation(items, compatibleFaceItems, "uiIndex", "compatiblefaceitemIndex"))
        ds.Relations.Add(MakeRelation(items, compatibleFaceItems, "uiIndex", "itemIndex"))

        ds.Relations.Add(MakeRelation(items, attachmentInfo, "uiIndex", "usItem"))
        ds.Relations.Add(MakeRelation(itemClasses, attachmentInfo, "id", "uiItemClass"))
        ds.Relations.Add(MakeRelation(skillCheckTypes, attachmentInfo, "id", "bAttachmentSkillCheck"))

        ds.Relations.Add(MakeRelation(items, attachmentComboMerges, "uiIndex", "usItem"))
        ds.Relations.Add(MakeRelation(items, attachmentComboMerges, "uiIndex", "usAttachment1"))
        ds.Relations.Add(MakeRelation(items, attachmentComboMerges, "uiIndex", "usAttachment2"))
        ds.Relations.Add(MakeRelation(items, attachmentComboMerges, "uiIndex", "usResult"))

        ds.Relations.Add(MakeRelation(items, attachments, "uiIndex", "attachmentIndex"))
        ds.Relations.Add(MakeRelation(items, attachments, "uiIndex", "itemIndex"))

        ds.Relations.Add(MakeRelation(armourClasses, armours, "id", "ubArmourClass"))

        ds.Relations.Add(MakeRelation(weaponTypes, weapons, "id", "ubWeaponType"))
        ds.Relations.Add(MakeRelation(weaponClasses, weapons, "id", "ubWeaponClass"))
        ds.Relations.Add(MakeRelation(ammoStrings, weapons, "uiIndex", "ubCalibre"))
        ds.Relations.Add(MakeRelation(sounds, weapons, "id", "sSound"))
        ds.Relations.Add(MakeRelation(sounds, weapons, "id", "SilencedSound"))
        ds.Relations.Add(MakeRelation(sounds, weapons, "id", "sReloadSound"))
        ds.Relations.Add(MakeRelation(sounds, weapons, "id", "sLocknLoadSound"))
        ds.Relations.Add(MakeRelation(sounds, weapons, "id", "ManualReloadSound"))
        ds.Relations.Add(MakeRelation(sounds, weapons, "id", "sSilencedBurstSound"))
        ds.Relations.Add(MakeRelation(sounds, weapons, "id", "sBurstSound"))

        ds.Relations.Add(MakeRelation(itemClasses, items, "id", "usItemClass"))
        ds.Relations.Add(MakeRelation(cursors, items, "id", "ubCursor"))

        ds.Relations.Add(MakeRelation(lbeClasses, loadBearingEquipment, "id", "lbeClass"))
        ds.Relations.Add(MakeRelation(silhouettes, pockets, "id", "pSilhouette"))

        ds.Relations.Add(MakeRelation(items, alberto, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, arnie, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, carlo, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, devin, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, elgin, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, frank, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, franz, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, fredo, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, gabby, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, herve, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, howard, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, jake, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, keith, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, manny, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, mickey, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, perko, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, peter, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, sam, "uiIndex", "sItemIndex"))
        ds.Relations.Add(MakeRelation(items, tony, "uiIndex", "sItemIndex"))

        ds.Relations.Add(MakeRelation(ammoTypes, enemyAmmoDrop, "uiIndex", "uiType"))
        ds.Relations.Add(MakeRelation(armourClasses, enemyArmourDrop, "id", "ubArmourClass"))
        ds.Relations.Add(MakeRelation(explosionTypes, enemyExplosiveDrop, "id", "ubType"))
        ds.Relations.Add(MakeRelation(itemClasses, enemyMiscDrop, "id", "usItemClass"))
        ds.Relations.Add(MakeRelation(weaponTypes, enemyWeaponDrop, "id", "ubWeaponType"))

        ' -------------------------
        'populate Lookup Tables
        ' -------------------------
        Dim lookupFilename As String

        ' SkillCheckTypes
        lookupFilename = XmlDB.BaseDirectory + "\\Lookup\\" + skillCheckTypes.TableName + ".xml"
        If System.IO.File.Exists(lookupFilename) = False Then
            AddLookupData(skillCheckTypes, 0, "None")
            AddLookupData(skillCheckTypes, 1, "Lockpick")
            AddLookupData(skillCheckTypes, 2, "Elec. Lockpick")
            AddLookupData(skillCheckTypes, 3, "Attach Timed Detonator")
            AddLookupData(skillCheckTypes, 4, "Attach Remote Detonator")
            AddLookupData(skillCheckTypes, 5, "Plant Timed Bomb")
            AddLookupData(skillCheckTypes, 6, "Plant Remote Bomb")
            AddLookupData(skillCheckTypes, 7, "Open With Crowbar")
            AddLookupData(skillCheckTypes, 8, "Smash Door")
            AddLookupData(skillCheckTypes, 9, "Disarm Trap")
            AddLookupData(skillCheckTypes, 10, "Unjam Gun")
            AddLookupData(skillCheckTypes, 11, "Notice Dart")
            AddLookupData(skillCheckTypes, 12, "Lie to Queen")
            AddLookupData(skillCheckTypes, 13, "Attach Special Item")
            AddLookupData(skillCheckTypes, 14, "Attach Special Elec. Item")
            AddLookupData(skillCheckTypes, 15, "Disarm Elec. Trap")
        Else
            LookupFile.AddLookupData(lookupFilename, skillCheckTypes)
        End If

        ' MergeTypes
        lookupFilename = XmlDB.BaseDirectory + "\\Lookup\\" + mergeTypes.TableName + ".xml"
        If System.IO.File.Exists(lookupFilename) = False Then
            AddLookupData(mergeTypes, 0, "Destruction")
            AddLookupData(mergeTypes, 1, "Combine")
            AddLookupData(mergeTypes, 2, "Treat Armour")
            AddLookupData(mergeTypes, 3, "Explosive")
            AddLookupData(mergeTypes, 4, "Easy")
            AddLookupData(mergeTypes, 5, "Electronic")
            AddLookupData(mergeTypes, 6, "Use Item")
            AddLookupData(mergeTypes, 7, "Use Item (Hard)")
        Else
            LookupFile.AddLookupData(lookupFilename, mergeTypes)
        End If

        'ExplosionTypes
        lookupFilename = XmlDB.BaseDirectory + "\\Lookup\\" + explosionTypes.TableName + ".xml"
        If System.IO.File.Exists(lookupFilename) = False Then
            AddLookupData(explosionTypes, 0, "Normal")
            AddLookupData(explosionTypes, 1, "Stun")
            AddLookupData(explosionTypes, 2, "Tear Gas")
            AddLookupData(explosionTypes, 3, "Mustard Gas")
            AddLookupData(explosionTypes, 4, "Flare")
            AddLookupData(explosionTypes, 5, "Noise")
            AddLookupData(explosionTypes, 6, "Smoke")
            AddLookupData(explosionTypes, 7, "Creature Gas")
            AddLookupData(explosionTypes, 8, "Fire")
            AddLookupData(explosionTypes, 9, "Flashbang")
        Else
            LookupFile.AddLookupData(lookupFilename, explosionTypes)
        End If

        'ExplosionSize
        lookupFilename = XmlDB.BaseDirectory + "\\Lookup\\" + explosionSize.TableName + ".xml"
        If System.IO.File.Exists(lookupFilename) = False Then
            AddLookupData(explosionSize, 0, "None")
            AddLookupData(explosionSize, 1, "Standard")
            AddLookupData(explosionSize, 2, "HighExplosive")
        Else
            LookupFile.AddLookupData(lookupFilename, explosionSize)
        End If

        'ArmourClasses
        lookupFilename = XmlDB.BaseDirectory + "\\Lookup\\" + armourClasses.TableName + ".xml"
        If System.IO.File.Exists(lookupFilename) = False Then
            AddLookupData(armourClasses, 0, "Helmet")
            AddLookupData(armourClasses, 1, "Vest")
            AddLookupData(armourClasses, 2, "Leggings")
            AddLookupData(armourClasses, 3, "Plate")
            AddLookupData(armourClasses, 4, "Monster")
            AddLookupData(armourClasses, 5, "Vehicle")
        Else
            LookupFile.AddLookupData(lookupFilename, armourClasses)
        End If

        'WeaponClasses
        lookupFilename = XmlDB.BaseDirectory + "\\Lookup\\" + weaponClasses.TableName + ".xml"
        If System.IO.File.Exists(lookupFilename) = False Then
            AddLookupData(weaponClasses, 0, "None")
            AddLookupData(weaponClasses, 1, "Handgun")
            AddLookupData(weaponClasses, 2, "Submachinegun")
            AddLookupData(weaponClasses, 3, "Rifle")
            AddLookupData(weaponClasses, 4, "Machinegun")
            AddLookupData(weaponClasses, 5, "Shotgun")
            AddLookupData(weaponClasses, 6, "Knife")
            AddLookupData(weaponClasses, 7, "Monster")
        Else
            LookupFile.AddLookupData(lookupFilename, weaponClasses)
        End If

        'WeaponTypes
        lookupFilename = XmlDB.BaseDirectory + "\\Lookup\\" + weaponTypes.TableName + ".xml"
        If System.IO.File.Exists(lookupFilename) = False Then
            AddLookupData(weaponTypes, 0, "None")
            AddLookupData(weaponTypes, 1, "Pistol")
            AddLookupData(weaponTypes, 2, "Machine Pistol")
            AddLookupData(weaponTypes, 3, "Submachinegun")
            AddLookupData(weaponTypes, 4, "Rifle")
            AddLookupData(weaponTypes, 5, "Sniper Rifle")
            AddLookupData(weaponTypes, 6, "Assault Rifle")
            AddLookupData(weaponTypes, 7, "Light Machinegun")
            AddLookupData(weaponTypes, 8, "Shotgun")
        Else
            LookupFile.AddLookupData(lookupFilename, weaponTypes)
        End If

        'Cursors
        lookupFilename = XmlDB.BaseDirectory + "\\Lookup\\" + cursors.TableName + ".xml"
        If System.IO.File.Exists(lookupFilename) = False Then
            AddLookupData(cursors, 0, "Invalid")
            AddLookupData(cursors, 1, "Quest")
            AddLookupData(cursors, 2, "Punch")
            AddLookupData(cursors, 3, "Target")
            AddLookupData(cursors, 4, "Knife")
            AddLookupData(cursors, 5, "Aid")
            AddLookupData(cursors, 6, "Toss")
            AddLookupData(cursors, 8, "Mine")
            AddLookupData(cursors, 9, "Lockpick")
            AddLookupData(cursors, 10, "Metal Detector")
            AddLookupData(cursors, 11, "Crowbar")
            AddLookupData(cursors, 12, "Surveillance Camera")
            AddLookupData(cursors, 13, "Camera")
            AddLookupData(cursors, 14, "Key")
            AddLookupData(cursors, 15, "Saw")
            AddLookupData(cursors, 16, "Wirecutter")
            AddLookupData(cursors, 17, "Remote")
            AddLookupData(cursors, 18, "Bomb")
            AddLookupData(cursors, 19, "Repair")
            AddLookupData(cursors, 20, "Trajectory")
            AddLookupData(cursors, 21, "Jar")
            AddLookupData(cursors, 22, "Tin can")
            AddLookupData(cursors, 23, "Refuel")
        Else
            LookupFile.AddLookupData(lookupFilename, cursors)
        End If

        'ItemClasses
        lookupFilename = XmlDB.BaseDirectory + "\\Lookup\\" + itemClasses.TableName + ".xml"
        If System.IO.File.Exists(lookupFilename) = False Then
            AddLookupData(itemClasses, 1, "Nothing")
            AddLookupData(itemClasses, 2, "Gun")
            AddLookupData(itemClasses, 4, "Knife")
            AddLookupData(itemClasses, 8, "Throwing Knife")
            AddLookupData(itemClasses, 16, "Launcher")
            AddLookupData(itemClasses, 32, "Tentacle")
            AddLookupData(itemClasses, 64, "Thrown Weapon")
            AddLookupData(itemClasses, 128, "Blunt Weapon")
            AddLookupData(itemClasses, 256, "Grenade")
            AddLookupData(itemClasses, 512, "Bomb")
            AddLookupData(itemClasses, 1024, "Ammo")
            AddLookupData(itemClasses, 2048, "Armour")
            AddLookupData(itemClasses, 4096, "Medkit")
            AddLookupData(itemClasses, 8192, "Kit")
            'AddLookupData(itemClasses, 16384, "(Unused)")
            AddLookupData(itemClasses, 32768, "Face Item")
            AddLookupData(itemClasses, 65536, "Key")
            AddLookupData(itemClasses, 131072, "Load Bearing Equipment")
            AddLookupData(itemClasses, 268435456, "Misc")
            AddLookupData(itemClasses, 536870912, "Money")
        Else
            LookupFile.AddLookupData(lookupFilename, itemClasses)
        End If

        'LBEClasses
        lookupFilename = XmlDB.BaseDirectory + "\\Lookup\\" + lbeClasses.TableName + ".xml"
        If System.IO.File.Exists(lookupFilename) = False Then
            AddLookupData(lbeClasses, 0, "Nothing")
            AddLookupData(lbeClasses, 1, "Thigh Pack")
            AddLookupData(lbeClasses, 2, "Vest")
            AddLookupData(lbeClasses, 3, "Combat Pack")
            AddLookupData(lbeClasses, 4, "Backpack")
        Else
            LookupFile.AddLookupData(lookupFilename, lbeClasses)
        End If

        'Silhouettes
        lookupFilename = XmlDB.BaseDirectory + "\\Lookup\\" + silhouettes.TableName + ".xml"
        If System.IO.File.Exists(lookupFilename) = False Then
            AddLookupData(silhouettes, 0, "Vest")
            AddLookupData(silhouettes, 1, "Right Thigh")
            AddLookupData(silhouettes, 2, "Left Thigh")
            AddLookupData(silhouettes, 3, "Backpack")
            AddLookupData(silhouettes, 4, "Combat Pack")
            AddLookupData(silhouettes, 5, "Gun")
            AddLookupData(silhouettes, 6, "Rocket Launcher")
            AddLookupData(silhouettes, 7, "Big Bag")
            AddLookupData(silhouettes, 8, "Medium Bag")
            AddLookupData(silhouettes, 9, "SMG")
            AddLookupData(silhouettes, 10, "Explosive")
            AddLookupData(silhouettes, 11, "Knife")
            AddLookupData(silhouettes, 12, "Small Bag")
            AddLookupData(silhouettes, 13, "Pistol")
            AddLookupData(silhouettes, 14, "GL Grenade")
            AddLookupData(silhouettes, 15, "Hand Grenade")
            AddLookupData(silhouettes, 16, "Pistol Ammo")
            AddLookupData(silhouettes, 17, "SMG Ammo")
            AddLookupData(silhouettes, 18, "Rifle Ammo")
            AddLookupData(silhouettes, 19, "Large Ammo")
            AddLookupData(silhouettes, 20, "Shirt Pocket")
            AddLookupData(silhouettes, 21, "Belt Clip")
            AddLookupData(silhouettes, 22, "Huge/Belt Ammo")
            AddLookupData(silhouettes, 23, "Revolver Ammo")
            AddLookupData(silhouettes, 24, "Large Medical Cross")
            AddLookupData(silhouettes, 25, "Small Medical Cross")
        Else
            LookupFile.AddLookupData(lookupFilename, silhouettes)
        End If

        'PocketSizes
        lookupFilename = XmlDB.BaseDirectory + "\\Lookup\\" + pocketSizes.TableName + ".xml"
        If System.IO.File.Exists(lookupFilename) = False Then
            AddLookupData(pocketSizes, 0, "None")
            AddLookupData(pocketSizes, 1, "Small")
            AddLookupData(pocketSizes, 2, "Medium")
            AddLookupData(pocketSizes, 3, "Large")
        Else
            LookupFile.AddLookupData(lookupFilename, pocketSizes)
        End If

        'AttachmentClass
        lookupFilename = XmlDB.BaseDirectory + "\\Lookup\\" + AttachmentClasses.TableName + ".xml"
        If System.IO.File.Exists(lookupFilename) = False Then
            AddLookupData(AttachmentClasses, 0, "None")
            AddLookupData(AttachmentClasses, 1, "Default")
            AddLookupData(AttachmentClasses, 2, "Barrel")
            AddLookupData(AttachmentClasses, 4, "Laser")
            AddLookupData(AttachmentClasses, 8, "Sight")
            AddLookupData(AttachmentClasses, 16, "Scope")
            AddLookupData(AttachmentClasses, 32, "Stock")
            AddLookupData(AttachmentClasses, 64, "Ammo")
            AddLookupData(AttachmentClasses, 128, "Internal")
            AddLookupData(AttachmentClasses, 256, "External")
            AddLookupData(AttachmentClasses, 512, "Underbarrel")
            AddLookupData(AttachmentClasses, 1024, "Grenade")
            AddLookupData(AttachmentClasses, 2048, "Rocket")
        Else
            LookupFile.AddLookupData(lookupFilename, AttachmentClasses)
        End If

        'AttachmentSystem
        lookupFilename = XmlDB.BaseDirectory + "\\Lookup\\" + AttachmentSystem.TableName + ".xml"
        If System.IO.File.Exists(lookupFilename) = False Then
            AddLookupData(AttachmentSystem, 0, "Any")
            AddLookupData(AttachmentSystem, 1, "OAS Only")
            AddLookupData(AttachmentSystem, 2, "NAS Only")
        Else
            LookupFile.AddLookupData(lookupFilename, AttachmentSystem)
        End If

        'write out to xml
        ds.WriteXmlSchema(SchemaFileName)

        For Each t As DataTable In ds.Tables
            Dim lookupFile As String = XmlDB.BaseDirectory + CStr(t.ExtendedProperties(TableProperty.FileName))
            ' RoWa21: Only create the file if it does not exist
            If System.IO.File.Exists(lookupFile) = False Then
                If t.Rows.Count > 0 Then
                    t.WriteXml(XmlDB.BaseDirectory & CStr(t.ExtendedProperties(TableProperty.FileName)))
                End If
            End If
        Next
    End Sub

    Private Function MakeColumn(ByVal columnName As String, ByVal caption As String, ByVal type As Type, Optional ByVal defaultValue As Integer = 0, _
    Optional ByVal Lookup_Table As String = "", Optional ByVal Lookup_ValueColumn As String = "", Optional ByVal Lookup_TextColumn As String = "", Optional ByVal Lookup_AddBlank As Boolean = False, Optional ByVal Lookup_Filter As String = "", Optional ByVal HideInGrid As Boolean = False, Optional ByVal maxLength As Integer = 0, Optional ByVal Lookup_Sort As String = "", Optional ByVal tooltipText As String = "") As DataColumn
        Dim c As DataColumn
        c = New DataColumn(columnName)
        With c
            .Caption = caption
            .DataType = type
            .AllowDBNull = False

            If type.Equals(GetType(Integer)) Or type.Equals(GetType(Boolean)) Or type.Equals(GetType(Decimal)) Or type.Equals(GetType(Byte)) Then
                .DefaultValue = defaultValue
            Else
                .DefaultValue = ""
                If maxLength > 0 Then .MaxLength = maxLength
            End If
            If Lookup_Table.Length > 0 Then .ExtendedProperties.Add(ColumnProperty.Lookup_Table, Lookup_Table)
            If Lookup_ValueColumn.Length > 0 Then .ExtendedProperties.Add(ColumnProperty.Lookup_ValueColumn, Lookup_ValueColumn)
            If Lookup_TextColumn.Length > 0 Then .ExtendedProperties.Add(ColumnProperty.Lookup_TextColumn, Lookup_TextColumn)
            If Lookup_Filter.Length > 0 Then .ExtendedProperties.Add(ColumnProperty.Lookup_Filter, Lookup_Filter)
            If Lookup_Sort.Length > 0 Then .ExtendedProperties.Add(ColumnProperty.Lookup_Sort, Lookup_Sort)
            If Lookup_Table.Length > 0 Then .ExtendedProperties.Add(ColumnProperty.Lookup_AddBlank, Lookup_AddBlank)
            If HideInGrid Then .ExtendedProperties.Add(ColumnProperty.Grid_Hidden, HideInGrid)

            If tooltipText.Length > 0 Then .ExtendedProperties.Add(ColumnProperty.ToolTip, tooltipText)
        End With
        Return c
    End Function

    Private Function MakeLookupTable(ByVal tableName As String) As DataTable
        Dim t As New DataTable(tableName)
        t.ExtendedProperties.Add(TableProperty.FileName, "Lookup\" & tableName & ".xml")

        t.Columns.Add(MakeColumn("id", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("name", "Name", GetType(String)))

        t.Columns("id").ReadOnly = True

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("id")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeMergeTable() As DataTable
        Dim t As New DataTable("MERGE")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Merges.xml")

        t.Columns.Add(MakeColumn("firstItemIndex", "First Item", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass <> 1 OR uiIndex=0", , , "szLongItemName"))
        t.Columns.Add(MakeColumn("secondItemIndex", "Second Item", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass <> 1 OR uiIndex=0", , , "szLongItemName"))
        t.Columns.Add(MakeColumn("firstResultingItemIndex", "First Result", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass <> 1 OR uiIndex=0", , , "szLongItemName"))
        t.Columns.Add(MakeColumn("secondResultingItemIndex", "Second Result", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass <> 1 OR uiIndex=0", , , "szLongItemName"))
        t.Columns.Add(MakeColumn("mergeType", "Type", GetType(Integer), , "MergeType", "id", "name"))
        t.Columns.Add(MakeColumn("APCost", "AP Cost", GetType(Integer)))

        AddConstraint(t, New String() {"firstItemIndex", "secondItemIndex", "firstResultingItemIndex", "secondResultingItemIndex"}, True)

        Return t
    End Function

    Private Function MakeMagazineTable() As DataTable
        Dim t As New DataTable("MAGAZINE")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Magazines.xml")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("ubCalibre", "Caliber", GetType(Integer), , "AMMO", "uiIndex", "AmmoCaliber"))
        t.Columns.Add(MakeColumn("ubMagSize", "Size", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubAmmoType", "Ammo Type", GetType(Integer), , "AMMOTYPE", "uiIndex", "name"))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        'AddConstraint(t, New String() {"ubCalibre", "ubMagSize", "ubAmmoType"}, False)

        Return t
    End Function

    Private Function MakeLaunchableTable() As DataTable
        Dim t As New DataTable("LAUNCHABLE")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Launchables.xml")

        t.Columns.Add(MakeColumn("launchableIndex", "Launchable", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass = 256 or usItemClass = 512", , , "szLongItemName"))
        t.Columns.Add(MakeColumn("itemIndex", "Launcher", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass = 16", , , "szLongItemName"))

        AddConstraint(t, New String() {"launchableIndex", "itemIndex"}, True)

        Return t
    End Function

    Private Function MakeIncompatibleAttachmentTable() As DataTable
        Dim t As New DataTable("INCOMPATIBLEATTACHMENT")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "IncompatibleAttachments.xml")
        t.ExtendedProperties.Add(TableProperty.TableHandlerName, "DuplicateEntryTable")

        t.Columns.Add(MakeColumn("itemIndex", "Attachment", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "Attachment=1 AND usItemClass <> 1", , , "szLongItemName"))
        t.Columns.Add(MakeColumn("incompatibleattachmentIndex", "Incompatible Attachment", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "Attachment=1 AND usItemClass <> 1", , , "szLongItemName"))

        AddConstraint(t, New String() {"itemIndex", "incompatibleattachmentIndex"}, True)

        Return t
    End Function

    Private Function MakeExplosionDataTable() As DataTable
        Dim t As New DataTable("EXPDATA")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "ExplosionData.xml")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("name", "Name", GetType(String)))
        t.Columns.Add(MakeColumn("TransKeyFrame", "Trans Key Frame", GetType(Integer)))
        t.Columns.Add(MakeColumn("DamageKeyFrame", "Damage Key Frame", GetType(Integer)))
        t.Columns.Add(MakeColumn("ExplosionSoundID", "Sound", GetType(Integer), , "SOUND", "id", "name"))
        t.Columns.Add(MakeColumn("AltExplosionSoundID", "Alt. Sound", GetType(Integer), , "SOUND", "id", "name"))
        t.Columns.Add(MakeColumn("BlastFilename", "Blast File", GetType(String)))
        t.Columns.Add(MakeColumn("BlastSpeed", "Blast Speed", GetType(Integer)))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeExplosiveTable() As DataTable
        Dim t As New DataTable("EXPLOSIVE")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Explosives.xml")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("ubType", "Type", GetType(Integer), , "ExplosionType", "id", "name"))
        t.Columns.Add(MakeColumn("ubDamage", "Damage", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubStunDamage", "Stun Damage", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubRadius", "Potential Radius", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubVolume", "Volume", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubVolatility", "Volatility", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubAnimationID", "Animation", GetType(Integer), , "EXPDATA", "uiIndex", "name"))
        t.Columns.Add(MakeColumn("ubDuration", "Duration", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubStartRadius", "Initial Radius", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubMagSize", "Magazine Size", GetType(Integer))) 'NAS

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeCompatibleFaceItemTable() As DataTable
        Dim t As New DataTable("COMPATIBLEFACEITEM")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "CompatibleFaceItems.xml")
        t.ExtendedProperties.Add(TableProperty.TableHandlerName, "DuplicateEntryTable")

        t.Columns.Add(MakeColumn("compatiblefaceitemIndex", "Face Item", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass = 32768", , , "szLongItemName"))
        t.Columns.Add(MakeColumn("itemIndex", "Face Item", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass = 32768", , , "szLongItemName"))

        AddConstraint(t, New String() {"compatiblefaceitemIndex", "itemIndex"}, True)

        Return t
    End Function

    Private Function MakeAttachmentInfoTable() As DataTable
        Dim t As New DataTable("ATTACHMENTINFO")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "AttachmentInfo.xml")
        t.ExtendedProperties.Add(TableProperty.TableHandlerName, "AutoIncrementTable")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer), , , , , , , False))
        t.Columns.Add(MakeColumn("usItem", "Item", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "Attachment=1 AND usItemClass <> 1", , , "szLongItemName"))
        t.Columns.Add(MakeColumn("uiItemClass", "Attachable To", GetType(Integer), , "ItemClass", "id", "name"))
        t.Columns.Add(MakeColumn("bAttachmentSkillCheck", "Skill", GetType(Integer), , "SkillCheckType", "id", "name"))
        t.Columns.Add(MakeColumn("bAttachmentSkillCheckMod", "Modifier", GetType(Integer)))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        AddConstraint(t, New String() {"usItem", "uiItemClass"}, False)

        Return t
    End Function

    Private Function MakeAttachmentComboMergeTable() As DataTable
        Dim t As New DataTable("ATTACHMENTCOMBOMERGE")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "AttachmentComboMerges.xml")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("usItem", "Item", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass<>1", , , "szLongItemName"))
        t.Columns.Add(MakeColumn("usAttachment1", "Attachment 1", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass<>1", , , "szLongItemName"))
        t.Columns.Add(MakeColumn("usAttachment2", "Attachment 2", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass<>1", , , "szLongItemName"))
        t.Columns.Add(MakeColumn("usResult", "Resulting Item", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass<>1", , , "szLongItemName"))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        AddConstraint(t, New String() {"usItem", "usAttachment1", "usAttachment2"}, False)

        Return t
    End Function

    Private Function MakeAttachmentTable() As DataTable
        Dim t As New DataTable("ATTACHMENT")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Attachments.xml")
        t.ExtendedProperties.Add(TableProperty.Trim, True)

        t.Columns.Add(MakeColumn("attachmentIndex", "Attachment", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "Attachment=1 AND usItemClass<>1", , , "szLongItemName"))
        t.Columns.Add(MakeColumn("itemIndex", "Item", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass<>1", , , "szLongItemName"))
        t.Columns.Add(MakeColumn("APCost", "AP Cost", GetType(Integer), 5))
        t.Columns.Add(MakeColumn("NASOnly", "NAS Only", GetType(Boolean)))

        AddConstraint(t, New String() {"attachmentIndex", "itemIndex"}, True)

        Return t
    End Function

    Private Function MakeArmourTable() As DataTable
        Dim t As New DataTable("ARMOUR")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Armours.xml")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("ubArmourClass", "Type", GetType(Integer), , "ArmourClass", "id", "name"))
        t.Columns.Add(MakeColumn("ubProtection", "Protection", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubCoverage", "Coverage", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubDegradePercent", "Degradation", GetType(Integer)))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeAmmoTypesTable() As DataTable
        Dim t As New DataTable("AMMOTYPE")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "AmmoTypes.xml")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("name", "Name", GetType(String)))
        t.Columns.Add(MakeColumn("fontColour", "Font Colour", GetType(Integer)))
        t.Columns.Add(MakeColumn("grayed", "Grayed Colour", GetType(Integer)))
        t.Columns.Add(MakeColumn("offNormal", "Off Image", GetType(Integer)))
        t.Columns.Add(MakeColumn("onNormal", "On Image", GetType(Integer)))
        t.Columns.Add(MakeColumn("structureImpactReductionMultiplier", "Structure Impact Reduction Multiplier", GetType(Integer)))
        t.Columns.Add(MakeColumn("structureImpactReductionDivisor", "Structure Impact Reduction Divisor", GetType(Integer)))
        t.Columns.Add(MakeColumn("armourImpactReductionMultiplier", "Armour Impact Reduction Multiplier", GetType(Integer)))
        t.Columns.Add(MakeColumn("armourImpactReductionDivisor", "Armour Impact Reduction Divisor", GetType(Integer)))
        t.Columns.Add(MakeColumn("beforeArmourDamageMultiplier", "Before Armour Damage Multiplier", GetType(Integer)))
        t.Columns.Add(MakeColumn("beforeArmourDamageDivisor", "Before Armour Damage Divisor", GetType(Integer)))
        t.Columns.Add(MakeColumn("afterArmourDamageMultiplier", "After Armour Damage Multiplier", GetType(Integer)))
        t.Columns.Add(MakeColumn("afterArmourDamageDivisor", "After Armour Damage Divisor", GetType(Integer)))
        t.Columns.Add(MakeColumn("zeroMinimumDamage", "Zero Min. Damage", GetType(Boolean)))
        t.Columns.Add(MakeColumn("canGoThrough", "Can Go Through", GetType(Boolean)))
        t.Columns.Add(MakeColumn("standardIssue", "Std. Issue", GetType(Boolean)))
        t.Columns.Add(MakeColumn("numberOfBullets", "# Bullets", GetType(Integer)))
        t.Columns.Add(MakeColumn("multipleBulletDamageMultiplier", "Multiple Bullet Damage Multiplier", GetType(Integer)))
        t.Columns.Add(MakeColumn("multipleBulletDamageDivisor", "Multiple Bullet Damage Divisor", GetType(Integer)))
        t.Columns.Add(MakeColumn("highExplosive", "High Explosive", GetType(Integer), , "ITEMTOEXPLOSIVE", "uiIndex", "szItemName", , , , , "szItemName"))
        t.Columns.Add(MakeColumn("explosionSize", "Explosion Size", GetType(Integer), , "ExplosionSize", "id", "name", , , , , "id"))
        t.Columns.Add(MakeColumn("antiTank", "Anti-Tank", GetType(Boolean)))
        t.Columns.Add(MakeColumn("dart", "Dart", GetType(Boolean)))
        t.Columns.Add(MakeColumn("knife", "Knife", GetType(Boolean)))
        t.Columns.Add(MakeColumn("monsterSpit", "Spit", GetType(Boolean)))
        t.Columns.Add(MakeColumn("acidic", "Acidic", GetType(Boolean)))
        t.Columns.Add(MakeColumn("ignoreArmour", "Ignore Armour", GetType(Boolean)))
        t.Columns.Add(MakeColumn("lockBustingPower", "Lock Buster Power", GetType(Integer)))
        t.Columns.Add(MakeColumn("tracerEffect", "Tracer Effect", GetType(Boolean)))
        t.Columns.Add(MakeColumn("spreadPattern", "Spread Pattern", GetType(String)))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeAmmoStringsTable() As DataTable
        Dim t As New DataTable("AMMO")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "AmmoStrings.xml")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("AmmoCaliber", "Caliber", GetType(String), , , , , , , , 20))
        t.Columns.Add(MakeColumn("BRCaliber", "BR Caliber", GetType(String), , , , , , , , 20))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeLocalizedAmmoStringsTable(ByVal language As String) As DataTable
        Dim t As New DataTable(language & "Ammo")
        t.ExtendedProperties.Add(TableProperty.FileName, language & ".AmmoStrings.xml")
        t.ExtendedProperties.Add(TableProperty.SourceTableName, "AMMO")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "AMMOLIST")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("AmmoCaliber", "Caliber", GetType(String), , , , , , , , 20))
        t.Columns.Add(MakeColumn("BRCaliber", "BR Caliber", GetType(String), , , , , , , , 20))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeWeaponsTable() As DataTable
        Dim t As New DataTable("WEAPON")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Weapons.xml")
        t.ExtendedProperties.Add(TableProperty.Trim, True)

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("szWeaponName", "Name", GetType(String), , , , , , , True))
        t.Columns.Add(MakeColumn("ubWeaponClass", "Class", GetType(Integer), , "WeaponClass", "id", "name"))
        t.Columns.Add(MakeColumn("ubWeaponType", "Type", GetType(Integer), , "WeaponType", "id", "name"))
        t.Columns.Add(MakeColumn("ubCalibre", "Caliber", GetType(Integer), , "AMMO", "uiIndex", "AmmoCaliber"))
        t.Columns.Add(MakeColumn("ubReadyTime", "Ready APs", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubShotsPer4Turns", "Shots Per 4 Turns", GetType(Decimal), , , , , , , , , , "If 2 values result in the same AP value, then the lower value will result in the desired AP value more often."))
        t.Columns.Add(MakeColumn("ubShotsPerBurst", "Shots Per Burst", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubBurstPenalty", "Burst Penalty", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubBulletSpeed", "Bullet Speed", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("ubImpact", "Damage", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubDeadliness", "Deadliness", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("bAccuracy", "Accuracy", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubMagSize", "Magazine Size", GetType(Integer)))
        t.Columns.Add(MakeColumn("usRange", "Range", GetType(Integer)))
        t.Columns.Add(MakeColumn("usReloadDelay", "Reload Delay", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("BurstAniDelay", "Burst Animation Delay", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("ubAttackVolume", "Volume", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("ubHitVolume", "Hit Volume", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("sSound", "Sound", GetType(Integer), , "SOUND", "id", "name", , , True))
        t.Columns.Add(MakeColumn("sBurstSound", "Burst Sound", GetType(Integer), , "BURSTSOUND", "id", "name", , , True))
        t.Columns.Add(MakeColumn("sSilencedBurstSound", "Silenced Burst Sound", GetType(Integer), , "BURSTSOUND", "id", "name", , , True))
        t.Columns.Add(MakeColumn("sReloadSound", "Reload Sound", GetType(Integer), , "SOUND", "id", "name", , , True))
        t.Columns.Add(MakeColumn("sLocknLoadSound", "Lock N Load Sound", GetType(Integer), , "SOUND", "id", "name", , , True))
        t.Columns.Add(MakeColumn("SilencedSound", "Silienced Sound", GetType(Integer), , "SOUND", "id", "name", , , True))
        t.Columns.Add(MakeColumn("bBurstAP", "Burst APs", GetType(Integer)))
        t.Columns.Add(MakeColumn("bAutofireShotsPerFiveAP", "Autofire Shots per 5 APs", GetType(Integer)))
        t.Columns.Add(MakeColumn("APsToReload", "APs to Reload", GetType(Integer)))
        t.Columns.Add(MakeColumn("SwapClips", "Swap Clips", GetType(Boolean)))
        t.Columns.Add(MakeColumn("MaxDistForMessyDeath", "Max Dist for Messy Death", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("AutoPenalty", "Auto Penalty", GetType(Integer)))
        t.Columns.Add(MakeColumn("NoSemiAuto", "No Semi Auto", GetType(Boolean)))
        t.Columns.Add(MakeColumn("EasyUnjam", "Easy UnJam", GetType(Boolean)))
        t.Columns.Add(MakeColumn("APsToReloadManually", "Manual Reload APs", GetType(Integer)))
        t.Columns.Add(MakeColumn("ManualReloadSound", "Manual Reload Sound", GetType(Integer), , "SOUND", "id", "name", , , True))
        t.Columns.Add(MakeColumn("nAccuracy", "NCTH Accuracy", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("bRecoilX", "Recoil X", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("bRecoilY", "Recoil Y", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("ubAimLevels", "Default Aim Levles", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("ubRecoilDelay", "Recoil Delay", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("Handling", "Weapon Handling", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("MalfunctionRate", "Malfunction Rate", GetType(Integer), , , , , , , ))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeItemsTable() As DataTable
        Dim t As New DataTable("ITEM")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Items.xml")
        t.ExtendedProperties.Add(TableProperty.TableHandlerName, "ItemTable")
        t.ExtendedProperties.Add(TableProperty.Trim, True)

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))

        t.Columns.Add(MakeColumn("szItemName", "Short Name", GetType(String), , , , , , , True, 80))
        t.Columns.Add(MakeColumn("szLongItemName", "Name", GetType(String), , , , , , , , 80))
        t.Columns.Add(MakeColumn("szItemDesc", "Description", GetType(String), , , , , , , True, 400))
        t.Columns.Add(MakeColumn("szBRName", "BR Name", GetType(String), , , , , , , True, 80))
        t.Columns.Add(MakeColumn("szBRDesc", "BR Desc", GetType(String), , , , , , , True, 400))
        t.Columns.Add(MakeColumn("usItemClass", "Class", GetType(Integer), , "ItemClass", "id", "name"))
        t.Columns.Add(MakeColumn("nasAttachmentClass", "Attachment Class", GetType(Integer), , "AttachmentClass", "id", "name", , , , , "id")) 'NAS
        t.Columns.Add(MakeColumn("nasLayoutClass", "Layout Class", GetType(Integer))) 'NAS
        t.Columns.Add(MakeColumn("ubClassIndex", "Foreign Key", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("ubCursor", "Cursor", GetType(Integer), , "Cursor", "id", "name", , , True))
        t.Columns.Add(MakeColumn("bSoundType", "Sound Type", GetType(Integer), 0, , , , , , True))
        t.Columns.Add(MakeColumn("ubGraphicType", "Graphic Type", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("ubGraphicNum", "Graphic #", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("ubWeight", "Weight", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubPerPocket", "# per Pocket", GetType(Integer)))
        t.Columns.Add(MakeColumn("ItemSize", "Size", GetType(Integer))) 'TODO: limit to 0-34 and 99
        t.Columns.Add(MakeColumn("ItemSizeBonus", "Size Adjustment", GetType(Integer)))
        t.Columns.Add(MakeColumn("usPrice", "Price", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubCoolness", "Coolness", GetType(Integer)))
        t.Columns.Add(MakeColumn("bReliability", "Reliability", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("bRepairEase", "Repair Ease", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("Damageable", "Damageable", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Repairable", "Repairable", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("WaterDamages", "Water Damages", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Metal", "Metal", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Sinks", "Sinks", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ShowStatus", "Show Status", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("HiddenAddon", "Hidden Addon", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("TwoHanded", "Two Handed", GetType(Boolean)))
        t.Columns.Add(MakeColumn("NotBuyable", "Not Buyable", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Attachment", "Attachment", GetType(Boolean)))
        t.Columns.Add(MakeColumn("HiddenAttachment", "Hidden Attachment", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("BigGunList", "Tons of Guns Mode", GetType(Boolean)))
        t.Columns.Add(MakeColumn("SciFi", "Sci-Fi", GetType(Boolean)))
        t.Columns.Add(MakeColumn("NotInEditor", "Not In Editor", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("DefaultUndroppable", "Undroppable", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Unaerodynamic", "Unaerodynamic", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Electronic", "Electronic", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Inseparable", "Inseparable", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("BR_NewInventory", "BR New Inventory", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("BR_UsedInventory", "BR Used Inventory", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("BR_ROF", "BR Rate of Fire", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("PercentNoiseReduction", "% Noise Reduction", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("HideMuzzleFlash", "Hide Muzzle Flash", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("Bipod", "Bipod", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("RangeBonus", "Range Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("ToHitBonus", "To-Hit Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("BestLaserRange", "Best Laser Range", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("AimBonus", "Aim Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("MinRangeForAimBonus", "Min. Range For Aim Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("MagSizeBonus", "Mag. Size Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("RateOfFireBonus", "Rate of Fire Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("BulletSpeedBonus", "Bullet Speed Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("BurstSizeBonus", "Burst Size Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("BurstToHitBonus", "Burst To-Hit Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("AutoFireToHitBonus", "Autofire To-Hit Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("APBonus", "Bonus APs", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("PercentBurstFireAPReduction", "% Burst Fire AP Reduction", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("PercentAutofireAPReduction", "% Autofire AP Reduction", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("PercentReadyTimeAPReduction", "% Ready AP Reduction", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("PercentReloadTimeAPReduction", "% Reload AP Reduction", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("PercentAPReduction", "% AP Reduction", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("PercentStatusDrainReduction", "% Status Drain Reduction", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("DamageBonus", "Damage Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("MeleeDamageBonus", "Melee Damage Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("GrenadeLauncher", "Grenade Launcher", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Duckbill", "Duckbill", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("GLGrenade", "GL Grenade", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Mine", "Mine", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Mortar", "Mortar", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("RocketLauncher", "Rocket Launcher", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("SingleShotRocketLauncher", "Single-Shot Rocket Launcher", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("DiscardedLauncherItem", "Discarded Launcher Item", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", True, "uiIndex=0 OR usItemClass=268435456", True, , "szLongItemName"))
        t.Columns.Add(MakeColumn("RocketRifle", "Rocket Rifle", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Cannon", "Cannon", GetType(Boolean), , , , , , , True))
        For i As Integer = 0 To 19
            t.Columns.Add(MakeColumn("DefaultAttachment" & i, "Default Attachment", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", True, "uiIndex=0 OR (Attachment=1 AND usItemClass <> 1)", True, , "szLongItemName"))
        Next
        t.Columns.Add(MakeColumn("BrassKnuckles", "Brass Knuckles", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Crowbar", "Crowbar", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("BloodiedItem", "Bloodied Item", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", True, "usItemClass = 8 OR uiIndex=0", True, , "szLongItemName"))
        t.Columns.Add(MakeColumn("Rock", "Rock", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("CamoBonus", "Camo Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("UrbanCamoBonus", "Urban Camo Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("DesertCamoBonus", "Desert Camo Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("SnowCamoBonus", "Snow Camo Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("StealthBonus", "Stealth Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("FlakJacket", "Flak Jacket", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("LeatherJacket", "Leather Jacket", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Detonator", "Detonator", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("RemoteDetonator", "Remote Detonator", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("RemoteTrigger", "Remote Trigger", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("LockBomb", "LockBomb", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Flare", "Flare", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("RobotRemoteControl", "Robot Remote Control", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Walkman", "Walkman", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("HearingRangeBonus", "Hearing Range Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("VisionRangeBonus", "Vision Range Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("NightVisionRangeBonus", "Night Vision Range Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("DayVisionRangeBonus", "Day Vision Range Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("CaveVisionRangeBonus", "Cave Vision Range Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("BrightLightVisionRangeBonus", "Bright Light Vision Range Bonus", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("PercentTunnelVision", "Percent Tunnel Vision", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("ThermalOptics", "Thermal Optics", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("GasMask", "Gas Mask", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Alcohol", "Alcohol", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Hardware", "Hardware", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Medical", "Medical", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("CamouflageKit", "Camouflage Kit", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("LocksmithKit", "Locksmith Kit", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Toolkit", "Toolkit", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("FirstAidKit", "First Aid Kit", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("MedicalKit", "Medical Kit", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("WireCutters", "Wire Cutters", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Canteen", "Canteen", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("GasCan", "Gas Can", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Marbles", "Marbles", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("CanAndString", "Can And String", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Jar", "Jar", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("XRay", "XRay", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("Batteries", "Batteries", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("NeedsBatteries", "Needs Batteries", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ContainsLiquid", "Contains Liquid", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("MetalDetector", "Metal Detector", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("FingerPrintID", "Finger Print ID", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("NewInv", "New Inventory Only", GetType(Boolean)))
        t.Columns.Add(MakeColumn("AttachmentSystem", "Attachment System", GetType(Integer), , "AttachmentSystem", "id", "name")) 'NAS
        t.Columns.Add(MakeColumn("AmmoCrate", "Ammo Crate", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ScopeMagFactor", "Scope Magnification Factor", GetType(Decimal), , , , , , , True))
        t.Columns.Add(MakeColumn("ProjectionFactor", "Laser Projection Factor", GetType(Decimal), , , , , , , True))
        t.Columns.Add(MakeColumn("RecoilModifierX", "Recoil X Modifier", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("RecoilModifierY", "Recoil Y Modifier", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("PercentRecoilModifier", "Recoil Modifier", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("PercentAccuracyModifier", "Accuracy Modifier", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("spreadPattern", "Spread Pattern", GetType(String), , , , , , , False))
        'CHRISL: Add new tags above this point.  Do not have new tags in the above section end in 1, 2 or 3 or the xml will not load correctly
        '   Only add new tags below this point if the tags are in the STAND_MODIFIERS, CROUCH_MODIFIERS and PRONE_MODIFIERS sections
        'Start STAND_MODIFIERS section
        t.Columns.Add(MakeColumn("FlatBase1", "Standing Flat Base Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentBase1", "Standing Base Percent Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("FlatAim1", "Standing Flat Aim Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentCap1", "Standing Cap Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentHandling1", "Standing Handling Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentTargetTrackingSpeed1", "Standing Tracking Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentDropCompensation1", "Standing Drop Compensation Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentMaxCounterForce1", "Standing CounterForce Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentCounterForceAccuracy1", "Standing CF Accuracy Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentCounterForceFrequency1", "Standing CF Frequency Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("AimLevels1", "Standing Aim Bonus", GetType(Integer), -101, , , , , , True))
        'Start CROUCH_MODIFIERS section
        t.Columns.Add(MakeColumn("FlatBase2", "Crouching Flat Base Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentBase2", "Crouching Base Percent Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("FlatAim2", "Crouching Flat Aim Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentCap2", "Crouching Cap Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentHandling2", "Crouching Handling Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentTargetTrackingSpeed2", "Crouching Tracking Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentDropCompensation2", "Crouching Drop Compensation Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentMaxCounterForce2", "Crouching CounterForce Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentCounterForceAccuracy2", "Crouching CF Accuracy Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentCounterForceFrequency2", "Crouching CF Frequency Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("AimLevels2", "Crouching Aim Bonus", GetType(Integer), -101, , , , , , True))
        'Start PRONE_MODIFIERS section
        t.Columns.Add(MakeColumn("FlatBase3", "Prone Flat Base Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentBase3", "Prone Base Percent Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("FlatAim3", "Prone Flat Aim Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentCap3", "Prone Cap Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentHandling3", "Prone Handling Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentTargetTrackingSpeed3", "Prone Tracking Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentDropCompensation3", "Prone Drop Compensation Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentMaxCounterForce3", "Prone CounterForce Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentCounterForceAccuracy3", "Prone CF Accuracy Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("PercentCounterForceFrequency3", "Prone CF Frequency Bonus", GetType(Integer), -101, , , , , , True))
        t.Columns.Add(MakeColumn("AimLevels3", "Prone Aim Bonus", GetType(Integer), -101, , , , , , True))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeLocalizedItemsTable(ByVal language As String) As DataTable
        Dim t As New DataTable(language & "Item")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "ITEMLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, language & ".Items.xml")
        t.ExtendedProperties.Add(TableProperty.SourceTableName, "ITEM")
        t.ExtendedProperties.Add(TableProperty.Trim, True)

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))

        t.Columns.Add(MakeColumn("szItemName", "Short Name", GetType(String), , , , , , , , 80))
        t.Columns.Add(MakeColumn("szLongItemName", "Name", GetType(String), , , , , , , , 80))
        t.Columns.Add(MakeColumn("szItemDesc", "Description", GetType(String), , , , , , , , 400))
        t.Columns.Add(MakeColumn("szBRName", "BR Name", GetType(String), , , , , , , , 80))
        t.Columns.Add(MakeColumn("szBRDesc", "BR Desc", GetType(String), , , , , , , , 400))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeSoundsTable() As DataTable
        Dim t As New DataTable("SOUND")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Sounds\Sounds.xml")
        t.ExtendedProperties.Add(TableProperty.TableHandlerName, "SoundTable")

        t.Columns.Add(MakeColumn("id", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("name", "Name", GetType(String)))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("id")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeBurstSoundsTable() As DataTable
        Dim t As New DataTable("BURSTSOUND")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "SOUNDLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Sounds\BurstSounds.xml")
        t.ExtendedProperties.Add(TableProperty.SourceTableName, "SOUND")
        t.ExtendedProperties.Add(TableProperty.TableHandlerName, "SoundTable")

        t.Columns.Add(MakeColumn("id", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("name", "Name", GetType(String)))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("id")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeShopKeeperInventoryTable(ByVal shopKeeperName As String) As DataTable
        Dim t As New DataTable(shopKeeperName & "Inventory")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "INVENTORYLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "NPCInventory\" & shopKeeperName & "Inventory.xml")
        t.ExtendedProperties.Add(TableProperty.SourceTableName, "INVENTORY")
        t.ExtendedProperties.Add(TableProperty.TableHandlerName, "AutoIncrementTable")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("sItemIndex", "Item", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass<>1", , , "szLongItemName"))
        t.Columns.Add(MakeColumn("ubOptimalNumber", "Amount", GetType(Integer), 0))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeShopKeeperControlTable(ByVal shopKeeperName As String) As DataTable
        Dim t As New DataTable(shopKeeperName & "Control")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "INVENTORYLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "NPCInventory\" & shopKeeperName & "Inventory.xml")
        t.ExtendedProperties.Add(TableProperty.SourceTableName, "CONTROL")

        t.Columns.Add(MakeColumn("ARMSDEALERINDEX", "AD Index", GetType(Integer)))
        t.Columns.Add(MakeColumn("SHOPKEEPERID", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("BUYCOSTMODIFIER", "Buy Cost Modifier", GetType(Decimal), , , , , , , True))
        t.Columns.Add(MakeColumn("SELLCOSTMODIFIER", "Sell Cost Modifier", GetType(Decimal), , , , , , , True))
        t.Columns.Add(MakeColumn("REPAIRSPEED", "Repair Speed", GetType(Decimal), , , , , , , True))
        t.Columns.Add(MakeColumn("REPAIRCOST", "Repair Cost", GetType(Decimal), , , , , , , True))
        'REORDERDAYSDELAY
        t.Columns.Add(MakeColumn("REORDERMINIMUM", "Minimum Reorder", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("REORDERMAXIMUM", "Maximum Reorder", GetType(Integer), , , , , , , True))
        'CASH
        t.Columns.Add(MakeColumn("INITIAL", "Initial Cash", GetType(Integer), , , , , , , True))
        'DAILY
        t.Columns.Add(MakeColumn("INCREMENT", "Cash Increment", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("CASHMAXIMUM", "Maximum Cash", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("RETAINED", "Cash Retained", GetType(Integer), , , , , , , True))
        'COOLNESS
        t.Columns.Add(MakeColumn("COOLMINIMUM", "Minimum Coolness", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("COOLMAXIMUM", "Maximum Coolness", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("COOLADD", "Add Coolness", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("PROGRESSRATE", "Coolness Progress", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("USEBOBBYRAYSETTING", "Use BR Settings", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ALLINVENTORYALWAYSAVAILBLE", "All Inventory", GetType(Boolean), , , , , , , True))
        'BASICDEALERFLAGS
        t.Columns.Add(MakeColumn("ARMS_DEALER_HANDGUNCLASS", "Handguns", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_SMGCLASS", "SMGs", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_RIFLECLASS", "Rifles", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_MGCLASS", "MGs", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_SHOTGUNCLASS", "Shotguns", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_KNIFECLASS", "Knives", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_BLADE", "Blades", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_LAUNCHER", "Launchers", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_ARMOUR", "Armor", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_MEDKIT", "Medkits", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_MISC", "Misc", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_AMMO", "Ammo", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_GRENADE", "Grenades", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_BOMB", "Bombs", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_EXPLOSV", "Explosives", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_KIT", "Kits", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_FACE", "Face Gear", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_DETONATORS", "Detonators", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_ATTACHMENTS", "Attachments", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_ALCOHOL", "Alcohol", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_ELECTRONICS", "Electronics", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_HARDWARE", "Hardware", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_MEDICAL", "Medical", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_CREATURE_PARTS", "Creature Parts", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_ROCKET_RIFLE", "Rocket Rifles", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_ONLY_USED_ITEMS", "Only Used Items", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_GIVES_CHANGE", "Gives Change", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_ACCEPTS_GIFTS", "Accepts Gifts", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_SOME_USED_ITEMS", "Used Items", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_HAS_NO_INVENTORY", "No Inventory", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_ALL_GUNS", "All Guns", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_BIG_GUNS", "Big Guns", GetType(Boolean), , , , , , , True))
        t.Columns.Add(MakeColumn("ARMS_DEALER_ALL_WEAPONS", "All Weapons", GetType(Boolean), , , , , , , True))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("ARMSDEALERINDEX")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeRelation(ByVal parentTable As DataTable, ByVal childTable As DataTable, ByVal parentKey As String, ByVal childKey As String, Optional ByVal cascadeUpdates As Boolean = True, Optional ByVal cascadeDeletes As Boolean = True) As DataRelation
        Dim parentCol As DataColumn = parentTable.Columns(parentKey)
        Dim childCol As DataColumn = childTable.Columns(childKey)
        Dim dr As New DataRelation(parentTable.TableName & childTable.TableName & "_" & parentCol.ColumnName & childCol.ColumnName, parentCol, childCol, True)

        'add cascading updates and deletes to the child table
        Dim fkc As New ForeignKeyConstraint(parentCol, childCol)
        fkc.AcceptRejectRule = AcceptRejectRule.None
        If cascadeUpdates Then fkc.UpdateRule = Rule.Cascade
        If cascadeDeletes Then fkc.DeleteRule = Rule.Cascade
        childTable.Constraints.Add(fkc)

        Return dr
    End Function

    Private Sub AddConstraint(ByVal t As DataTable, ByVal columnNames() As String, ByVal primaryKey As Boolean)
        Dim cols(columnNames.GetUpperBound(0)) As DataColumn
        For i As Integer = 0 To columnNames.GetUpperBound(0)
            cols(i) = t.Columns(columnNames(i))
        Next
        Dim uc As UniqueConstraint = New UniqueConstraint("unique_constraint", cols, primaryKey)
        t.Constraints.Add(uc)
    End Sub

    Private Sub AddLookupData(ByVal t As DataTable, ByVal id As Integer, ByVal name As String)
        Dim r As DataRow = t.NewRow
        r("id") = id
        r("name") = name
        t.Rows.Add(r)
    End Sub

    Private Function MakeIMPItemsTable() As DataTable
        Dim t As New DataTable("IMPITEMCHOICES")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "IMPItemChoices.xml")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("name", "Type", GetType(String)))
        t.Columns.Add(MakeColumn("ubChoices", "# Choices", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubNumItems", "# Items", GetType(Integer)))

        For i As Integer = 1 To 50
            t.Columns.Add(MakeColumn("bItemNo" & i, "Item " & i, GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "usItemClass <> 1"))
        Next

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeEnemyGunsTable() As DataTable
        Dim t As New DataTable("ENEMYGUNCHOICES")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "EnemyGunChoices.xml")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("name", "Progress", GetType(String)))
        t.Columns.Add(MakeColumn("ubChoices", "# Choices", GetType(Integer)))

        For i As Integer = 1 To 50
            t.Columns.Add(MakeColumn("bItemNo" & i, "Item " & i, GetType(Integer), 0, "ITEM", "uiIndex", "szLongItemName", , "uiIndex = 0 OR usItemClass = " & ItemClass.Gun))
        Next

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeEnemyItemsTable() As DataTable
        Dim t As New DataTable("ENEMYITEMCHOICES")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "EnemyItemChoices.xml")
        t.ExtendedProperties.Add(TableProperty.TableHandlerName, "EnemyItemTable")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("name", "Type", GetType(String)))
        t.Columns.Add(MakeColumn("ubChoices", "# Choices", GetType(Integer)))

        For i As Integer = 1 To 50
            t.Columns.Add(MakeColumn("bItemNo" & i, "Item " & i, GetType(Integer), 0, "ITEM", "uiIndex", "szLongItemName", , "usItemClass <> 1"))
        Next

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeEnemyAmmoTable() As DataTable
        Dim t As New DataTable("ENEMYAMMOCHOICES")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("name", "Type", GetType(String)))
        t.Columns.Add(MakeColumn("ubChoices", "# Choices", GetType(Integer)))

        For i As Integer = 1 To 50
            t.Columns.Add(MakeColumn("bItemNo" & i, "Ammo Type " & i, GetType(Integer), 0, "AMMOTYPE", "uiIndex", "name"))
        Next

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeEnemyAmmoDropTable() As DataTable
        Dim t As New DataTable("EnemyAmmoDrops")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "AMMODROPLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "EnemyAmmoDrops.xml")
        t.ExtendedProperties.Add(TableProperty.SourceTableName, "DROPITEM")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("uiType", "Type", GetType(Integer), , "AMMOTYPE", "uiIndex", "name"))
        t.Columns.Add(MakeColumn("ubEnemyDropRate", "Enemy %", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubMilitiaDropRate", "Militia %", GetType(Integer)))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeEnemyArmourDropTable() As DataTable
        Dim t As New DataTable("EnemyArmourDrops")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "ARMOURDROPLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "EnemyArmourDrops.xml")
        t.ExtendedProperties.Add(TableProperty.SourceTableName, "DROPITEM")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubArmourClass", "Type", GetType(Integer), , "ArmourClass", "id", "name"))
        t.Columns.Add(MakeColumn("ubEnemyDropRate", "Enemy %", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubMilitiaDropRate", "Militia %", GetType(Integer)))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeEnemyExplosiveDropTable() As DataTable
        Dim t As New DataTable("EnemyExplosiveDrops")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "EXPLOSIVEDROPLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "EnemyExplosiveDrops.xml")
        t.ExtendedProperties.Add(TableProperty.SourceTableName, "DROPITEM")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubType", "Type", GetType(Integer), , "ExplosionType", "id", "name"))
        t.Columns.Add(MakeColumn("ubEnemyDropRate", "Enemy %", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubMilitiaDropRate", "Militia %", GetType(Integer)))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeEnemyWeaponDropTable() As DataTable
        Dim t As New DataTable("EnemyWeaponDrops")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "WEAPONDROPLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "EnemyWeaponDrops.xml")
        t.ExtendedProperties.Add(TableProperty.SourceTableName, "DROPITEM")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubWeaponType", "Type", GetType(Integer), , "WeaponType", "id", "name"))
        t.Columns.Add(MakeColumn("ubEnemyDropRate", "Enemy %", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubMilitiaDropRate", "Militia %", GetType(Integer)))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeEnemyMiscItemDropTable() As DataTable
        Dim t As New DataTable("EnemyMiscDrops")
        t.ExtendedProperties.Add(TableProperty.DataSetName, "MISCDROPLIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "EnemyMiscDrops.xml")
        t.ExtendedProperties.Add(TableProperty.SourceTableName, "DROPITEM")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("usItemClass", "Type", GetType(Integer), , "ItemClass", "id", "name"))
        t.Columns.Add(MakeColumn("ubEnemyDropRate", "Enemy %", GetType(Integer)))
        t.Columns.Add(MakeColumn("ubMilitiaDropRate", "Militia %", GetType(Integer)))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeLoadBearingEquipmentTable() As DataTable
        Dim t As New DataTable("LOADBEARINGEQUIPMENT")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "LoadBearingEquipment.xml")

        t.Columns.Add(MakeColumn("lbeIndex", "ID", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("lbeClass", "Type", GetType(Integer), , "LBEClass", "id", "name"))
        t.Columns.Add(MakeColumn("lbeCombo", "Combo #", GetType(Integer)))
        t.Columns.Add(MakeColumn("lbeFilledSize", "Filled Size", GetType(Integer)))
        For i As Integer = 1 To 12
            t.Columns.Add(MakeColumn("lbePocketIndex" & i, "Pocket #" & i, GetType(Integer), , "POCKET", "pIndex", "pName"))
        Next

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("lbeIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakePocketsTable() As DataTable
        Dim t As New DataTable("POCKET")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "Pockets.xml")

        t.Columns.Add(MakeColumn("pIndex", "ID", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("pName", "Name", GetType(String)))
        t.Columns.Add(MakeColumn("pSilhouette", "Silhouette", GetType(Integer), , "Silhouette", "id", "name"))
        t.Columns.Add(MakeColumn("pType", "Type", GetType(Integer), , "PocketSize", "id", "name"))
        t.Columns.Add(MakeColumn("pRestriction", "Restriction", GetType(Integer), ItemClass.None, "ItemClass", "id", "name", True))

        If Not ItemSizesRead Then ReadItemSizes()
        For i As Integer = 0 To ItemSizeMax
            t.Columns.Add(MakeColumn("ItemCapacityPerSize" & i, "Size " & i, GetType(Integer), , , , , , , , , , "Capacity for Size " & i))
        Next

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("pIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeMercStartingGearTable() As DataTable
        Dim t As New DataTable("MERCGEAR")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "MercStartingGear.xml")

        t.Columns.Add(MakeColumn("mIndex", "ID", GetType(Integer)))
        t.Columns.Add(MakeColumn("mName", "Name", GetType(String)))
        For x As Integer = 1 To 5
            t.Columns.Add(MakeColumn("mPriceMod" & x, "Price Mod", GetType(Integer)))
            t.Columns.Add(MakeColumn("mGearKitName" & x, "Gearkit Name", GetType(String)))
            t.Columns.Add(MakeColumn("mAbsolutePrice" & x, "Absolute Price", GetType(Integer), -1))
            t.Columns.Add(MakeColumn("mHelmet" & x, "Helmet", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "uiIndex = 0 OR usItemClass = " & ItemClass.Armour, True))
            t.Columns.Add(MakeColumn("mHelmetDrop" & x, "Helmet Droppable", GetType(Boolean), , , , , , , True))
            t.Columns.Add(MakeColumn("mHelmetStatus" & x, "Helmet Status", GetType(Integer), , , , , , , True))
            t.Columns.Add(MakeColumn("mVest" & x, "Vest", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "uiIndex = 0 OR usItemClass = " & ItemClass.Armour, True))
            t.Columns.Add(MakeColumn("mVestDrop" & x, "Vest Droppable", GetType(Boolean), , , , , , , True))
            t.Columns.Add(MakeColumn("mVestStatus" & x, "Vest Status", GetType(Integer), , , , , , , True))
            t.Columns.Add(MakeColumn("mLeg" & x, "Legs", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "uiIndex = 0 OR usItemClass = " & ItemClass.Armour, True))
            t.Columns.Add(MakeColumn("mLegDrop" & x, "Legs Droppable", GetType(Boolean), , , , , , , True))
            t.Columns.Add(MakeColumn("mLegStatus" & x, "Legs Status", GetType(Integer), , , , , , , True))
            t.Columns.Add(MakeColumn("mWeapon" & x, "Weapon", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , , True))
            t.Columns.Add(MakeColumn("mWeaponDrop" & x, "Weapon Droppable", GetType(Boolean), , , , , , , True))
            t.Columns.Add(MakeColumn("mWeaponStatus" & x, "Weapon Status", GetType(Integer), , , , , , , True))
            For i As Integer = 0 To 3
                t.Columns.Add(MakeColumn("mBig" & i & x, "Big Item " & i, GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , , True))
                t.Columns.Add(MakeColumn("mBig" & i & "Status" & x, "Big Item " & i & " Status", GetType(Integer), , , , , , , True))
                t.Columns.Add(MakeColumn("mBig" & i & "Quantity" & x, "Big Item " & i & " Quantity", GetType(Integer), , , , , , , True))
                t.Columns.Add(MakeColumn("mBig" & i & "Drop" & x, "Big Item " & i & " Droppable", GetType(Boolean), , , , , , , True))
            Next
            For i As Integer = 0 To 7
                t.Columns.Add(MakeColumn("mSmall" & i & x, "Small Item " & i, GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , , True))
                t.Columns.Add(MakeColumn("mSmall" & i & "Status" & x, "Small Item " & i & " Status", GetType(Integer), , , , , , , True))
                t.Columns.Add(MakeColumn("mSmall" & i & "Quantity" & x, "Small Item " & i & " Quantity", GetType(Integer), , , , , , , True))
            Next
            t.Columns.Add(MakeColumn("lVest" & x, "LBE Vest", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "uiIndex = 0 OR usItemClass = " & ItemClass.LBE, True))
            t.Columns.Add(MakeColumn("lVestStatus" & x, "LBE Vest Status", GetType(Integer), , , , , , , True))
            t.Columns.Add(MakeColumn("lLeftThigh" & x, "LBE Left Thigh", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "uiIndex = 0 OR usItemClass = " & ItemClass.LBE, True))
            t.Columns.Add(MakeColumn("lLeftThighStatus" & x, "LBE Left Thigh Status", GetType(Integer), , , , , , , True))
            t.Columns.Add(MakeColumn("lRightThigh" & x, "LBE Right Thigh", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "uiIndex = 0 OR usItemClass = " & ItemClass.LBE, True))
            t.Columns.Add(MakeColumn("lRightThighStatus" & x, "LBE Right Thigh Status", GetType(Integer), , , , , , , True))
            t.Columns.Add(MakeColumn("lCPack" & x, "LBE Combat Pack", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "uiIndex = 0 OR usItemClass = " & ItemClass.LBE, True))
            t.Columns.Add(MakeColumn("lCPackStatus" & x, "LBE Combat Pack Status", GetType(Integer), , , , , , , True))
            t.Columns.Add(MakeColumn("lBPack" & x, "LBE Backpack", GetType(Integer), , "ITEM", "uiIndex", "szLongItemName", , "uiIndex = 0 OR usItemClass = " & ItemClass.LBE, True))
            t.Columns.Add(MakeColumn("lBPackStatus" & x, "LBE Backpack Status", GetType(Integer), , , , , , , True))
        Next

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("mIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeAttachmentSlotsTable() As DataTable
        Dim t As New DataTable("ATTACHMENTSLOT")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "AttachmentSlots.xml")

        t.Columns.Add(MakeColumn("uiSlotIndex", "ID", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("szSlotName", "Name", GetType(String), , , , , , , , 200))
        t.Columns.Add(MakeColumn("nasAttachmentClass", "Attachment Class", GetType(Integer), , "AttachmentClass", "id", "name")) 'NAS
        t.Columns.Add(MakeColumn("nasLayoutClass", "Layout Class", GetType(Integer))) 'NAS
        t.Columns.Add(MakeColumn("usDescPanelPosX", "X Coord", GetType(Integer)))
        t.Columns.Add(MakeColumn("usDescPanelPosY", "Y Coord", GetType(Integer)))
        t.Columns.Add(MakeColumn("fMultiShot", "Multi-Shot", GetType(Boolean)))
        t.Columns.Add(MakeColumn("fBigSlot", "Big Slot", GetType(Boolean)))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiSlotIndex")
        t.PrimaryKey = pk

        Return t
    End Function

    Private Function MakeITETable() As DataTable
        Dim t As New DataTable("ITEMTOEXPLOSIVE")
        t.ExtendedProperties.Add(TableProperty.DataSetName, t.TableName & "LIST")
        t.ExtendedProperties.Add(TableProperty.FileName, "")

        t.Columns.Add(MakeColumn("uiIndex", "ID", GetType(Integer), , , , , , , True))
        t.Columns.Add(MakeColumn("szItemName", "Short Name", GetType(String), , , , , , , True, 80))

        Dim pk(0) As DataColumn
        pk(0) = t.Columns("uiIndex")
        t.PrimaryKey = pk

        Return t
    End Function

End Module