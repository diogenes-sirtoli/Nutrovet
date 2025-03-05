using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagarMe;

namespace PagarMeOld
{
    public class PlanoPMO
    {
        public bllRetornoPMO Inserir(TOPlanoPMO _plano, bool _serverLocal)
        {
            PagarMeServiceKey.PagarMeServiceKeyInicializator(_serverLocal);

            try
            {
                Plan _planoInsert = new Plan
                {
                    Name = _plano.Name,
                    Amount = _plano.Amount,
                    Days = _plano.Days,
                    TrialDays = _plano.TrialDays

                };

                _planoInsert.Save();

                return bllRetornoPMO.GeraRetorno(true, "Plano Criado com Sucesso!!!");
            }
            catch (PagarMeException errPagarMe)
            {
                string _msg = "";

                foreach (var item in errPagarMe.Error.Errors)
                {
                    _msg += item.Message;
                }

                return bllRetornoPMO.GeraRetorno(false, _msg);
            }
        }

        public bllRetornoPMO Alterar(int _idPlano, TOPlanoPMO _plano, bool _serverLocal)
        {
            PagarMeServiceKey.PagarMeServiceKeyInicializator(_serverLocal);

            try
            {
                var planoAlterar = PagarMeService.GetDefaultService().Plans.Find(_idPlano);

                planoAlterar.Name = _plano.Name;
                planoAlterar.TrialDays = _plano.TrialDays;

                planoAlterar.Save();

                return bllRetornoPMO.GeraRetorno(true, "Plano Alterado com Sucesso!!!");
            }
            catch (PagarMeException errPagarMe)
            {
                string _msg = "";

                foreach (var item in errPagarMe.Error.Errors)
                {
                    _msg += item.Message;
                }

                return bllRetornoPMO.GeraRetorno(false, _msg);
            }
        }

        public TOPlanoPMO Carregar(int _idPlano, bool _serverLocal)
        {
            TOPlanoPMO _retPlano = new TOPlanoPMO();

            PagarMeServiceKey.PagarMeServiceKeyInicializator(_serverLocal);

            try
            {
                var planoCarregar = PagarMeService.GetDefaultService().Plans.Find(_idPlano);

                if (planoCarregar != null)
                {
                    _retPlano.Amount = planoCarregar.Amount;
                    _retPlano.Charges = planoCarregar.Charges;
                    _retPlano.Color = planoCarregar.Color;
                    _retPlano.DateCreated = planoCarregar.DateCreated;
                    _retPlano.DateUpdated = planoCarregar.DateUpdated;
                    _retPlano.Days = planoCarregar.Days;
                    _retPlano.Id = planoCarregar.Id;
                    _retPlano.Installments = planoCarregar.Installments;
                    _retPlano.InvoiceReminder = planoCarregar.InvoiceReminder;
                    _retPlano.Loaded = planoCarregar.Loaded;
                    _retPlano.Name = planoCarregar.Name;
                    _retPlano.TrialDays = planoCarregar.TrialDays;
                    _retPlano.ErrorHash = false;
                    _retPlano.ErrorMessage = "";
                }
            }
            catch (PagarMeException errPagarMe)
            {
                string _msg = "";

                foreach (var item in errPagarMe.Error.Errors)
                {
                    _msg += item.Message;
                }

                _retPlano.ErrorHash = true;
                _retPlano.ErrorMessage = _msg;
            }
            return _retPlano;
        }

        public List<Plan> Listar(bool _serverLocal)
        {
            List<Plan> _listaRet = null;

            PagarMeServiceKey.PagarMeServiceKeyInicializator(_serverLocal);

            try
            {
                _listaRet = PagarMeService.GetDefaultService().Plans.FindAll(new Plan()).ToList();
            }
            catch (Exception _err)
            {
                var erro = _err;
                _listaRet = null;
            }

            return _listaRet;
        }
    }
}
