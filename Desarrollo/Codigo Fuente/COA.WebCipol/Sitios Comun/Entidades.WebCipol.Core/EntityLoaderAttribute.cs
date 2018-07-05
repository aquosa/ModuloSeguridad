using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.Core
{
    [global::System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class EntityLoaderAttribute : Attribute
    {
        public InspectionPolicy InspectionPolicy { get; private set; }

        public EntityLoaderAttribute(InspectionPolicy inspectionPolicy)
        {
            InspectionPolicy = inspectionPolicy;
        }
    }
}
