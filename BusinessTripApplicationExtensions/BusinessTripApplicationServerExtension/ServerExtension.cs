using Autofac;
using BusinessTripApplicationServerExtension.CardLifeCycle;
using BusinessTripApplicationServerExtension.Feature1.Services;
using BusinessTripApplicationServerExtension.Plugins;
using DocsVision.BackOffice.CardLib.CardDefs;
using DocsVision.Layout.WebClient.Services;
using DocsVision.WebClient.Extensibility;
using DocsVision.WebClient.Helpers;
using DocsVision.WebClientLibrary.ObjectModel.Services.EntityLifeCycle;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Resources;

namespace BusinessTripApplicationServerExtension
{
    /// <summary>
    /// Задаёт описание расширения для WebClient, которое задано в текущей сборке
    /// </summary>
    public class BusinessTripApplicationServerExtension : WebClientExtension
    {
        /// <summary>
        /// Создаёт новый экземпляр <see cref="BusinessTripApplicationServerExtension" />
        /// </summary>
        /// <param name="serviceProvider">Сервис-провайдер</param>
        public BusinessTripApplicationServerExtension(IServiceProvider serviceProvider)
            : base()
        {
        }

        /// <summary>
        /// Получить название расширения
        /// </summary>
        public override string ExtensionName
        {
            get { return Assembly.GetAssembly(typeof(BusinessTripApplicationServerExtension)).GetName().Name; }
        }

        /// <summary>
        /// Получить версию расширения
        /// </summary>
        public override Version ExtensionVersion
        {
            get { return new Version(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion); }
        }

        /// <summary>
        /// Регистрация типов в IoC контейнере
        /// </summary>
        /// <param name="containerBuilder"></param>
        public override void InitializeServiceCollection(IServiceCollection services)
        {
            services.AddSingleton<IBusinessTripAppService, BusinessTripAppService>();
            // Декорируем базовый CardLifeCycle через Scrutor
            services.Decorate<ICardLifeCycleEx>((original, serviceProvider) => {
                var typeId = original.CardTypeId;
                if (typeId == CardDocument.ID)
                {
                    var BusinessTripService = serviceProvider.GetRequiredService<IBusinessTripAppService>();
                    return new BusinessTripLifeCycle(original, BusinessTripService);
                }
                return original;
            });

            services.AddSingleton<IDataGridControlPlugin, BusinessTripAppPlugin>();
            // Примеры регистрации различных типов ВК 
            // services.AddSingleton<YourServiceInterface, YourServiceClass>();
            // services.AddSingleton<IBindingConverter, YourBindingConverterType>();
            // services.AddSingleton<IBindingResolver, YourBindingResolverType>();            
            // services.AddSingleton<IControlResolver, YourControlResolverType>();
            // services.AddSingleton<IPropertyResolver, YourPropertyResolverType>();  
            // services.AddTransient<ICardLifeCycle, YourCardLifeCycle>();
            // services.AddTransient<IRowLifeCycle, YourRowLifeCycle>(); 
        }

        /// <summary>
        /// Gets resource managers for layout extension
        /// </summary>
        /// <returns></returns>
        protected override List<ResourceManager> GetLayoutExtensionResourceManagers()
        {
            return new List<ResourceManager>
            {

            };
        }
    }
}