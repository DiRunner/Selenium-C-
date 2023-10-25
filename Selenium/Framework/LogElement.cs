using log4net;
using OpenQA.Selenium;

namespace Selenium.Framework
{
    public class LogElement : ElementDecorator
    {
        protected ILog Logger;
        public LogElement(Element element)
        : base(element)
        {
            Logger = LogManager.GetLogger(GetType());
        }

        public override By By => Element?.By;

        public override string Text
        {
            get
            {
                Logger.Info($"Element Text = {Element?.Text}");
                return Element?.Text;
            }
        }

        public override bool? Enabled
        {
            get
            {
                Logger.Info($"Element Enabled = {Element?.Enabled}");
                return Element?.Enabled;
            }
        }

        public override bool? Displayed
        {
            get
            {
                Logger.Info($"Element Displayed = {Element?.Displayed}");
                return Element?.Displayed;
            }
        }

        public override void Click()
        {
            Logger.Info($"Element Clicked");
            Element?.Click();
        }

        public override string GetAttribute(string attributeName)
        {
            Logger.Info($"Get Element's Attribute = {attributeName}");
            return Element?.GetAttribute(attributeName);
        }

        public override void TypeText(string text)
        {
            Logger.Info($"Type Text = {text}");
            Element?.TypeText(text);
        }
    }
}
