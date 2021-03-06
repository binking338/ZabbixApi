﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZabbixApi.Entities;
using ZabbixApi.Helper;
using ZabbixApi;

namespace ZabbixApi.Services
{
    public interface IApplicationService : ICRUDService<Application, ApplicationInclude>
    {

    }
    
    public class ApplicationService : CRUDService<Application, ApplicationService.ApplicationsidsResult, ApplicationInclude>, IApplicationService
    {
        public ApplicationService(IContext context) : base(context, "application") { }

        public override IEnumerable<Application> Get(object filter = null, IEnumerable<ApplicationInclude> include = null, Dictionary<string, object> @params = null)
        {
            var includeHelper = new IncludeHelper(include == null ? 1 : include.Sum(x => (int)x));
            if(@params == null)
                @params = new Dictionary<string, object>();

            @params.AddOrReplace("output", "extend");
            @params.AddOrReplace("selectHosts", includeHelper.WhatShouldInclude(ApplicationInclude.Hosts));
            @params.AddOrReplace("selectItems", includeHelper.WhatShouldInclude(ApplicationInclude.Items));

            @params.AddOrReplace("filter", filter);
            
            return BaseGet(@params);
        }

        public class ApplicationsidsResult : EntityResultBase
        {
            [JsonProperty("applicationids")]
            public override string[] ids { get; set; }
        }
    }

    public enum ApplicationInclude
    {
        All = 1,
        None = 2,
        Hosts = 4,
        Items = 8
    }
}
