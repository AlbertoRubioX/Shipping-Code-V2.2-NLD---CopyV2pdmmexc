using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class GlobalVar
    {
        private static string _User_Check = string.Empty;
        private static string _usuario = string.Empty;
        private static int _n_acceso;
        private static int _nombre_user;
        private static int _Compania;

        public static string User_Check
        {
            get
            {
                return _User_Check;
            }
            set
            {
                _User_Check = value;
            }
        }

        public static string usuario
        {
            get
            {
                return _usuario;
            }
            set
            {
                _usuario = value;
            }
        }

        public static int n_acceso
        {
            get
            {
                return _n_acceso;
            }
            set
            {
                _n_acceso = value;
            }
        }

        public static int nombre_user
        {
            get
            {
                return _nombre_user;
            }
            set
            {
                _nombre_user = value;
            }
        }

        public static int Compania
        {
            get
            {
                return _Compania;
            }
            set
            {
                _Compania = value;
            }
        }
    }
}
