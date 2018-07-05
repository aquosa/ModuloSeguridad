using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.VisualBasic;

namespace COA.WebCipol.Presentacion.PageBuilders
{
    public class Utiles
    {
        public static bool TieneCaracterInvalido(string TextoOriginal)
        {
            string strCaracteres = "? * < > :";
            int intIni = 0;
            if (!string.IsNullOrEmpty(TextoOriginal.Trim()))
            {
                if (TextoOriginal.Trim().Length > 1 & Strings.InStr("ABCDEFGHIJKLMNÑOPQRSTUVWXYZ", TextoOriginal.Substring(0, 1), CompareMethod.Text) > 0)
                {
                    if (TextoOriginal.Substring(1, 1) == ":")
                    {
                        strCaracteres = "? * < >";
                        intIni = 3;
                    }
                }
                for (int intI = intIni; intI <= TextoOriginal.Length - 1; intI++)
                {
                    if (Strings.InStr(strCaracteres, TextoOriginal.Substring(intI, 1), CompareMethod.Text) > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static string VerifNombreNETBIOS(string TextoOrigen)
        {
            string strChrAlfa = "abcdefghijklmnñopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789";
            string strChrExps = "!@#$%^&()-‘{}.~";
            if (Strings.InStr(strChrAlfa, TextoOrigen.Substring(0, 1), CompareMethod.Text) == 0)
            {
                return "El nombre de NETBIOS no puede comenzar con un caracter especial";
            }
            if (Strings.InStr(strChrAlfa, TextoOrigen.Substring(TextoOrigen.Length - 1, 1), CompareMethod.Text) == 0)
            {
                return "El nombre de NETBIOS no puede terminar con un caracter especial";
            }
            for (int intI = 0; intI <= TextoOrigen.Length - 1; intI++)
            {
                if (Strings.InStr(strChrAlfa + strChrExps, TextoOrigen.Substring(intI, 1), CompareMethod.Text) == 0)
                {
                    return "El Nombre NETBIOS no puede contener caracteres inválidos.";
                }
            }
            return "";
        }
    }
}