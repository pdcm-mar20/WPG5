using System.Collections;
using Authentication;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class RegisterTest
    {

        #region UI Automation Test Scenario

        /* 1. Memberikan event click pada btn login dan mengecek apakah sudah berhasil pindah ke scene login
         * 2. Mengecek error saat field name tidak terisi
         * 3. Mengecek error saat field password tidak terisi
         * 4. Mengecek fungsi generate userId jika tidak sama dg null
         * 5. Mengecek field name & password ketika terisi dan memberikan event click pada register untuk
         *    membuat akun baru dan pindah ke scene main menu
         */
        #endregion
        
        private GameObject gameGameObject;
        private InputField[] fields;
        private Register register;

        [OneTimeSetUp]
        public void Setup()
        {
            SceneManager.LoadScene("Register");
        }

        [UnityTest, Order(2)]
        public IEnumerator FieldNameEmpty()
        {
            gameGameObject = GameObject.Find("Canvas");
            
            //get component inputfield in scene register
            fields = gameGameObject.GetComponentsInChildren<InputField>();
            register = gameGameObject.GetComponent<Register>();
            
            register.name = fields[0];

            register.errName = register.name.gameObject.GetComponentsInChildren<Text>()[2];

            register.name.text = "";

            yield return new WaitForSeconds(1);
            
            GameObject.Find("Register").GetComponent<Button>().onClick.Invoke();
            
            //check if component input field name is empty 
            Assert.AreEqual(register.name.text, "");

            //check error component
            Assert.AreEqual(register.errName.text, "Username Harus Diisi!");
        }

        [UnityTest, Order(3)]
        public IEnumerator FieldPasswordEmpty()
        {
            gameGameObject = GameObject.Find("Canvas");
            
            //get component inputfield in scene register
            fields = gameGameObject.GetComponentsInChildren<InputField>();
            register = gameGameObject.GetComponent<Register>();
            
            register.pwd = fields[1];
            register.errPwd = register.pwd.gameObject.GetComponentsInChildren<Text>()[2];
            Debug.Log(register.pwd);
            Debug.Log(register.errPwd);
            register.name.text = "T"+Random.Range(0,1000)+"est"+Random.Range(0,1000);
            register.pwd.text = "";
            yield return new WaitForSeconds(1);
            
            GameObject.Find("Register").GetComponent<Button>().onClick.Invoke();
            //check if component input field pwd is empty 
            Assert.AreEqual(register.pwd.text, "");
            
            //check error component
            Assert.AreEqual(register.errPwd.text, "Password Harus Diisi!");
        }

        [UnityTest, Order(5)]
        public IEnumerator FieldNotEmpty()
        {
            
            gameGameObject = GameObject.Find("Canvas");
            
            //get component inputfield in scene register
            fields = gameGameObject.GetComponentsInChildren<InputField>();
            register = gameGameObject.GetComponent<Register>();

            register.name = fields[0];
            register.pwd = fields[1];
            
            //check if component input field is not empty
            var name = "T"+Random.Range(0, 1000)+"est" + Random.Range(0, 1000);
            const string pwd = "123";
            register.name.text = name;
            register.pwd.text = pwd;
            yield return new WaitForSeconds(1);
            Assert.AreEqual(register.name.text, name);
            Assert.AreEqual(register.pwd.text, pwd);
            
            GameObject.Find("Register").GetComponent<Button>().onClick.Invoke();
            yield return new WaitForSeconds(1);
            Assert.AreEqual(SceneManager.GetActiveScene().name, "MainMenu");
        }

        [UnityTest, Order(4)]
        public IEnumerator UserId()
        {
            // check if generate userId not null
            yield return new WaitForSeconds(1);
            Assert.AreNotEqual(register.UserId(), null);
        }
        
        [UnityTest, Order(1)]
        public IEnumerator GoToLogin()
        {
            GameObject.Find("Login").GetComponent<Button>().onClick.Invoke();
            yield return null;
            Assert.AreEqual(SceneManager.GetActiveScene().name, "Login");
            GameObject.Find("Register").GetComponent<Button>().onClick.Invoke();
        }
    }
}