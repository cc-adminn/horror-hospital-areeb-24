using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace DizzyMedia.HFPS_Puzzler {

    [CustomEditor(typeof(Puzzler_CameraCont))]
    public class Puzzler_CameraContEditor : Editor {


    //////////////////////////
    //
    //      EDITOR DISPLAY
    //
    //////////////////////////


        Puzzler_CameraCont puzzCamCont;
        GUISkin oldSkin;

        public bool showTips;

        private void OnEnable() {

            puzzCamCont = (Puzzler_CameraCont)target;

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

            GUILayout.BeginHorizontal("Camera Controller", "HeaderText_Small");

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

            puzzCamCont.tabs = GUILayout.SelectionGrid(puzzCamCont.tabs, new string[] { "User Options", "Auto/Debug"}, 2);

            EditorGUILayout.Space();

            EditorGUI.BeginChangeCheck();

            SerializedProperty camAnim = serializedObject.FindProperty("camAnim");
            SerializedProperty moveTypes = serializedObject.FindProperty("moveTypes");

            if(puzzCamCont.tabs == 0){

                if(showTips){

                    EditorGUILayout.HelpBox("\n" + "Click the toggles below to show the options for each section." + "\n", MessageType.Info);

                }//showTips

                EditorGUILayout.Space();

                puzzCamCont.genOpts = GUILayout.Toggle(puzzCamCont.genOpts, "General Options", GUI.skin.button);

                if(puzzCamCont.genOpts){

                    EditorGUILayout.Space();

                    if(showTips){

                        EditorGUILayout.HelpBox("\n" + "Wait time before item is disabled after hiding." + "\n", MessageType.Info);

                        EditorGUILayout.Space();

                    }//showTips 

                    puzzCamCont.disableDelay = EditorGUILayout.FloatField("Disable Delay", puzzCamCont.disableDelay);

                }//genOpts

                EditorGUILayout.Space();

                puzzCamCont.animOpts = GUILayout.Toggle(puzzCamCont.animOpts, "Animation Options", GUI.skin.button);

                if(puzzCamCont.animOpts){

                    EditorGUILayout.Space();

                    if(showTips){

                        EditorGUILayout.Space();

                        EditorGUILayout.HelpBox("\n" + "Animator reference for the camera controller." + "\n", MessageType.Info);

                        EditorGUILayout.Space();

                    }//showTips

                    EditorGUILayout.PropertyField(camAnim, new GUIContent("Camera Animator"), true);

                    EditorGUILayout.Space();

                    if(showTips){

                        EditorGUILayout.Space();

                        EditorGUILayout.HelpBox("\n" + "Move settings for each individual move type." + "\n", MessageType.Info);

                        EditorGUILayout.Space();

                    }//showTips

                    EditorGUILayout.PropertyField(moveTypes, true);

                }//animOpts

            }//tabs = user options

            if(puzzCamCont.tabs == 1){

                EditorGUILayout.Space();

                EditorGUILayout.LabelField("Automatic Values", EditorStyles.centeredGreyMiniLabel);

                EditorGUILayout.Space();

                if(showTips){

                    EditorGUILayout.HelpBox("\n" + "These values are automatically handled by the system." + "\n", MessageType.Info);

                    EditorGUILayout.Space();

                }//showTips

                puzzCamCont.tempSlot = EditorGUILayout.IntField("Temp Slot", puzzCamCont.tempSlot);
                puzzCamCont.hasMoved = EditorGUILayout.Toggle("Has Moved?", puzzCamCont.hasMoved);

            }//tabs = auto

            EditorGUILayout.Space();

            if(EditorGUI.EndChangeCheck()){

                serializedObject.ApplyModifiedProperties();

            }//EndChangeCheck

            if(GUI.changed){

                EditorUtility.SetDirty(puzzCamCont);

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


    }//Puzzler_CameraContEditor


}//namespace
