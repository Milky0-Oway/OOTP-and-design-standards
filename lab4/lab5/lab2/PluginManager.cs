using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace lab2
{
    public class PluginManager
    {
        private List<IAnimalPlugin> plugins;
        private readonly PluginVerifier pluginVerifier;

        public PluginManager()
        {
            plugins = new List<IAnimalPlugin>();
            pluginVerifier = new PluginVerifier("D:/Study/OOP/lab2/publickey.snk");
        }

        public void LoadPlugins(string pluginDirectory)
        {
            string[] pluginFiles = Directory.GetFiles(pluginDirectory, "*.dll");
            foreach (string file in pluginFiles)
            {
                if (pluginVerifier.VerifyAssemblySignature(file))
                {
                    Assembly assembly = Assembly.LoadFrom(file);
                    Type[] types = assembly.GetTypes();
                    foreach (Type type in types)
                    {
                        if (typeof(IAnimalPlugin).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                        {
                            IAnimalPlugin plugin = (IAnimalPlugin)Activator.CreateInstance(type);
                            plugins.Add(plugin);
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Плагин с недействительной подписью: {file}");
                }
            }
        }

        public List<IAnimalPlugin> GetPlugins()
        {
            return plugins;
        }
    }
}