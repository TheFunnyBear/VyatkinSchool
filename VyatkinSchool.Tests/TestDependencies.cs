using SpecFlow.Unity;
using System.Linq;
using System;
using TechTalk.SpecFlow;
using Microsoft.Practices.Unity;

namespace VyatkinSchool.Tests
{
    public static class TestDependencies
    {
        [ScenarioDependencies]
        public static IUnityContainer CreateContainer()
        {
            // create container with the runtime dependencies
            var container = Dependencies.CreateContainer();

            // Registers the build steps, this gives us dependency resolution using the container.
            // NB If you need named parameters into the steps you should override specific registrations
            container.RegisterTypes(typeof(TestDependencies).Assembly.GetTypes().Where(t => Attribute.IsDefined(t, typeof(BindingAttribute))),
                                    WithMappings.FromMatchingInterface,
                                    WithName.Default,
                                    WithLifetime.ContainerControlled);

            return container;
        }
    }
}
