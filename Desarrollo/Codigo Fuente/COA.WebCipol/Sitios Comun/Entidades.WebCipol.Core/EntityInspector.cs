using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.Core
{
    public static class EntityInspector
    {
        static IEntityDefinitionProvider entityProvider;
        /// <summary>
        /// The entity provider set up for this instance
        /// </summary>
        public static IEntityDefinitionProvider EntityProvider
        {
            get
            {
                if (null == entityProvider)
                {
                    // Create an instance of the default entity provider
                    entityProvider = new EntityDefinitionProvider();
                }
                return entityProvider;
            }
            set { entityProvider = value; }
        }
    }
}
