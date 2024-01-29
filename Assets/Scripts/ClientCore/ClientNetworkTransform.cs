using Unity.Netcode.Components;
using UnityEngine;

namespace ClientCore
{
    [DisallowMultipleComponent]
    public class ClientNetworkTransform : NetworkTransform
    {
        protected override bool OnIsServerAuthoritative()
        {
            return false;
        }
        
    }
}