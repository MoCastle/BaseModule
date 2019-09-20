using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ConFirm
{

    public static void CreateConfirm(string txt, System.Action okCallBak, int type = 1, System.Action noCallBak = null)
    {
        Scene scene = SceneManager.GetActiveScene();
        GameObject[] rgo = scene.GetRootGameObjects();

        for(int i = 0;i < rgo.Length;i ++)
        {
            if(rgo[i].name.Equals("Canvas"))
            {
                GameObject prefab = Resources.Load<GameObject>("UI/ConFirmUI");
                GameObject obj = GameObject.Instantiate(prefab) as GameObject;
                ConFirmUI conFirmUI = obj.GetComponent<ConFirmUI>();
                obj.transform.SetParent(rgo[i].transform,false);
                conFirmUI.InitConFirmContent(txt, type, okCallBak, noCallBak);
                break;
            }
        }
    }
}
