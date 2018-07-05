using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.Core
{
    /// <summary>
    /// Default entity definition provider
    /// </summary>
    public sealed class EntityDefinitionProvider : IEntityDefinitionProvider
    {
        public bool IsEntity(Type t)
        {
            return false;
        }
    }
}
