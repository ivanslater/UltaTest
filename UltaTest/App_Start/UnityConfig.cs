using AutoMapper;
using System;
using UltaTest.Data.Domains;
using UltaTest.Models;
using UltaTest.Services;
using Unity;

namespace UltaTest
{
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        public static IUnityContainer Container => container.Value;
        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IPatientService, PatientService>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Patient, PatientModel>().ReverseMap();
            });

            IMapper mapper = config.CreateMapper();
            container.RegisterInstance(mapper);
        }
    }
}