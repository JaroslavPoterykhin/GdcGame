﻿using EntityFX.Gdcame.DataAccess.Contract.GameData;
using EntityFX.Gdcame.DataAccess.Contract.User;
using EntityFX.Gdcame.Infrastructure.Common;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace EntityFX.Gdcame.DataAccess.Service
{
    public class ContainerBootstrapper : IContainerBootstrapper
    {
        public IUnityContainer Configure(IUnityContainer container)
        {
            container.RegisterType<IUserDataAccessService, UserDataAccessService>(
               new InterceptionBehavior<PolicyInjectionBehavior>()
               , new Interceptor<InterfaceInterceptor>()
               );
            container.RegisterType<IGameDataRetrieveDataAccessService, GameDataRetrieveDataAccessDocumentService>(
                new InterceptionBehavior<PolicyInjectionBehavior>()
                , new Interceptor<InterfaceInterceptor>()
                );
            container.RegisterType<IGameDataStoreDataAccessService, GameDataStoreDataAccessDocumentService>(
                new InterceptionBehavior<PolicyInjectionBehavior>()
                , new Interceptor<InterfaceInterceptor>()
                );
            return container;
        }
    }
}