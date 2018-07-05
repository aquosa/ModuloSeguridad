using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.Core
{
    /// <summary>
    /// Interface to identify entities
    /// </summary>
    public interface IEntityDefinitionProvider
    {
        /// <summary>
        /// Determines if the type specified is an entity type
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool IsEntity(Type t);
    }
}
