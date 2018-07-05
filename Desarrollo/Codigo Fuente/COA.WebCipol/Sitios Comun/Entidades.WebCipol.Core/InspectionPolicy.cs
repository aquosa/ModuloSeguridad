using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.Core
{
    /// <summary>
    /// Determines that nature by which the entity properties will
    /// be selected for data mapping
    /// </summary>
    public enum InspectionPolicy
    {
        /// <summary>
        /// Properties will be explcitly marked with DataInclude attribute
        /// </summary>
        OptIn,
        /// <summary>
        /// Properties will be explcitly marked with DataExclude attribute
        /// </summary>
        OptOut
    }
}
