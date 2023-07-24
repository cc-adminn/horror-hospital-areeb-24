using UnityEngine;
using UnityEditor;
using System.IO;

[InitializeOnLoad]
public class PreloadSigningAlias {

    static PreloadSigningAlias () {
        PlayerSettings.Android.keystorePass = "123456";
        PlayerSettings.Android.keyaliasName = "key";
        PlayerSettings.Android.keyaliasPass = "123456";
    }
}