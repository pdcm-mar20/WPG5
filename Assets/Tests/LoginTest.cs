using System.Collections;
using Authentication;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests
{
    public class LoginTest
    {
        #region UI Automation Test Scenario

        /* 1. Memberikan event click pada btn register dan mengecek apakah sudah berhasil pindah ke scene register
         * 2. Mengecek error saat field name tidak terisi
         * 3. Mengecek error saat username salah
         * 4. Mengecek error saat password salah
         * 5. Mengecek success login dan pindah ke scene main menu
         */
        #endregion
        
        private GameObject gameGameObject;
        private InputField[] fields;
        private Login login;

        [OneTimeSetUp]
        public void Setup()
        {
            SceneManager.LoadScene("Login");
        }
        
        [UnityTest, Order(0)]
        public IEnumerator GoToRegister()
        {
            GameObject.Find("Register").GetComponent<Button>().onClick.Invoke();
            yield return null;
            Assert.AreEqual(SceneManager.GetActiveScene().name, "Register");
            GameObject.Find("Login").GetComponent<Button>().onClick.Invoke();
        }
        
        [UnityTest, Order(1)]
        public IEnumerator FieldEmpty()
        {
            gameGameObject = GameObject.Find("CanvasLogin");
            fields = gameGameObject.GetComponentsInChildren<InputField>();
            login = gameGameObject.GetComponent<Login>();
            
            login.name = fields[0];
            login.pwd = fields[1];
            
            yield return new WaitForSeconds(1);
            Assert.AreEqual(login.name.text, "");
            Assert.AreEqual(login.pwd.text, "");
        }
        
        [UnityTest, Order(2)]
        public IEnumerator UsernameNotExist()
        {
            gameGameObject = GameObject.Find("CanvasLogin");
            fields = gameGameObject.GetComponentsInChildren<InputField>();
            login = gameGameObject.GetComponent<Login>();
            
            login.name = fields[0];
            login.pwd = fields[1];
            
            login.name.text = "RandomTest";
            login.pwd.text = "123";
            
            GameObject.Find("Login").GetComponent<Button>().onClick.Invoke();
            
            yield return new WaitForSeconds(1);
            login.err = GameObject.Find("Err").GetComponent<Text>();
           
            Assert.AreEqual(login.err.text, "Name Tidak Ada!");
        }
        
        [UnityTest, Order(3)]
        public IEnumerator PasswordWrong()
        {
            gameGameObject = GameObject.Find("CanvasLogin");
            fields = gameGameObject.GetComponentsInChildren<InputField>();
            login = gameGameObject.GetComponent<Login>();
            
            login.name = fields[0];
            login.pwd = fields[1];
            
            login.name.text = "Test";
            login.pwd.text = "123";
            
            GameObject.Find("Login").GetComponent<Button>().onClick.Invoke();
            
            yield return new WaitForSeconds(1);
            login.err = GameObject.Find("Err").GetComponent<Text>();
           
            Assert.AreEqual(login.err.text, "Password Salah!");
        }
        
        [UnityTest, Order(4)]
        public IEnumerator SuccessLogin()
        {
            gameGameObject = GameObject.Find("CanvasLogin");
            fields = gameGameObject.GetComponentsInChildren<InputField>();
            login = gameGameObject.GetComponent<Login>();
            
            login.name = fields[0];
            login.pwd = fields[1];
            
            login.name.text = "Test";
            login.pwd.text = "Test";
            
            GameObject.Find("Login").GetComponent<Button>().onClick.Invoke();
            
            yield return new WaitForSeconds(1);

            Assert.AreEqual(SceneManager.GetActiveScene().name, "MainMenu");
        }

    }
}