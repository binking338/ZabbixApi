﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZabbixApi.Entities;
using ZabbixApi.Helper;

namespace ZabbixApi.Services
{
    public interface IGlobalMacroService
    {
        IEnumerable<GlobalMacro> Get(object filter = null, IEnumerable<GlobalMacroInclude> include = null, Dictionary<string, object> @params = null);
    }

    public class GlobalMacroService : ServiceBase<GlobalMacro>, IGlobalMacroService
    {
        public GlobalMacroService(IContext context) : base(context, "usermacro") { }

        public IEnumerable<GlobalMacro> Get(object filter = null, IEnumerable<GlobalMacroInclude> include = null, Dictionary<string, object> @params = null)
        {
            var includeHelper = new IncludeHelper(include == null ? 1 : include.Sum(x => (int)x));
            if(@params == null)
                @params = new Dictionary<string, object>();

            @params.AddOrReplace("output", "extend");
            @params.AddOrReplace("globalmacro", true);
            
            @params.AddOrReplace("filter", filter);
            
            return BaseGet(@params);
        }
    }

    public enum GlobalMacroInclude
    {
        All = 1,
        None = 2
    }

    public interface IHostMacroService : ICRUDService<HostMacro, HostMacroInclude>
    {

    }

    public class HostMacroService : CRUDService<HostMacro, HostMacroService.HostMacrosidsResult, HostMacroInclude>, IHostMacroService
    {
        public HostMacroService(IContext context) : base(context, "usermacro") { }

        public override IEnumerable<HostMacro> Get(object filter = null, IEnumerable<HostMacroInclude> include = null, Dictionary<string, object> @params = null)
        {
            var includeHelper = new IncludeHelper(include == null ? 1 : include.Sum(x => (int)x));
            if(@params == null)
                @params = new Dictionary<string, object>();

            @params.AddOrReplace("output", "extend");
            @params.AddOrReplace("selectGroups", includeHelper.WhatShouldInclude(HostMacroInclude.Groups));
            @params.AddOrReplace("selectHosts", includeHelper.WhatShouldInclude(HostMacroInclude.Hosts));
            @params.AddOrReplace("selectTemplates", includeHelper.WhatShouldInclude(HostMacroInclude.Templates));

            @params.AddOrReplace("filter", filter);
            
            return BaseGet(@params);
        }

        public class HostMacrosidsResult : EntityResultBase
        {
            [JsonProperty("hostmacroids")]
            public override string[] ids { get; set; }
        }

        
    }
    public enum HostMacroInclude
    {
        All = 1,
        None = 2,
        Groups = 4,
        Hosts = 8,
        Templates = 16
    }
    
}
