﻿using OpenQA.Selenium;

namespace Selenium.Framework
{
    public abstract class ElementDecorator : Element
    {
        protected readonly Element Element;

        protected ElementDecorator(Element element)
        {
            Element = element;
        }

        public override By By => Element?.By;

        public override string Text => Element?.Text;

        public override bool? Enabled => Element?.Enabled;

        public override bool? Displayed => Element?.Displayed;

        public override void Click()
        {
            Element?.Click();
        }

        public override string GetAttribute(string attributeName)
        {
            return Element?.GetAttribute(attributeName);
        }

        public override void TypeText(string text)
        {
            Element?.TypeText(text);
        }
    }
}
