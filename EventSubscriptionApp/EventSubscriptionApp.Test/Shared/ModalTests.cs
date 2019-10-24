using EventSubscriptionApp.Shared;
using Microsoft.AspNetCore.Components.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventSubscriptionApp.Test.Shared
{
    [TestClass]
    public class ModalTests
    {
        TestHost _host = new TestHost();

        [TestMethod]
        public async Task ShowConfirmation_ShouldDo()
        {
            var component = _host.AddComponent<Modal>();
            await component.Instance.ShowConfirmation();
            Assert.IsNotNull(component.Find(".show"));
            Assert.AreEqual("Confirmation", component.Find("#exampleModalLabel").InnerHtml);
            Assert.IsTrue(component.Find(".modal").Attributes["style"].Value.Contains("block"));
        }

        [TestMethod]
        public async Task ShowError_ShouldDo()
        {
            var component = _host.AddComponent<Modal>();
            await component.Instance.ShowError();
            Assert.IsNotNull(component.Find(".show"));
            Assert.AreEqual("Error", component.Find("#exampleModalLabel").InnerHtml);
            Assert.IsTrue(component.Find(".modal").Attributes["style"].Value.Contains("block"));
        }

        [TestMethod]
        public async Task Hide_ShouldDo()
        {
            var component = _host.AddComponent<Modal>();
            await component.Instance.Hide();
            Assert.IsNull(component.Find(".show"));
            Assert.IsTrue(component.Find(".modal").Attributes["style"].Value.Contains("none"));
        }

        [TestMethod]
        public void BodyText_ShouldRender()
        {
            string expectedBody = "Hola desde el body.";
            var parameters = new Dictionary<string, object>();
            parameters.Add("BodyText", expectedBody);
            var component = _host.AddComponent<Modal>(parameters);
            Assert.AreEqual(expectedBody,component.Find(".modal-body").InnerText.Trim());
        }

        [TestMethod]
        public async Task CloseCallback_ShouldBeInvoqued()
        {
            bool wasCloseCallbackCalled = false;
            var parameters = new Dictionary<string, object>();
            parameters.Add("CloseCallback", (Action) (() => wasCloseCallbackCalled = true));
            var component = _host.AddComponent<Modal>(parameters);
            await component.Instance.Hide();
            Assert.IsTrue(wasCloseCallbackCalled);
        }

        [TestMethod]
        public async Task XButton_Works()
        {            
            var component = _host.AddComponent<Modal>();
            var button = component.Find(".close");
            await button.ClickAsync();
            Assert.IsNull(component.Find(".show"));
            Assert.IsTrue(component.Find(".modal").Attributes["style"].Value.Contains("none"));
        }

        [TestMethod]
        public async Task CloseButton_Works()
        {
            var component = _host.AddComponent<Modal>();
            var button = component.Find(".btn-secondary");
            await button.ClickAsync();
            Assert.IsNull(component.Find(".show"));
            Assert.IsTrue(component.Find(".modal").Attributes["style"].Value.Contains("none"));
        }
    }
}
