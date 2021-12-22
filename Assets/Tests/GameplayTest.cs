using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class GameplayTest
{

    [OneTimeSetUp]
    public void Setup()
    {
        SceneManager.LoadScene("GameplayTest");
    }

    [UnityTest]
    public IEnumerator PlayTest()
    {
        var host = GameObject.Find("Host").GetComponent<Button>();
        host.onClick.Invoke();
        var player = GameObject.FindWithTag("Player");
        yield return new WaitForSeconds(5);
        Assert.Greater(player.transform.position.x, 0);
        
    }

}
