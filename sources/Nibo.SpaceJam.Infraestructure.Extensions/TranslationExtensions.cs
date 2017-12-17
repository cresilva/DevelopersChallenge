using Nibo.SpaceJam.Infraestructure.Resources;
using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;

namespace System
{
    /// <summary>
    /// Features for globalization
    /// </summary>
    public static class TranslationExtensions
    {
        private static readonly ResourceManager _modelsResourceManager;
        private static readonly ResourceManager _servicesResourceManager;
        private static readonly ResourceManager _webAPIResourceManager;

        /// <summary>
        /// Set availlable resources
        /// </summary>
        static TranslationExtensions()
        {
            _modelsResourceManager = new ResourceManager(typeof(Models));
            _servicesResourceManager = new ResourceManager(typeof(Services));
            _webAPIResourceManager = new ResourceManager(typeof(WebAPI));
        }

        /// <summary>
        /// Translate text using resources
        /// </summary>
        /// <param name="resourceKey">Key of resource</param>
        /// <returns>Translated text</returns>
        public static string Translate(this string resourceKey)
        {
            //Try get text using Models resource
            var resultText = _modelsResourceManager.GetString(resourceKey);

            //Try get text using Services resource
            if (string.IsNullOrWhiteSpace(resultText))
                resultText = _servicesResourceManager.GetString(resourceKey);

            //Try get text using WebAPI resource
            if (string.IsNullOrWhiteSpace(resultText))
                resultText = _webAPIResourceManager.GetString(resourceKey);

            return string.IsNullOrEmpty(resultText) ? resourceKey : resultText;
        }
    }
}
