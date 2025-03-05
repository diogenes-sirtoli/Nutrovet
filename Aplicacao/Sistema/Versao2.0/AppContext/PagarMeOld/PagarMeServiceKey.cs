using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagarMe;

namespace PagarMeOld
{
    public static class PagarMeServiceKey
    {
        public static void PagarMeServiceKeyInicializator(bool _serverLocal)
        {
            if (_serverLocal)
            {
                PagarMeService.DefaultEncryptionKey = "ek_test_CO56Ihyug989EVL7ObCDqLkDgvPzTo";
                PagarMeService.DefaultApiKey = "ak_test_aCsUqdiE1HAosPWLS3qf4A4aaETTut";
            }
            else
            {
                PagarMeService.DefaultEncryptionKey = "ek_live_2xSZkpu0OMnFvmavVWY2oDTQ2eyqqf";
                PagarMeService.DefaultApiKey = "ak_live_Rf9aOSL54oyBwbryrwQwFMx8IE8mBh";
            }
        }
    }
}
