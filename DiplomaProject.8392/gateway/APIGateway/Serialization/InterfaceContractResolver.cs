using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace APIGateway.Serialization
{
    //this contract resolver allows to ignore properties with JsonIgnore attribute defined in interfaces
    //designed for usage with classes generated from proto messages
    //since grpc generated classes are auto generated, JsonIgnore attribute cannot be set on their attributes
    //but since classes are partial, they can implement interfaces where properties can be defined with JsonIgnore attribute
    public class InterfaceContractResolver : DefaultContractResolver, IContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            var interfaces = member.DeclaringType.GetInterfaces();
            foreach (var @interface in interfaces)
            {
                
                foreach (var interfaceProperty in @interface.GetProperties())
                {
                    //find properties of the class from implemented interfaces
                    if (interfaceProperty.Name == member.Name && interfaceProperty.MemberType == member.MemberType)
                    {
                        //if property of interface has JsonIgnore attribute, ignore the same property in class
                        if (interfaceProperty.GetCustomAttributes(typeof(JsonIgnoreAttribute), true).Any())
                        {
                            property.Ignored = true;
                            return property;
                        }
                    }
                }
            }
            return property;
        }

    }
}
