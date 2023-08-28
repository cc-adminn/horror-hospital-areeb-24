using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DizzyMedia.HFPS_Puzzler {

    [AddComponentMenu("Dizzy Media/Puzzler for HFPS/Components/Camera/Camera Controller")]
    public class Puzzler_CameraCont : MonoBehaviour {


    //////////////////////////
    //
    //      INSTANCE
    //
    //////////////////////////


        public static Puzzler_CameraCont instance;


    //////////////////////////
    //
    //      CLASSES
    //
    //////////////////////////


        [System.Serializable]
        public class Move_Type {

            [Space]

            public string name;
            public bool isDefault;

            [Space]

            [Header("Animation")]

            public string moveIn;
            public string moveOut;

        }//Move_Type


    //////////////////////////
    //
    //      VALUES
    //
    //////////////////////////


        public float disableDelay;

        public Animator camAnim;
        public List<Move_Type> moveTypes;

        [Header("Auto")]

        public int tempSlot;
        public bool hasMoved;

        public int tabs;

        public bool genOpts;
        public bool animOpts;


    //////////////////////////
    //
    //      START ACTIONS
    //
    //////////////////////////


        void Awake(){

            instance = this;

        }//Awake

        void Start() {

            StartInit();

        }//Start

        public void StartInit(){

            camAnim.enabled = false;

            tempSlot = -1;
            hasMoved = false;

        }//StartInit


    //////////////////////////
    //
    //      MOVE TYPE ACTIONS
    //
    //////////////////////////

    //////////////////////////
    //
    //      DEFAULT
    //
    //////////////////////////


        public void MoveType_SetDefault(bool autoPlay){

            tempSlot = -1;

            for(int i = 0; i < moveTypes.Count; i++){

                if(moveTypes[i].isDefault){

                    tempSlot = i;
                    break;

                }//isDefault

            }//for i moveTypes

            if(tempSlot > -1){

                if(autoPlay){

                    Move_Check();

                }//autoPlay

            }//tempSlot > -1

        }//MoveType_SetDefault


    //////////////////////////
    //
    //      CUSTOM
    //
    //////////////////////////


        public void MoveType_Set(string name){

            tempSlot = -1;

            for(int i = 0; i < moveTypes.Count; i++){

                if(moveTypes[i].name == name){

                    tempSlot = i;
                    break;

                }//name = name

            }//for i moveTypes

        }//MoveType_Set

        public void MoveType_Set(string name, bool autoPlay){

            tempSlot = -1;

            for(int i = 0; i < moveTypes.Count; i++){

                if(moveTypes[i].name == name){

                    tempSlot = i;
                    break;

                }//name = name

            }//for i moveTypes

            if(tempSlot > -1){

                if(autoPlay){

                    Move_Check();

                }//autoPlay

            }//tempSlot > -1

        }//MoveType_Set


    //////////////////////////
    //
    //      MOVE ACTIONS
    //
    //////////////////////////


        public void Move_Check(){

            if(tempSlot > -1){

                if(!hasMoved){

                    if(!camAnim.enabled){

                        camAnim.enabled = true;

                    }//!enabled

                    camAnim.Play(moveTypes[tempSlot].moveIn);

                    hasMoved = true;

                //!hasMoved
                } else {

                    camAnim.Play(moveTypes[tempSlot].moveOut);

                    hasMoved = false;

                    StartCoroutine("DisableDelayed");

                }//!hasMoved

            }//tempSlot > -1

        }//Move_Check


    //////////////////////////
    //
    //      DISABLE ACTIONS
    //
    //////////////////////////


        private IEnumerator DisableDelayed(){

            yield return new WaitForSeconds(disableDelay);

            camAnim.enabled = false;

        }//DisableDelayed


    }//Puzzler_CameraCont


}//namespace
