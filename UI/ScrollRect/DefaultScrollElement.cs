using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameProject.UI
{

    public class DefaultScrollElement : BaseScrollFormElement
    {
        public Text text;
        public override void Despawn()
        {
        }
        public void Init(int info)
        {
            text.text = "" + info;
        }
    }

}