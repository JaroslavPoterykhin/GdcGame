﻿using System.Collections.Generic;
using System.Linq;
using EntityFX.Gdcame.Common.Contract.Items;
using EntityFX.Gdcame.DataAccess.Model.Ef;
using EntityFX.Gdcame.DataAccess.Repository.Contract;
using EntityFX.Gdcame.DataAccess.Repository.Contract.Criterions.CustomRule;
using EntityFX.Gdcame.Infrastructure.Common;
using EntityFX.Gdcame.Infrastructure.Repository.UnitOfWork;

namespace EntityFX.Gdcame.DataAccess.Repository.Ef
{
    class CustomRuleRepository : ICustomRuleRepository
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IMapper<CustomRuleEntity, CustomRule> _customRuleMapper;

        public CustomRuleRepository(IUnitOfWorkFactory unitOfWorkFactory
            , IMapper<CustomRuleEntity, CustomRule> customRuleMapper
            )
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _customRuleMapper = customRuleMapper;
        }

        public CustomRule[] FindAll(GetAllCustomRulesCriterion criterion)
        {
            using (var uow = _unitOfWorkFactory.Create())
            {
                var findQuery = uow.BuildQuery();
                return findQuery.For<IEnumerable<CustomRuleEntity>>()
                    .With(criterion)
                    .Select(_ => _customRuleMapper.Map(_))
                    .ToArray();
            }
        }
    }
}