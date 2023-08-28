using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

using DizzyMedia.Shared;

using Newtonsoft.Json.Linq;
using ThunderWire.Utility;
using HFPS.Systems;
using HFPS.UI;

namespace DizzyMedia.HFPS_Puzzler {

    [AddComponentMenu("Dizzy Media/Puzzler for HFPS/Components/Systems/Puzzler Handler")]
    public class Puzzler_Handler : MonoBehaviour, IItemSelect, ISaveable {


    //////////////////////////////////////
    ///
    ///     CLASSES
    ///
    ///////////////////////////////////////


        [Serializable]
        public class Item_Save {

            public string name;
            public int slot;
            public int ID;
            public int amount;
            public int switcherID;
            public string shortcut;
            public List<ItemDataPair> customData;

        }//Item_Save 

        [System.Serializable]
        public class Solo_Slot {

            [Space]

            public string name;
            public Transform holder;

            [Space]

            public SoloItem_Events events;

            [Space]

            [Header("Auto")]

            public bool active;

        }//Solo_Slot

        [System.Serializable]
        public class SoloItem_Events {

            [Space]

            public UnityEvent onActivate;
            public UnityEvent onActivateLoad;

        }//SoloItem_Events

        [System.Serializable]
        public class Multi_Slot {

            [Space]

            public string name;

            [Space]

            public Slot_Check slotCheck;
            public Slot_Use slotUse;

            [Space]

            public Transform holder;

            [Space]

            [InventorySelector]
            public int itemID;

            [Space]

            public MultiItem_Events events;

            [Space]

            [Header("Auto")]

            public bool filled;
            public bool active;

        }//Multi_Slot

        [System.Serializable]
        public class MultiItem_Events {

            [Space]

            public UnityEvent onPlace;
            public UnityEvent onReset;
            public UnityEvent onActivate;
            public UnityEvent onFilledLoad;

        }//MultiItem_Events

        [System.Serializable]
        public class Rotate_Slot {

            [Space]

            public string name;

            [Space]

            public Transform pivot;

            [Space]

            public bool useStartRotation;
            public Quaternion startRotation;

            [Space]

            public Vector3 activateRotation;

            [Space]

            public Rotate_Events events;

            [Space]

            [Header("Auto")]

            public Vector3 currentRotation;

            [Space]

            public bool active;

        }//Rotate_Slot

        [System.Serializable]
        public class Rotate_Events {

            [Space]

            public UnityEvent onReset;
            public UnityEvent onActivate;

        }//Rotate_Events

        [System.Serializable]
        public class RotateModule {

            [Space]

            public string name;

            public Module_Type moduleType;

            [Space]

            public Transform pivot;
            public Transform parent;

            [Space]

            public bool useStartRotation;
            public Quaternion startRotation;

            [Space]

            public float rotateAmount;
            public Rotate_Type rotateType;

            [Space]

            public List<RotateAdv_Slot> rotateSlots;

            [Space]

            public RotateAdv_Events events;

            [Space]

            [Space]

            [Header("Auto")]

            public Vector3 currentRotation;

            [Space]

            public bool active;

        }//RotateModule

        [System.Serializable]
        public class RotateAdv_Slot {

            [Space]

            public string name;
            public RotateSlot_Type slotType;

            [Space]

            public Transform parent;
            public Transform holder;

            [Space]

            public int correctModule;
            public int correctPosition;

            [Space]

            public RotateAdv_Events events;

            [Space]

            [Header("Auto")]

            [Space]

            public int curModule;
            public int curPosition;

            [Space]

            public bool active;

        }//RotateAdv_Slot

        [System.Serializable]
        public class RotateAdv_Events {

            [Space]

            public UnityEvent onActivate;
            public UnityEvent onDeactivate;

        }//RotateAdv_Events

        [System.Serializable]
        public class RotateModule_Temp {

            public string name;

            public List<RotateAdvSlot_Temp> rotateSlots;

            public Vector3 currentRotation;
            public bool active;

        }//RotateModule_Temp

        [System.Serializable]
        public class RotateAdvSlot_Temp {

            public string name;

            public int curModule;
            public int curPosition;
            public bool active;

        }//RotateAdvSlot_Temp

        [System.Serializable]
        public class Multi_Prefabs {

            [Space]

            public string name;

            [Space]

            [InventorySelector]
            public int itemID;

            [Space]

            public GameObject prefab;

        }//Multi_Prefabs

        [System.Serializable]
        public class Sequence {

            [Space]

            public string name;

            [Space]

            public Sequence_Events events;

            [Space]

            [Header("Auto")]

            public bool active;

        }//Sequence

        [System.Serializable]
        public class Sequence_Events {

            [Space]

            public UnityEvent onInteract;
            public UnityEvent onReset;

        }//Sequence_Events

        [System.Serializable]
        public class Light {

            [Space]

            public string name;
            public InteractiveLight interactLight;

            [Space]

            [Header("Auto")]

            public bool active;

        }//Light

        [System.Serializable]
        public class Switch {

            [Space]

            public string name;
            public DynamicObject dynamObject;

            [Space]

            [Header("Auto")]

            public bool active;

        }//Switch

        [System.Serializable]
        public class Module_Slot {

            [Space]

            public string name;

            [Space]

            public GameObject holder;

            [Space]

            public List<Wave_Slot> waveSlots;

            [Space]

            [Header("Auto")]

            public bool active;

        }//Module_Slot

        [System.Serializable]
        public class Wave_Slot {

            [Space]

            public string name;

            [Space]

            public Puzzler_Wave wave;

            [Space]

            public List<WaveType_Check> waveChecks;

            [Space]

            [Header("Auto")]

            public bool active;

        }//Wave_Slot

        [System.Serializable]
        public class WaveType_Check {

            [Space]

            public string name;

            [Space]

            public Wave_Type type;

            [Space]

            public float value;

            [Space]

            [Header("Auto")]

            public float curValue;
            public bool active;

        }//WaveType_Check

        [System.Serializable]
        public class WaveModule_Temp {

            public string name;

            public List<WaveSlot_Temp> waveSlots;

            public bool active;

        }//WaveModule_Temp

        [System.Serializable]
        public class WaveSlot_Temp {

            public string name;

            public List<WaveCheck_Temp> waveChecks;

            public bool active;

        }//WaveSlot_Temp

        [System.Serializable]
        public class WaveCheck_Temp {

            public string name;

            public float curValue;
            public bool active;

        }//WaveCheck_Temp

        [System.Serializable]
        public class Weight_Items {

            [Space]

            public string name;

            [Space]

            public float weight;

            [Space]

            [InventorySelector]
            public int itemID;

            [Space]

            public GameObject prefab;

        }//Weight_Items

        [System.Serializable]
        public class Weight_Module {

            [Space]

            public string name;

            [Space]

            public float correctWeight;
            public float maxWeight;

            [Space]

            public List<Weight_Slots> weightSlots;

            [Space]

            [Header("Auto")]

            public float curWeight;
            public bool active;

        }//Weight_Module

        [System.Serializable]
        public class Weight_Slots {

            [Space]

            public string name;
            public Transform holder;

            [Space]

            public Weight_Events events;

            [Space]

            [Header("Auto")]

            public bool filled;

        }//Weight_Slots

        [System.Serializable]
        public class Weight_Events {

            [Space]

            public UnityEvent onUse;
            public UnityEvent onReset;
            public UnityEvent onFilledLoad;

        }//Weight_Events

        [System.Serializable]
        public class WeightModule_Temp {

            public string name;

            public List<int> weightSlots;

        }//WeightModule_Temp

        [System.Serializable]
        public class SoundLibrary {

            [Space]

            public string name;
            public AudioClip sound;

            [Space]

            public bool useCustomVolume;
            public float customVolume;

            [Space]

            public bool useCustomPitch;
            public float customPitch;

        }//SoundLibrary


    //////////////////////////////////////
    ///
    ///     ENUMS
    ///
    ///////////////////////////////////////


        public enum Puzzle_Type {

            SoloItem = 0,
            MultiItems = 1,
            Sequential = 2,
            Rotate = 3,
            RotateAdvanced = 4,
            Lights = 5,
            Switches = 6,
            Wave = 7,
            Weight = 8,

        }//Puzzle_Type

        public enum Reset_Type {

            OnFail = 0,
            OnFinish = 1,

        }//Reset_Type

        public enum Complete_Type {

            Auto = 0,
            Manual = 1,

        }//Complete_Type

        public enum Lock_Type {

            None = 0,
            OnComplete = 1,

        }//Lock_Type

        public enum Interact_Type {

            OpenInventory = 0,
            AutoDetect = 1,

        }//Interact_Type

        public enum Item_Type {

            Regular = 0,
            Switcher = 1,

        }//Item_Type

        public enum ItemUse_Type {

            Keep = 0,
            Remove = 1,

        }//ItemUse_Type

        public enum Wave_Type {

            Amplitude = 0,
            WaveLength = 1,
            WaveSpeed = 2,

        }//Wave_Type

        public enum Module_Type {

            Regular = 0,
            Switcher = 1,

        }//Module_Type

        public enum SaveState {

            NotActive = 0,
            Active = 1,

        }//SaveState

        public enum Slot_Type {

            Normal = 0,
            NoPrefab = 1,

        }//Slot_Type

        public enum Slot_Check {

            Empty = 0,
            Item = 1,

        }//Slot_Check

        public enum Slot_Use {

            AllPrefabItems = 0,
            OnlyThisItem = 1,

        }//Slot_Use

        public enum Rotate_Type {

            Global = 0,
            Local = 1,

        }//Rotate_Type

        public enum RotateSlot_Type {

            Empty = 0,
            Filled = 1,

        }//RotateSlot_Type


    //////////////////////////////////////
    ///
    ///     VALUES
    ///
    ///////////////////////////////////////

    ///////////////////////////////
    ///
    ///     USER OPTIONS
    ///
    ///////////////////////////////

    /////////////////////////
    ///
    ///     GENERAL OPTIONS
    ///
    /////////////////////////


        public Puzzle_Type puzzleType;
        public Reset_Type resetType;
        public Complete_Type completeType;
        public Lock_Type lockType;
        public SaveState saveState;

        public bool useCompleteDelay;
        public float completeDelay;

        public bool linkComplete;
        public List<Puzzler_Handler> handlers;


    /////////////////////////
    ///
    ///     ITEM OPTIONS
    ///
    /////////////////////////


        public bool requireItem;

        [InventorySelector]
        public int itemID;


    /////////////////////////
    ///
    ///     ROTATE OPTIONS
    ///
    /////////////////////////


        public Rotate_Type rotateType;
        public float rotateAmount;


    /////////////////////////
    ///
    ///     OBJECTIVE OPTIONS
    ///
    /////////////////////////


        public bool showObjectiveUpdate;
        public int objectiveID;

        public bool objectiveUpdateDelay;
        public float objectiveUpdateWait;


    /////////////////////////
    ///
    ///     INTERACTION OPTIONS
    ///
    /////////////////////////


        public Interact_Type interactType;
        public Item_Type itemType;
        public ItemUse_Type itemUseType;
        public bool detectItemShowing;
        public int switcherSlot = -1;

        public string selectText;
        public string emptyText;
        public string fullText;
        public string wrongItemText;

        public bool useInteractSound;
        public string interactSound;


    /////////////////////////
    ///
    ///     SLOTS OPTIONS
    ///
    /////////////////////////


        public GameObject soloPrefab;
        public List<Multi_Prefabs> multiPrefabs;

        public Slot_Type soloItemSlotType;
        public int soloItemCount;

        public List<Solo_Slot> soloSlots;
        public List<Multi_Slot> multiSlots;

        public List<Rotate_Slot> rotateSlots;
        public List<RotateModule> rotateModules;

        public List<Weight_Items> weightItems;
        public List<Weight_Module> weightModules;


    /////////////////////////
    ///
    ///     SEQUENCE OPTIONS
    ///
    /////////////////////////


        public List<int> sequenceOrder;
        public List<Sequence> sequence;


    /////////////////////////
    ///
    ///     LIGHTS OPTIONS
    ///
    /////////////////////////


        public List<bool> lightsActive;
        public List<Light> lights;


    /////////////////////////
    ///
    ///     SWITCHES OPTIONS
    ///
    /////////////////////////


        public List<bool> switchesActive;
        public List<Switch> switches;


    /////////////////////////
    ///
    ///     WAVE OPTIONS
    ///
    /////////////////////////


        public List<Module_Slot> moduleSlots;


    /////////////////////////
    ///
    ///     SOUND OPTIONS
    ///
    /////////////////////////


        public bool useSounds;

        public AudioSource soundSource;
        public SoundLibrary[] soundLibrary;


    ///////////////////////////////
    ///
    ///     EVENTS
    ///
    ///////////////////////////////


        public UnityEvent onCorrectItem;
        public UnityEvent onIncorrectItem;
        public UnityEvent onPuzzleComplete;
        public UnityEvent onCompleteLoad;

        public bool useDelayedEvent;
        public float delayEventWait;
        public UnityEvent onPuzzleCompleteDelayed;

        public UnityEvent onPuzzleFail;


    ///////////////////////////////
    ///
    ///     AUTO
    ///
    ///////////////////////////////


        public int itemCount;
        public int tempSoloItemCount;
        public int tempItemCount;

        public string tempSelectText;
        public string tempEmptyText;
        public string tempWrongText;

        public List<int> tempInts;
        public List<bool> tempBools;
        public List<Vector3> tempVects;
        public List<RotateModule_Temp> rotateModulesTemp;
        public List<WaveModule_Temp> waveModulesTemp;
        public List<WeightModule_Temp> weightModulesTemp;

        public int currentSlot;
        public int moduleSlot;
        public int waveSlot;
        public int waveCheckSlot;

        public int activeCount;
        public int wavesActive;
        public int modulesActive;
        public int modulesCount;

        public float tempRot;

        public bool present;
        public bool complete;
        public bool fail;
        public bool locked;

        private HFPS_References refs;

        private Inventory inventory;
        private HFPS_GameManager gameManager;
        private ObjectiveManager objectiveManager;

        private Item_Save itemSave;
        private Item[] items;
        private InventoryItemData invItemData;

        public List<Puzzler_Holder> puzzlerHolders;

        public int puzzlerTabs;

        public bool genOpts;
        public bool itemOpts;
        public bool rotOpts;
        public bool objOpts;
        public bool interactOpts;
        public bool slotsOpts;
        public bool seqOpts;
        public bool lightOpts;
        public bool switchOpts;
        public bool weightOpts;
        public bool soundOpts;


    //////////////////////////////////////
    ///
    ///     START ACTIONS
    ///
    ///////////////////////////////////////


        void Awake(){

            if(HFPS_References.instance != null){

                refs = HFPS_References.instance;

            }//instance != null

            inventory = Inventory.Instance;
            gameManager = HFPS_GameManager.Instance;
            objectiveManager = ObjectiveManager.Instance;

        }//Awake

        void Start() {

            StartInit();

        }//Start

        public void StartInit(){

            itemCount = 0;
            tempSoloItemCount = 0;
            tempItemCount = 0;

            currentSlot = 0;

            moduleSlot = 1;
            waveSlot = 1;
            waveCheckSlot = 1;

            activeCount = 0;
            wavesActive = 0;
            modulesActive = 0;
            modulesCount = 0;

            tempRot = 0;

            tempInts = new List<int>();
            tempBools = new List<bool>();
            tempVects = new List<Vector3>();

            puzzlerHolders = new List<Puzzler_Holder>();
            rotateModulesTemp = new List<RotateModule_Temp>();
            waveModulesTemp = new List<WaveModule_Temp>();
            weightModulesTemp = new List<WeightModule_Temp>();

            if(puzzleType == Puzzle_Type.SoloItem){

                items = new Item[1];
                items[0] = inventory.GetItem(itemID);

                if(items[0] != null){

                    if(tempSelectText != selectText + " " + items[0].Title){

                        tempSelectText = selectText + " " + items[0].Title;

                    }//tempSelectText

                    if(tempEmptyText != emptyText + " " + items[0].Title){

                        tempEmptyText = emptyText + " " + items[0].Title;

                    }//tempEmptyText

                }//items[0] != null

                if(soloSlots.Count > 0){

                    for(int i = 0; i < soloSlots.Count; i++) {

                        bool tempBool = false;

                        tempBools.Add(tempBool);

                    }//soloSlots

                }//soloSlots.Count > 0

            }//puzzle type = solo item

            if(puzzleType == Puzzle_Type.MultiItems){

                items = new Item[multiSlots.Count];

                for(int i = 0; i < items.Length; i++) {

                    items[i] = inventory.GetItem(multiSlots[i].itemID);

                }//for i items

                if(tempSelectText != selectText){

                    tempSelectText = selectText;

                }//tempSelectText

                if(tempEmptyText != emptyText){

                    tempEmptyText = emptyText;

                }//tempEmptyText

                for(int ms = 0; ms < multiSlots.Count; ms++) {

                    int newInt = 0;

                    tempInts.Add(newInt);

                    if(multiSlots[ms].slotCheck == Slot_Check.Empty){

                        multiSlots[ms].active = true;

                    }//slotCheck = empty

                }//multiSlots

            }//puzzleType = multi items

            if(puzzleType == Puzzle_Type.Sequential | puzzleType == Puzzle_Type.Rotate | puzzleType == Puzzle_Type.RotateAdvanced){

                if(requireItem){

                    items = new Item[1];
                    items[0] = inventory.GetItem(itemID);

                    if(items[0] != null){

                        if(tempSelectText != selectText + " " + items[0].Title){

                            tempSelectText = selectText + " " + items[0].Title;

                        }//tempSelectText

                        if(tempEmptyText != emptyText + " " + items[0].Title){

                            tempEmptyText = emptyText + " " + items[0].Title;

                        }//tempEmptyText

                        if(tempWrongText != items[0].Title + " " + wrongItemText){

                            tempWrongText = items[0].Title + " " + wrongItemText;

                        }//tempEmptyText

                    }//items[0] != null

                }//requireItem

            }//puzzleType = sequential, rotate or rotate adv

            if(puzzleType == Puzzle_Type.Rotate){

                for(int i = 0; i < rotateSlots.Count; i++) {

                    if(rotateSlots[i].useStartRotation){

                        if(rotateType == Rotate_Type.Global){

                            rotateSlots[i].pivot.eulerAngles = new Vector3(rotateSlots[i].startRotation.x, rotateSlots[i].startRotation.y, rotateSlots[i].startRotation.z);

                            //Debug.Log(i + "" + rotateSlots[i].pivot.eulerAngles);

                        }//rotateType = global

                        if(rotateType == Rotate_Type.Local){

                            rotateSlots[i].pivot.localEulerAngles = new Vector3(rotateSlots[i].startRotation.x, rotateSlots[i].startRotation.y, rotateSlots[i].startRotation.z);

                            //Debug.Log(i + "" + rotateSlots[i].pivot.localEulerAngles);

                        }//rotateType = local

                    }//useStartRotation

                    if(rotateSlots[i].startRotation.x > 0 | rotateSlots[i].startRotation.x < 0){

                        rotateSlots[i].currentRotation.x = rotateSlots[i].pivot.eulerAngles.x - 360;

                    }//startRotation.x > 0 or < 0

                    if(rotateSlots[i].startRotation.y > 0 | rotateSlots[i].startRotation.y < 0){

                        rotateSlots[i].currentRotation.y = rotateSlots[i].pivot.eulerAngles.y - 360;

                    }//startRotation.y > 0 or < 0

                    if(rotateSlots[i].startRotation.z > 0 | rotateSlots[i].startRotation.z < 0){

                        rotateSlots[i].currentRotation.z = rotateSlots[i].pivot.eulerAngles.z - 360;

                    }//startRotation.z > 0 or < 0

                    tempVects.Add(rotateSlots[i].currentRotation);

                }//for i rotateSlots

            }//puzzleType = rotate

            if(puzzleType == Puzzle_Type.RotateAdvanced){

                for(int i = 0; i < rotateModules.Count; i++) {

                    RotateModule_Temp newTempModule = new RotateModule_Temp();
                    newTempModule.name = rotateModules[i].name;
                    newTempModule.rotateSlots = new List<RotateAdvSlot_Temp>();

                    if(rotateModules[i].moduleType != Module_Type.Switcher){

                        modulesCount += 1;

                    }//moduleType != switcher

                    if(rotateModules[i].useStartRotation){

                        rotateModules[i].pivot.eulerAngles = new Vector3(rotateModules[i].startRotation.x, rotateModules[i].startRotation.y, rotateModules[i].startRotation.z);

                        if(rotateModules[i].pivot.eulerAngles.x > 0 | rotateModules[i].pivot.eulerAngles.x < 0){

                            rotateModules[i].currentRotation.x = rotateModules[i].pivot.localEulerAngles.x - 360;

                            if(rotateModules[i].currentRotation.x == -360 | rotateModules[i].currentRotation.x == 360){

                                rotateModules[i].currentRotation.x = 0;

                            }//currentRotation.x == -360 or 360

                        }//pivot.eulerAngles.x > 0 or < 0

                        if(rotateModules[i].pivot.eulerAngles.y > 0 | rotateModules[i].pivot.eulerAngles.y < 0){

                            rotateModules[i].currentRotation.y = rotateModules[i].pivot.localEulerAngles.y - 360;

                            if(rotateModules[i].currentRotation.y == -360 | rotateModules[i].currentRotation.y == 360){

                                rotateModules[i].currentRotation.y = 0;

                            }//currentRotation.y == -360 or 360

                        }//pivot.eulerAngles.y > 0 or < 0

                        if(rotateModules[i].pivot.eulerAngles.z > 0 | rotateModules[i].pivot.eulerAngles.z < 0){

                            rotateModules[i].currentRotation.z = rotateModules[i].pivot.localEulerAngles.z - 360;

                            if(rotateModules[i].currentRotation.z == -360 | rotateModules[i].currentRotation.z == 360){

                                rotateModules[i].currentRotation.z = 0;

                            }//currentRotation.z == -360 or 360

                        }//pivot.eulerAngles.z > 0 or < 0

                    }//useStartRotation

                    for(int i2 = 0; i2 < rotateModules[i].rotateSlots.Count; i2++) {

                        rotateModules[i].rotateSlots[i2].curModule = i + 1;
                        rotateModules[i].rotateSlots[i2].curPosition = i2 + 1;

                        RotateAdvSlot_Temp newTempSlot = new RotateAdvSlot_Temp();
                        newTempSlot.name = rotateModules[i].rotateSlots[i2].name;
                        newTempSlot.curModule = rotateModules[i].rotateSlots[i2].curModule;
                        newTempSlot.curPosition = rotateModules[i].rotateSlots[i2].curPosition;

                        if(rotateModules[i].rotateSlots[i2].slotType == RotateSlot_Type.Empty){

                            rotateModules[i].rotateSlots[i2].active = true;
                            newTempSlot.active = true;

                        }//slotType = empty

                        newTempModule.rotateSlots.Add(newTempSlot);

                    }//for i2 rotateSlots

                    newTempModule.currentRotation = rotateModules[i].currentRotation;

                    rotateModulesTemp.Add(newTempModule);

                }//for i rotateModules

            }//puzzleType = rotate advanced

            if(puzzleType == Puzzle_Type.Lights){

                for(int i = 0; i < lights.Count; i++) {

                    tempBools.Add(lights[i].interactLight.isPoweredOn);

                }//for i switches

            }//puzzleType = switches

            if(puzzleType == Puzzle_Type.Switches){

                for(int i = 0; i < switches.Count; i++) {

                    tempBools.Add(switches[i].dynamObject.isOpened);

                }//for i switches

            }//puzzleType = switches

            if(puzzleType == Puzzle_Type.Wave){

                if(moduleSlots.Count > 0){

                    for(int i = 0; i < moduleSlots.Count; i++) {

                        WaveModule_Temp tempModule = new WaveModule_Temp();
                        tempModule.name = moduleSlots[i].name;

                        tempModule.waveSlots = new List<WaveSlot_Temp>();

                        if(moduleSlots[i].waveSlots.Count > 0){

                            for(int i2 = 0; i2 < moduleSlots[i].waveSlots.Count; i2++) {

                                WaveSlot_Temp tempSlot = new WaveSlot_Temp();
                                tempSlot.name = moduleSlots[i].waveSlots[i2].name;

                                tempSlot.waveChecks = new List<WaveCheck_Temp>();

                                if(moduleSlots[i].waveSlots[i2].wave != null){

                                    if(moduleSlots[i].waveSlots[i2].waveChecks.Count > 0){

                                        for(int i3 = 0; i3 < moduleSlots[i].waveSlots[i2].waveChecks.Count; i3++) {

                                            WaveCheck_Temp tempWaveCheck = new WaveCheck_Temp();
                                            tempWaveCheck.name = moduleSlots[i].waveSlots[i2].waveChecks[i3].name;

                                            if(moduleSlots[i].waveSlots[i2].waveChecks[i3].type == Wave_Type.Amplitude){

                                                moduleSlots[i].waveSlots[i2].waveChecks[i3].curValue = moduleSlots[i].waveSlots[i2].wave.curAmplitude;

                                            }//type = amplitude

                                            if(moduleSlots[i].waveSlots[i2].waveChecks[i3].type == Wave_Type.WaveLength){

                                                moduleSlots[i].waveSlots[i2].waveChecks[i3].curValue = moduleSlots[i].waveSlots[i2].wave.curWavelength;

                                            }//type = wave length

                                            if(moduleSlots[i].waveSlots[i2].waveChecks[i3].type == Wave_Type.WaveSpeed){

                                                moduleSlots[i].waveSlots[i2].waveChecks[i3].curValue = moduleSlots[i].waveSlots[i2].wave.curWaveSpeed;

                                            }//type = wave speed

                                            tempWaveCheck.curValue = moduleSlots[i].waveSlots[i2].waveChecks[i3].curValue;

                                            tempSlot.waveChecks.Add(tempWaveCheck);

                                        }//for i3 waveChecks

                                        tempModule.waveSlots.Add(tempSlot);

                                    }//waveChecks.Count > 0

                                }//wave != null

                            }//for i2 waveSlots

                        }//waveSlots.Count > 0

                        waveModulesTemp.Add(tempModule);

                    }//for i moduleSlots

                }//moduleSlots.Count > 0

            }//puzzleType = wave

            if(puzzleType == Puzzle_Type.Weight){

                items = new Item[weightItems.Count];

                for(int i = 0; i < items.Length; i++) {

                    items[i] = inventory.GetItem(weightItems[i].itemID);

                }//for i items

                if(items.Length > 1){

                    if(tempSelectText != selectText){

                        tempSelectText = selectText;

                    }//tempSelectText

                    if(tempEmptyText != emptyText){

                        tempEmptyText = emptyText;

                    }//tempEmptyText

                //items.Length > 1
                } else {

                    if(tempSelectText != selectText + " " + items[0].Title){

                        tempSelectText = selectText + " " + items[0].Title;

                    }//tempSelectText

                    if(tempEmptyText != emptyText + " " + items[0].Title){

                        tempEmptyText = emptyText + " " + items[0].Title;

                    }//tempEmptyText    

                }//items.Length > 1

                for(int wm = 0; wm < weightModules.Count; wm++) {

                    WeightModule_Temp tempModule = new WeightModule_Temp();
                    tempModule.name = weightModules[wm].name;

                    tempModule.weightSlots = new List<int>();

                    for(int wms = 0; wms < weightModules[wm].weightSlots.Count; wms++) {

                        int tempInt = 0;

                        tempModule.weightSlots.Add(tempInt);

                    }//for wms weightSlots

                    weightModulesTemp.Add(tempModule);

                }//for wmt weightModulesTemp

            }//puzzleType = weight

            if(lockType != Lock_Type.None){

                Puzzler_Holder[] allChildren = GetComponentsInChildren<Puzzler_Holder>();

                foreach(Puzzler_Holder child in allChildren) {    

                    puzzlerHolders.Add(child.GetComponent<Puzzler_Holder>());

                }//foreach child

            }//lockType != none

            present = false;
            fail = false;

            Complete_State(false);
            Locked_State(false);

        }//StartInit


    //////////////////////////////////////
    ///
    ///     USE ACTIONS
    ///
    ///////////////////////////////////////

    ///////////////////////////
    ///
    ///     INIT
    ///
    ///////////////////////////


        public void Interaction_Init(){

            if(!locked) {

                if(puzzleType == Puzzle_Type.SoloItem){

                    if(interactType == Interact_Type.OpenInventory){

                        ItemData_Catch();

                        inventory.OnInventorySelect(new int[1] { itemID }, new string[0], this, tempSelectText, tempEmptyText);

                    }//interactType = OpenInventory

                    if(interactType == Interact_Type.AutoDetect){

                        ItemCheck(itemID);

                    }//AutoDetect

                }//puzzleType = solo

                if(puzzleType == Puzzle_Type.MultiItems | puzzleType == Puzzle_Type.Weight){

                    if(interactType == Interact_Type.OpenInventory){

                        if(puzzleType == Puzzle_Type.MultiItems){

                            if(multiSlots[currentSlot - 1].slotUse == Slot_Use.AllPrefabItems){

                                int[] tempIDs = new int[multiSlots.Count];

                                for(int i = 0; i < multiSlots.Count; ++i ) {

                                    tempIDs[i] = multiSlots[i].itemID;

                                }//for i multiSlots

                                inventory.OnInventorySelect(tempIDs, new string[0], this, tempSelectText, tempEmptyText);

                            }//slotUse = AllPrefabItems

                            if(multiSlots[currentSlot - 1].slotUse == Slot_Use.OnlyThisItem){

                                int[] tempIDs = new int[1];
                                tempIDs[0] = multiSlots[currentSlot - 1].itemID;

                                inventory.OnInventorySelect(tempIDs, new string[0], this, tempSelectText, tempEmptyText);

                            }//slotUse = OnlyThisItem

                        }//puzzleType = multi

                        if(puzzleType == Puzzle_Type.Weight){

                            int tempCount = 0;
                            ItemData_Catch();

                            for(int ws = 0; ws < weightModules[moduleSlot - 1].weightSlots.Count; ++ws ) {

                                if(weightModules[moduleSlot - 1].weightSlots[ws].filled){

                                    tempCount += 1;

                                    if(tempCount > weightModules[moduleSlot - 1].weightSlots.Count){

                                        tempCount = weightModules[moduleSlot - 1].weightSlots.Count;

                                    }//tempCount > weightSlots.Count

                                }//filled

                            }//for ws weightSlots

                            if(tempCount != weightModules[moduleSlot - 1].weightSlots.Count){

                                int[] tempIDs = new int[weightItems.Count];

                                for(int i = 0; i < weightItems.Count; ++i ) {

                                    tempIDs[i] = weightItems[i].itemID;

                                }//for i weightItems

                                inventory.OnInventorySelect(tempIDs, new string[0], this, tempSelectText, tempEmptyText);

                            //tempCount != weightSlots.Count
                            } else {

                                gameManager.ShowQuickMessage(fullText, "ModuleFull");

                            }//tempCount != weightSlots.Count

                        }//puzzleType = weight

                    }//interactType = OpenInventory

                }//puzzleType = multi

                if(puzzleType == Puzzle_Type.Sequential | puzzleType == Puzzle_Type.Rotate | puzzleType == Puzzle_Type.RotateAdvanced){

                    if(!requireItem){

                        if(puzzleType == Puzzle_Type.Sequential){

                            Sequence_Check();

                        }//puzzleType = Sequential

                        if(puzzleType == Puzzle_Type.Rotate){

                            Rotate_Check();

                        }//puzzleType = Rotate

                        if(puzzleType == Puzzle_Type.RotateAdvanced){

                            RotateAdvanced_Check();

                        }//puzzleType = RotateAdvanced

                    //!requireItem
                    } else {

                        ItemData_Catch();

                        if(interactType == Interact_Type.OpenInventory){

                            if(itemType == Item_Type.Regular){

                                inventory.OnInventorySelect(new int[1] { itemID }, new string[0], this, tempSelectText, tempEmptyText);

                            }//itemType = regular

                            if(itemType == Item_Type.Switcher){

                                if(!detectItemShowing){

                                    inventory.OnInventorySelect(new int[1] { itemID }, new string[0], this, tempSelectText, tempEmptyText);

                                //!detectItemShowing
                                } else {

                                    ItemCheck(itemID);

                                }//!detectItemShowing

                            }//itemType = switcher

                        }//interactType = OpenInventory

                        if(interactType == Interact_Type.AutoDetect){

                            ItemCheck(itemID);

                        }//AutoDetect

                    }//!requireItem

                }//puzzleType = sequential, rotate or rotate adv

                if(puzzleType == Puzzle_Type.Lights){

                    Lights_Check();

                }//puzzleType = switches

                if(puzzleType == Puzzle_Type.Switches){

                    Switches_Check();

                }//puzzleType = switches

                if(puzzleType == Puzzle_Type.Wave){

                    Wave_Check();

                }//puzzleType = wave

            }//!locked

        }//Interaction_Init


    ///////////////////////////
    ///
    ///     ITEM ACTIONS
    ///
    ///////////////////////////


        private void ItemData_Catch(){

            itemSave = new Item_Save();

            ItemData itemData = new ItemData();
            invItemData = new InventoryItemData();

            invItemData = inventory.ItemDataOfItem(itemID);

            if(invItemData != null){

                itemData = invItemData.data;

                itemSave.customData = new List<ItemDataPair>();

                if(itemData.data.Count > 0){

                    foreach(var data in itemData.data) {

                        ItemDataPair tempPair = new ItemDataPair(data.Key, data.Value.ToString());
                        itemSave.customData.Add(tempPair);

                    }//foreach data

                }//itemData.Count > 0

                itemSave.name = invItemData.itemTitle;
                itemSave.slot = invItemData.slotID;
                itemSave.ID = invItemData.itemID;
                itemSave.amount = invItemData.itemAmount;
                itemSave.switcherID = invItemData.item.Settings.useSwitcherID;

                if(!string.IsNullOrEmpty(invItemData.shortcut)){

                    itemSave.shortcut = invItemData.shortcut;

                //shortcut != null
                } else {

                    itemSave.shortcut = "";

                }//shortcut != null

            }//data != null

        }//ItemData_Catch

        public void OnItemSelect(int ID, ItemData data) {

            ItemCheck(ID);

        }//OnItemSelect

        private void ItemCheck(int ID){

            present = false;
            Complete_State(false);

            if(puzzleType == Puzzle_Type.SoloItem | puzzleType == Puzzle_Type.Sequential | puzzleType == Puzzle_Type.Rotate | puzzleType == Puzzle_Type.RotateAdvanced){

                if(interactType == Interact_Type.OpenInventory){

                    if(puzzleType == Puzzle_Type.SoloItem){

                        if(ID == itemID){

                            present = true;

                        }//ID = itemID

                    }//puzzleType = solo item

                    if(puzzleType == Puzzle_Type.Sequential | puzzleType == Puzzle_Type.Rotate | puzzleType == Puzzle_Type.RotateAdvanced){

                        if(itemType == Item_Type.Regular){

                            if(ID == itemID){

                                present = true;

                            }//ID = itemID

                        }//itemType = regular

                        if(itemType == Item_Type.Switcher){

                            if(!detectItemShowing){

                                if(ID == itemID){

                                    present = true;

                                }//ID = itemID

                            //!detectItemShowing
                            } else {

                                if(inventory.CheckItemInventory(ID)) {

                                    present = true;

                                }//CheckItemInventory

                            }//!detectItemShowing

                        }//itemType = switcher

                    }//puzzleType = sequential or rotate

                }//interactType = open inventory

                if(interactType == Interact_Type.AutoDetect){

                    if(inventory.CheckItemInventory(ID)) {

                        present = true;

                    }//CheckItemInventory

                }//interactType = auto detect

            }//puzzleType = solo, sequence or rotate

            if(puzzleType == Puzzle_Type.MultiItems | puzzleType == Puzzle_Type.Weight){

                if(interactType == Interact_Type.OpenInventory){

                    if(puzzleType == Puzzle_Type.MultiItems){

                        for(int i = 0; i < multiSlots.Count; i++) {

                            if(multiSlots[i].itemID == ID){

                                present = true;

                            }//itemID = ID

                        }//for i multiSlots

                    }//puzzleType = multi

                    if(puzzleType == Puzzle_Type.Weight){

                        for(int i = 0; i < weightItems.Count; i++) {

                            if(weightItems[i].itemID == ID){

                                present = true;

                            }//itemID = ID

                        }//for i weightItems

                    }//puzzleType = weight

                }//interactType = open inventory

            }//puzzleType = multi or weight

            if(present){

                activeCount = 0;

                if(puzzleType == Puzzle_Type.SoloItem | puzzleType == Puzzle_Type.MultiItems | puzzleType == Puzzle_Type.Weight){

                    onCorrectItem.Invoke();

                }//puzzleType = solo item, multi items or weight

                if(puzzleType == Puzzle_Type.Sequential | puzzleType == Puzzle_Type.Rotate | puzzleType == Puzzle_Type.RotateAdvanced){

                    if(itemType == Item_Type.Regular){

                        onCorrectItem.Invoke();

                    }//itemType = regular

                }//puzzleType = sequential, rotate or rotate adv

                if(puzzleType == Puzzle_Type.SoloItem | puzzleType == Puzzle_Type.Weight){

                    if(interactType == Interact_Type.OpenInventory){

                        tempItemCount = inventory.GetItemAmount(itemID) + 1;

                    }//interactType = open inventory

                    if(interactType == Interact_Type.AutoDetect){

                        tempItemCount = inventory.GetItemAmount(itemID);

                    }//interactType = auto detect

                }//puzzleType = solo, sequence, rotate or weight

                if(puzzleType == Puzzle_Type.SoloItem){

                    Solo_Check();

                }//puzzleType = solo

                if(puzzleType == Puzzle_Type.MultiItems){

                    Multi_Check(ID);

                }//puzzleType = multi

                if(puzzleType == Puzzle_Type.Sequential){

                    if(itemType == Item_Type.Regular){

                        Sequence_Check();

                    }//itemType = regular

                    if(itemType == Item_Type.Switcher){

                        if(!detectItemShowing){

                            Sequence_Check();

                        }//!detectItemShowing

                    }//itemType = switcher

                }//puzzleType = sequence

                if(puzzleType == Puzzle_Type.Rotate){

                    if(itemType == Item_Type.Regular){

                        Rotate_Check();

                    }//itemType = regular

                    if(itemType == Item_Type.Switcher){

                        if(!detectItemShowing){

                            Rotate_Check();

                        }//!detectItemShowing

                    }//itemType = switcher

                }//puzzleType = rotate

                if(puzzleType == Puzzle_Type.RotateAdvanced){

                    if(itemType == Item_Type.Regular){

                        RotateAdvanced_Check();

                    }//itemType = regular

                    if(itemType == Item_Type.Switcher){

                        if(!detectItemShowing){

                            RotateAdvanced_Check();

                        }//!detectItemShowing

                    }//itemType = switcher

                }//puzzleType == rotate advanced

                if(puzzleType == Puzzle_Type.Weight){

                    Weight_Check(ID);

                }//puzzleType = weight

                if(puzzleType == Puzzle_Type.SoloItem){

                    if(itemUseType == ItemUse_Type.Remove){

                        if(interactType == Interact_Type.OpenInventory){

                            inventory.RemoveSelectedItem(true);

                        }//interactType = open inventory

                    }//itemUseType = remove

                }//puzzleType = solo

                if(puzzleType == Puzzle_Type.Sequential | puzzleType == Puzzle_Type.Rotate | puzzleType == Puzzle_Type.RotateAdvanced){

                    if(itemType == Item_Type.Regular){

                        if(itemUseType == ItemUse_Type.Keep){

                            if(interactType == Interact_Type.OpenInventory){

                                StartCoroutine("Item_AddDelayed");

                            }//interactType = open inventory

                        }//itemUseType = keep

                        if(itemUseType == ItemUse_Type.Remove){

                            if(interactType == Interact_Type.OpenInventory){

                                inventory.RemoveSelectedItem(false);

                            }//interactType = open inventory

                            if(interactType == Interact_Type.AutoDetect){

                                inventory.RemoveItemAmount(ID, itemSave.amount);

                            }//interactType = auto detect

                        }//itemUseType = remove

                    }//itemType = regular

                    if(itemType == Item_Type.Switcher){

                        if(!detectItemShowing){

                            onCorrectItem.Invoke();

                            if(itemUseType == ItemUse_Type.Keep){

                                if(interactType == Interact_Type.OpenInventory){

                                    StartCoroutine("Item_AddDelayed");

                                }//interactType = open inventory

                            }//itemUseType = keep

                            if(itemUseType == ItemUse_Type.Remove){

                                if(interactType == Interact_Type.OpenInventory){

                                    inventory.RemoveSelectedItem(false);

                                }//interactType = open inventory

                                if(interactType == Interact_Type.AutoDetect){

                                    inventory.RemoveItemAmount(ID, itemSave.amount);

                                }//interactType = auto detect

                            }//itemUseType = remove

                        //!detectItemShowing
                        } else {

                            if(refs.itemSwitcher.currentItem > - 1){

                                if(refs.itemSwitcher.currentItem == switcherSlot){

                                    onCorrectItem.Invoke();

                                    if(puzzleType == Puzzle_Type.Sequential){

                                        Sequence_Check();

                                    }//puzzleType = sequence

                                    if(puzzleType == Puzzle_Type.Rotate){

                                        Rotate_Check();

                                    }//puzzleType = rotate

                                    if(puzzleType == Puzzle_Type.RotateAdvanced){

                                        RotateAdvanced_Check();

                                    }//puzzleType == rotate advanced

                                //currentItem = switcherSlot
                                } else {

                                    onIncorrectItem.Invoke();

                                    gameManager.ShowQuickMessage(tempWrongText, "NoItems");

                                }//currentItem = switcherSlot

                            //currentItem > -1
                            } else {

                                onIncorrectItem.Invoke();

                                gameManager.ShowQuickMessage(tempWrongText, "NoItems");

                            }//currentItem > -1

                        }//!detectItemShowing

                    }//itemType = switcher

                }//puzzleType = sequence, rotate or rotate adv

                if(puzzleType == Puzzle_Type.SoloItem | puzzleType == Puzzle_Type.Weight){

                    if(itemUseType == ItemUse_Type.Keep){

                        if(interactType == Interact_Type.OpenInventory){

                            StartCoroutine("Item_AddDelayed");

                        }//interactType = open inventory

                    }//itemUseType = keep

                    if(itemUseType == ItemUse_Type.Remove){

                        if(interactType == Interact_Type.AutoDetect){

                            inventory.RemoveItemAmount(ID, tempItemCount);

                        }//interactType = auto detect

                    }//itemUseType = remove

                }//puzzleType = solo or weight

                if(useInteractSound){

                    PlaySound(interactSound);

                }//useInteractSound

                if(completeType == Complete_Type.Auto){

                    CompleteCheck();

                }//completeType = auto

            //present
            } else {

                if(tempEmptyText != "") {

                    gameManager.ShowQuickMessage(tempEmptyText, "NoItems");

                }//tempEmptyText != null

            }//present

        }//ItemCheck

        public IEnumerator Item_AddDelayed(){

            #if PUZZLER_PRESENT

                if(itemSave != null){

                    yield return new WaitForSeconds(0.1f);

                    inventory.AddItemToSlotCustom(itemSave.slot, itemSave.ID, 1, invItemData, false);

                    yield return new WaitForSeconds(0.1f);

                    if(inventory.CheckItemInventory(itemSave.ID)) {

                        if(!string.IsNullOrEmpty(itemSave.shortcut)){

                            yield return new WaitForSeconds(0.2f);

                            inventory.ShortcutBindCustom(itemSave.ID, itemSave.slot, itemSave.shortcut);

                        }//shortcut != null

                    }//has item

                }//itemSave != null

            #else 

                yield return new WaitForSeconds(0.1f);

            #endif

        }//Item_AddDelayed


    //////////////////////////////////////
    ///
    ///     SOLO ITEM ACTIONS
    ///
    ///////////////////////////////////////


        public void Solo_Check(){

            if(soloItemSlotType == Slot_Type.Normal){

                if(soloSlots.Count > 0){

                    for(int i = 0; i < soloSlots.Count; ++i ) {

                        if(soloSlots[i].active){

                            activeCount += 1;

                        }//active

                    }//for i soloSlots

                    for(int i2 = 0; i2 < soloSlots.Count; ++i2 ) {

                        if(itemUseType == ItemUse_Type.Keep){

                            if(i2 < tempItemCount){

                                if(!soloSlots[i2].active){

                                    if(soloPrefab != null){

                                        GameObject newItem = Instantiate(soloPrefab, soloSlots[i2].holder);

                                    }//soloPrefab != null

                                    tempBools[i2] = true;

                                    soloSlots[i2].active = true;

                                    soloSlots[i2].events.onActivate.Invoke();

                                    itemCount += 1;

                                }//!active

                            }//i2 < tempItemCount

                        }//itemUseType = keep

                        if(itemUseType == ItemUse_Type.Remove){

                            if(i2 <= itemCount && i2 <= activeCount + tempItemCount - 1){

                                if(!soloSlots[i2].active){

                                    if(soloPrefab != null){

                                        GameObject newItem = Instantiate(soloPrefab, soloSlots[i2].holder);

                                    }//soloPrefab != null

                                    tempBools[i2] = true;

                                    soloSlots[i2].active = true;

                                    soloSlots[i2].events.onActivate.Invoke();

                                    itemCount += 1;

                                }//!active

                            }

                        }//itemUseType = remove

                    }//for i soloSlots

                }//soloSlots.Count > 0

            }//soloItemSlotType = normal 

            if(soloItemSlotType == Slot_Type.NoPrefab){

                itemCount += tempItemCount;
                tempSoloItemCount = itemCount;

            }//soloItemSlotType = No Prefab

        }//Solo_Check


    //////////////////////////////////////
    ///
    ///     MULTI ITEM ACTIONS
    ///
    ///////////////////////////////////////


        public void Multi_Check(int ID){

            if(currentSlot != 0){

                for(int i = 0; i < multiPrefabs.Count; ++i ) {

                    if(multiPrefabs[i].itemID == ID){

                        GameObject newItem = Instantiate(multiPrefabs[i].prefab, multiSlots[currentSlot - 1].holder);

                        if(newItem.GetComponent<Puzzler_Holder>() != null){    

                            newItem.GetComponent<Puzzler_Holder>().puzzlerHand = this;
                            newItem.GetComponent<Puzzler_Holder>().slot = currentSlot;

                            puzzlerHolders.Add(newItem.GetComponent<Puzzler_Holder>());

                        }//Puzzler_Holder != null

                        multiSlots[currentSlot - 1].filled = true;

                        multiSlots[currentSlot - 1].events.onPlace.Invoke();

                        tempInts[currentSlot - 1] = ID;

                    }//itemID = ID

                }//for i multiPrefabs

                if(multiSlots[currentSlot -1].slotCheck == Slot_Check.Empty){

                    multiSlots[currentSlot - 1].active = false;

                }//slotCheck = empty

                if(multiSlots[currentSlot -1].slotCheck == Slot_Check.Item){

                    if(multiSlots[currentSlot - 1].itemID == ID){

                        multiSlots[currentSlot - 1].active = true;

                    }//itemID = ID

                }//slotCheck = item

            }//currentSlot != 0

            for(int i2 = 0; i2 < multiSlots.Count; ++i2 ) {

                if(multiSlots[i2].active){

                    activeCount += 1;

                }//!active

            }//for i2 multiSlots

        }//Multi_Check


    //////////////////////////////////////
    ///
    ///     SEQUENCE ACTIONS
    ///
    ///////////////////////////////////////


        private void Sequence_Check(){

            Complete_State(false);
            fail = false;

            if(currentSlot != 0){

                if(!tempInts.Contains(currentSlot)){

                    tempInts.Add(currentSlot);

                }//!Contains

                sequence[currentSlot - 1].events.onInteract.Invoke();
                sequence[currentSlot - 1].active = true;

                complete = CompareInts(sequenceOrder, tempInts);

                if(!requireItem){

                    if(useInteractSound){

                        PlaySound(interactSound);

                    }//useInteractSound

                    if(completeType == Complete_Type.Auto){

                        CompleteCheck();

                    }//completeType = auto

                }//!requireItem

            }//currentSlot != 0

        }//Sequence_Check

        public void Sequence_Reset(){

            if(sequence.Count > 0){

                for(int i = 0; i < sequence.Count; ++i ) {

                    if(sequence[i].active){

                        sequence[i].events.onReset.Invoke();
                        sequence[i].active = false;

                    }//active

                }//for i sequence

            }//sequence.Count > 0

            tempInts = new List<int>();

        }//Sequence_Reset


    //////////////////////////////////////
    ///
    ///     ROTATE ACTIONS
    ///
    ///////////////////////////////////////


        private void Rotate_Check(){

            Complete_State(false);
            tempRot = 0;
            activeCount = 0;

            if(currentSlot != 0){

                if(rotateType == Rotate_Type.Global){

                    tempRot = rotateSlots[currentSlot - 1].pivot.eulerAngles.z - 360 + rotateAmount;
                    rotateSlots[currentSlot - 1].pivot.eulerAngles = new Vector3(0, 0, tempRot);

                    if(rotateSlots[currentSlot - 1].pivot.eulerAngles.x > 0 | rotateSlots[currentSlot - 1].pivot.eulerAngles.x < 0){

                        rotateSlots[currentSlot - 1].currentRotation.x = rotateSlots[currentSlot - 1].pivot.eulerAngles.x - 360;

                        if(rotateSlots[currentSlot - 1].currentRotation.x == -360 | rotateSlots[currentSlot - 1].currentRotation.x == 360){

                            rotateSlots[currentSlot - 1].currentRotation.x = 0;

                        }//currentRotation.x == -360 or 360

                    }//pivot.eulerAngles.x > 0 or < 0

                    if(rotateSlots[currentSlot - 1].pivot.eulerAngles.y > 0 | rotateSlots[currentSlot - 1].pivot.eulerAngles.y < 0){

                        rotateSlots[currentSlot - 1].currentRotation.y = rotateSlots[currentSlot - 1].pivot.eulerAngles.y - 360;

                        if(rotateSlots[currentSlot - 1].currentRotation.y == -360 | rotateSlots[currentSlot - 1].currentRotation.y == 360){

                            rotateSlots[currentSlot - 1].currentRotation.y = 0;

                        }//currentRotation.y == -360 or 360

                    }//pivot.eulerAngles.y > 0 or < 0

                    if(rotateSlots[currentSlot - 1].pivot.eulerAngles.z > 0 | rotateSlots[currentSlot - 1].pivot.eulerAngles.z < 0){

                        rotateSlots[currentSlot - 1].currentRotation.z = rotateSlots[currentSlot - 1].pivot.eulerAngles.z - 360;

                        if(rotateSlots[currentSlot - 1].currentRotation.z == -360 | rotateSlots[currentSlot - 1].currentRotation.z == 360){

                            rotateSlots[currentSlot - 1].currentRotation.z = 0;

                        }//currentRotation.z == -360 or 360

                    }//pivot.eulerAngles.z > 0 or < 0

                }//rotateType = global

                if(rotateType == Rotate_Type.Local){

                    tempRot = rotateSlots[currentSlot - 1].pivot.localEulerAngles.z - 360 + rotateAmount;
                    rotateSlots[currentSlot - 1].pivot.localEulerAngles = new Vector3(0, 0, tempRot);

                    if(rotateSlots[currentSlot - 1].pivot.localEulerAngles.x > 0 | rotateSlots[currentSlot - 1].pivot.localEulerAngles.x < 0){

                        rotateSlots[currentSlot - 1].currentRotation.x = rotateSlots[currentSlot - 1].pivot.localEulerAngles.x - 360;

                        if(rotateSlots[currentSlot - 1].currentRotation.x == -360 | rotateSlots[currentSlot - 1].currentRotation.x == 360){

                            rotateSlots[currentSlot - 1].currentRotation.x = 0;

                        }//currentRotation.x == -360 or 360

                    }//pivot.eulerAngles.x > 0 or < 0

                    if(rotateSlots[currentSlot - 1].pivot.localEulerAngles.y > 0 | rotateSlots[currentSlot - 1].pivot.localEulerAngles.y < 0){

                        rotateSlots[currentSlot - 1].currentRotation.y = rotateSlots[currentSlot - 1].pivot.localEulerAngles.y - 360;

                        if(rotateSlots[currentSlot - 1].currentRotation.y == -360 | rotateSlots[currentSlot - 1].currentRotation.y == 360){

                            rotateSlots[currentSlot - 1].currentRotation.y = 0;

                        }//currentRotation.y == -360 or 360

                    }//pivot.eulerAngles.y > 0 or < 0

                    if(rotateSlots[currentSlot - 1].pivot.localEulerAngles.z > 0 | rotateSlots[currentSlot - 1].pivot.localEulerAngles.z < 0){

                        rotateSlots[currentSlot - 1].currentRotation.z = rotateSlots[currentSlot - 1].pivot.localEulerAngles.z - 360;

                        if(rotateSlots[currentSlot - 1].currentRotation.z == -360 | rotateSlots[currentSlot - 1].currentRotation.z == 360){

                            rotateSlots[currentSlot - 1].currentRotation.z = 0;

                        }//currentRotation.z == -360 or 360

                    }//pivot.eulerAngles.z > 0 or < 0

                }//rotateType = local

                tempVects[currentSlot - 1] = rotateSlots[currentSlot - 1].currentRotation;

                if(rotateSlots[currentSlot - 1].currentRotation == rotateSlots[currentSlot - 1].activateRotation){

                    rotateSlots[currentSlot - 1].active = true;

                    rotateSlots[currentSlot - 1].events.onActivate.Invoke();

                //currentRotation = activateRotation
                } else {

                    rotateSlots[currentSlot - 1].active = false;

                }//currentRotation = activateRotation

                for(int i = 0; i < rotateSlots.Count; i++) {

                    if(rotateSlots[i].active){

                        activeCount += 1;

                    }//active

                }//rotateSlots

                if(!requireItem){

                    if(useInteractSound){

                        PlaySound(interactSound);

                    }//useInteractSound

                    if(completeType == Complete_Type.Auto){

                        CompleteCheck();

                    }//completeType = auto

                }//!requireItem

            }//currentSlot != 0

        }//Rotate_Check

        public void Rotate_Reset(){

            if(rotateSlots.Count > 0){

                for(int i = 0; i < rotateSlots.Count; i++) {

                    if(rotateSlots[i].useStartRotation){

                        if(rotateType == Rotate_Type.Global){

                            rotateSlots[i].pivot.eulerAngles = new Vector3(rotateSlots[i].startRotation.x, rotateSlots[i].startRotation.y, rotateSlots[i].startRotation.z);

                        }//rotateType = global

                        if(rotateType == Rotate_Type.Local){

                            rotateSlots[i].pivot.localEulerAngles = new Vector3(rotateSlots[i].startRotation.x, rotateSlots[i].startRotation.y, rotateSlots[i].startRotation.z);

                        }//rotateType = local

                        if(rotateSlots[i].startRotation.x > 0 | rotateSlots[i].startRotation.x < 0){

                            rotateSlots[i].currentRotation.x = rotateSlots[i].pivot.eulerAngles.x - 360;

                        }//startRotation.x > 0 or < 0

                        if(rotateSlots[i].startRotation.y > 0 | rotateSlots[i].startRotation.y < 0){

                            rotateSlots[i].currentRotation.y = rotateSlots[i].pivot.eulerAngles.y - 360;

                        }//startRotation.y > 0 or < 0

                        if(rotateSlots[i].startRotation.z > 0 | rotateSlots[i].startRotation.z < 0){

                            rotateSlots[i].currentRotation.z = rotateSlots[i].pivot.eulerAngles.z - 360;

                        }//startRotation.z > 0 or < 0

                    }//useStartRotation

                    rotateSlots[i].events.onReset.Invoke();

                    rotateSlots[i].active = false;

                }//for i rotateSlots

                activeCount = 0;

            }//rotateSlots.Count > 0

        }//Rotate_Reset


    //////////////////////////////////////
    ///
    ///     ROTATE ADVANCED ACTIONS
    ///
    ///////////////////////////////////////


        public void RotateAdvanced_Check(){

            Complete_State(false);
            tempRot = 0;
            activeCount = 0;
            modulesActive = 0;

            if(moduleSlot != 0){

                if(rotateModules[moduleSlot - 1].moduleType != Module_Type.Switcher){

                    for(int rm = 0; rm < rotateModules.Count; rm++) {

                        for(int rmrs = 0; rmrs < rotateModules[rm].rotateSlots.Count; rmrs++) {

                            if(rotateModules[rm].rotateSlots[rmrs].curModule == moduleSlot){

                                rotateModules[rm].rotateSlots[rmrs].curPosition += 1;

                                if(rotateModules[rm].rotateSlots[rmrs].curPosition > rotateModules[rm].rotateSlots.Count){

                                    rotateModules[rm].rotateSlots[rmrs].curPosition = 1;

                                }//curPosition > rotateSlots.Count

                                rotateModulesTemp[rm].rotateSlots[rmrs].curPosition = rotateModules[rm].rotateSlots[rmrs].curPosition;

                            }//curModule = moduleSlot

                        }//for rmrs rotateSlots

                    }//for rm rotateModules

                    if(rotateModules[moduleSlot - 1].rotateType == Rotate_Type.Global){

                        tempRot = rotateModules[moduleSlot - 1].pivot.eulerAngles.z - 360 + rotateModules[moduleSlot - 1].rotateAmount;

                        rotateModules[moduleSlot - 1].pivot.eulerAngles = new Vector3(0, 0, tempRot);

                        if(rotateModules[moduleSlot - 1].pivot.eulerAngles.x > 0 | rotateModules[moduleSlot - 1].pivot.eulerAngles.x < 0){

                            rotateModules[moduleSlot - 1].currentRotation.x = rotateModules[moduleSlot - 1].pivot.eulerAngles.x - 360;

                            if(rotateModules[moduleSlot - 1].currentRotation.x == -360 | rotateModules[moduleSlot - 1].currentRotation.x == 360){

                                rotateModules[moduleSlot - 1].currentRotation.x = 0;

                            }//currentRotation.x == -360 or 360

                        }//pivot.eulerAngles.x > 0 or < 0

                        if(rotateModules[moduleSlot - 1].pivot.eulerAngles.y > 0 | rotateModules[moduleSlot - 1].pivot.eulerAngles.y < 0){

                            rotateModules[moduleSlot - 1].currentRotation.y = rotateModules[moduleSlot - 1].pivot.eulerAngles.y - 360;

                            if(rotateModules[moduleSlot - 1].currentRotation.y == -360 | rotateModules[moduleSlot - 1].currentRotation.y == 360){

                                rotateModules[moduleSlot - 1].currentRotation.y = 0;

                            }//currentRotation.y == -360 or 360

                        }//pivot.eulerAngles.y > 0 or < 0

                        if(rotateModules[moduleSlot - 1].pivot.eulerAngles.z > 0 | rotateModules[moduleSlot - 1].pivot.eulerAngles.z < 0){

                            rotateModules[moduleSlot - 1].currentRotation.z = rotateModules[moduleSlot - 1].pivot.eulerAngles.z - 360;

                            if(rotateModules[moduleSlot - 1].currentRotation.z == -360 | rotateModules[moduleSlot - 1].currentRotation.z == 360){

                                rotateModules[moduleSlot - 1].currentRotation.z = 0;

                            }//currentRotation.z == -360 or 360

                        }//pivot.eulerAngles.z > 0 or < 0    

                    }//rotateType = global

                    if(rotateModules[moduleSlot - 1].rotateType == Rotate_Type.Local){

                        tempRot = rotateModules[moduleSlot - 1].pivot.localEulerAngles.z - 360 + rotateModules[moduleSlot - 1].rotateAmount;

                        rotateModules[moduleSlot - 1].pivot.localEulerAngles = new Vector3(0, 0, tempRot);

                        if(rotateModules[moduleSlot - 1].pivot.localEulerAngles.x > 0 | rotateModules[moduleSlot - 1].pivot.localEulerAngles.x < 0){

                            rotateModules[moduleSlot - 1].currentRotation.x = rotateModules[moduleSlot - 1].pivot.localEulerAngles.x - 360;

                            if(rotateModules[moduleSlot - 1].currentRotation.x == -360 | rotateModules[moduleSlot - 1].currentRotation.x == 360){

                                rotateModules[moduleSlot - 1].currentRotation.x = 0;

                            }//currentRotation.x == -360 or 360

                        }//pivot.localEulerAngles.x > 0 or < 0

                        if(rotateModules[moduleSlot - 1].pivot.localEulerAngles.y > 0 | rotateModules[moduleSlot - 1].pivot.localEulerAngles.y < 0){

                            rotateModules[moduleSlot - 1].currentRotation.y = rotateModules[moduleSlot - 1].pivot.localEulerAngles.y - 360;

                            if(rotateModules[moduleSlot - 1].currentRotation.y == -360 | rotateModules[moduleSlot - 1].currentRotation.y == 360){

                                rotateModules[moduleSlot - 1].currentRotation.y = 0;

                            }//currentRotation.y == -360 or 360

                        }//pivot.localEulerAngles.y > 0 or < 0

                        if(rotateModules[moduleSlot - 1].pivot.localEulerAngles.z > 0 | rotateModules[moduleSlot - 1].pivot.localEulerAngles.z < 0){

                            rotateModules[moduleSlot - 1].currentRotation.z = rotateModules[moduleSlot - 1].pivot.localEulerAngles.z - 360;

                            if(rotateModules[moduleSlot - 1].currentRotation.z == -360 | rotateModules[moduleSlot - 1].currentRotation.z == 360){

                                rotateModules[moduleSlot - 1].currentRotation.z = 0;

                            }//currentRotation.z == -360 or 360

                        }//pivot.localEulerAngles.z > 0 or < 0

                    }//rotateType = local

                    rotateModulesTemp[moduleSlot - 1].currentRotation = rotateModules[moduleSlot - 1].currentRotation;

                //moduleType != switcher
                } else {

                    for(int rm = 0; rm < rotateModules.Count; rm++) {

                        if(rotateModules[rm].moduleType != Module_Type.Switcher){

                            for(int i2 = 0; i2 < rotateModules[rm].rotateSlots.Count; i2++) {

                                if(rotateModules[rm].rotateSlots[i2].curPosition == 1){

                                    rotateModules[rm].rotateSlots[i2].holder.parent = rotateModules[moduleSlot - 1].parent;

                                }//curPosition = 1

                            }//for i2 rotateSlots

                        }//moduleType != switcher

                    }//for i rotateModules

                    if(rotateModules[moduleSlot - 1].rotateType == Rotate_Type.Global){

                        tempRot = rotateModules[moduleSlot - 1].pivot.eulerAngles.z - 360 + rotateModules[moduleSlot - 1].rotateAmount;

                        rotateModules[moduleSlot - 1].pivot.eulerAngles = new Vector3(0, 0, tempRot);

                    }//rotateType = global

                    if(rotateModules[moduleSlot - 1].rotateType == Rotate_Type.Local){

                        tempRot = rotateModules[moduleSlot - 1].pivot.localEulerAngles.z - 360 + rotateModules[moduleSlot - 1].rotateAmount;

                        rotateModules[moduleSlot - 1].pivot.localEulerAngles = new Vector3(0, 0, tempRot);

                    }//rotateType = local

                    for(int rm2 = 0; rm2 < rotateModules.Count; rm2++) {

                        if(rotateModules[rm2].moduleType != Module_Type.Switcher){

                            for(int rmrs2 = 0; rmrs2 < rotateModules[rm2].rotateSlots.Count; rmrs2++) {

                                if(rotateModules[rm2].rotateSlots[rmrs2].curPosition == 1){

                                    rotateModules[rm2].rotateSlots[rmrs2].curModule += 1;

                                    if(rotateModules[rm2].rotateSlots[rmrs2].curModule >= rotateModules.Count){

                                        rotateModules[rm2].rotateSlots[rmrs2].curModule = 1;

                                    }//curPosition > rotateSlots.Count

                                    rotateModules[rm2].rotateSlots[rmrs2].holder.parent = rotateModules[rotateModules[rm2].rotateSlots[rmrs2].curModule - 1].rotateSlots[rotateModules[rm2].rotateSlots[rmrs2].curPosition - 1].parent;

                                    rotateModulesTemp[rm2].rotateSlots[rmrs2].curModule = rotateModules[rm2].rotateSlots[rmrs2].curModule;

                                }//curPosition = 1

                            }//for i2 rotateSlots

                        }//moduleType != switcher

                    }//for i rotateModules

                }//moduleType != switcher

                for(int rm3 = 0; rm3 < rotateModules.Count; rm3++) {

                    activeCount = 0;

                    for(int rmrs3 = 0; rmrs3 < rotateModules[rm3].rotateSlots.Count; rmrs3++) {

                        if(rotateModules[rm3].rotateSlots[rmrs3].slotType != RotateSlot_Type.Empty){

                            if(rotateModules[rm3].rotateSlots[rmrs3].curModule == rotateModules[rm3].rotateSlots[rmrs3].correctModule && rotateModules[rm3].rotateSlots[rmrs3].curPosition == rotateModules[rm3].rotateSlots[rmrs3].correctPosition){

                                rotateModules[rm3].rotateSlots[rmrs3].active = true;

                                activeCount += 1;

                                rotateModules[rm3].rotateSlots[rmrs3].events.onActivate.Invoke();

                                if(activeCount == rotateModules[rm3].rotateSlots.Count){

                                    modulesActive += 1;

                                    rotateModules[rm3].active = true;

                                    rotateModules[rm3].events.onActivate.Invoke();

                                //activeCount = rotateSlots.Count
                                } else {

                                    if(rotateModules[rm3].active){

                                        rotateModules[rm3].events.onDeactivate.Invoke();

                                    }//active

                                    rotateModules[rm3].active = false;

                                }//activeCount = rotateSlots.Count

                                rotateModulesTemp[rm3].active = rotateModules[rm3].active;
                                rotateModulesTemp[rm3].rotateSlots[rmrs3].active = rotateModules[rm3].rotateSlots[rmrs3].active;

                            //curModule = correctModule & curPosition = correctPosition
                            } else {

                                if(rotateModules[rm3].rotateSlots[rmrs3].active){

                                    rotateModules[rm3].rotateSlots[rmrs3].events.onDeactivate.Invoke();

                                }//active

                                rotateModules[rm3].rotateSlots[rmrs3].active = false;

                                rotateModulesTemp[rm3].rotateSlots[rmrs3].active = rotateModules[rm3].rotateSlots[rmrs3].active;

                            }//curModule = correctModule & curPosition = correctPosition

                        //slotType != empty
                        } else {

                            activeCount += 1;

                            if(activeCount == rotateModules[rm3].rotateSlots.Count){

                                modulesActive += 1;

                                rotateModules[rm3].active = true;

                                rotateModules[rm3].events.onActivate.Invoke();

                            //activeCount = rotateSlots.Count
                            } else {

                                if(rotateModules[rm3].active){

                                    rotateModules[rm3].events.onDeactivate.Invoke();

                                }//active

                                rotateModules[rm3].active = false;

                            }//activeCount = rotateSlots.Count

                            rotateModulesTemp[rm3].active = rotateModules[rm3].active;
                            rotateModulesTemp[rm3].rotateSlots[rmrs3].active = rotateModules[rm3].rotateSlots[rmrs3].active;

                        }//slotType != empty

                    }//for i2 rotateSlots

                }//for i rotateModules

                if(!requireItem){

                    if(useInteractSound){

                        PlaySound(interactSound);

                    }//useInteractSound

                    if(completeType == Complete_Type.Auto){

                        CompleteCheck();

                    }//completeType = auto

                }//!requireItem

            }//moduleSlot != 0

        }//RotateAdvanced_Check


    //////////////////////////////////////
    ///
    ///     LIGHTS ACTIONS
    ///
    ///////////////////////////////////////


        public void Lights_Check(){

            Complete_State(false);

            if(currentSlot != 0){

                lights[currentSlot - 1].active = lights[currentSlot - 1].interactLight.isPoweredOn;

                tempBools[currentSlot - 1] = lights[currentSlot - 1].active;

                complete = CompareBools(tempBools, lightsActive);

                if(useInteractSound){

                    PlaySound(interactSound);

                }//useInteractSound

                if(complete){

                    if(completeType == Complete_Type.Auto){

                        CompleteCheck();

                    }//completeType = auto

                }//complete

            }//currentSlot != 0

        }//Lights_Check



    //////////////////////////////////////
    ///
    ///     SWITCHES ACTIONS
    ///
    ///////////////////////////////////////


        public void Switches_Check(){

            Complete_State(false);

            if(currentSlot != 0){

                switches[currentSlot - 1].active = switches[currentSlot - 1].dynamObject.isOpened;

                tempBools[currentSlot - 1] = switches[currentSlot - 1].active;

                complete = CompareBools(tempBools, switchesActive);

                if(useInteractSound){

                    PlaySound(interactSound);

                }//useInteractSound

                if(complete){

                    if(completeType == Complete_Type.Auto){

                        CompleteCheck();

                    }//completeType = auto

                }//complete

            }//currentSlot != 0

        }//Switches_Check


    //////////////////////////////////////
    ///
    ///     WAVE ACTIONS
    ///
    ///////////////////////////////////////


        public void Wave_Check(){

            Complete_State(false);

            activeCount = 0;
            wavesActive = 0;
            modulesActive = 0;

            if(moduleSlots[moduleSlot - 1].waveSlots[waveSlot - 1].waveChecks.Count > 0){

                for(int i = 0; i < moduleSlots[moduleSlot - 1].waveSlots[waveSlot - 1].waveChecks.Count; i++) {

                    if(moduleSlots[moduleSlot - 1].waveSlots[waveSlot - 1].waveChecks[i].curValue == moduleSlots[moduleSlot - 1].waveSlots[waveSlot - 1].waveChecks[i].value){

                        moduleSlots[moduleSlot - 1].waveSlots[waveSlot - 1].waveChecks[i].active = true;

                        waveModulesTemp[moduleSlot - 1].waveSlots[waveSlot - 1].waveChecks[i].active = true;

                        activeCount += 1;

                    //curValue = value
                    } else {

                        moduleSlots[moduleSlot - 1].waveSlots[waveSlot - 1].waveChecks[i].active = false;

                        waveModulesTemp[moduleSlot - 1].waveSlots[waveSlot - 1].waveChecks[i].active = false;

                    }//curValue = value

                }//for i waveChecks

            }//waveChecks.Count > 0

            if(useInteractSound){

                PlaySound(interactSound);

            }//useInteractSound

            if(activeCount == moduleSlots[moduleSlot - 1].waveSlots[waveSlot - 1].waveChecks.Count){

                moduleSlots[moduleSlot - 1].waveSlots[waveSlot - 1].active = true;

                waveModulesTemp[moduleSlot - 1].waveSlots[waveSlot - 1].active = true;

            //activeCount = waveChecks.Count
            } else {

                moduleSlots[moduleSlot - 1].waveSlots[waveSlot - 1].active = false;

                waveModulesTemp[moduleSlot - 1].waveSlots[waveSlot - 1].active = false;

            }//activeCount = waveChecks.Count

            for(int i2 = 0; i2 < moduleSlots[moduleSlot - 1].waveSlots.Count; i2++) {

                if(moduleSlots[moduleSlot - 1].waveSlots[i2].active){

                    wavesActive += 1;

                }//active

            }//for i2 waveSlots

            if(wavesActive == moduleSlots[moduleSlot - 1].waveSlots.Count){

                moduleSlots[moduleSlot - 1].active = true;

                waveModulesTemp[moduleSlot - 1].active = true;

            //wavesActive = waveSlots.Count
            } else {

                moduleSlots[moduleSlot - 1].active = false;

                waveModulesTemp[moduleSlot - 1].active = false;

            }//wavesActive = waveSlots.Count

            for(int i3 = 0; i3 < moduleSlots.Count; i3++) {

                if(moduleSlots[i3].active){

                    modulesActive += 1;

                }//active

            }//for i3 moduleSlots

            if(modulesActive == moduleSlots.Count){

                Complete_State(true);

            }//modulesActive = moduleSlots.Count

            if(complete){

                if(completeType == Complete_Type.Auto){

                    CompleteCheck();

                }//completeType = auto

            }//complete

        }//WaveCheck

        public void Modules_Update(){

            for(int i = 0; i < moduleSlots.Count; i++) {

                moduleSlots[i].holder.SetActive(false);

            }//for i moduleSlots

            moduleSlots[moduleSlot - 1].holder.SetActive(true);

        }//Modules_Update

        public void WaveCheck_Update(float value){

            moduleSlots[moduleSlot - 1].waveSlots[waveSlot - 1].waveChecks[waveCheckSlot - 1].curValue = value;

            waveModulesTemp[moduleSlot - 1].waveSlots[waveSlot - 1].waveChecks[waveCheckSlot - 1].curValue = value;

            if(moduleSlots[moduleSlot - 1].waveSlots[waveSlot - 1].wave != null){

                if(moduleSlots[moduleSlot - 1].waveSlots[waveSlot - 1].waveChecks[waveCheckSlot - 1].type == Wave_Type.Amplitude){

                    moduleSlots[moduleSlot - 1].waveSlots[waveSlot - 1].wave.Amplitude_Set(value);

                }//type = amplitude

                if(moduleSlots[moduleSlot - 1].waveSlots[waveSlot - 1].waveChecks[waveCheckSlot - 1].type == Wave_Type.WaveLength){

                    moduleSlots[moduleSlot - 1].waveSlots[waveSlot - 1].wave.WaveLength_Set(value);

                }//type = wave length

                if(moduleSlots[moduleSlot - 1].waveSlots[waveSlot - 1].waveChecks[waveCheckSlot - 1].type == Wave_Type.WaveSpeed){

                    moduleSlots[moduleSlot - 1].waveSlots[waveSlot - 1].wave.WaveSpeed_Set(value);

                }//type = wave speed

            }//wave != null

        }//WaveCheck_Update


    //////////////////////////////////////
    ///
    ///     WEIGHT ACTIONS
    ///
    ///////////////////////////////////////


        public void Weight_Check(int ID){

            if(moduleSlot != 0){

                modulesActive = 0;

                int tempSlot = WeightSlot_Check() + 1;

                for(int i = 0; i < weightItems.Count; ++i ) {

                    if(weightItems[i].itemID == ID){

                        GameObject newItem = Instantiate(weightItems[i].prefab, weightModules[moduleSlot - 1].weightSlots[tempSlot - 1].holder);

                        weightModulesTemp[moduleSlot - 1].weightSlots[tempSlot - 1] = ID;

                        if(newItem.GetComponent<Puzzler_Holder>() != null){

                            newItem.GetComponent<Puzzler_Holder>().puzzlerHand = this;
                            newItem.GetComponent<Puzzler_Holder>().slot = moduleSlot;
                            newItem.GetComponent<Puzzler_Holder>().secondSlot = tempSlot;
                            newItem.GetComponent<Puzzler_Holder>().weight = weightItems[i].weight;

                            puzzlerHolders.Add(newItem.GetComponent<Puzzler_Holder>());

                        }//Puzzler_Holder != null

                        weightModules[moduleSlot - 1].curWeight += weightItems[i].weight;

                        weightModules[moduleSlot - 1].weightSlots[tempSlot - 1].filled = true;
                        weightModules[moduleSlot - 1].weightSlots[tempSlot - 1].events.onUse.Invoke();

                    }//itemID = ID

                }//for i weightItems

                if(weightModules[moduleSlot - 1].curWeight > weightModules[moduleSlot - 1].maxWeight){

                    weightModules[moduleSlot - 1].curWeight = weightModules[moduleSlot - 1].maxWeight;

                }//curWeight > maxWeight

                for(int i = 0; i < weightModules.Count; i++) {

                    if(weightModules[i].curWeight == weightModules[i].correctWeight){

                        weightModules[i].active = true;

                    //curWeight = correctWeight
                    } else {

                        weightModules[i].active = false;

                    }//curWeight = correctWeight

                    if(weightModules[i].active){

                        modulesActive += 1;

                    }//active

                }//for i weightModules

            }//moduleSlot != 0

        }//Weight_Check

        public void Weight_Check(){

            if(moduleSlot != 0){

                modulesActive = 0;

                for(int i = 0; i < weightModules.Count; i++) {

                    if(weightModules[i].curWeight > weightModules[i].maxWeight){

                        weightModules[i].curWeight = weightModules[i].maxWeight;

                    }//curWeight > maxWeight

                    if(weightModules[i].curWeight == weightModules[i].correctWeight){

                        weightModules[i].active = true;

                    //curWeight = correctWeight
                    } else {

                        weightModules[i].active = false;

                    }//curWeight = correctWeight

                    if(weightModules[i].active){

                        modulesActive += 1;

                    }//active

                }//for i weightModules

            }//moduleSlot != 0

        }//Weight_Check


    ////////////////////////////
    ///
    ///     WEIGHT RETURNS
    ///
    ////////////////////////////


        public int WeightSlot_Check(){

            for(int ws = 0; ws < weightModules[moduleSlot - 1].weightSlots.Count; ++ws) {

                if(!weightModules[moduleSlot - 1].weightSlots[ws].filled){

                    return ws;

                }//!filled

            }//for wm weightSlots

            return -1;

        }//WeightSlot_Check

        public float Weight_Catch(){

            return weightModules[moduleSlot - 1].curWeight;

        }//Weight_Catch

        public float TotalWeight_Catch(){

            float tempWeight = 0;

            for(int i = 0; i < weightModules.Count; ++i) {

                tempWeight += weightModules[i].curWeight;

            }//for i weightModules

            return tempWeight;

        }//TotalWeight_Catch


    //////////////////////////////////////
    ///
    ///     SLOT ACTIONS
    ///
    ///////////////////////////////////////

    ////////////////////////////
    ///
    ///     BASIC
    ///
    ////////////////////////////


        public void Slot_Set(int slot){

            currentSlot = slot;

        }//Slot_Set

        public void Slot_Clear(int slot){

            currentSlot = 0;

        }//Slot_Clear


    ////////////////////////////
    ///
    ///     MODULE
    ///
    ////////////////////////////


        public void ModuleSlot_Set(int slot){

            moduleSlot = slot;

        }//ModuleSlot_Set

        public void ModuleSlot_Clear(int slot){

            moduleSlot = 0;

        }//ModuleSlot_Clear


    ////////////////////////////
    ///
    ///     WAVE
    ///
    ////////////////////////////


        public void WaveSlot_Set(int slot){

            waveSlot = slot;

        }//WaveSlot_Set

        public void WaveSlot_Clear(int slot){

            waveSlot = 0;

        }//WaveSlot_Clear


    ////////////////////////////
    ///
    ///     WAVE CHECK
    ///
    ////////////////////////////


        public void WaveCheckSlot_Set(int slot){

            waveCheckSlot = slot;

        }//WaveCheckSlot_Set

        public void WaveCheckSlot_Clear(int slot){

            waveCheckSlot = 0;

        }//WaveCheckSlot_Clear


    //////////////////////////////////////
    ///
    ///     OBJECTIVES ACTIONS
    ///
    ///////////////////////////////////////


        private void Objective_Update(int objectiveID, bool sound = true) {

            #if PUZZLER_PRESENT

                if(!objectiveManager.CheckObjective(objectiveID)) {

                    ObjectiveModel objModel = objectiveManager.activeObjectives.FirstOrDefault(o => o.identifier == objectiveID);

                    if(!objModel.isCompleted) {

                        string text = objModel.objectiveText;

                        if(text.Contains("{") && text.Contains("}")) {

                            text = string.Format(text, objModel.completion, objModel.toComplete);

                        }//Contains

                        objectiveManager.PushObjectiveText(text, objectiveManager.CompleteTime);

                        if(sound) { 

                            objectiveManager.PlaySound(objectiveManager.newObjective); 

                        }//sound

                    }//!isCompleted

                }//!CheckObjective

            #else 

                Debug.Log("Puzzler is not active!");

            #endif

        }//Objective_Update

        private IEnumerator ObjectiveUpdate_Buff(){

            yield return new WaitForSeconds(objectiveUpdateWait);

            Objective_Update(objectiveID);

        }//ObjectiveUpdate_Buff


    //////////////////////////////////////
    ///
    ///     HOLDERS ACTIONS
    ///
    ///////////////////////////////////////


        public void Holders_ActiveState(bool state){

            if(puzzlerHolders.Count > 0){

                for(int i = 0; i < puzzlerHolders.Count; ++i ) {

                    puzzlerHolders[i].ActiveState(state);

                }//for i puzzlerHolders

            }//puzzlerHolders.Count > 0

        }//Holders_ActiveState

        public void Holders_ActiveStateCheck(bool state){

            if(!complete){

                if(puzzlerHolders.Count > 0){

                    for(int i = 0; i < puzzlerHolders.Count; ++i ) {

                        puzzlerHolders[i].ActiveState(state);

                    }//for i puzzlerHolders

                }//puzzlerHolders.Count > 0

            //!complete
            } else {

                if(puzzlerHolders.Count > 0){

                    for(int i = 0; i < puzzlerHolders.Count; ++i ) {

                        puzzlerHolders[i].ActiveState(false);

                    }//for i puzzlerHolders

                }//puzzlerHolders.Count > 0

            }//!complete

        }//Holders_ActiveStateCheck


    //////////////////////////////////////
    ///
    ///     COMPLETE ACTIONS
    ///
    ///////////////////////////////////////


        public void CompleteCheck(){

            if(puzzleType == Puzzle_Type.SoloItem){

                if(soloItemSlotType == Slot_Type.Normal){

                    if(soloSlots.Count > 0){

                        if(itemCount == soloSlots.Count){

                            Complete_State(true);

                        }//itemCount = soloSlots.Count

                    }//soloSlots.Count > 0

                }//soloItemSlotType = normal

                if(soloItemSlotType == Slot_Type.NoPrefab){

                    if(itemCount == soloItemCount){

                        Complete_State(true);

                    }//itemCount == soloItemCount

                }//soloItemSlotType = no prefab

            }//puzzleType = solo

            if(puzzleType == Puzzle_Type.MultiItems){

                if(activeCount == multiSlots.Count){

                    Complete_State(true);

                }//activeCount = multiSlots.Count

            }//puzzleType = multi

            if(puzzleType == Puzzle_Type.Rotate){

                if(activeCount == rotateSlots.Count){

                    Complete_State(true);

                }//activeCount = rotateSlots.Count

            }//puzzleType = rotate

            if(puzzleType == Puzzle_Type.RotateAdvanced){

                if(modulesActive == modulesCount){

                    Complete_State(true);

                }//modulesActive = modulesCount

            }//puzzleType = rotate advanced

            if(puzzleType == Puzzle_Type.Weight){

                if(modulesActive == weightModules.Count){

                    Complete_State(true);

                }//modulesActive = weightModules.Count

            }//puzzleType = weight

            if(complete){

                if(lockType == Lock_Type.OnComplete){

                    Holders_ActiveState(false);

                }//lockType = OnComplete

                Locked_State(true);

                if(linkComplete){

                    if(handlers.Count > 0){

                        for(int i = 0; i < handlers.Count; ++i ) {

                            handlers[i].Complete_State(true);
                            handlers[i].Locked_State(true);

                        }//handlers

                    }//handlers.Count > 0

                }//linkComplete

                if(useCompleteDelay){

                    CompleteCheck_Delayed();

                //useCompleteDelay
                } else {

                    onPuzzleComplete.Invoke();

                }//useCompleteDelay

                if(useDelayedEvent){

                    Complete_Delayed();

                }//useDelayedEvent

            //complete
            } else {

                if(showObjectiveUpdate){

                    if(objectiveUpdateDelay){

                        StartCoroutine("ObjectiveUpdate_Buff");

                    //objectiveUpdateDelay
                    } else {

                        Objective_Update(objectiveID);

                    }//objectiveUpdateDelay

                }//showObjectiveUpdate

                if(puzzleType == Puzzle_Type.Sequential){

                    if(resetType == Reset_Type.OnFail){

                        if(tempInts.Count > 1){

                            for(int i = 0; i < tempInts.Count; ++i ) {

                                if(tempInts[i] != sequenceOrder[i]){

                                    fail = true;

                                }//tempInts[i] != sequenceOrder[i]

                            }//for i tempInts

                            if(fail){

                                for(int i2 = 0; i2 < sequence.Count; ++i2 ) {

                                    if(sequence[i2].active){

                                        sequence[i2].events.onReset.Invoke();

                                        sequence[i2].active = false;

                                    }//active

                                }//for i2 sequence

                                tempInts = new List<int>();

                                onPuzzleFail.Invoke();

                            }//fail

                        }//tempInts.Count > 1

                    }//resetType = on fail

                    if(resetType == Reset_Type.OnFinish){

                        if(tempInts.Count == sequenceOrder.Count){

                            for(int i = 0; i < sequence.Count; ++i ) {

                                sequence[i].events.onReset.Invoke();

                                sequence[i].active = false;

                            }//for i sequence

                            tempInts = new List<int>();

                            onPuzzleFail.Invoke();

                        //tempInts.Count = sequenceOrder.Count
                        } else {

                            if(showObjectiveUpdate){

                                if(objectiveUpdateDelay){

                                    StartCoroutine("ObjectiveUpdate_Buff");

                                //objectiveUpdateDelay
                                } else {

                                    Objective_Update(objectiveID);

                                }//objectiveUpdateDelay

                            }//showObjectiveUpdate

                        }//tempInts.Count = sequenceOrder.Count

                    }//resetType = on finish

                }//puzzleType = sequential

                if(puzzleType == Puzzle_Type.Rotate && completeType == Complete_Type.Manual){

                    onPuzzleFail.Invoke();

                }//puzzleType = rotate & completeType = manual

            }//complete

        }//CompleteCheck

        public void CompleteCheck_Delayed(){

            StartCoroutine("CompleteCheck_Delay");

        }//CompleteCheck_Delayed

        private IEnumerator CompleteCheck_Delay(){

            yield return new WaitForSeconds(completeDelay);

            onPuzzleComplete.Invoke();

        }//CompleteCheck_Delay

        public void Complete_Delayed(){

            StartCoroutine("CompleteDelayed_Buff");

        }//Complete_Delayed

        private IEnumerator CompleteDelayed_Buff(){

            yield return new WaitForSeconds(delayEventWait);

            onPuzzleCompleteDelayed.Invoke();

        }//CompleteDelayed_Buff

        public void Complete_State(bool state){

            complete = state;

        }//Complete_State

        public bool Complete_Get(){

            return complete;

        }//Complete_Get


    //////////////////////////////////////
    ///
    ///     SOUND ACTIONS
    ///
    ///////////////////////////////////////


        public void PlaySound(string name){

            if(useSounds){

                float tempPitch = soundSource.pitch;

                for(int sl = 0; sl < soundLibrary.Length; sl++) {

                    if(soundLibrary[sl].name == name){

                        if(soundLibrary[sl].useCustomVolume | soundLibrary[sl].useCustomPitch){

                            if(soundLibrary[sl].useCustomVolume && !soundLibrary[sl].useCustomPitch){

                                soundSource.PlayOneShot(soundLibrary[sl].sound, soundLibrary[sl].customVolume);

                            }//useCustomVolume & !useCustomPitch

                            if(!soundLibrary[sl].useCustomVolume && soundLibrary[sl].useCustomPitch){

                                soundSource.pitch = soundLibrary[sl].customPitch;
                                soundSource.PlayOneShot(soundLibrary[sl].sound);

                            }//!useCustomVolume & useCustomPitch

                            if(soundLibrary[sl].useCustomVolume && soundLibrary[sl].useCustomPitch){

                                soundSource.pitch = soundLibrary[sl].customPitch;
                                soundSource.PlayOneShot(soundLibrary[sl].sound, soundLibrary[sl].customVolume);

                            }//useCustomVolume & useCustomPitch

                        //useCustomVolume or useCustomPitch
                        } else {

                            soundSource.PlayOneShot(soundLibrary[sl].sound);

                        }//useCustomVolume or useCustomPitch

                    }//name = name

                }//for sl soundLibrary

                if(soundSource.pitch != tempPitch){

                    soundSource.pitch = tempPitch;

                }//pitch != tempPitch

            }//useSounds

        }//PlaySound (string)

        public void PlaySound(int slot){

            if(useSounds){

                float tempPitch = soundSource.pitch;

                if(soundLibrary[slot - 1].useCustomVolume | soundLibrary[slot - 1].useCustomPitch){

                    if(soundLibrary[slot - 1].useCustomVolume && !soundLibrary[slot - 1].useCustomPitch){

                        soundSource.PlayOneShot(soundLibrary[slot - 1].sound, soundLibrary[slot - 1].customVolume);

                    }//useCustomVolume & !useCustomPitch

                    if(!soundLibrary[slot - 1].useCustomVolume && soundLibrary[slot - 1].useCustomPitch){

                        soundSource.pitch = soundLibrary[slot - 1].customPitch;
                        soundSource.PlayOneShot(soundLibrary[slot - 1].sound, soundSource.volume);

                    }//!useCustomVolume & useCustomPitch

                    if(soundLibrary[slot - 1].useCustomVolume && soundLibrary[slot - 1].useCustomPitch){

                        soundSource.pitch = soundLibrary[slot - 1].customPitch;
                        soundSource.PlayOneShot(soundLibrary[slot - 1].sound, soundLibrary[slot - 1].customVolume);

                    }//useCustomVolume & useCustomPitch

                //useCustomVolume or useCustomPitch
                } else {

                    soundSource.PlayOneShot(soundLibrary[slot - 1].sound);

                }//useCustomVolume or useCustomPitch

                if(soundSource.pitch != tempPitch){

                    soundSource.pitch = tempPitch;

                }//pitch != tempPitch

            }//useSounds

        }//PlaySound (int)


    //////////////////////////////////////
    ///
    ///     LOCK ACTIONS
    ///
    ///////////////////////////////////////


        public void Locked_State(bool state){

            locked = state;

        }//Locked_State    


    //////////////////////////////////////
    ///
    ///     SAVE/LOAD ACTIONS
    ///
    ///////////////////////////////////////

    ////////////////////////////
    ///
    ///     SAVE ACTIONS
    ///
    ////////////////////////////


        public Dictionary<string, object> OnSave() {

            return new Dictionary<string, object> {

                {"moduleSlot", moduleSlot},
                {"tempSoloItemCount", tempSoloItemCount},
                {"tempInts", tempInts},
                {"tempBools", tempBools},
                {"tempVects", tempVects},
                {"rotateModulesTemp", rotateModulesTemp},
                {"waveModulesTemp", waveModulesTemp},
                {"weightModulesTemp", weightModulesTemp},
                {"complete", complete},
                {"locked", locked }

            };//Dictionary

        }//OnSave


    ////////////////////////////
    ///
    ///     LOAD ACTIONS
    ///
    ////////////////////////////


        public void OnLoad(JToken token) {

            moduleSlot = (int)token["moduleSlot"];

            tempSoloItemCount = (int)token["tempSoloItemCount"];
            tempInts = token["tempInts"].ToObject<List<int>>();
            tempBools = token["tempBools"].ToObject<List<bool>>();
            tempVects = token["tempVects"].ToObject<List<Vector3>>();

            rotateModulesTemp = token["rotateModulesTemp"].ToObject<List<RotateModule_Temp>>();
            waveModulesTemp = token["waveModulesTemp"].ToObject<List<WaveModule_Temp>>();
            weightModulesTemp = token["weightModulesTemp"].ToObject<List<WeightModule_Temp>>();

            complete = (bool)token["complete"];
            locked = (bool)token["locked"];

            if(complete) {

                if(puzzleType == Puzzle_Type.SoloItem){

                    if(soloItemSlotType == Slot_Type.Normal){

                        if(soloSlots.Count > 0){

                            for(int i = 0; i < soloSlots.Count; ++i ) {

                                soloSlots[i].active = true;

                                if(soloPrefab != null){

                                    GameObject newItem = Instantiate(soloPrefab, soloSlots[i].holder);

                                }//soloPrefab != null

                            }//for i soloSlots

                        }//soloSlots.Count > 0

                    }//soloItemSlotType = normal

                }//puzzleType = solo item

                if(puzzleType == Puzzle_Type.MultiItems){

                    for(int i = 0; i < multiSlots.Count; ++i ) {

                        if(multiSlots[i].slotCheck == Slot_Check.Item){

                            multiSlots[i].filled = true;

                        }//slotCheck = item

                        multiSlots[i].active = true;

                        for(int mp = 0; mp < multiPrefabs.Count; ++mp ) {

                            if(multiSlots[i].slotCheck == Slot_Check.Item){

                                if(multiPrefabs[mp].itemID == multiSlots[i].itemID){

                                    GameObject newItem = Instantiate(multiPrefabs[mp].prefab, multiSlots[i].holder);
                                    puzzlerHolders.Add(newItem.GetComponent<Puzzler_Holder>());

                                }//itemID = itemID

                            }//slotCheck = item

                        }//for mp multiPrefabs

                    }//for i multiSlots

                }//puzzleType = multi item

                if(puzzleType == Puzzle_Type.Sequential){

                    for(int i = 0; i < sequence.Count; ++i ) {

                        sequence[i].active = true;

                    }//for i sequence

                }//puzzleType = sequential

                if(puzzleType == Puzzle_Type.Rotate){

                    StartCoroutine("CompleteLoad_Buff", Puzzle_Type.Rotate);

                }//puzzleType = rotate

                if(puzzleType == Puzzle_Type.RotateAdvanced){

                    StartCoroutine("CompleteLoad_Buff", Puzzle_Type.RotateAdvanced);

                }//puzzleType = rotate

                if(puzzleType == Puzzle_Type.Lights){

                    for(int i = 0; i < lights.Count; ++i ) {

                        lights[i].active = lightsActive[i];

                    }//for i lights

                }//puzzleType = lights

                if(puzzleType == Puzzle_Type.Switches){

                    for(int i = 0; i < switches.Count; ++i ) {

                        switches[i].active = switchesActive[i];

                    }//for i switches

                }//puzzleType = switches

                if(puzzleType == Puzzle_Type.Wave){

                    StartCoroutine("CompleteLoad_Buff", Puzzle_Type.Wave);

                }//puzzleType = wave

                if(puzzleType == Puzzle_Type.Weight){

                    for(int i = 0; i < weightModules.Count; ++i ) {

                        weightModules[i].curWeight = weightModules[i].correctWeight;
                        weightModules[i].active = true;

                        for(int ws = 0; ws < weightModules[i].weightSlots.Count; ++ws ) {

                            if(weightModulesTemp[i].weightSlots[ws] > 0){

                                for(int wi = 0; wi < weightItems.Count; ++wi ) {

                                    if(weightItems[wi].itemID == weightModulesTemp[i].weightSlots[ws]){

                                        GameObject newItem = Instantiate(weightItems[wi].prefab, weightModules[i].weightSlots[ws].holder);
                                        puzzlerHolders.Add(newItem.GetComponent<Puzzler_Holder>());

                                        weightModules[i].weightSlots[ws].filled = true;

                                    }//itemID = weightSlots[ws]

                                }//for wi weightItems

                            }//weightSlots[ws] > 0

                        }//for ws weightSlots

                    }//for i weightModules

                }//puzzleType = weight

                Holders_ActiveState(false);

                onCompleteLoad.Invoke();

            //complete
            } else {

                if(saveState == SaveState.Active){

                    if(puzzleType == Puzzle_Type.SoloItem){

                        if(soloItemSlotType == Slot_Type.Normal){

                            if(tempBools.Count > 0){

                                for(int i = 0; i < tempBools.Count; ++i ) {

                                    soloSlots[i].active = tempBools[i];

                                    if(tempBools[i]){

                                        itemCount += 1;

                                        GameObject newItem = Instantiate(soloPrefab, soloSlots[i].holder);

                                        soloSlots[i].events.onActivateLoad.Invoke();

                                    }//tempBools[i] = true

                                }//for i tempBools

                            }//tempBools.Count > 0

                        }//soloItemSlotType = normal

                        if(soloItemSlotType == Slot_Type.NoPrefab){

                            itemCount = tempSoloItemCount;

                        }//soloItemSlotType = no prefab

                    }//puzzleType = solo item

                    if(puzzleType == Puzzle_Type.MultiItems){

                        for(int i = 0; i < tempInts.Count; ++i ) {

                            if(tempInts[i] > 0){

                                for(int mp = 0; mp < multiPrefabs.Count; ++mp ) {

                                    if(multiPrefabs[mp].itemID == tempInts[i]){

                                        GameObject newItem = Instantiate(multiPrefabs[mp].prefab, multiSlots[i].holder);

                                        newItem.GetComponent<Puzzler_Holder>().puzzlerHand = this;
                                        newItem.GetComponent<Puzzler_Holder>().slot = i + 1;

                                        puzzlerHolders.Add(newItem.GetComponent<Puzzler_Holder>());

                                        multiSlots[i].filled = true;

                                        multiSlots[i].events.onFilledLoad.Invoke();

                                    }//itemID = tempInts[i]

                                }//for mp multiPrefabs

                                if(tempInts[i] == multiSlots[i].itemID){

                                    multiSlots[i].active = true;

                                }//tempInts[i] = itemID

                            }//tempInts[i] > 0

                        }//for i tempInts

                    }//puzzleType = multi items

                    if(puzzleType == Puzzle_Type.Rotate){

                        for(int i = 0; i < rotateSlots.Count; ++i ) {

                            rotateSlots[i].currentRotation = tempVects[i];

                            if(rotateType == Rotate_Type.Global){

                                rotateSlots[i].pivot.eulerAngles = tempVects[i];

                            }//rotateType = global

                            if(rotateType == Rotate_Type.Local){

                                rotateSlots[i].pivot.localEulerAngles = tempVects[i];

                            }//rotateType = local

                            if(rotateSlots[i].currentRotation == rotateSlots[i].activateRotation){

                                rotateSlots[i].active = true;

                            }//currentRotation = activateRotation

                        }//for i rotateSlots

                    }//puzzleType = rotate

                    if(puzzleType == Puzzle_Type.RotateAdvanced){

                        StartCoroutine("NotCompleteLoad_Buff", Puzzle_Type.RotateAdvanced);

                    }//puzzleType = rotate advanced

                    if(puzzleType == Puzzle_Type.Lights){

                        StartCoroutine("NotCompleteLoad_Buff", Puzzle_Type.Lights);

                    }//puzzleType = lights

                    if(puzzleType == Puzzle_Type.Switches){

                        StartCoroutine("NotCompleteLoad_Buff", Puzzle_Type.Switches);

                    }//puzzleType = switches

                    if(puzzleType == Puzzle_Type.Wave){

                        StartCoroutine("NotCompleteLoad_Buff", Puzzle_Type.Wave);

                    }//puzzleType = wave

                    if(puzzleType == Puzzle_Type.Weight){

                        for(int i = 0; i < weightModules.Count; ++i ) {

                            for(int ws = 0; ws < weightModules[i].weightSlots.Count; ++ws ) {

                                if(weightModulesTemp[i].weightSlots[ws] > 0){

                                    for(int wi = 0; wi < weightItems.Count; ++wi ) {

                                        if(weightItems[wi].itemID == weightModulesTemp[i].weightSlots[ws]){

                                            GameObject newItem = Instantiate(weightItems[wi].prefab, weightModules[i].weightSlots[ws].holder);

                                            newItem.GetComponent<Puzzler_Holder>().puzzlerHand = this;
                                            newItem.GetComponent<Puzzler_Holder>().slot = i + 1;
                                            newItem.GetComponent<Puzzler_Holder>().secondSlot = ws + 1;
                                            newItem.GetComponent<Puzzler_Holder>().weight = weightItems[wi].weight;

                                            puzzlerHolders.Add(newItem.GetComponent<Puzzler_Holder>());

                                            weightModules[i].curWeight += weightItems[wi].weight;

                                            weightModules[i].weightSlots[ws].filled = true;

                                            weightModules[i].weightSlots[ws].events.onFilledLoad.Invoke();

                                        }//itemID = weightSlots[ws]

                                    }//for wi weightItems

                                }//weightSlots[ws] > 0

                            }//for ws weightSlots

                        }//for i weightModulesTemp

                    }//puzzleType = weight

                }//saveState = active

            }//complete

        }//OnLoad

        private IEnumerator CompleteLoad_Buff(Puzzle_Type newType){

            yield return new WaitForSeconds(0.2f);

            if(newType == Puzzle_Type.Rotate){

                for(int i = 0; i < rotateSlots.Count; ++i ) {

                    rotateSlots[i].active = true;

                    rotateSlots[i].currentRotation = rotateSlots[i].activateRotation;

                    if(rotateType == Rotate_Type.Global){

                        rotateSlots[i].pivot.eulerAngles = new Vector3(rotateSlots[i].activateRotation.x, rotateSlots[i].activateRotation.y, rotateSlots[i].activateRotation.z);

                    }//rotateType = global

                    if(rotateType == Rotate_Type.Local){

                        rotateSlots[i].pivot.localEulerAngles = new Vector3(rotateSlots[i].activateRotation.x, rotateSlots[i].activateRotation.y, rotateSlots[i].activateRotation.z);

                    }//rotateType = local

                }//for i rotateSlots

            }//newType = rotate

            if(newType == Puzzle_Type.RotateAdvanced){

                for(int i = 0; i < rotateModules.Count; ++i ) {

                    rotateModules[i].active = true;

                    for(int i2 = 0; i2 < rotateModules[i].rotateSlots.Count; i2++) {

                        rotateModules[i].rotateSlots[i2].curModule = rotateModules[i].rotateSlots[i2].correctModule;
                        rotateModules[i].rotateSlots[i2].curPosition = rotateModules[i].rotateSlots[i2].correctPosition;

                        rotateModules[i].rotateSlots[i2].active = true;

                        rotateModules[i].rotateSlots[i2].holder.parent = rotateModules[rotateModules[i].rotateSlots[i2].correctModule - 1].rotateSlots[rotateModules[i].rotateSlots[i2].correctPosition - 1].parent;

                        if(rotateModules[i].rotateType == Rotate_Type.Global){

                            rotateModules[i].rotateSlots[i2].holder.eulerAngles = new Vector3(0, 0, 0);

                        }//rotateType = global

                        if(rotateModules[i].rotateType == Rotate_Type.Local){

                            rotateModules[i].rotateSlots[i2].holder.localEulerAngles = new Vector3(0, 0, 0);

                        }//rotateType = local

                    }//for i2 rotateSlots

                    rotateModules[i].currentRotation = rotateModulesTemp[i].currentRotation;

                    if(rotateModules[i].rotateType == Rotate_Type.Global){

                        rotateModules[i].pivot.eulerAngles = new Vector3(rotateModules[i].currentRotation.x, rotateModules[i].currentRotation.y, rotateModules[i].currentRotation.z);

                    }//rotateType = global

                    if(rotateModules[i].rotateType == Rotate_Type.Local){

                        rotateModules[i].pivot.localEulerAngles = new Vector3(rotateModules[i].currentRotation.x, rotateModules[i].currentRotation.y, rotateModules[i].currentRotation.z);

                    }//rotateType = local

                }//for i rotateModules

            }//newType = rotate advanced

            if(newType == Puzzle_Type.Wave){

                for(int i = 0; i < moduleSlots.Count; ++i ) {

                    moduleSlots[i].active = true;

                    for(int ws = 0; ws < moduleSlots[i].waveSlots.Count; ++ws ) {

                        moduleSlots[i].waveSlots[ws].active = true;

                        for(int wc = 0; wc < moduleSlots[i].waveSlots[ws].waveChecks.Count; ++wc ) {

                            moduleSlots[i].waveSlots[ws].waveChecks[wc].curValue = moduleSlots[i].waveSlots[ws].waveChecks[wc].value;
                            moduleSlots[i].waveSlots[ws].waveChecks[wc].active = true;

                        }//for wc waveChecks

                    }//for ws waveSlots

                }//for i moduleSlots

                Modules_Update();

            }//newType = wave

        }//CompleteLoad_Buff

        private IEnumerator NotCompleteLoad_Buff(Puzzle_Type newType){

            yield return new WaitForSeconds(0.2f);

            if(newType == Puzzle_Type.RotateAdvanced){

                for(int i = 0; i < rotateModules.Count; ++i ) {

                    rotateModules[i].active = rotateModulesTemp[i].active;

                    for(int i2 = 0; i2 < rotateModules[i].rotateSlots.Count; i2++) {

                        rotateModules[i].rotateSlots[i2].curModule = rotateModulesTemp[i].rotateSlots[i2].curModule;
                        rotateModules[i].rotateSlots[i2].curPosition = rotateModulesTemp[i].rotateSlots[i2].curPosition;

                        rotateModules[i].rotateSlots[i2].active = rotateModulesTemp[i].rotateSlots[i2].active;

                        if(rotateModules[i].rotateSlots[i2].holder.parent != rotateModules[rotateModules[i].rotateSlots[i2].curModule - 1].rotateSlots[rotateModules[i].rotateSlots[i2].curPosition - 1].parent){

                            rotateModules[i].rotateSlots[i2].holder.parent = rotateModules[rotateModules[i].rotateSlots[i2].curModule - 1].rotateSlots[rotateModules[i].rotateSlots[i2].curPosition - 1].parent;

                            if(rotateModules[i].rotateType == Rotate_Type.Global){

                                rotateModules[i].rotateSlots[i2].holder.eulerAngles = new Vector3(0, 0, 0);

                            }//rotateType = global

                            if(rotateModules[i].rotateType == Rotate_Type.Local){

                                rotateModules[i].rotateSlots[i2].holder.localEulerAngles = new Vector3(0, 0, 0);

                            }//rotateType = local

                        }//parent != parent

                    }//for i2 rotateSlots

                    rotateModules[i].currentRotation = rotateModulesTemp[i].currentRotation;

                    if(rotateModules[i].rotateType == Rotate_Type.Global){

                        if(rotateModules[i].pivot.eulerAngles != rotateModulesTemp[i].currentRotation){

                            rotateModules[i].pivot.eulerAngles = new Vector3(rotateModules[i].currentRotation.x, rotateModules[i].currentRotation.y, rotateModules[i].currentRotation.z);

                        }//eulerAngles != currentRotation

                    }//rotateType = global

                    if(rotateModules[i].rotateType == Rotate_Type.Local){

                        if(rotateModules[i].pivot.localEulerAngles != rotateModulesTemp[i].currentRotation){

                            rotateModules[i].pivot.localEulerAngles = new Vector3(rotateModules[i].currentRotation.x, rotateModules[i].currentRotation.y, rotateModules[i].currentRotation.z);

                        }//localEulerAngles != currentRotation

                    }//rotateType = global

                }//for i rotateModules

            }//newType = rotate advanced

            if(newType == Puzzle_Type.Lights){

                for(int i = 0; i < lights.Count; ++i ) {

                    lights[i].active = lights[i].interactLight.isPoweredOn;

                    tempBools[i] = lights[i].active;

                }//for i lights

            }//newType = lights

            if(newType == Puzzle_Type.Switches){

                for(int i = 0; i < switches.Count; ++i ) {

                    switches[i].active = switches[i].dynamObject.isOpened;

                    tempBools[i] = switches[i].active;

                }//for i switches

            }//newType = switches

            if(newType == Puzzle_Type.Wave){

                for(int i = 0; i < moduleSlots.Count; ++i ) {

                    moduleSlots[i].active = waveModulesTemp[i].active;

                    for(int ws = 0; ws < moduleSlots[i].waveSlots.Count; ++ws ) {

                        moduleSlots[i].waveSlots[ws].active = waveModulesTemp[i].waveSlots[ws].active;

                        for(int wc = 0; wc < moduleSlots[i].waveSlots[ws].waveChecks.Count; ++wc ) {

                            moduleSlots[i].waveSlots[ws].waveChecks[wc].curValue = waveModulesTemp[i].waveSlots[ws].waveChecks[wc].curValue;
                            moduleSlots[i].waveSlots[ws].waveChecks[wc].active = waveModulesTemp[i].waveSlots[ws].waveChecks[wc].active;

                        }//for wc waveChecks

                    }//for ws waveSlots

                }//for i moduleSlots

                Modules_Update();

            }//newType = wave

        }//NotCompleteLoad_Buff


    //////////////////////////
    //
    //      EXTRAS
    //
    //////////////////////////


        private bool CompareInts(List<int> list1, List<int> list2){

            return list1.SequenceEqual(list2);

        }//CompareInts

        private bool CompareBools(List<bool> list1, List<bool> list2){

            return list1.SequenceEqual(list2);

        }//CompareBools


    }//Puzzler_Handler


}//namespace