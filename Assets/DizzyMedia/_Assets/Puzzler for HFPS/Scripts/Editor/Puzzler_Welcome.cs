using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Debug = UnityEngine.Debug;
using System.IO;

using DizzyMedia.Version;
using DizzyMedia.Extension;

namespace DizzyMedia.Welcome {

    [InitializeOnLoad]
    public class Puzzler_Welcome : EditorWindow {


    //////////////////////////////////////
    ///
    ///     INTERNAL VALUES
    ///
    ///////////////////////////////////////


        private static Puzzler_Welcome window;
        private static Vector2 windowsSize = new Vector2(530, 600f);

        private const string isShowAtStartEditorPrefs = "Puzzler_WelcomeStart";
        public static bool showOnStart = true;
        private static bool isInited;

        private static DM_Version dmVersion;
        private static string versionName = "Puzzler Version";
        private static string verNumb = "";
        private static bool versionCheckStatic = false;

        public static DM_InternEnums.Language language;
        private static DM_MenusLocData dmMenusLocData;
        private static string menusLocDataName = "DM_M_Data";
        private static int menusLocDataSlot;
        private static bool languageLock = false; 

        private string fileDocs = "Puzzler Documentation";

        private int puzzlerTabs;
        private int integrationTabs;

        private string currBuildSettings = "";
        private string defineSymb = "PUZZLER_PRESENT";
        private bool defineChanged = false;
        private bool barShowing = false;
        private static bool gizmosChecked = false;
        private static bool gizmosPresent = false;

        Vector2 scrollPos;
        Vector2 scrollPos2;


    //////////////////////////////////////
    ///
    ///     SHOW AT START CHECKS
    ///
    ///////////////////////////////////////


        static Puzzler_Welcome() {

            EditorApplication.update -= GetShowAtStart;
            EditorApplication.update += GetShowAtStart;

        }//WelcomeScreen

        private static void GetShowAtStart() {

            EditorApplication.update -= GetShowAtStart;

            if(EditorPrefs.HasKey(isShowAtStartEditorPrefs)){

                showOnStart = EditorPrefs.GetBool(isShowAtStartEditorPrefs);

            //HasKey
            } else {

                showOnStart = true;
                EditorPrefs.SetBool(isShowAtStartEditorPrefs, showOnStart);

            }//HasKey

            if(showOnStart) {

                EditorApplication.update -= OpenAtStartup;
                EditorApplication.update += OpenAtStartup;

            }//showOnStart

        }//GetShowAtStart

        private static void OpenAtStartup() {

            OpenWizard();
            EditorApplication.update -= OpenAtStartup;

        }//OpenAtStartup


    //////////////////////////////////////
    ///
    ///     EDITOR WINDOW
    ///
    ///////////////////////////////////////


        [MenuItem("Tools/Dizzy Media/Puzzler for HFPS/Tutorials", false , 13)]
        public static void OpenTutorials() {

            Application.OpenURL("https://www.youtube.com/playlist?list=PL1-QGLv4h0rJm9HXkZP2tWE_v3K9Mlz-h");

        }//OpenTutorials
        
        [MenuItem("Tools/Dizzy Media/Puzzler for HFPS/Review Asset", false , 13)]
        public static void OpenReview() {

            Application.OpenURL("https://u3d.as/2Hn8#reviews");

        }//OpenReview

        [MenuItem("Tools/Dizzy Media/Puzzler for HFPS/Puzzler Welcome", false , 12)]
        public static void OpenWizard() {

            if(dmVersion == null){

                versionCheckStatic = false;
                Version_FindStatic();

            //dmVersion == null
            } else {

                verNumb = dmVersion.version;

                window = GetWindow<Puzzler_Welcome>(false, "Puzzler" + " v" + verNumb, true);
                window.maxSize = window.minSize = windowsSize;

            }//dmVersion == null

            if(dmMenusLocData == null){

                languageLock = false;
                DM_LocDataFind();

            //dmMenusLocData = null
            } else {

                language = (DM_InternEnums.Language)(int)dmMenusLocData.currentLanguage;

            }//dmMenusLocData = null

            Gizmos_Check();

        }//OpenWizard

        private void OnGUI() {

            Puzzler_WelcomeScreen();

        }//OnGUI


    //////////////////////////////////////
    ///
    ///     EDITOR DISPLAY
    ///
    ///////////////////////////////////////


        public void Puzzler_WelcomeScreen(){

            GUI.skin.button.alignment = TextAnchor.MiddleCenter;

            Texture t0 = (Texture)Resources.Load("EditorContent/Puzzler-Logo");

            var style = new GUIStyle(GUI.skin.label) {alignment = TextAnchor.MiddleCenter};

            GUILayout.Box(t0, style, GUILayout.ExpandWidth(true), GUILayout.Height(200));

            GUILayout.Space(10);

            if(dmMenusLocData != null){

                showOnStart = EditorGUILayout.Toggle(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[1].singleValues[0].local, showOnStart);

            }//dmMenusLocData != null

            GUILayout.Space(5);

            EditorGUILayout.BeginHorizontal();

            language = (DM_InternEnums.Language)EditorGUILayout.EnumPopup("Language", language); 

            if(dmMenusLocData != null){

                if(GUILayout.Button(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[1].buttons[0].local)) {

                    Language_Save();

                }//Button

            }//dmMenusLocData != null

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            if(dmMenusLocData != null){

                if(verNumb == "Unknown"){

                    EditorGUILayout.HelpBox(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[0].texts[0].text, MessageType.Info);

                //verNumb == "Unknown"
                } else {

                    puzzlerTabs = GUILayout.SelectionGrid(puzzlerTabs, new string[] { dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[1].local, dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].local, dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[3].local}, 3);

                    EditorGUILayout.Space();

                    if(puzzlerTabs == 0){

                        #if PUZZLER_PRESENT

                            EditorGUILayout.HelpBox(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[1].texts[1].text, MessageType.Info);

                        #else

                            EditorGUILayout.HelpBox(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[1].texts[0].text, MessageType.Error);

                        #endif

                        scrollPos = GUILayout.BeginScrollView(scrollPos, false, true, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));

                        EditorGUILayout.HelpBox(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[1].texts[2].text, MessageType.Info);

                        EditorGUILayout.HelpBox(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[1].texts[3].text, MessageType.Info);

                        EditorGUILayout.EndScrollView();

                        EditorGUILayout.Space();

                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                        EditorGUILayout.BeginHorizontal();

                        if(GUILayout.Button(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[1].buttons[1].local)){

                            File_Find(fileDocs);

                        }//Button

                        GUILayout.Space(5);

                        if(GUILayout.Button(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[1].buttons[2].local)){

                            OpenTutorials();

                        }//Button
                        
                        GUILayout.Space(5);

                        if(GUILayout.Button(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[1].buttons[3].local)){

                            OpenReview();

                        }//Button

                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.Space();

                    }//puzzlerTabs = Welcome

                    if(puzzlerTabs == 1){

                        #if PUZZLER_PRESENT

                            EditorGUILayout.HelpBox(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].texts[1].text, MessageType.Info);

                        #else

                            EditorGUILayout.HelpBox(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].texts[0].text, MessageType.Error);

                        #endif

                        scrollPos2 = GUILayout.BeginScrollView(scrollPos2, false, true, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));

                        EditorGUILayout.HelpBox(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].texts[2].text, MessageType.Info);

                        EditorGUILayout.EndScrollView();

                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                        EditorGUILayout.BeginHorizontal();

                        if(GUILayout.Button(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].buttons[0].local)){

                            Launch_ScriptEditor();

                        }//Button

                        if(GUILayout.Button(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].buttons[1].local)){

                            Launch_VersionDetect();

                        }//Button

                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.Space();

                        EditorGUILayout.BeginHorizontal();

                        #if PUZZLER_PRESENT

                            if(GUILayout.Button(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].buttons[3].local)) {

                                if(EditorUtility.DisplayDialog(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].prompts[1].header, dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].prompts[1].message, dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].prompts[1].buttons[0].local, dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].prompts[1].buttons[1].local)){

                                    Symbol_Remove(defineSymb);

                                }//DisplayDialog

                            }//Button

                        #else

                            if(GUILayout.Button(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].buttons[2].local)) {

                                if(EditorUtility.DisplayDialog(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].prompts[0].header, dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].prompts[0].message, dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].prompts[0].buttons[0].local, dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].prompts[0].buttons[1].local)){

                                    Symbol_Add(defineSymb);

                                }//DisplayDialog

                            }//Button

                        #endif

                        //GUILayout.Space(5);

                        if(gizmosChecked){

                            if(!gizmosPresent){

                                GUI.enabled = true;

                                if(GUILayout.Button(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].buttons[4].local)){

                                    if(EditorUtility.DisplayDialog(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].prompts[3].header, dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].prompts[3].message, dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].prompts[3].buttons[0].local, dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].prompts[3].buttons[1].local)){

                                        Gizmos_Move(false);

                                    }//DisplayDialog

                                }//Button

                            //!gizmosPresent
                            } else {

                                if(GUILayout.Button(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].buttons[5].local)){

                                    if(EditorUtility.DisplayDialog(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].prompts[4].header, dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].prompts[4].message, dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].prompts[4].buttons[0].local, dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].prompts[4].buttons[1].local)){

                                        Gizmos_Move(true);

                                    }//DisplayDialog

                                }//Button

                            }//!gizmosPresent

                        //gizmosChecked
                        } else {

                            Gizmos_Check();

                        }//gizmosChecked

                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.Space();

                    }//puzzlerTabs = Setup

                    if(puzzlerTabs == 2){

                        integrationTabs = GUILayout.SelectionGrid(integrationTabs, new string[] { dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[3].singleValues[0].local, dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[3].singleValues[1].local}, 2);

                        EditorGUILayout.Space();

                        if(integrationTabs == 0){

                            EditorGUILayout.HelpBox(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[3].texts[0].text, MessageType.Info);

                        }//integrationTabs = dizzy media

                        if(integrationTabs == 1){

                            EditorGUILayout.HelpBox(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[3].texts[1].text, MessageType.Warning);

                        }//integrationTabs = third party

                    }//puzzlerTabs = integrations

                }//verNumb == "Unknown"

            //dmMenusLocData != null 
            } else {

                if(!languageLock){

                    DM_LocDataFind();

                }//!languageLock 

            }//dmMenusLocData != null

            if(!EditorApplication.isCompiling){

                if(defineChanged && barShowing){

                    barShowing = false;
                    EditorUtility.ClearProgressBar();

                }//defineChanged & barShowing

            }//isCompiling

        }//WelcomeScreen


    //////////////////////////////////////
    ///
    ///     LANGUAGE ACTIONS
    ///
    //////////////////////////////////////


        public static void DM_LocDataFind(){

            if(dmMenusLocData == null){

                //Debug.Log("Find Start");

                //AssetDatabase.Refresh();

                string[] results;
                DM_MenusLocData tempMenusLocData = ScriptableObject.CreateInstance<DM_MenusLocData>();

                results = AssetDatabase.FindAssets(menusLocDataName);

                if(results.Length > 0){

                    foreach(string guid in results){

                        if(File.Exists(AssetDatabase.GUIDToAssetPath(guid))){

                            tempMenusLocData = AssetDatabase.LoadAssetAtPath<DM_MenusLocData>(AssetDatabase.GUIDToAssetPath(guid));

                            if(tempMenusLocData != null){

                                dmMenusLocData = tempMenusLocData;

                                if(dmMenusLocData != null){

                                    if(!languageLock){

                                        languageLock = true;

                                        Language_Check();

                                    }//!languageLock

                                }//dmMenusLocData != null

                            }//tempMenusLocData != null

                            //Debug.Log("Menus Loc Data Found");

                        }//file.exists

                    }//foreach guid

                }//results.Length > 0

            //dmMenusLocData = null
            } else {

                if(!languageLock){

                    languageLock = true;

                    language = (DM_InternEnums.Language)(int)dmMenusLocData.currentLanguage;

                }//!languageLock

            }//dmMenusLocData = null

        }//DM_LocDataFind

        public static void Language_Check(){

            if(dmMenusLocData != null){

                for(int d = 0; d < dmMenusLocData.dictionary.Count; d++){

                    if(dmMenusLocData.dictionary[d].asset == "Puzzler"){

                        menusLocDataSlot = d;

                        //Debug.Log("Loc Data Slot = " + menusLocDataSlot);

                    }//asset = IWC

                }//for d dictionary

                language = (DM_InternEnums.Language)(int)dmMenusLocData.currentLanguage;

            }//dmMenusLocData != null

        }//Language_Check

        public void Language_Save(){

            if(dmMenusLocData != null){

                if((int)dmMenusLocData.currentLanguage != (int)language){

                    dmMenusLocData.currentLanguage = (DM_InternEnums.Language)(int)language;

                }//currentLanguage != language

            }//dmMenusLocData != null

            Debug.Log("Language Saved");

        }//Language_Save


    //////////////////////////////////////
    ///
    ///     VERSION ACTIONS
    ///
    //////////////////////////////////////


        public static void Version_FindStatic(){

            if(!versionCheckStatic){

                versionCheckStatic = true;

                AssetDatabase.Refresh();

                string[] results;
                DM_Version tempVersion = ScriptableObject.CreateInstance<DM_Version>();

                results = AssetDatabase.FindAssets(versionName);

                if(results.Length > 0){

                    foreach(string guid in results){

                        if(File.Exists(AssetDatabase.GUIDToAssetPath(guid))){

                            tempVersion = AssetDatabase.LoadAssetAtPath<DM_Version>(AssetDatabase.GUIDToAssetPath(guid));

                            if(tempVersion != null){

                                dmVersion = tempVersion;
                                verNumb = dmVersion.version;

                                window = GetWindow<Puzzler_Welcome>(false, "Puzzler" + " v" + verNumb, true);
                                window.maxSize = window.minSize = windowsSize;

                                //Debug.Log("Puzzler Version found");

                            //tempVersion != null
                            } else {

                                if(verNumb == ""){

                                    verNumb = "Unknown";

                                }//verNumb = null

                                window = GetWindow<Puzzler_Welcome>(false, "Puzzler " + verNumb, true);
                                window.maxSize = window.minSize = windowsSize;

                                //Debug.Log("Puzzler Version NOT found");

                            }//tempVersion != null

                        //Exists
                        } else {

                            //Debug.Log("Puzzler Version NOT found"); 

                        }//Exists

                    }//foreach guid

                //results.Length > 0
                } else {

                    verNumb = "Unknown";

                    window = GetWindow<Puzzler_Welcome>(false, "Puzzler " + verNumb, true);
                    window.maxSize = window.minSize = windowsSize;

                }//results.Length > 0

            }//!versionCheckStatic

        }//Version_FindStatic


    //////////////////////////////////////
    ///
    ///     LAUNCH ACTIONS
    ///
    ///////////////////////////////////////


        public void Launch_ScriptEditor(){

            DM_ScriptEditor window = (DM_ScriptEditor)EditorWindow.GetWindow<DM_ScriptEditor>(false, "Script Editor", true);
            window.OpenWizard_Single();

        }//Launch_ScriptEditor

        public void Launch_VersionDetect(){

            DM_VersionDetect window = (DM_VersionDetect)EditorWindow.GetWindow<DM_VersionDetect>(false, "Version Detect", true);
            window.OpenWizard_Single();

        }//Launch_VersionDetect


    //////////////////////////////////////
    ///
    ///     GIZMOS ACTIONS
    ///
    ///////////////////////////////////////


        public static void Gizmos_Check(){

            if(Directory.Exists("Assets/Gizmos/")){

                string[] results;
                string[] tempPaths = new string[] {"Assets/Gizmos"};

                results = AssetDatabase.FindAssets("Puzzler-Icon", tempPaths);

                if(results.Length > 0){

                    gizmosPresent = true;

                    /*

                    foreach(string guid in results){

                        Debug.Log(AssetDatabase.GUIDToAssetPath(guid));

                    }//foreach guid

                    */

                //results.Length > 0
                } else {

                    gizmosPresent = false;

                }//results.Length > 0

            //gizmos folder exists
            } else {

                gizmosPresent = false;

            }//gizmos folder exists

            gizmosChecked = true;

        }//Gizmos_Check

        public void Gizmos_Move(bool remove){

            if(!remove){

                if(!Directory.Exists("Assets/Gizmos/")){

                    AssetDatabase.CreateFolder("Assets", "Gizmos");

                }//!exists gizmos folder

                if(File.Exists("Assets/DizzyMedia/Resources/Gizmos/Puzzler/Puzzler-Icon.png.meta")){
                
                    FileUtil.MoveFileOrDirectory("Assets/DizzyMedia/Resources/Gizmos/Puzzler/Puzzler-Icon.png.meta", "Assets/Gizmos/Puzzler-Icon.png.meta");
                
                }//file exists
                
                if(File.Exists("Assets/DizzyMedia/Resources/Gizmos/Puzzler/Puzzler-Icon.png")){
                
                    FileUtil.MoveFileOrDirectory("Assets/DizzyMedia/Resources/Gizmos/Puzzler/Puzzler-Icon.png", "Assets/Gizmos/Puzzler-Icon.png");
                        
                }//file exists
                
                gizmosPresent = true;

            //!remove
            } else {

                if(File.Exists("Assets/Gizmos/Puzzler-Icon.png.meta")){
                
                    FileUtil.MoveFileOrDirectory("Assets/Gizmos/Puzzler-Icon.png.meta", "Assets/DizzyMedia/Resources/Gizmos/Puzzler/Puzzler-Icon.png.meta");
                
                }//file exists
                
                if(File.Exists("Assets/Gizmos/Puzzler-Icon.png")){
                
                    FileUtil.MoveFileOrDirectory("Assets/Gizmos/Puzzler-Icon.png", "Assets/DizzyMedia/Resources/Gizmos/Puzzler/Puzzler-Icon.png");

                }//file exists
                
                gizmosPresent = false;

            }//!remove

            AssetDatabase.Refresh();

        }//Gizmos_Move


    //////////////////////////////////////
    ///
    ///     SYMBOLS ACTIONS
    ///
    ///////////////////////////////////////


        private void Symbol_Add(string newSymbol){

            defineChanged = false;
            currBuildSettings = "";

            currBuildSettings = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);

            if(!currBuildSettings.Contains(newSymbol)) {

                if(string.IsNullOrEmpty(currBuildSettings)) {

                    currBuildSettings = newSymbol;

                //currBuildSettings IsNullOrEmpty
                } else {

                    currBuildSettings += ";" + newSymbol;

                }//currBuildSettings IsNullOrEmpty

                defineChanged = true;

            //!Contains newSymbol
            } else {

                if(EditorUtility.DisplayDialog(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].prompts[2].header, dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].prompts[2].message, dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].prompts[2].buttons[0].local)){}

            }//!Contains newSymbol

            if(defineChanged) {

                PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, currBuildSettings);

                EditorUtility.DisplayProgressBar(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].singleValues[0].local, dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].singleValues[1].local + newSymbol, 0.5f);

                barShowing = true;

            }//defineChanged

        }//Symbol_Add

        public void Symbol_Remove(string newSymbol){

            defineChanged = false;
            currBuildSettings = "";

            currBuildSettings = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);

            //Debug.Log("Build Settings = " + currBuildSettings);

            string tempSettings = "";
            string[] tempArray = currBuildSettings.Split(';');
            List<string> tempList = new List<string>();

            if(tempArray.Length > 0){

                //Debug.Log("tempArray Count = " + tempArray.Length);

                for(int i = 0; i < tempArray.Length; i++){

                    tempList.Add(tempArray[i]);

                }//for i tempArray

            }//tempArray.Length > 0

            if(tempList.Count > 0){

                //Debug.Log("tempList Count = " + tempList.Count);

                if(tempList.Contains(newSymbol)){

                    tempList.Remove(newSymbol);

                    defineChanged = true;

                }//contains newSymbol

                for(int i2 = 0; i2 < tempList.Count; i2++){

                    if(i2 != tempList.Count - 1){

                        tempSettings += tempList[i2] + ";";

                    //i2 != tempList.Count
                    } else {

                        tempSettings += tempList[i2];

                    }//i2 !+ tempList.COunt

                }//for i2

                //Debug.Log("tempSettings = " + tempSettings);

                currBuildSettings = tempSettings;

            }//tempList.Count > 0

            if(defineChanged) {

                PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, currBuildSettings);

                EditorUtility.DisplayProgressBar(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].singleValues[0].local, dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].singleValues[2].local + newSymbol, 0.5f);

                barShowing = true;

            //defineChanged
            } else {

                Debug.Log(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].singleValues[4].local);

            }//defineChanged

        }//Symbol_Remove

        public void Symbols_Remove(List<string> newSymbols){

            defineChanged = false;
            currBuildSettings = "";

            currBuildSettings = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);

            //Debug.Log("Build Settings = " + currBuildSettings);

            string tempSettings = "";
            string[] tempArray = currBuildSettings.Split(';');
            List<string> tempList = new List<string>();

            if(tempArray.Length > 0){

                //Debug.Log("tempArray Count = " + tempArray.Length);

                for(int i = 0; i < tempArray.Length; i++){

                    tempList.Add(tempArray[i]);

                }//for i tempArray

            }//tempArray.Length > 0

            if(tempList.Count > 0){

                //Debug.Log("tempList Count = " + tempList.Count);

                for(int ns = 0; ns < newSymbols.Count; ns++){

                    if(!defineChanged){

                        if(tempList.Contains(newSymbols[ns])){

                            tempList.Remove(newSymbols[ns]);

                            defineChanged = true;

                        }//contains newSymbol

                    }//!defineChanged

                }//for ns newSymbols

                for(int i2 = 0; i2 < tempList.Count; i2++){

                    if(i2 != tempList.Count - 1){

                        tempSettings += tempList[i2] + ";";

                    //i2 != tempList.Count
                    } else {

                        tempSettings += tempList[i2];

                    }//i2 !+ tempList.COunt

                }//for i2

                //Debug.Log("tempSettings = " + tempSettings);

                currBuildSettings = tempSettings;

            }//tempList.Count > 0

            if(defineChanged) {

                PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, currBuildSettings);

                EditorUtility.DisplayProgressBar(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].singleValues[0].local, dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].singleValues[3].local, 0.5f);

                barShowing = true;

            //defineChanged
            } else {

                Debug.Log(dmMenusLocData.dictionary[menusLocDataSlot].menuLoc.localization.languages[(int)language].windows[0].sections[2].singleValues[4].local);

            }//defineChanged

        }//Symbols_Remove


    //////////////////////////////////////
    ///
    ///     EXTRAS
    ///
    ///////////////////////////////////////


        public void File_Find(string fileName){

            string[] results = new string[0];

            results = AssetDatabase.FindAssets(fileName);

            if(results.Length > 0){

                UnityEngine.Object[] objects = new UnityEngine.Object[results.Length];

                string[] paths = new string[results.Length];

                for(int i = 0; i < results.Length; i++) {

                    paths[i] = AssetDatabase.GUIDToAssetPath(results[i]);

                }//for i results

                if(paths.Length > 0){

                    for(int p = 0; p < paths.Length; p++) {

                        objects[p] = AssetDatabase.LoadAssetAtPath(paths[p], typeof(UnityEngine.Object));

                    }//for p paths

                }//paths.Length > 0

                if(objects.Length > 0){

                    Selection.objects = objects;

                    Debug.Log(fileName + " Found!");

                }//objects.Length > 0

            //results > 0
            } else {

                Debug.Log(fileName + " Not Found!");

            }//results > 0

        }//File_Find

        private void OnDestroy() {

            window = null;
            EditorPrefs.SetBool(isShowAtStartEditorPrefs, showOnStart);

            verNumb = "";

            if(barShowing){

                barShowing = false;
                EditorUtility.ClearProgressBar();

            }//barShowing

        }//OnDestroy


    }//Puzzler_Welcome


}//namespace