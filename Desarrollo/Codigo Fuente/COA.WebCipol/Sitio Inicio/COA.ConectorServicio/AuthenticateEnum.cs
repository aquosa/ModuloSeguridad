using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace COA.ConectorServicio
{
    public enum AuthenticateEnum : int
    {
        /// <summary>
        /// 1
        /// </summary>
        [Description("Default (Network Credentials)")]
        DEFAULT = 1,
        /// <summary>
        /// 2
        /// </summary>
        [Description("Básica (Usr/Pass)")]
        BASICA = 2,
        /// <summary>
        /// 3
        /// </summary>
        [Description("Active Directory (NTLM)")]
        ACTIVE_DIRECTORY = 3
    }
}
