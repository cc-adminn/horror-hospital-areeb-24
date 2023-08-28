using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DizzyMedia.HFPS_Puzzler {

    [AddComponentMenu("Dizzy Media/Puzzler for HFPS/Components/General/Puzzler Holder")]
    public class Puzzler_Holder : MonoBehaviour {


    //////////////////////////////////////
    ///
    ///     VALUES
    ///
    ///////////////////////////////////////

    ///////////////////////////
    ///
    ///     REFERENCES
    ///
    ///////////////////////////


        public Collider trigger;
        public Rigidbody rigid;


    ///////////////////////////
    ///
    ///     AUTO
    ///
    ///////////////////////////


        public Puzzler_Handler puzzlerHand;

        public int slot;
        public int secondSlot;
        public float weight;

        public int tabs;


    //////////////////////////////////////
    ///
    ///     START ACTIONS
    ///
    ///////////////////////////////////////


        void Start(){}


    //////////////////////////////////////
    ///
    ///     SLOT ACTIONS
    ///
    ///////////////////////////////////////


        public void Slot_Empty(){

            if(puzzlerHand.puzzleType == Puzzler_Handler.Puzzle_Type.MultiItems){

                puzzlerHand.multiSlots[slot - 1].events.onReset.Invoke();

                puzzlerHand.tempInts[slot - 1] = 0;
                puzzlerHand.multiSlots[slot - 1].filled = false;

                if(puzzlerHand.multiSlots[slot - 1].slotCheck == Puzzler_Handler.Slot_Check.Empty){

                    puzzlerHand.multiSlots[slot - 1].active = true;

                    puzzlerHand.activeCount = 0;

                    for(int i2 = 0; i2 < puzzlerHand.multiSlots.Count; ++i2 ) {

                        if(puzzlerHand.multiSlots[i2].active){

                            puzzlerHand.activeCount += 1;

                        }//!active

                    }//for i2 multiSlots

                    puzzlerHand.CompleteCheck();

                }//slotCheck = empty

                if(puzzlerHand.multiSlots[slot - 1].slotCheck == Puzzler_Handler.Slot_Check.Item){

                    puzzlerHand.multiSlots[slot - 1].active = false;

                }//slotCheck = item

            }//puzzleType = multi items

            if(puzzlerHand.puzzleType == Puzzler_Handler.Puzzle_Type.Weight){

                puzzlerHand.weightModules[slot - 1].weightSlots[secondSlot - 1].filled = false;

                puzzlerHand.weightModules[slot - 1].curWeight -= weight;

                puzzlerHand.weightModules[slot - 1].weightSlots[secondSlot - 1].events.onReset.Invoke();

                puzzlerHand.weightModulesTemp[slot - 1].weightSlots[secondSlot - 1] = 0;

                puzzlerHand.Weight_Check();
                puzzlerHand.CompleteCheck();

            }//puzzleType = weight

            puzzlerHand.puzzlerHolders.Remove(this);

        }//Slot_Empty


    //////////////////////////////////////
    ///
    ///     STATE ACTIONS
    ///
    ///////////////////////////////////////


        public void ActiveState(bool state){

            trigger.enabled = state;

            if(rigid != null){

                if(state){

                    rigid.isKinematic = false;
                    rigid.useGravity = true;

                //state
                } else {

                    rigid.isKinematic = true;
                    rigid.useGravity = false;

                }//state

            }//rigidbody != null

        }//ActiveState


    }//Puzzler_Holder


}//namespace