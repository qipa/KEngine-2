using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace Kupiakos.KEngine
{
    /// <summary>
    /// Manages the many Entities throughout the game - 
    /// their names, the types they are registered to,
    /// and other little tidbits.
    /// </summary>
    public class EntityManager
    {
        public Engine Engine { get; set; }
        private Dictionary<string, Type> registeredEntities;

        public EntityManager(Engine e)
        {
            this.Engine = e;
            this.FindEntities();
        }

        public void RegisterEntityType(Type e)
        {
            if (!e.IsSubclassOf(typeof(Entity)))
                throw new NotSupportedException("Cannot add a type that is not an Entity.");
            registeredEntities[e.Name] = e;
        }

        public Type GetEntityType(string name)
        {
            return this.registeredEntities[name];
        }

        /// <summary>
        /// Find all entities in the entry assembly and add them to the registered Entities list.
        /// </summary>
        private void FindEntities()
        {
            var asm = System.Reflection.Assembly.GetEntryAssembly();
            foreach (var t in asm.GetTypes().Where((t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(Entity)))))
            {
                this.registeredEntities.Add(t.Name, t);
            }
        }
    }
}

