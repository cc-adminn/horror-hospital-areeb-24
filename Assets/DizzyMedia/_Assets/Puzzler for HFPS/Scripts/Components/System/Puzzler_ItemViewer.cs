using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HFPS.Systems;

namespace DizzyMedia.HFPS_Puzzler {

    [AddComponentMenu("Dizzy Media/Puzzler for HFPS/Components/Systems/Item Viewer/Item Viewer")]
    public class Puzzler_ItemViewer : MonoBehaviour {


    //////////////////////////////////////
    ///
    ///     INSTANCE
    ///
    ///////////////////////////////////////


        public static Puzzler_ItemViewer instance;


    //////////////////////////////////////
    ///
    ///     CLASSES
    ///
    ///////////////////////////////////////


        [System.Serializable]
        public class Item {

            [Space]

            public string name;
            public GameObject parent;

            [Space]

            [InventorySelector]
            public int itemID;

            [Space]

            public Animation animation;

        }//Items

        [System.Serializable]
        public class Animation {

            [Space]

            public Animator anim;

            [Space]

            public AnimationClip show;
            public AnimationClip hide;

            [Space]

            public AnimationClip lookAt;
            public AnimationClip lookAtReturn;

        }//Animation


    //////////////////////////////////////
    ///
    ///     VALUES
    ///
    ///////////////////////////////////////


        public List<Item> items;

        public bool isLooking;
        public int tempID;
        public int tempSlot;

        public int tabs;

        public bool genOpts;


    ///////////////////////////
    ///
    ///     START ACTIONS
    ///
    ///////////////////////////


        void Awake(){

            instance = this;

        }//Awake

        void Start() {

            StartInit();

        }//start

        public void StartInit(){

            isLooking = false;
            tempID = -1;
            tempSlot = -1;

        }//StartInit


    ///////////////////////////
    ///
    ///     ITEM ACTIONS
    ///
    ///////////////////////////


        public void Item_Catch(int newID){

            bool present = false;

            for(int i = 0; i < items.Count; i++) {

                if(!present){

                    if(items[i].itemID == newID){

                        tempSlot = i;
                        tempID = items[i].itemID;

                        present = true;

                    }//itemID = newID

                }//!present

            }//for i items

        }//Item_Catch

        public void ItemInit_Delayed(float delay){

            StartCoroutine("ItemInit_Buff", delay);

        }//ItemInit_Delayed

        private IEnumerator ItemInit_Buff(float delay){

            yield return new WaitForSeconds(delay);

            Item_Show();

        }//ItemInit_Buff

        public void Item_Show(){

            items[tempSlot].parent.SetActive(true);

            items[tempSlot].animation.anim.Play(items[tempSlot].animation.show.name);

        }//Item_Show

        public void Item_Hide(){

            StartCoroutine("ItemHide_Buff");

        }//Item_Hide

        private IEnumerator ItemHide_Buff(){

            isLooking = false;

            items[tempSlot].animation.anim.Play(items[tempSlot].animation.hide.name);

            yield return new WaitForSeconds(1f);

            items[tempSlot].parent.SetActive(false);

        }//ItemHide_Buff

        public void Item_LookAt(){

            if(!isLooking){

                items[tempSlot].animation.anim.Play(items[tempSlot].animation.lookAt.name);

                isLooking = true;

            //!isLooking
            } else {

                items[tempSlot].animation.anim.Play(items[tempSlot].animation.lookAtReturn.name);

                isLooking = false;

            }//!isLooking

        }//Item_LookAt


    }//Puzzler_ItemViewer


}//namespace
