using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class ProfileTest
    {
        [OneTimeSetUp]
        public void Setup()
        {
            SceneManager.LoadScene("EditProfile");
        }

        [UnityTest, Order(1)]
        public IEnumerator UserDataTest()
        {
            yield return new WaitForSeconds(3);

            var name = GameObject.Find("Name").GetComponent<InputField>().text;
            var pwd = GameObject.Find("Pwd").GetComponent<InputField>().text;
            var coin = GameObject.Find("Coin").GetComponent<InputField>().text;
            var score = GameObject.Find("Score").GetComponent<InputField>().text;
            
            Assert.AreNotEqual(name, null);
            Assert.AreNotEqual(pwd, null);
            Assert.AreNotEqual(coin, null);
            Assert.AreNotEqual(score, null);
        }

        [UnityTest, Order(2)]
        public IEnumerator UpdateDataTest()
        {
            var name = GameObject.Find("Name").GetComponent<InputField>();
            var pwd = GameObject.Find("Pwd").GetComponent<InputField>();

            var nameTest = "Test" + Random.Range(0, 1000);
            var pwdTest = "Test" + Random.Range(0, 1000);
            name.text = nameTest;
            pwd.text = pwdTest;

            GameObject.Find("Update").GetComponent<Button>().onClick.Invoke();
            yield return new WaitForSeconds(3);
            
            Assert.AreEqual(GameObject.Find("Message").GetComponent<Text>().text, "Success");
        }
    }
}