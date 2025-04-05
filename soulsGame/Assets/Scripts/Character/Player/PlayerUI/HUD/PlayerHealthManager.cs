using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SG{
    public class PlayerHealthManager : CharacterHealthManager
    {
        protected override void Awake()
        {
            base.Awake();
        }


        protected override void HandleHealthBarValue(float value)
        {
            base.HandleHealthBarValue(value);
        }
        
    }
}
