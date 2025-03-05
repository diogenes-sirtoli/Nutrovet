using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class DominiosBll
    {
        public enum Sexo
        {
            Macho = 1,
            Fêmea
        }

        public enum Periodo
        {
            Mensal = 1,
            Anual
        }

        public enum AcessosPlanosAuxSituacao
        {
            Cadastrado = 1,
            Pago,
            Permitido,
            Cancelado,
            Alterado,
            Renovado
        }

        public enum Unidades
        {
            µg = 1,
            g,
            mg,
            Kcal,
            UI,
            Proporção,
            mcg_Kg,
            mg_animal,
            mg_Kg,
            UI_Kg,
        }

        public enum DietasAlimentosRecomendacao
        {
            Indicado = 1,
            Contraindicado
        }

        public enum ExigenciasNutrAuxTabelas
        {
            FEDIAF = 1,
            NRC,
            AAFCO
        }

        public enum ExigenciasNutrAuxTipoValor
        {
            Mínimo = 1,
            Máximo,
            Adequado,
            Recomendado
        }

        public enum StatusTransacao
        {
            Aguardando_Pagamento = 1,
            Em_Análise,
            Pago,
            Disponível,
            Em_Disputa,
            Devolvido,
            Cancelado
        }

        public enum PessoasAuxTipos
        {
            Administrador_do_Sistema = 1,
            Cliente,
            Tutor
        }

        public enum ExigenciasNutrAuxIndicacoes
        {
            Adulto = 1,
            CresInicial,
            CresFinal,
            Gestante,
            Lactante
        }

        public enum CupomDescontoTipos
        {
            Percentual = 1,
            Valor,
            Mensal,
            Anual
        }

        public enum PortalContatoAuxSituacao
        {
            Enviada = 1,
            Recebida,
            Lida,
            Respondida,
            Arquivada
        }
    }
}
