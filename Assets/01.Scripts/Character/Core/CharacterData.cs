using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Game
{
    public class CharacterData : ScriptableObject
    {
        [Header("General")]
        public float Health = 1;

        [Header("Animation")]
        public RuntimeAnimatorController RuntimeAnimatorController;
    }

}