using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Player
{
    public class PlayerMovementTests
    {
        // [Test]
        // public void PlayerMovementTestsSimplePasses()
        // {
            // GameObject Player
        // }

        [UnityTest]
        public IEnumerator PlayerMovementTestsWithEnumeratorPasses()
        {
            GameObject playerObject = new GameObject("test");
            GameObject go = UnityEngine.Object.Instantiate(playerObject,
                Vector3.zero,
                Quaternion.identity) as GameObject;
            Assert.AreEqual(Vector3.zero, go.transform.position);

            go.AddComponent<PlayerMovement>();
            yield return null;
        }
    }
}