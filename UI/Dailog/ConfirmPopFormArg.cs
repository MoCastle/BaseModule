using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameProject.UI;

namespace GameProject
{
    public struct ConfirmPopFormArg
    {
        public string message;
        public Action confirmAction;
        public Action cancelAction;

        public ConfirmPopFormArg(Action openAction,Action closeAction,string message)
        {
            this.message = message;
            confirmAction = openAction;
            this.cancelAction = closeAction;
        }
    }
}