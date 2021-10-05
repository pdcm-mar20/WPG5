using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Lobby
{
    public class ConnectionInfo : MonoBehaviour
    {
        [SerializeField] private Text infoText;
    
        public void SetText(string _text, bool isRemoved)
        {
            infoText.text = _text;

            if (isRemoved)
                StartCoroutine(RemoveText(2f));
        }

        private IEnumerator RemoveText(float _waitTime)
        {
            yield return new WaitForSeconds(_waitTime);

            this.gameObject.SetActive(false);
        }
    }
}