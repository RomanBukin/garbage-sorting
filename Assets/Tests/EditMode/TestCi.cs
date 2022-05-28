using System;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode
{
    public class TestCi
    {
        [Test]
        public void TestCiSimplePasses()
        {
            Assert.True(true);
            
            Debug.Log("Debug log");
            Console.WriteLine("Console write line");
            
            Assert.False(false);
        }
    }
}
