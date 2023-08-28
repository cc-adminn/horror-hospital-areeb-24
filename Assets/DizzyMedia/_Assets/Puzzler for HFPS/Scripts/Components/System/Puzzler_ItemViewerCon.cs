using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DizzyMedia.HFPS_Puzzler {

    [AddComponentMenu("Dizzy Media/Puzzler for HFPS/Components/Systems/Item Viewer/Item Viewer Connect")]
    public class Puzzler_ItemViewerCon : MonoBehaviour {


    ///////////////////////////
    ///
    ///     START ACTIONS
    ///
    ///////////////////////////


        void Start(){}


    ///////////////////////////
    ///
    ///     ITEM ACTIONS
    ///
    ///////////////////////////


        public void Item_LookAt(){

            Puzzler_ItemViewer.instance.Item_LookAt();

        }//Item_LookAt


    }//Puzzler_ItemViewerCon


}//namespace