using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace DizzyMedia.HFPS_Puzzler {

    [CustomEditor(typeof(Puzzler_ItemViewer))]
    public class Puzzler_ItemViewerEditor : Editor {


    //////////////////////////
    //
    //      EDITOR DISPLAY
    //
    //////////////////////////


        Puzzler_ItemViewer puzzItemView;
        GUISkin oldSkin;

        public bool showTips;

        private void OnEnable() {

            puzzItemView = (Puzzler_ItemViewer)target;

        }//OnEnable

        public override void OnInspectorGUI() { 

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            GUILayout.Space(15);

            var style = new GUIStyle(EditorStyles.largeLabel) {alignment = TextAnchor.MiddleCenter};

            if(oldSkin == null){

                if(oldSkin != Resources.Load("EditorContent/Puzzler Skin") as GUISkin){

                    oldSkin = GUI.skin;

                    //Debug.Log("Old Skin Name " + GUI.skin.name);

                }//oldSkin != Puzzler Skin

            }//oldSkin == null

            GUI.skin = Resources.Load("EditorContent/Puzzler Skin") as GUISkin;

            Texture2D t = (Texture2D)Resources.Load("EditorContent/Puzzler-Editor-Icon");
            Texture2D t2 = (Texture2D)Resources.Load("EditorContent/DM_InfoIcon");
            Texture2D t3 = (Texture2D)Resources.Load("EditorContent/DM_InfoIconActive");

            GUILayout.BeginHorizontal("Item Viewer", "HeaderText");

            GUILayout.Label(t, "headerIcon");

            GUILayout.FlexibleSpace();

            if(!showTips){

                if(GUILayout.Button(t2, "infoIcon")){

                    ShowTips_Check();

                }//Button

            }//!showTips

            if(showTips){

                if(GUILayout.Button(t3, "infoIcon")){

                    ShowTips_Check();

                }//Button

            }//showTips

            EditorGUILayout.EndHorizontal();

            GUILayout.Space(5);

            GUI.skin = oldSkin;

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            EditorGUILayout.Space();

            EditorGUILayout.BeginVertical();

            puzzItemView.tabs = GUILayout.SelectionGrid(puzzItemView.tabs, new string[] { "User Options", "Auto/Debug"}, 2);

            EditorGUILayout.Space();

            EditorGUI.BeginChangeCheck();

            SerializedProperty items = serializedObject.FindProperty("items");

            if(puzzItemView.tabs == 0){

                if(showTips){

                    EditorGUILayout.HelpBox("\n" + "Click the toggles below to show the options for each section." + "\n", MessageType.Info);

                }//showTips

                EditorGUILayout.Space();

                puzzItemView.genOpts = GUILayout.Toggle(puzzItemView.genOpts, "General Options", GUI.skin.button);

                if(puzzItemView.genOpts){

                    EditorGUILayout.Space();

                    if(showTips){

                        EditorGUILayout.HelpBox("\n" + "Item settings for each displayed item." + "\n", MessageType.Info);

                        EditorGUILayout.Space();

                    }//showTips 

                    EditorGUILayout.PropertyField(items, true);

                }//genOpts

            }//tabs = user options

            if(puzzItemView.tabs == 1){

                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Automatic Values", EditorStyles.centeredGreyMiniLabel);

                EditorGUILayout.Space();

                if(showTips){

                    EditorGUILayout.HelpBox("\n" + "These values are automatically handled by the system." + "\n", MessageType.Info);

                    EditorGUILayout.Space();

                }//showTips

                puzzItemView.isLooking = EditorGUILayout.Toggle("Is Looking?", puzzItemView.isLooking);
                puzzItemView.tempID = EditorGUILayout.IntField("Temp ID", puzzItemView.tempID);
                puzzItemView.tempSlot = EditorGUILayout.IntField("Temp Slot", puzzItemView.tempSlot);

            }//tabs = auto

            EditorGUILayout.Space();

            if(EditorGUI.EndChangeCheck()){

                serializedObject.ApplyModifiedProperties();

            }//EndChangeCheck

            if(GUI.changed){

                EditorUtility.SetDirty(puzzItemView);

                if(!EditorApplication.isPlaying){

                    EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());

                }//!isPlaying

            }//changed

            EditorGUILayout.EndVertical();

            EditorGUILayout.EndVertical();

        }//OnInspectorGUI


    //////////////////////////
    //
    //      TIPS ACTIONS
    //
    //////////////////////////


        public void ShowTips_Check(){

            if(showTips){

                showTips = false;

            //showTips
            } else {

                showTips = true;

            }//showTips

        }//ShowTips_Check


    }//Puzzler_ItemViewerEditor


}//namespace
