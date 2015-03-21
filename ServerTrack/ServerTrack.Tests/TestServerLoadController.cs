using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerTrack.Controllers;
using ServerTrack.Models;

namespace ServerTrack.Tests
{
    [TestClass]
    public class TestServerLoadController
    {
        [TestMethod]
        public void Test1_SimplePost()
        {
            ServerLoadController controller = new ServerLoadController(new ServerDataMemoryRepository());

            LoadInfo loadInfo = new LoadInfo("Test1_SimplePost", new DateTime(2015, 03, 21, 1, 1, 4), 60, 30);
            controller.Post(loadInfo);
        }

        [TestMethod]
        public void Test1_SimpleGet()
        {
            ServerLoadController controller = new ServerLoadController(new ServerDataMemoryRepository());

            LoadInfo loadInfo = new LoadInfo("Test1_SimpleGet", new DateTime(2015, 03, 21, 1, 1, 4), 60, 30);
            controller.Post(loadInfo);

            ServerLoadReport report = controller.Get("Test1_SimpleGet");

            Assert.IsNotNull(report);
            Assert.AreEqual("Test1_SimpleGet", report.ServerName);
        }

        [TestMethod]
        public void Test1_SimpleGetNoData()
        {
            ServerLoadController controller = new ServerLoadController(new ServerDataMemoryRepository());

            ServerLoadReport report = controller.Get("Test1_SimpleGet");

            Assert.IsNotNull(report);
            Assert.AreEqual(0, report.AverageLoadByMinutes.Count);
            Assert.AreEqual(0, report.AverageLoadByHours.Count);
        }


        [TestMethod]
        public void Test1_Simple1()
        {
            ServerLoadController controller = new ServerLoadController(new ServerDataMemoryRepository());

            LoadInfo loadInfo = new LoadInfo("Test1_Simple1", new DateTime(2015, 03, 21, 1, 1, 4), 60, 30);
            controller.Post(loadInfo);

            loadInfo = new LoadInfo("Test1_Simple1", new DateTime(2015, 03, 21, 1, 2, 4), 50, 40);
            controller.Post(loadInfo);

            loadInfo = new LoadInfo("Test1_Simple1", new DateTime(2015, 03, 21, 1, 2, 33), 20, 10);
            controller.Post(loadInfo);

            ServerLoadReport report = controller.Get("Test1_Simple1");

            Assert.IsNotNull(report);
        }

        [TestMethod]
        public void Test_Simple2WithValues()
        {
            ServerLoadController controller = new ServerLoadController(new ServerDataMemoryRepository());
            DateTime now = DateTime.Now;

            LoadInfo[] loadInfoArray = new LoadInfo[]
            {
                new LoadInfo("server1",
                    new DateTime(now.Year, now.Month, now.Day, now.Hour,
                        now.Minute, 44), 10, 100),
                new LoadInfo("server1",
                    new DateTime(now.Year, now.Month, now.Day, now.Hour,
                        now.Minute, 44), 20, 200),
                new LoadInfo("server1",
                    new DateTime(now.Year, now.Month, now.Day, now.Hour,
                        now.AddMinutes(-1).Minute, 44), 30, 300),
                new LoadInfo("server1",
                    new DateTime(now.Year, now.Month, now.Day, now.Hour,
                        now.AddMinutes(-1).Minute, 44), 40, 400)
            };
            foreach (LoadInfo loadInfo in loadInfoArray)
            {
                controller.Post(loadInfo);
            }

            ServerLoadReport report = controller.Get("server1");

            Assert.AreEqual(15, report.AverageLoadByMinutes[0].AverageCpuLoad);
            Assert.AreEqual(150, report.AverageLoadByMinutes[0].AverageMemoryLoad);

            Assert.AreEqual(35, report.AverageLoadByMinutes[1].AverageCpuLoad);
            Assert.AreEqual(350, report.AverageLoadByMinutes[1].AverageMemoryLoad);


            Assert.AreEqual(25, report.AverageLoadByHours[0].AverageCpuLoad);
            Assert.AreEqual(250, report.AverageLoadByHours[0].AverageMemoryLoad);

            Assert.IsNotNull(report);
        }

        [TestMethod]
        public void Test_MultipleServers()
        {
            ServerLoadController controller = new ServerLoadController(new ServerDataMemoryRepository());
            DateTime now = DateTime.Now;

            LoadInfo[] loadInfoArray = new LoadInfo[]
            {
                new LoadInfo("Test_MultipleServers1",
                    new DateTime(now.Year, now.Month, now.Day, now.Hour,
                        now.Minute, 44), 10, 100),
                new LoadInfo("Test_MultipleServers1",
                    new DateTime(now.Year, now.Month, now.Day, now.Hour,
                        now.Minute, 55), 20, 200),
                new LoadInfo("Test_MultipleServers2",
                    new DateTime(now.Year, now.Month, now.Day, now.Hour,
                        now.AddMinutes(-1).Minute, 44), 30, 300),
                new LoadInfo("Test_MultipleServers2",
                    new DateTime(now.Year, now.Month, now.Day, now.Hour,
                        now.AddMinutes(-1).Minute, 22), 40, 400)
            };
            foreach (LoadInfo loadInfo in loadInfoArray)
            {
                controller.Post(loadInfo);
            }

            ServerLoadReport report = controller.Get("Test_MultipleServers1");
            Assert.AreEqual(1, report.AverageLoadByMinutes.Count);
            Assert.AreEqual(1, report.AverageLoadByHours.Count);

            Assert.AreEqual(15, report.AverageLoadByMinutes[0].AverageCpuLoad);
            Assert.AreEqual(150, report.AverageLoadByMinutes[0].AverageMemoryLoad);

            report = controller.Get("Test_MultipleServers2");

            Assert.AreEqual(35, report.AverageLoadByMinutes[0].AverageCpuLoad);
            Assert.AreEqual(350, report.AverageLoadByMinutes[0].AverageMemoryLoad);

            Assert.IsNotNull(report);
        }

        [TestMethod]
        public void Test_LargeInputs()
        {
            ServerLoadController controller = new ServerLoadController(new ServerDataMemoryRepository());
            DateTime now = DateTime.Now;

            List<LoadInfo> loadInfoList = new List<LoadInfo>();
            for(int i = 0; i < 9999; ++i)
            {
                loadInfoList.Add(new LoadInfo("Test_LargeInputs",
                    new DateTime(now.Year, now.Month, now.Day, now.Hour,
                        now.Minute, 44), 99999999, 99999999));
            };
            foreach (LoadInfo loadInfo in loadInfoList)
            {
                controller.Post(loadInfo);
            }

            ServerLoadReport report = controller.Get("Test_LargeInputs");
            
            Assert.IsNotNull(report);
        }
    }
}
