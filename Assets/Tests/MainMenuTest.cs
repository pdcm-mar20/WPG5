using System.Collections;
using System.Collections.Generic;
using Authentication;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class MainMenuTest
{
    private Button btn;

    [OneTimeSetUp]
    public void Setup()
    {
        // MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/CanvasMainMenu"));
        SceneManager.LoadScene("MainMenu");
    }

    [UnityTest]
    public IEnumerator GoToLeaderboardTest()
    {
        btn = GameObject.Find("LeaderBoard").GetComponent<Button>();
        
        btn.onClick.Invoke();
        yield return new WaitForSeconds(1);
        
        Assert.AreEqual(SceneManager.GetActiveScene().name, "Leaderboard");
        
        GameObject.Find("Back").GetComponent<Button>().onClick.Invoke();
    }

    [UnityTest]
    public IEnumerator GoToCredit()
    {
        btn = GameObject.Find("Credit").GetComponent<Button>();
        btn.onClick.Invoke();
        yield return new WaitForSeconds(3);
        Assert.AreEqual(SceneManager.GetActiveScene().name, "Credit");
        
        GameObject.Find("Back").GetComponent<Button>().onClick.Invoke();
    }
    
    [UnityTest]
    public IEnumerator GoToProfile()
    {
        btn = GameObject.Find("Edit").GetComponent<Button>();
        btn.onClick.Invoke();
        yield return new WaitForSeconds(3);
        Assert.AreEqual(SceneManager.GetActiveScene().name, "EditProfile");
        
        GameObject.Find("Back").GetComponent<Button>().onClick.Invoke();
    }
    
    [UnityTest]
    public IEnumerator GoToShop()
    {
        btn = GameObject.Find("Shop").GetComponent<Button>();
        btn.onClick.Invoke();
        yield return new WaitForSeconds(3);
        Assert.AreEqual(SceneManager.GetActiveScene().name, "Shop");
        
        GameObject.Find("Back").GetComponent<Button>().onClick.Invoke();
    }
    
    
    [UnityTest]
    public IEnumerator GoToPlay()
    {
        btn = GameObject.Find("Play").GetComponent<Button>();
        btn.onClick.Invoke();
        yield return new WaitForSeconds(3);
        Assert.AreEqual(SceneManager.GetActiveScene().name, "SampleScene");
        
        GameObject.Find("Back").GetComponent<Button>().onClick.Invoke();
    }
}