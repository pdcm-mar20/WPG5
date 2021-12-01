using System.Collections;
using System.Collections.Generic;
using Authentication;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class RegisterTest
    {
        private GameObject gameGameObject;
        private InputField[] fields;
        private Register register;

        [OneTimeSetUp]
        public void Setup()
        {
            gameGameObject =
                MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Canvas"));
            fields = gameGameObject.GetComponentsInChildren<InputField>();
            register = gameGameObject.GetComponent<Register>();
            
            register.name = fields[0];
            register.pwd = fields[1];
        }

        [UnityTest]
        public IEnumerator FieldEmpty()
        {
            yield return new WaitForSeconds(1);
            Assert.AreEqual(register.name.text, "");
            Assert.AreEqual(register.pwd.text, "");
        }

        [UnityTest]
        public IEnumerator FieldNotEmpty()
        {
            register.name.text = "Febrian";
            register.pwd.text = "123";
            yield return new WaitForSeconds(1);
            Assert.AreEqual(register.name.text, "Febrian");
            Assert.AreEqual(register.pwd.text, "123");
        }

        [UnityTest]
        public IEnumerator UserId()
        {
            yield return new WaitForSeconds(1);
            Assert.AreNotEqual(register.UserId(), null);
        }
    }
}