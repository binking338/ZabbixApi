using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZabbixApi.Helper
{
    public class LimitPropsContractResolver : DefaultContractResolver
    {
        string[] props = null;

        bool retain;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="props">properties array</param>
        /// <param name="retain"></param>
        public LimitPropsContractResolver(string[] props, bool retain = true)
        {
            this.props = props;
            this.retain = retain;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> list = base.CreateProperties(type, memberSerialization);
            return list.Where(p =>
            {
                if (retain)
                {
                    return props.Contains(p.PropertyName);
                }
                else
                {
                    return !props.Contains(p.PropertyName);
                }
            }).ToList();
        }
    }
}
