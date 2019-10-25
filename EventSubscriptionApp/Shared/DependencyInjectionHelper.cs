using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Shared
{
    public class DependencyInjectionHelper
    {
        /// <summary>
        /// Gets all services per IClassName convention
        /// </summary>
        /// <param name="assemblyName">Full assembly name</param>
        /// <returns></returns>
        public static List<Tuple<Type,Type>> GetServices(string assemblyName)
        {
            List<Tuple<Type, Type>> result = new List<Tuple<Type, Type>>();

            var assemblyTypes = Assembly.Load(assemblyName)?.GetTypes();

            if (assemblyTypes == null) return result;

            var serviceList = assemblyTypes
                .Where(t => t.IsAbstract && t.Name.StartsWith("I"))
                .ToList();

            var implementationDictionary = assemblyTypes
                .Where(t => !t.IsAbstract)
                .ToDictionary(i => i.FullName, i => i);

            //Services per IClass convention
            serviceList.ForEach(s => {
                string className = $"{s.Namespace}.{s.Name.Substring(1)}";
                if (implementationDictionary.ContainsKey(className)) result.Add(new Tuple<Type, Type>(s, implementationDictionary[className]));
            });

            //foreach(var repo in implementationDictionary.Where(x => x.Key.EndsWith("Repository")))
            //{
            //    var interfaces = repo.Value.FindInterfaces((t,o) => t.Name.StartsWith("IRepository"),null);
            //    if(interfaces.Length == 1) result.Add(new Tuple<Type, Type>(interfaces[0], repo.Value));
            //}

            return result;
        }

    }
}
