using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if COMPONENTS_PRESENT

    using DizzyMedia.HFPS_Components;

#endif

using DizzyMedia.Shared;

using HFPS.Editors;
using HFPS.Player;
using HFPS.Systems;

namespace DizzyMedia.HFPS_Puzzler {

    public class Puzzler_Menu : EditorWindow {


    //////////////////////////////////////
    ///
    ///     MENU BUTTONS
    ///
    ///////////////////////////////////////

    ////////////////////////////////
    ///
    ///     COMPONENTS CREATE
    ///
    ////////////////////////////////

    ////////////////////
    ///
    ///     CAMERA
    ///
    ////////////////////


        [MenuItem("Tools/Dizzy Media/Puzzler for HFPS/Components/Camera/Camera Controller", false , 0)]
        public static void Create_CamCont() {

            if(Selection.gameObjects.Length > 0){

                Selection.gameObjects[0].AddComponent<Puzzler_CameraCont>();

            //Selection > 0
            } else {

                if(EditorUtility.DisplayDialog("Error", "You must select an object to add the component to.", "Ok")){}

            }//Selection > 0

        }//Create_CamCont


    ////////////////////
    ///
    ///     DYNAMIC
    ///
    ////////////////////


        [MenuItem("Tools/Dizzy Media/Puzzler for HFPS/Components/Dynamic/Puzzler Dial", false , 0)]
        public static void Create_PuzzDial() {

            if(Selection.gameObjects.Length > 0){

                Selection.gameObjects[0].AddComponent<Puzzler_Dial>();

            //Selection > 0
            } else {

                if(EditorUtility.DisplayDialog("Error", "You must select an object to add the component to.", "Ok")){}

            }//Selection > 0

        }//Create_PuzzDial

        [MenuItem("Tools/Dizzy Media/Puzzler for HFPS/Components/Dynamic/Puzzler Wave", false , 0)]
        public static void Create_PuzzWave() {

            if(Selection.gameObjects.Length > 0){

                Selection.gameObjects[0].AddComponent<Puzzler_Wave>();

                if(Selection.gameObjects[0].GetComponent<LineRenderer>() != null){

                    Selection.gameObjects[0].GetComponent<Puzzler_Wave>().lineRenderer = Selection.gameObjects[0].GetComponent<LineRenderer>();

                }//LineRenderer != null

            //Selection > 0
            } else {

                if(EditorUtility.DisplayDialog("Error", "You must select an object to add the component to.", "Ok")){}

            }//Selection > 0

        }//Create_PuzzWave


    ////////////////////
    ///
    ///     GENERAL
    ///
    ////////////////////


        [MenuItem("Tools/Dizzy Media/Puzzler for HFPS/Components/General/Puzzler Holder", false , 0)]
        public static void Create_PuzzHold() {

            if(Selection.gameObjects.Length > 0){

                Selection.gameObjects[0].AddComponent<Puzzler_Holder>();

            //Selection > 0
            } else {

                if(EditorUtility.DisplayDialog("Error", "You must select an object to add the component to.", "Ok")){}

            }//Selection > 0

        }//Create_PuzzHold


    ////////////////////
    ///
    ///     SYSTEM
    ///
    ////////////////////


        [MenuItem("Tools/Dizzy Media/Puzzler for HFPS/Components/Systems/Puzzler Handler", false , 0)]
        public static void Create_PuzzHand() {

            if(Selection.gameObjects.Length > 0){

                Selection.gameObjects[0].AddComponent<Puzzler_Handler>();

            //Selection > 0
            } else {

                if(EditorUtility.DisplayDialog("Error", "You must select an object to add the component to.", "Ok")){}

            }//Selection > 0

        }//Create_PuzzHand

        [MenuItem("Tools/Dizzy Media/Puzzler for HFPS/Components/Systems/Item Viewer/Item Viewer", false , 0)]
        public static void Create_ItemView() {

            if(Selection.gameObjects.Length > 0){

                Selection.gameObjects[0].AddComponent<Puzzler_ItemViewer>();

            //Selection > 0
            } else {

                if(EditorUtility.DisplayDialog("Error", "You must select an object to add the component to.", "Ok")){}

            }//Selection > 0

        }//Create_ItemView

        [MenuItem("Tools/Dizzy Media/Puzzler for HFPS/Components/Systems/Item Viewer/Item Viewer Connect", false , 0)]
        public static void Create_ItemViewCon() {

            if(Selection.gameObjects.Length > 0){

                Selection.gameObjects[0].AddComponent<Puzzler_ItemViewerCon>();

            //Selection > 0
            } else {

                if(EditorUtility.DisplayDialog("Error", "You must select an object to add the component to.", "Ok")){}

            }//Selection > 0

        }//Create_ItemViewCon


    ////////////////////////////////
    ///
    ///     HELPERS
    ///
    ////////////////////////////////

    /////////////////////////
    ///
    ///     PLAYER
    ///
    /////////////////////////


            [MenuItem("Tools/Dizzy Media/Puzzler for HFPS/Helpers/Player/Player Update", false , 0)]
            public static void PlayerUpdate() {

                bool playerSelected = false;

                if(Selection.gameObjects.Length > 0){

                    if(Selection.gameObjects[0].GetComponent<PlayerController>() != null){

                        playerSelected = true;

                    //PlayerController != null
                    } else {

                        playerSelected = false;

                        if(EditorUtility.DisplayDialog("Error", "You must select a player object to update.", "Ok")){}

                    }//PlayerController != null

                //Selection > 0
                } else {

                    playerSelected = false;

                    if(EditorUtility.DisplayDialog("Error", "You must select a player object to update.", "Ok")){}

                }//Selection > 0

                bool done = false;

                GameObject curPlayer = null;
                GameObject camCont = null;
                GameObject curCamCont = null;
                GameObject newCamCont = null;
                GameObject itemViewer = null;
                GameObject newItemViewer = null;

                Transform camRoot = null;
                Transform mouseLook = null;

                HFPS_References tempRefs = null;
                ItemSwitcher tempItemSwitch = null;

                if(playerSelected){

                    curPlayer = Selection.gameObjects[0];

                    if(curPlayer.GetComponent<HFPS_References>() == null){

                        tempRefs = curPlayer.AddComponent<HFPS_References>();

                        Debug.Log("HFPS References Added");

                    //HFPS_References = null
                    } else {

                        tempRefs = curPlayer.GetComponent<HFPS_References>();

                        Debug.Log("HFPS References Present");

                    }//HFPS_References = null

                    foreach(Transform child in curPlayer.transform.GetComponentsInChildren<Transform>()){

                        if(child.GetComponent<CameraShaker>() != null){

                            camRoot = child;

                        }//CameraShaker != null

                        if(child.GetComponent<ItemSwitcher>() != null){

                            tempItemSwitch = child.GetComponent<ItemSwitcher>();

                        }//ScriptManager != null

                        if(child.GetComponent<Puzzler_CameraCont>() != null){

                            curCamCont = child.gameObject;

                        }//Puzzler_CameraCont != null

                        if(child.GetComponent<ScriptManager>() != null){

                            mouseLook = child;

                        }//ScriptManager != null

                    }//foreach child

                    if(tempRefs != null && curPlayer != null && tempItemSwitch != null && mouseLook != null){

                        tempRefs.playCont = curPlayer.GetComponent<PlayerController>();
                        tempRefs.mouseLook = mouseLook.GetComponent<MouseLook>();
                        tempRefs.itemSwitcher = tempItemSwitch;

                        #if COMPONENTS_PRESENT

                            if(curPlayer.GetComponent<HFPS_PlayerMan>() != null){

                                tempRefs.playerMan = curPlayer.GetComponent<HFPS_PlayerMan>();

                            }//HFPS_PlayerMan != null

                        #endif

                        Debug.Log("HFPS References Caught");

                    }//tempRefs != null & curPlayer != null & tempItemSwitch != null & mouseLook != null

                    if(tempRefs.itemSwitcher != null){

                        itemViewer = (GameObject)Resources.Load("Prefabs (Puzzler)/Item Viewer/Puzzler_ItemViewer");

                        newItemViewer = Instantiate(itemViewer);
                        newItemViewer.name = "Puzzler_ItemViewer";

                        newItemViewer.transform.parent = tempRefs.itemSwitcher.WallHitTransform;
                        newItemViewer.transform.localPosition = new Vector3(0, 0, 0f);
                        newItemViewer.transform.localEulerAngles = new Vector3(0, 0, 0);
                        newItemViewer.transform.localScale = new Vector3(1, 1, 1);

                        Debug.Log("Puzzler Item Viewer Added");

                    }//itemSwitcher != null

                    if(curCamCont == null){

                        if(camRoot != null && mouseLook != null){

                            camCont = (GameObject)Resources.Load("Prefabs (Puzzler)/Camera/Puzzler Camera Controller");

                            newCamCont = Instantiate(camCont);
                            newCamCont.name = "Puzzler Camera Controller";

                            newCamCont.transform.parent = camRoot;
                            newCamCont.transform.localPosition = new Vector3(0, 0, 0.25f);
                            newCamCont.transform.localEulerAngles = new Vector3(0, 0, 0);
                            newCamCont.transform.localScale = new Vector3(1, 1, 1);

                            mouseLook.parent = newCamCont.transform;

                            mouseLook.localPosition = new Vector3(0, 0.9f, 0);
                            mouseLook.localEulerAngles = new Vector3(0, 0, 0);

                            done = true;

                            Debug.Log("Puzzler Camera Controller Added");

                        }//camRoot != null & mouseLook != null

                    //curCamCont
                    } else {

                        Debug.Log("Puzzler Camera Controller Present");

                    }//curCamCont

                    if(done){

                        Debug.Log("Player Updated for Puzzler");

                    //done
                    } else {

                        Debug.Log("Player NOT Updated for Puzzler");

                    }//done

                }//playerSelected

            }//PlayerUpdate


    /////////////////////////
    ///
    ///     SCENE
    ///
    /////////////////////////


        #if PUZZLER_PRESENT

            [MenuItem("Tools/Dizzy Media/Puzzler for HFPS/Helpers/Scene/Scene Update (1.6.3a - 1.6.3b)", false , 0)]
            public static void SceneUpdate_163aNb() {

                bool gameUIselected = false;

                if(Selection.gameObjects.Length > 0){

                    for(int i = 0; i < Selection.gameObjects.Length; i++) {

                        if(Selection.gameObjects[i].GetComponent<SaveGameHandler>() != null){

                            gameUIselected = true;
                            break;

                        }//SaveGameHandler != null

                    }//for i selections

                }//Selection.gameObjects.Length > 0

                if(gameUIselected){

                    GameObject oldPlayer = null;
                    GameObject compPlayer = null;
                    GameObject newPlayer = null;

                    var playerConts = FindObjectsOfType<PlayerController>(true);
                    var gameMan = FindObjectsOfType<HFPS_GameManager>(true);
                    var saveGameHand = FindObjectsOfType<SaveGameHandler>(true);
                    SaveGameHandlerEditor[] tempSaveEditors = (SaveGameHandlerEditor[])Resources.FindObjectsOfTypeAll(typeof(SaveGameHandlerEditor));

                    if(saveGameHand.Length > 0){

                        saveGameHand[0].objectReferences = (ObjectReferences)Resources.Load("GameData/Puzzler ObjectReferences");

                        Debug.Log("Object References Updated for HFPS 1.6.3a - 1.6.3b");

                    }//saveGameHand.Length > 0

                    if(playerConts.Length > 0){

                        //Debug.Log(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].singleValues[8].local);

                        oldPlayer = playerConts[0].gameObject;

                    }//playerConts.Length > 0

                    compPlayer = (GameObject)Resources.Load("Prefabs (Puzzler)/Player/HEROPLAYER (puzzler)");

                    newPlayer = Instantiate(compPlayer);
                    newPlayer.name = "HEROPLAYER (puzzler)(New)";

                    newPlayer.transform.parent = oldPlayer.transform;
                    newPlayer.transform.localPosition = new Vector3(0, 0, 0);
                    newPlayer.transform.localEulerAngles = new Vector3(0, 0, 0);
                    newPlayer.transform.parent = null;

                    DestroyImmediate(oldPlayer);

                    Debug.Log("Player Updated for HFPS 1.6.3a - 1.6.3b");

                    if(gameMan.Length > 0){

                        gameMan[0].m_PlayerObj = newPlayer;

                        Debug.Log("Game Manager Updated for HFPS 1.6.3a - 1.6.3b");

                    }//gameMan.Length > 0

                    if(tempSaveEditors.Length > 0){

                        Debug.Log("Saveables Save Start");

                        tempSaveEditors[0].FindSaveables_Start();

                    }//tempSaveEditors.Length > 0

                //gameUIselected
                } else {

                    Debug.Log("Game UI object NOT SELECTED | Save Game Handler NOT SELECTED");
                    Debug.Log("SELECT _GAMEUI | SELECT SAVE GAME HANDLER SCRIPT OBJECT");

                }//gameUIselected

            }//SceneUpdate_163aNb

            [MenuItem("Tools/Dizzy Media/Puzzler for HFPS/Helpers/Scene/Scene Update (1.6.3c)", false , 0)]
            public static void SceneUpdate_163c() {

                bool gameUIselected = false;

                if(Selection.gameObjects.Length > 0){

                    for(int i = 0; i < Selection.gameObjects.Length; i++) {

                        if(Selection.gameObjects[i].GetComponent<SaveGameHandler>() != null){

                            gameUIselected = true;
                            break;

                        }//SaveGameHandler != null

                    }//for i selections

                }//Selection.gameObjects.Length > 0

                if(gameUIselected){

                    GameObject oldPlayer = null;
                    GameObject compPlayer = null;
                    GameObject newPlayer = null;

                    var playerConts = FindObjectsOfType<PlayerController>(true);
                    var gameMan = FindObjectsOfType<HFPS_GameManager>(true);
                    var saveGameHand = FindObjectsOfType<SaveGameHandler>(true);
                    SaveGameHandlerEditor[] tempSaveEditors = (SaveGameHandlerEditor[])Resources.FindObjectsOfTypeAll(typeof(SaveGameHandlerEditor));

                    if(saveGameHand.Length > 0){

                        saveGameHand[0].objectReferences = (ObjectReferences)Resources.Load("GameData/Puzzler ObjectReferences 1.6.3c");

                        Debug.Log("Object References Updated for HFPS 1.6.3c");

                    }//saveGameHand.Length > 0

                    if(playerConts.Length > 0){

                        //Debug.Log(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].singleValues[8].local);

                        oldPlayer = playerConts[0].gameObject;

                    }//playerConts.Length > 0

                    compPlayer = (GameObject)Resources.Load("Prefabs (Puzzler)/Player/HEROPLAYER (puzzler) (1.6.3c)");

                    newPlayer = Instantiate(compPlayer);
                    newPlayer.name = "HEROPLAYER (puzzler)(1.6.3c)(New)";

                    newPlayer.transform.parent = oldPlayer.transform;
                    newPlayer.transform.localPosition = new Vector3(0, 0, 0);
                    newPlayer.transform.localEulerAngles = new Vector3(0, 0, 0);
                    newPlayer.transform.parent = null;

                    DestroyImmediate(oldPlayer);

                    Debug.Log("Player Updated for HFPS 1.6.3c");

                    if(gameMan.Length > 0){

                        gameMan[0].m_PlayerObj = newPlayer;

                        Debug.Log("Game Manager Updated for HFPS 1.6.3c");

                    }//gameMan.Length > 0

                    if(tempSaveEditors.Length > 0){

                        Debug.Log("Saveables Save Start");

                        tempSaveEditors[0].FindSaveables_Start();

                    }//tempSaveEditors.Length > 0

                //gameUIselected
                } else {

                    Debug.Log("Game UI object NOT SELECTED | Save Game Handler NOT SELECTED");
                    Debug.Log("SELECT _GAMEUI | SELECT SAVE GAME HANDLER SCRIPT OBJECT");

                }//gameUIselected

            }//SceneUpdate_163c

        #endif


    }//Puzzler_Menu
    
    
}//namespace
