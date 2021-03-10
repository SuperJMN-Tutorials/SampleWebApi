using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using AutoMapper;
using BikingUltimate.Client;
using EasyRpc.DynamicClient;
using EasyRpc.DynamicClient.Grace;
using Grace.DependencyInjection;
using SampleApp.ViewModels;
using SampleApp.Views;
using Uno.Extensions;
using Zafiro.Uno.Infrastructure;

namespace SampleApp
{
    public class Composition : CompositionBase
    {
        protected override void ConfigureServices(DependencyInjectionContainer container)
        {
            var mapper = new MapperConfiguration(x =>
            {
                x.ConstructServicesUsing(container.Locate);
            }).CreateMapper();

            container.Configure(c =>
            {
                c.ExportInstance(mapper).As<IMapper>();
                c.ExportFactory(BikingUltimateClient.Create).As<IBikingService>();
            });

            var uri = GetServiceUri();
        }

        private static string GetServiceUri()
        {
            var port = 61207;
#if __ANDROID__
            var domain = "10.0.2.2";
#else
            var domain = "localhost";
#endif
            return $"http://{domain}:{port}";
        }

        protected override void ConfigureViewModelToViewMaps(IDictionary<Type, Type> map)
        {
            var vmToViewMaps = new Dictionary<Type, Type>
            {
                {typeof(UsersViewModel), typeof(Users)},
            };
            
            map.AddRange(vmToViewMaps);
        }

        protected override void ConfigureSections(List<Section> mappings)
        {
            var sections = new[]
            {
                new Section("Usuarios", typeof(UsersViewModel)){ Icon = new SymbolIcon(Symbol.OtherUser)},
            };

            mappings.AddRange(sections);
        }
    }
}